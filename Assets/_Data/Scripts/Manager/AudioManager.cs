using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseBehaviour
{
    protected static AudioManager instance;
    public static AudioManager Instance => instance;

    [Header("Audio Mixer (Optional)")]
    [SerializeField] protected AudioMixer audioMixer;
    [SerializeField] protected AudioMixerGroup masterGroup;
    [SerializeField] protected AudioMixerGroup bgmGroup;
    [SerializeField] protected AudioMixerGroup voiceGroup;
    [SerializeField] protected AudioMixerGroup sfxGroup;
    [SerializeField] protected AudioMixerGroup announcerGroup;
    [SerializeField] protected AudioMixerGroup uiGroup;

    [Header("Dedicated Audio Sources (Multi-Channel)")]
    [SerializeField] protected AudioSource bgmSource;
    [SerializeField] protected AudioSource voiceSource;
    [SerializeField] protected AudioSource sfxSource;
    [SerializeField] protected AudioSource announcerSource;
    [SerializeField] protected AudioSource uiSource;

    // Backward-compatible primary AudioSource accessor
    public AudioSource AudioSource => sfxSource != null ? sfxSource : (voiceSource != null ? voiceSource : bgmSource);

    [Header("Sound ScriptableObjects")]
    [SerializeField] protected AnnouncerSoundSO announcerSoundSO;
    [SerializeField] protected CharacterSoundSO characterSoundSO;
    [SerializeField] protected UISoundSO uiSoundSO;

    // Fast O(1) audio clip lookup caches
    protected readonly Dictionary<AnnouncerSoundType, AudioClip> announcerSoundDict = new();
    protected readonly Dictionary<CharacterSoundType, AudioClip> characterSoundDict = new();
    protected readonly Dictionary<UISoundType, AudioClip> uiSoundDict = new();

    // Volume levels per channel (0.0 to 1.0)
    protected readonly Dictionary<AudioChannel, float> channelVolumes = new();
    protected readonly Dictionary<AudioChannel, bool> channelMutes = new();

    private Coroutine bgmFadeCoroutine;

    protected override void Awake()
    {
        base.Awake();
        if (AudioManager.instance != null && AudioManager.instance != this)
        {
            Debug.LogWarning("Only 1 AudioManager allows to exist");
            Destroy(gameObject);
            return;
        }
        instance = this;
        this.InitializeAudioSources();
        this.InitializeSoundDictionaries();
        this.LoadVolumeSettings();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.InitializeAudioSources();
        this.LoadAnnouncerSoundSO();
        this.LoadCharacterSoundSO();
        this.LoadUISoundSO();
        this.InitializeSoundDictionaries();
    }

    protected void InitializeAudioSources()
    {
        // Auto-create or setup dedicated AudioSources if not present
        this.bgmSource = SetupAudioSource(this.bgmSource, "BGM_Source", bgmGroup, true);
        this.voiceSource = SetupAudioSource(this.voiceSource, "Voice_Source", voiceGroup, false);
        this.sfxSource = SetupAudioSource(this.sfxSource, "SFX_Source", sfxGroup, false);
        this.announcerSource = SetupAudioSource(this.announcerSource, "Announcer_Source", announcerGroup, false);
        this.uiSource = SetupAudioSource(this.uiSource, "UI_Source", uiGroup, false);
    }

    protected AudioSource SetupAudioSource(AudioSource source, string childName, AudioMixerGroup group, bool loop)
    {
        if (source != null)
        {
            if (group != null) source.outputAudioMixerGroup = group;
            source.loop = loop;
            return source;
        }

        Transform existingChild = transform.Find(childName);
        if (existingChild != null && existingChild.TryGetComponent(out AudioSource existingSrc))
        {
            if (group != null) existingSrc.outputAudioMixerGroup = group;
            existingSrc.loop = loop;
            return existingSrc;
        }

        GameObject childObj = new GameObject(childName);
        childObj.transform.SetParent(this.transform, false);
        AudioSource newSource = childObj.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        newSource.loop = loop;
        if (group != null) newSource.outputAudioMixerGroup = group;
        return newSource;
    }

    protected void InitializeSoundDictionaries()
    {
        if (this.announcerSoundSO != null && this.announcerSoundSO.sounds != null)
        {
            this.announcerSoundDict.Clear();
            foreach (var s in this.announcerSoundSO.sounds)
            {
                if (s != null && !this.announcerSoundDict.ContainsKey(s.type))
                    this.announcerSoundDict[s.type] = s.clip;
            }
        }

        if (this.characterSoundSO != null && this.characterSoundSO.sounds != null)
        {
            this.characterSoundDict.Clear();
            foreach (var s in this.characterSoundSO.sounds)
            {
                if (s != null && !this.characterSoundDict.ContainsKey(s.type))
                    this.characterSoundDict[s.type] = s.clip;
            }
        }

        if (this.uiSoundSO != null && this.uiSoundSO.sounds != null)
        {
            this.uiSoundDict.Clear();
            foreach (var s in this.uiSoundSO.sounds)
            {
                if (s != null && !this.uiSoundDict.ContainsKey(s.type))
                    this.uiSoundDict[s.type] = s.clip;
            }
        }
    }

    protected void LoadAnnouncerSoundSO()
    {
        if (this.announcerSoundSO != null) return;
        this.announcerSoundSO = Resources.Load<AnnouncerSoundSO>("Sounds/AnnouncerSoundData");
    }

    protected void LoadCharacterSoundSO()
    {
        if (this.characterSoundSO != null) return;
        this.characterSoundSO = Resources.Load<CharacterSoundSO>("Sounds/CharacterSoundData");
    }

    protected void LoadUISoundSO()
    {
        if (this.uiSoundSO != null) return;
        this.uiSoundSO = Resources.Load<UISoundSO>("Sounds/UISoundData");
    }

    #region Volume & Channel Control
    public void LoadVolumeSettings()
    {
        foreach (AudioChannel channel in (AudioChannel[])System.Enum.GetValues(typeof(AudioChannel)))
        {
            float vol = PlayerPrefs.GetFloat("Audio_" + channel.ToString(), 1.0f);
            bool mute = PlayerPrefs.GetInt("Mute_" + channel.ToString(), 0) == 1;
            this.channelVolumes[channel] = vol;
            this.channelMutes[channel] = mute;
            this.ApplyChannelVolume(channel);
        }
    }

    public void SetVolume(AudioChannel channel, float volume)
    {
        volume = Mathf.Clamp01(volume);
        this.channelVolumes[channel] = volume;
        PlayerPrefs.SetFloat("Audio_" + channel.ToString(), volume);
        this.ApplyChannelVolume(channel);
    }

    public float GetVolume(AudioChannel channel)
    {
        if (this.channelVolumes.TryGetValue(channel, out float vol))
            return vol;
        return 1.0f;
    }

    public void SetMute(AudioChannel channel, bool mute)
    {
        this.channelMutes[channel] = mute;
        PlayerPrefs.SetInt("Mute_" + channel.ToString(), mute ? 1 : 0);
        this.ApplyChannelVolume(channel);
    }

    public bool IsMuted(AudioChannel channel)
    {
        if (this.channelMutes.TryGetValue(channel, out bool muted))
            return muted;
        return false;
    }

    protected void ApplyChannelVolume(AudioChannel channel)
    {
        float masterVol = (this.IsMuted(AudioChannel.Master)) ? 0f : this.GetVolume(AudioChannel.Master);
        float channelVol = (this.IsMuted(channel)) ? 0f : this.GetVolume(channel);
        float effectiveVol = masterVol * channelVol;

        // If Unity AudioMixer is connected, set decibel volume
        if (this.audioMixer != null)
        {
            string paramName = channel.ToString() + "_Vol";
            float dB = (effectiveVol > 0.0001f) ? Mathf.Log10(effectiveVol) * 20f : -80f;
            this.audioMixer.SetFloat(paramName, dB);
        }

        // Software fallback
        switch (channel)
        {
            case AudioChannel.BGM:
                if (this.bgmSource != null) this.bgmSource.volume = effectiveVol;
                break;
            case AudioChannel.Voice:
                if (this.voiceSource != null) this.voiceSource.volume = effectiveVol;
                break;
            case AudioChannel.SFX:
                if (this.sfxSource != null) this.sfxSource.volume = effectiveVol;
                break;
            case AudioChannel.Announcer:
                if (this.announcerSource != null) this.announcerSource.volume = effectiveVol;
                break;
            case AudioChannel.UI:
                if (this.uiSource != null) this.uiSource.volume = effectiveVol;
                break;
            case AudioChannel.Master:
                // Re-apply to all channels
                ApplyChannelVolume(AudioChannel.BGM);
                ApplyChannelVolume(AudioChannel.Voice);
                ApplyChannelVolume(AudioChannel.SFX);
                ApplyChannelVolume(AudioChannel.Announcer);
                ApplyChannelVolume(AudioChannel.UI);
                break;
        }
    }
    #endregion

    #region Playback APIs
    // BGM
    public void PlayBGM(AudioClip clip, bool loop = true, float fadeDuration = 0.5f)
    {
        if (clip == null || this.bgmSource == null) return;
        if (this.bgmSource.clip == clip && this.bgmSource.isPlaying) return;

        if (bgmFadeCoroutine != null) StopCoroutine(bgmFadeCoroutine);
        bgmFadeCoroutine = StartCoroutine(FadeToNewBGM(clip, loop, fadeDuration));
    }

    private IEnumerator FadeToNewBGM(AudioClip newClip, bool loop, float fadeDuration)
    {
        float targetVolume = this.IsMuted(AudioChannel.BGM) ? 0f : this.GetVolume(AudioChannel.Master) * this.GetVolume(AudioChannel.BGM);
        if (this.bgmSource.isPlaying && fadeDuration > 0)
        {
            float startVol = this.bgmSource.volume;
            for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
            {
                this.bgmSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
                yield return null;
            }
        }

        this.bgmSource.clip = newClip;
        this.bgmSource.loop = loop;
        this.bgmSource.Play();

        if (fadeDuration > 0)
        {
            for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
            {
                this.bgmSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
                yield return null;
            }
        }
        this.bgmSource.volume = targetVolume;
    }

    public void StopBGM(float fadeDuration = 0.5f)
    {
        if (this.bgmSource == null || !this.bgmSource.isPlaying) return;
        if (bgmFadeCoroutine != null) StopCoroutine(bgmFadeCoroutine);
        bgmFadeCoroutine = StartCoroutine(FadeOutBGM(fadeDuration));
    }

    private IEnumerator FadeOutBGM(float fadeDuration)
    {
        float startVol = this.bgmSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            this.bgmSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            yield return null;
        }
        this.bgmSource.Stop();
        this.bgmSource.clip = null;
    }

    // Announcer
    public AudioClip GetAnnouncerSound(AnnouncerSoundType type)
    {
        if (this.announcerSoundDict.TryGetValue(type, out AudioClip clip))
            return clip;

        if (this.announcerSoundSO != null && this.announcerSoundSO.sounds != null)
        {
            foreach (var sound in this.announcerSoundSO.sounds)
            {
                if (sound != null && sound.type == type)
                {
                    this.announcerSoundDict[type] = sound.clip;
                    return sound.clip;
                }
            }
        }
        return null;
    }

    public void PlayAnnouncerSound(AnnouncerSoundType type)
    {
        AudioClip clip = this.GetAnnouncerSound(type);
        if (clip != null)
        {
            if (this.announcerSource != null) this.announcerSource.PlayOneShot(clip);
            else if (this.sfxSource != null) this.sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"AnnouncerSound not found: {type}");
        }
    }

    // Character Voice
    public AudioClip GetCharacterSound(CharacterSoundType type)
    {
        if (this.characterSoundDict.TryGetValue(type, out AudioClip clip))
            return clip;

        if (this.characterSoundSO != null && this.characterSoundSO.sounds != null)
        {
            foreach (var sound in this.characterSoundSO.sounds)
            {
                if (sound != null && sound.type == type)
                {
                    this.characterSoundDict[type] = sound.clip;
                    return sound.clip;
                }
            }
        }
        return null;
    }

    public void PlayCharacterSound(CharacterSoundType type, bool loop = false)
    {
        AudioClip clip = this.GetCharacterSound(type);
        if (clip == null)
        {
            Debug.LogWarning($"CharacterSound not found: {type}");
            return;
        }

        AudioSource targetSource = (this.voiceSource != null) ? this.voiceSource : this.sfxSource;
        if (loop)
        {
            targetSource.clip = clip;
            targetSource.loop = true;
            if (!targetSource.isPlaying) targetSource.Play();
        }
        else
        {
            targetSource.PlayOneShot(clip);
        }
    }

    public void StopCharacterSound()
    {
        AudioSource targetSource = (this.voiceSource != null) ? this.voiceSource : this.sfxSource;
        if (targetSource != null)
        {
            targetSource.Stop();
            targetSource.loop = false;
        }
    }

    // SFX
    public void PlaySFX(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip == null) return;
        if (this.sfxSource != null)
        {
            this.sfxSource.PlayOneShot(clip, volumeScale);
        }
    }

    // UI
    public AudioClip GetUISound(UISoundType type)
    {
        if (this.uiSoundDict.TryGetValue(type, out AudioClip clip))
            return clip;

        if (this.uiSoundSO != null && this.uiSoundSO.sounds != null)
        {
            foreach (var sound in this.uiSoundSO.sounds)
            {
                if (sound != null && sound.type == type)
                {
                    this.uiSoundDict[type] = sound.clip;
                    return sound.clip;
                }
            }
        }
        return null;
    }

    public void PlayUISound(UISoundType type)
    {
        AudioClip clip = this.GetUISound(type);
        if (clip != null)
        {
            if (this.uiSource != null) this.uiSource.PlayOneShot(clip);
            else if (this.sfxSource != null) this.sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"UISound not found: {type}");
        }
    }
    #endregion
}
