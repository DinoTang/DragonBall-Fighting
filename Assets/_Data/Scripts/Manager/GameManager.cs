using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance => instance;
    [SerializeField] protected CharacterCtrl winner;
    public CharacterCtrl Winner => winner;
    [SerializeField] protected CharacterCtrl redPlayer;
    [SerializeField] protected CharacterCtrl bluePlayer;

    public CharacterCtrl RedPlayer => redPlayer;
    public CharacterCtrl BluePlayer => bluePlayer;
    [SerializeField] protected bool isGameOver;
    public bool IsGameOver => isGameOver;
    [SerializeField] protected bool isRedWin;
    public bool IsRedWin => isRedWin;


    [SerializeField] private float gameOverTimer = 0f;
    [SerializeField] private float gameOverDelay = 2.5f;   // khoảng chờ sau khi game over

    public float GameOverTimer => gameOverTimer;
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
        this.GameOver();
        this.CheckTeamWin();
    }
    protected void GameOver()
    {
        if (!this.redPlayer.CharacterDamageReceiver.isDead && !this.bluePlayer.CharacterDamageReceiver.isDead) return;
        if (!this.isGameOver)
        {
            this.isGameOver = true;
            this.winner = this.redPlayer.CharacterDamageReceiver.isDead ? this.BluePlayer : redPlayer;
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

        // TODO: hiện chữ KO ở đây nếu cần

        // trở lại bình thường
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    protected void CheckTeamWin()
    {
        if (this.bluePlayer.CharacterDamageReceiver.isDead) this.isRedWin = true;
        else if (this.redPlayer.CharacterDamageReceiver.isDead) this.isRedWin = false;
    }

    public bool CanShowResult()
    {
        if (!this.isGameOver) return false;
        if (this.gameOverTimer < this.gameOverDelay)
        {
            this.gameOverTimer += Time.deltaTime;
            return false;
        }

        if (this.winner.StateManager.CurrentState is IdleCombatState && this.winner.Opponent.StateManager.CurrentState is DeadState)
            return true;

        return false;
    }
}
