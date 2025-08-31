using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseBehaviour
{
    protected static AudioManager instance;
    public static AudioManager Instance => instance;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip koSound;
    public AudioClip KoSound => koSound;
    [SerializeField] protected AudioClip redWinSound;
    [SerializeField] protected AudioClip blueWinSound;
    [SerializeField] protected AudioClip timeOverSound;
    [SerializeField] protected AudioClip readySound;
    [SerializeField] protected AudioClip fightSound;
    public bool hasKoSound;
    public bool hasTimeOverSound;
    public bool hasRedWinSound;
    public bool hasBlueWinSound;
    public bool hasReadySound;
    public bool hasFightSound;
    protected override void Awake()
    {
        base.Awake();
        if (AudioManager.instance != null) Debug.LogWarning("Only AudioManager allows to exist");
        instance = this;
    }
    protected void Update()
    {
        this.transform.position = this.mainCam.transform.position;
        this.PlayKOSound();
        this.PlayTeamWinSound();
        this.PlayTimeOverSound();
    }
    public void PlayTimeOverSound()
    {
        if (!GameManager.Instance.IsTimeOver) return;
        if (!this.hasTimeOverSound)
        {
            this.hasTimeOverSound = true;
            this.audioSource.PlayOneShot(this.timeOverSound);
        }
    }
    public void PlayKOSound()
    {
        if (GameManager.Instance.IsGameOver)
        {
            if (GameManager.Instance.IsTimeOver) return;
            if (!this.hasKoSound)
            {
                this.hasKoSound = true;
                this.audioSource.PlayOneShot(this.koSound);
            }
        }
    }
    protected void PlayTeamWinSound()
    {
        if (GameManager.Instance.GameOverTimer != GameManager.Instance.GameOverDelay) return;
        if (GameManager.Instance.Winner.StateManager.CurrentState is not IdleCombatState) return;
        if (GameManager.Instance.Winner == GameManager.Instance.RedPlayer && !this.hasRedWinSound)
        {
            this.hasRedWinSound = true;
            this.audioSource.PlayOneShot(this.redWinSound);
        }
        else if (GameManager.Instance.Winner == GameManager.Instance.BluePlayer && !this.hasBlueWinSound)
        {
            this.hasBlueWinSound = true;
            this.audioSource.PlayOneShot(this.blueWinSound);
        }

    }

    public void PlayReadySound()
    {
        if (!this.hasReadySound)
        {
            this.hasReadySound = true;
            this.audioSource.PlayOneShot(this.readySound);
        }
    }

    public void PlayFightSound()
    {
        if (!this.hasFightSound)
        {
            this.hasFightSound = true;
            this.audioSource.PlayOneShot(this.fightSound);
        }
    }
}
