using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] protected bool enableIntro = false;
    public bool EnableIntro { get => enableIntro; set => enableIntro = value; }
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
    [SerializeField] protected bool isSkillFreezing = false;
    public bool IsSkillFreezing { get => isSkillFreezing; set => isSkillFreezing = value; }


    [Header("Timing Match")]
    [SerializeField] protected bool isNoTiming = false;
    [SerializeField] protected bool isTimeOver;
    public bool IsTimeOver => isTimeOver;
    [SerializeField] protected float timerMatchMax = 90f;
    [SerializeField] protected float timerMatchCurrent = 90f;
    public float GameOverTimer => gameOverTimer;
    public float GameOverDelay => gameOverDelay;
    public float TimerMatch => timerMatchCurrent;

    [SerializeField] private float gameOverTimer = 0f;
    [SerializeField] private float gameOverDelay = 3.5f;   // khoảng chờ sau khi game over
    private bool canStartReadyFight = false;
    private bool hasTransitionedToPostMatch = false;


    [SerializeField] protected bool hasWinSound;
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogWarning("Only 1 GameManager allows to exist");
        instance = this;
        FrameRateManager.ApplyFrameRateConfig(60, false, true, true);
    }
    protected override void Start()
    {
        base.Start();
        this.InitializeMatch();
    }
    public void InitializeMatch()
    {
        SuperFreezeManager.Instance?.EndSuperFreeze(true);
        this.isSkillFreezing = false;
        this.matchState = this.enableIntro ? MatchStateEnum.Intro : MatchStateEnum.Ready;
        this.timerMatchCurrent = this.timerMatchMax;
        this.isGameOver = false;
        this.isCanShowResult = false;
        this.isTimeOver = false;
        this.hasWinSound = false;
        this.winner = null;
        this.playerIsHit = null;
        this.gameOverTimer = 0f;
        this.canStartReadyFight = false;
        this.hasTransitionedToPostMatch = false;

        if (this.redPlayer != null && this.redPlayer.CharacterDamageReceiver != null)
        {
            this.redPlayer.CharacterDamageReceiver.ResetHP();
        }
        if (this.bluePlayer != null && this.bluePlayer.CharacterDamageReceiver != null)
        {
            this.bluePlayer.CharacterDamageReceiver.ResetHP();
        }
    }
    protected void Update()
    {
        this.HandleMatchState();

        // Phím R để khởi động lại trận đấu bất kỳ lúc nào
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.RestartMatch();
        }

        // Khi kết thúc trận đấu, cho phép bấm Space/Enter để tới PostMatchScene
        if (this.isGameOver && !this.hasTransitionedToPostMatch)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                this.hasTransitionedToPostMatch = true;
                GameSession.Instance?.GoToPostMatch();
            }
        }
    }
    public void RestartMatch()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f / 60f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        CharacterCtrl[] characters = FindObjectsByType<CharacterCtrl>(FindObjectsInactive.Exclude);
        foreach (var c in characters)
        {
            if (c == null) continue;
            if (c.GetComponentInChildren<PlayerRed>()) this.redPlayer = c;
            else if (c.GetComponentInChildren<PlayerBlue>()) this.bluePlayer = c;
        }

        if (this.redPlayer != null && this.bluePlayer != null)
        {
            this.redPlayer.SetOpponent(this.bluePlayer);
            this.bluePlayer.SetOpponent(this.redPlayer);
            this.redPlayer.LoadCharacterCtrl();
            this.bluePlayer.LoadCharacterCtrl();
            if (GameSession.Instance != null && GameSession.Instance.RedCharacterDataSO != null)
                this.redCharacterSelected = GameSession.Instance.RedCharacterDataSO;
            if (GameSession.Instance != null && GameSession.Instance.BlueCharacterDataSO != null)
                this.blueCharacterSelected = GameSession.Instance.BlueCharacterDataSO;
            this.redPlayer.CharacterDataSO = this.redCharacterSelected;
            this.bluePlayer.CharacterDataSO = this.blueCharacterSelected;
        }
    }
    protected void PlayIntro()
    {
        // Nhấn Space hoặc Enter để bỏ qua Intro vào thẳng Ready
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            this.SkipIntro();
            return;
        }

        if (this.redPlayer == null || this.bluePlayer == null)
        {
            this.matchState = MatchStateEnum.Ready;
            return;
        }

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
    public void SkipIntro()
    {
        if (this.redPlayer != null)
        {
            this.redPlayer.CharacterModalCtrl.StopAllCoroutines();
            this.redPlayer.StateManager.SetNextState(new IdleState());
        }
        if (this.bluePlayer != null)
        {
            this.bluePlayer.CharacterModalCtrl.StopAllCoroutines();
            this.bluePlayer.StateManager.SetNextState(new IdleState());
        }
        this.matchState = MatchStateEnum.Ready;
    }
    protected void PlayFight()
    {
        if (!this.isNoTiming && !this.isGameOver && !this.isSkillFreezing)
        {
            this.timerMatchCurrent -= Time.deltaTime;
            if (this.timerMatchCurrent <= 0) this.timerMatchCurrent = 0;
        }

        bool isTimeUp = !this.isNoTiming && this.timerMatchCurrent <= 0;
        bool isCharacterDead = (this.redPlayer != null && this.redPlayer.CharacterDamageReceiver != null && this.redPlayer.CharacterDamageReceiver.isDead)
                            || (this.bluePlayer != null && this.bluePlayer.CharacterDamageReceiver != null && this.bluePlayer.CharacterDamageReceiver.isDead);

        if (isTimeUp || isCharacterDead)
        {
            this.matchState = MatchStateEnum.GameOver;
        }
    }
    protected void GameOver()
    {
        if (this.isGameOver) return;

        bool redDead = this.redPlayer != null && this.redPlayer.CharacterDamageReceiver != null && this.redPlayer.CharacterDamageReceiver.isDead;
        bool blueDead = this.bluePlayer != null && this.bluePlayer.CharacterDamageReceiver != null && this.bluePlayer.CharacterDamageReceiver.isDead;
        bool isTimeUp = !this.isNoTiming && this.timerMatchCurrent <= 0;

        if (!redDead && !blueDead && !isTimeUp) return;

        this.isGameOver = true;
        SuperFreezeManager.Instance?.EndSuperFreeze(true);
        this.isSkillFreezing = false;

        // --- Trường hợp hết giờ ---
        if (isTimeUp && !redDead && !blueDead)
        {
            float redHp = this.redPlayer.CharacterDamageReceiver.CurrentHP;
            float blueHp = this.bluePlayer.CharacterDamageReceiver.CurrentHP;

            this.winner = (redHp >= blueHp) ? this.redPlayer : this.bluePlayer;
            this.isCanShowResult = true;
            this.isTimeOver = true;
            this.RecordGameSessionResult();
            OnMatchGameOver?.Invoke(this.winner, this.isTimeOver);
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.TimeOver);
            return;
        }

        // --- Trường hợp chết máu (K.O.) ---
        if (redDead || blueDead)
        {
            this.winner = redDead ? this.bluePlayer : this.redPlayer;
            this.isCanShowResult = true;
            this.RecordGameSessionResult();
            OnMatchGameOver?.Invoke(this.winner, this.isTimeOver);
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.KO);
            StartCoroutine(this.SlowMotionGameOver());
        }
    }
    private IEnumerator SlowMotionGameOver()
    {
        float originalFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
    public void CanShowResult()

    {
        if (!this.isCanShowResult) return;
        this.gameOverTimer += Time.deltaTime;
        if (this.gameOverTimer < this.gameOverDelay) return;

        this.gameOverTimer = this.gameOverDelay;
        if (!this.hasWinSound)
        {
            this.hasWinSound = true;
            if (this.winner == this.RedPlayer) AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.RedWin);
            else AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.BlueWin);
        }

        if (GameSession.Instance != null && !this.hasTransitionedToPostMatch)
        {
            this.hasTransitionedToPostMatch = true;
            GameSession.Instance.GoToPostMatch();
        }
    }

    private void RecordGameSessionResult()
    {
        if (GameSession.Instance == null) return;

        float redHp = (this.redPlayer != null && this.redPlayer.CharacterDamageReceiver != null) ? this.redPlayer.CharacterDamageReceiver.CurrentHP : 0;
        float blueHp = (this.bluePlayer != null && this.bluePlayer.CharacterDamageReceiver != null) ? this.bluePlayer.CharacterDamageReceiver.CurrentHP : 0;
        string winTeam = (this.winner == this.redPlayer) ? "PLAYER 1" : "PLAYER 2";
        string winName = (this.winner != null && this.winner.CharacterDataSO != null) ? this.winner.CharacterDataSO.characterName.ToString() : "VEGETA";
        Sprite winPortrait = (this.winner != null && this.winner.CharacterDataSO != null) ? this.winner.CharacterDataSO.portrait : null;

        GameSession.Instance.RecordMatchResult(new GameSession.MatchResultData
        {
            WinnerPlayer = (this.winner == this.redPlayer) ? GameSession.Instance.RedSelectedCharacter : GameSession.Instance.BlueSelectedCharacter,
            WinnerTeam = winTeam,
            WinnerDisplayName = winName.ToUpper() + " (SUPER SAIYAN)",
            WinnerPortrait = winPortrait,
            RedHP = redHp,
            BlueHP = blueHp,
            MatchDuration = Mathf.Max(1f, this.timerMatchMax - this.timerMatchCurrent),
            IsTimeOver = this.isTimeOver,
            MaxCombo = 10,
            PerformanceRank = (redHp > 80f || blueHp > 80f) ? "S RANK - PERFECT!" : (redHp > 40f || blueHp > 40f) ? "A RANK - GREAT FIGHT" : "B RANK - CLOSE MATCH"
        });
    }
    public static event System.Action OnMatchReadyPhase;
    public static event System.Action OnMatchFightPhase;
    public static event System.Action OnMatchFightStarted;
    public static event System.Action<CharacterCtrl, bool> OnMatchGameOver;

    protected void PlayReadyFight()
    {
        if (!this.canStartReadyFight)
        {
            CameraManager.Instance.SetActiveWall(true);
            this.canStartReadyFight = true;
            StartCoroutine(StartReadyFight());
        }
    }
    private IEnumerator StartReadyFight()
    {
        OnMatchReadyPhase?.Invoke();
        AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Ready);
        yield return new WaitForSeconds(1.5f);

        OnMatchFightPhase?.Invoke();
        AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Fight);
        yield return new WaitForSeconds(0.5f);

        OnMatchFightStarted?.Invoke();
        // bắt đầu trận đấu
        matchState = MatchStateEnum.Fight;
        timerMatchCurrent = this.timerMatchMax; // bắt đầu đếm giờ
    }

}
