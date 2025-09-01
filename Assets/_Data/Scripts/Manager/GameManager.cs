using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance => instance;
    public enum MatchStateEnum { Intro, Ready, Fight, GameOver }
    [SerializeField] private MatchStateEnum matchState = MatchStateEnum.Intro;
    public MatchStateEnum MatchState => matchState;
    [SerializeField] protected CharacterCtrl winner;
    public CharacterCtrl Winner => winner;
    [SerializeField] protected CharacterCtrl redPlayer;
    [SerializeField] protected CharacterCtrl bluePlayer;

    public CharacterCtrl RedPlayer => redPlayer;
    public CharacterCtrl BluePlayer => bluePlayer;
    [SerializeField] protected bool isGameOver;
    public bool IsGameOver => isGameOver;
    [SerializeField] protected bool isCanShowResult;
    [SerializeField] protected bool isTimeOver;
    public bool IsTimeOver => isTimeOver;
    [SerializeField] private float gameOverTimer = 0f;
    [SerializeField] private float gameOverDelay = 2.5f;   // khoảng chờ sau khi game over

    public float GameOverTimer => gameOverTimer;
    public float GameOverDelay => gameOverDelay;
    [SerializeField] protected float timerMatchMax = 90f;
    [SerializeField] protected float timerMatchCurrent;
    public float TimerMatch => timerMatchCurrent;

    private bool canStartReadyFight = false;
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogWarning("Only 1 GameManager allows to exist");
        instance = this;
    }
    protected override void LoadComponent()
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
    }
    protected void Update()
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
    protected void PlayIntro()
    {
        if (!this.redPlayer.CharacterIntro.IsReady)
        {
            this.redPlayer.CharacterIntro.StartIntroCharacter();
            return;
        }
        if (!this.bluePlayer.CharacterIntro.IsReady)
        {
            this.bluePlayer.CharacterIntro.StartIntroCharacter();
            return;
        }

        if (this.redPlayer.CharacterIntro.IsReady && this.bluePlayer.CharacterIntro.IsReady)
        {
            this.matchState = MatchStateEnum.Ready;
        }
    }
    protected void PlayFight()
    {
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
            return;
        }

        // --- Trường hợp chết máu ---
        if (this.redPlayer.CharacterDamageReceiver.isDead || this.bluePlayer.CharacterDamageReceiver.isDead)
        {
            this.winner = this.redPlayer.CharacterDamageReceiver.isDead ? this.bluePlayer : this.redPlayer;
            this.isCanShowResult = true;
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
        AudioManager.Instance.PlayReadySound();
        yield return new WaitForSeconds(1.5f);

        // hiện chữ "Fight"
        UIManager.Instance.GetUIType(UIType.Ready).gameObject.SetActive(false);
        UIManager.Instance.GetUIType(UIType.Fight).gameObject.SetActive(true);
        AudioManager.Instance.PlayFightSound();
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.GetUIType(UIType.Fight).gameObject.SetActive(false);
        // bắt đầu trận đấu
        matchState = MatchStateEnum.Fight;
        timerMatchCurrent = this.timerMatchMax; // bắt đầu đếm giờ
    }

}
