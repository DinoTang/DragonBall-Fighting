using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance => instance;
    public enum MatchStateEnum { Intro, Ready, Fight, GameOver }

    [Header("Character Manager")]
    [SerializeField] protected CharactersDataSO charactersDataSO;
    [SerializeField] protected CharacterSelectSO characterSelectSO;
    [SerializeField] protected CharacterDataSO redCharacterSelected;
    [SerializeField] protected CharacterDataSO blueCharacterSelected;
    protected void LoadCharactersDataSO()
    {
        if (this.charactersDataSO != null) return;
        this.charactersDataSO = Resources.Load<CharactersDataSO>("CharacterData/CharactersDataSO");
        Debug.Log(transform.name + ": LoadCharactersDataSO");
    }
    protected void LoadCharacterSelectSO()
    {
        if (this.characterSelectSO != null) return;
        this.characterSelectSO = Resources.Load<CharacterSelectSO>("CharacterData/CharacterSelectSO");
        Debug.Log(transform.name + ": LoadCharacterSelectSO");
    }
    protected void LoadCharacterSelected()
    {
        if (this.redCharacterSelected != null && this.blueCharacterSelected != null) return;
        this.redCharacterSelected = this.charactersDataSO.GetCharacter(this.characterSelectSO.redCharacterName);
        this.blueCharacterSelected = this.charactersDataSO.GetCharacter(this.characterSelectSO.blueCharacterName);
        Debug.Log(transform.name + ": LoadCharacterSelected");
    }

    [Header("Match Manager")]
    [SerializeField] private MatchStateEnum matchState = MatchStateEnum.Ready;
    public MatchStateEnum MatchState => matchState;
    [SerializeField] protected CharacterCtrl winner;
    public CharacterCtrl Winner => winner;
    [SerializeField] protected CharacterCtrl redPlayer;
    [SerializeField] protected CharacterCtrl bluePlayer;

    public CharacterCtrl RedPlayer => redPlayer;
    public CharacterCtrl BluePlayer => bluePlayer;
    [SerializeField] protected CharacterCtrl playerIsHit;
    public CharacterCtrl PlayerIsHit { get => playerIsHit; set => playerIsHit = value; }
    [SerializeField] protected bool isGameOver;
    public bool IsGameOver => isGameOver;
    [SerializeField] protected bool isCanShowResult;


    [Header("Timing Match")]
    [SerializeField] protected bool isNoTiming = false;
    [SerializeField] protected bool isTimeOver;
    public bool IsTimeOver => isTimeOver;
    [SerializeField] protected float timerMatchMax = 90f;
    [SerializeField] protected float timerMatchCurrent;
    public float GameOverTimer => gameOverTimer;
    public float GameOverDelay => gameOverDelay;
    public float TimerMatch => timerMatchCurrent;

    [SerializeField] private float gameOverTimer = 0f;
    [SerializeField] private float gameOverDelay = 3f;   // khoảng chờ sau khi game over
    private bool canStartReadyFight = false;


    [SerializeField] protected bool hasWinSound;
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogWarning("Only 1 GameManager allows to exist");
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        this.timerMatchCurrent = this.timerMatchMax;
    }
    protected void Update()
    {
        this.HandleMatchState();
    }
    protected override void LoadComponent()
    {
        this.LoadCharactersDataSO();
        this.LoadCharacterSelectSO();
        this.LoadCharacterSelected();
        this.LoadOpponentCharacter();
    }

    protected void HandleMatchState()
    {
        switch (this.matchState)
        {
            case MatchStateEnum.Intro:
                this.PlayIntro();
                break;
            case MatchStateEnum.Ready:
                this.PlayReadyFight();
                break;
            case MatchStateEnum.Fight:
                this.PlayFight();
                break;
            case MatchStateEnum.GameOver:
                this.GameOver();
                this.CanShowResult();
                break;
        }
    }
    protected void LoadOpponentCharacter()
    {
        CharacterCtrl[] characters = FindObjectsOfType<CharacterCtrl>();
        if (characters[0].GetComponentInChildren<PlayerRed>())
        {
            this.redPlayer = characters[0];
            this.bluePlayer = characters[1];
            this.redPlayer.SetOpponent(this.bluePlayer);
            this.bluePlayer.SetOpponent(this.redPlayer);
        }

        if (characters[0].GetComponentInChildren<PlayerBlue>())
        {
            this.bluePlayer = characters[0];
            this.redPlayer = characters[1];
            this.redPlayer.SetOpponent(this.bluePlayer);
            this.bluePlayer.SetOpponent(this.redPlayer);
        }

        this.redPlayer.LoadCharacterCtrl();
        this.bluePlayer.LoadCharacterCtrl();
        this.redPlayer.CharacterDataSO = this.redCharacterSelected;
        this.bluePlayer.CharacterDataSO = this.blueCharacterSelected;
    }
    protected void PlayIntro()

    {
        if (!this.redPlayer.CharacterModalCtrl.IsReady)
        {
            this.redPlayer.CharacterModalCtrl.StartIntroCharacter(2);
            return;
        }
        if (!this.bluePlayer.CharacterModalCtrl.IsReady)
        {
            this.bluePlayer.CharacterModalCtrl.StartIntroCharacter(1);
            return;
        }

        if (this.redPlayer.CharacterModalCtrl.IsReady && this.bluePlayer.CharacterModalCtrl.IsReady)
        {
            this.matchState = MatchStateEnum.Ready;
        }
    }
    protected void PlayFight()

    {
        if (this.isNoTiming) return;
        if (!this.isGameOver) this.timerMatchCurrent -= Time.deltaTime;
        if (this.timerMatchCurrent <= 0) this.timerMatchCurrent = 0;
        if ((this.timerMatchCurrent == 0)
        || this.redPlayer.CharacterDamageReceiver.isDead || this.bluePlayer.CharacterDamageReceiver.isDead)
            this.matchState = MatchStateEnum.GameOver;

    }
    protected void GameOver()

    {
        // nếu cả 2 chưa chết và chưa hết giờ -> chưa game over
        if (!this.redPlayer.CharacterDamageReceiver.isDead
            && !this.bluePlayer.CharacterDamageReceiver.isDead
            && this.timerMatchCurrent > 0) return;

        if (this.isGameOver) return; // tránh gọi nhiều lần
        this.isGameOver = true;

        // --- Trường hợp hết giờ ---
        if (this.timerMatchCurrent == 0)
        {
            float redHp = this.redPlayer.CharacterDamageReceiver.CurrentHP;
            float blueHp = this.bluePlayer.CharacterDamageReceiver.CurrentHP;

            this.winner = (redHp > blueHp) ? this.redPlayer : this.bluePlayer;

            this.isCanShowResult = true; // hiển thị kết quả luôn
            this.isTimeOver = true;
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.TimeOver);
            return;
        }

        // --- Trường hợp chết máu ---
        if (this.redPlayer.CharacterDamageReceiver.isDead || this.bluePlayer.CharacterDamageReceiver.isDead)
        {
            this.winner = this.redPlayer.CharacterDamageReceiver.isDead ? this.bluePlayer : this.redPlayer;
            this.isCanShowResult = true;
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.KO);
            StartCoroutine(this.SlowMotionGameOver());
        }
    }
    private IEnumerator SlowMotionGameOver()

    {
        // làm chậm lại
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // giữ physics mượt

        // chờ 1.5s trong thời gian thật (không phụ thuộc timeScale)
        yield return new WaitForSecondsRealtime(1.5f);


        // trở lại bình thường
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
    public void CanShowResult()

    {
        if (!this.isCanShowResult) return;
        this.gameOverTimer += Time.deltaTime;
        if (this.gameOverTimer < this.gameOverDelay) return;

        this.gameOverTimer = this.gameOverDelay;
        if (this.hasWinSound) return;

        this.hasWinSound = true;
        if (this.winner == this.RedPlayer) AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.RedWin);
        else AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.BlueWin);
    }
    protected void PlayReadyFight()

    {
        if (!this.canStartReadyFight)
        {
            this.canStartReadyFight = true;
            StartCoroutine(StartReadyFight());
        }
    }
    private IEnumerator StartReadyFight()
    {
        UIManager.Instance.GetUIType(UIType.RedInfoBar).gameObject.SetActive(true);
        UIManager.Instance.GetUIType(UIType.BlueInfoBar).gameObject.SetActive(true);
        UIManager.Instance.GetUIType(UIType.Clock).gameObject.SetActive(true);
        // hiện chữ "Ready" trên UI
        UIManager.Instance.GetUIType(UIType.Ready).gameObject.SetActive(true);
        AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Ready);
        yield return new WaitForSeconds(1.5f);

        // hiện chữ "Fight"
        UIManager.Instance.GetUIType(UIType.Ready).gameObject.SetActive(false);
        UIManager.Instance.GetUIType(UIType.Fight).gameObject.SetActive(true);
        AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Fight);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.GetUIType(UIType.Fight).gameObject.SetActive(false);
        // bắt đầu trận đấu
        matchState = MatchStateEnum.Fight;
        timerMatchCurrent = this.timerMatchMax; // bắt đầu đếm giờ
    }

}
