using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseBehaviour
{
    protected static AudioManager instance;
    public static AudioManager Instance => instance;
    [SerializeField] protected AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    [SerializeField] protected AnnouncerSoundSO announcerSoundSO;
    [SerializeField] protected CharacterSoundSO characterSoundSO;
    [SerializeField] protected UISoundSO uiSoundSO;
    protected override void Awake()
    {
        base.Awake();
        if (AudioManager.instance != null) Debug.LogWarning("Only AudioManager allows to exist");
        instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAudioSource();
        this.LoadAnnouncerSoundSO();
        this.LoadCharacterSoundSO();
        this.LoadUISoundSO();
    }
    protected void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();
        Debug.Log(transform.name + ": LoadAudioSource");
    }

    protected void LoadAnnouncerSoundSO()
    {
        if (this.announcerSoundSO != null) return;
        this.announcerSoundSO = Resources.Load<AnnouncerSoundSO>("Sounds/AnnouncerSoundData");
        Debug.Log(transform.name + ": LoadAnnouncerSoundSO");
    }

    protected void LoadCharacterSoundSO()
    {
        if (this.characterSoundSO != null) return;
        this.characterSoundSO = Resources.Load<CharacterSoundSO>("Sounds/CharacterSoundData");
        Debug.Log(transform.name + ": LoadCharacterSoundSO");
    }

    protected void LoadUISoundSO()
    {
        if (this.uiSoundSO != null) return;
        this.uiSoundSO = Resources.Load<UISoundSO>("Sounds/UISoundData");
        Debug.Log(transform.name + ": LoadUISoundSO");
    }
    
    // Announcer
    public AudioClip GetAnnouncerSound(AnnouncerSoundType type)
    {
        foreach (var sound in this.announcerSoundSO.sounds)
        {
            if (sound.type == type)
                return sound.clip;
        }
        return null;
    }

    public void PlayAnnouncerSound(AnnouncerSoundType type)
    {
        AudioClip clip = this.GetAnnouncerSound(type);
        if (clip != null) this.audioSource.PlayOneShot(clip);
        else Debug.LogWarning($"AnnouncerSound not found: {type}");
    }


    // Character
    public AudioClip GetCharacterSound(CharacterSoundType type)
    {
        foreach (var sound in this.characterSoundSO.sounds)
        {
            if (sound.type == type)
                return sound.clip;
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

        if (loop)
        {
            this.audioSource.clip = clip;
            this.audioSource.loop = true;
            if (!this.audioSource.isPlaying)
                this.audioSource.Play();
        }
        else
        {
            this.audioSource.PlayOneShot(clip);
        }
    }

    public void StopCharacterSound()
    {
        this.audioSource.Stop();
        this.audioSource.loop = false;
    }


    // UI
    public AudioClip GetUISound(UISoundType type)
    {
        foreach (var sound in this.uiSoundSO.sounds)
        {
            if (sound.type == type)
                return sound.clip;
        }
        return null;
    }

    public void PlayUISound(UISoundType type)
    {
        AudioClip clip = this.GetUISound(type);
        if (clip != null) this.audioSource.PlayOneShot(clip);
        else Debug.LogWarning($"UISound not found: {type}");
    }

}
