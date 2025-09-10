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
    public AudioClip PlayAnnouncerSound(AnnouncerSoundType type)
    {
        foreach (var sound in this.announcerSoundSO.sounds)
        {
            if (sound.type != type) continue;
            this.audioSource.PlayOneShot(sound.clip);
            return sound.clip;
        }
        return null;
    }
    public AudioClip PlayCharacterSound(CharacterSoundType type)
    {
        foreach (var sound in this.characterSoundSO.sounds)
        {
            if (sound.type != type) continue;
            this.audioSource.PlayOneShot(sound.clip);
            return sound.clip;
        }
        return null;
    }
    public AudioClip PlayUISound(UISoundType type)
    {
        foreach (var sound in this.uiSoundSO.sounds)
        {
            if (sound.type != type) continue;
            this.audioSource.PlayOneShot(sound.clip);
            return sound.clip;
        }
        return null;
    }
}
