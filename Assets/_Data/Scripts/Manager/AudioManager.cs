using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseBehaviour
{
    protected static AudioManager instance;
    public static AudioManager Instance => instance;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip koClip;
    public AudioClip KoClip => koClip;
    [SerializeField] protected AudioClip redWinClip;
    [SerializeField] protected AudioClip blueWinClip;
    public bool hasKoClip;
    public bool hasRedWinClip;
    public bool hasBlueWinClip;
    protected override void Awake()
    {
        base.Awake();
        if (AudioManager.instance != null) Debug.LogWarning("Only AudioManager allows to exist");
        instance = this;
    }
    protected void Update()
    {
        this.transform.position = this.mainCam.transform.position;
        if (GameManager.Instance.IsGameOver) this.PlayKOSound();
        if (GameManager.Instance.CanShowResult()) this.PlayTeamWinSound();
    }
    public void PlayKOSound()
    {
        if (!this.hasKoClip)
        {
            this.hasKoClip = true;
            this.audioSource.PlayOneShot(koClip);
        }
    }

    protected void PlayTeamWinSound()
    {
        if (GameManager.Instance.IsRedWin && !this.hasRedWinClip)
        {
            this.hasRedWinClip = true;
            this.audioSource.PlayOneShot(this.redWinClip);
        }
        else if (GameManager.Instance.IsRedWin && !this.hasBlueWinClip)
        {
            this.hasBlueWinClip = true;
            this.audioSource.PlayOneShot(this.blueWinClip);
        }

    }
}
