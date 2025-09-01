using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseBehaviour
{
    protected static UIManager instance;
    public static UIManager Instance => instance;
    [SerializeField] protected List<UICtrl> uiChilds = new();
    public List<UICtrl> UIChilds => uiChilds;
    public float kOUiTimeOff = 3;
    public float timeOverUiTimeOff = 3;
    public float timer;

    public bool hasShowKOUI;
    public bool hasShowWinnerUI;
    public bool hasShowTimeOverUI;
    protected override void Awake()
    {
        base.Awake();
        if (UIManager.instance != null) Debug.LogWarning("Only 1 UIManager allows to exist");
        instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIChilds();
    }
    protected override void Start()
    {
        base.Start();
        this.HideAll();
        this.ShowTimeUI();
    }
    protected void LoadUIChilds()
    {
        if (this.uiChilds.Count > 0) return;

        foreach (RectTransform child in this.transform)
        {
            UICtrl uiCtrlChild = child.GetComponent<UICtrl>();
            if (uiCtrlChild as UIFade) continue;
            this.uiChilds.Add(uiCtrlChild);
        }
        Debug.Log(transform.name + ": LoadUIChilds");
    }

    protected void Update()
    {
        this.ShowKOUI();
        this.ShowWinnerUI();
        this.ShowTimeOverUI();
        this.ShowTimeUI();
        this.ShowHitCountUI();
    }

    protected void ShowKOUI()
    {
        if (GameManager.Instance.IsTimeOver) return;
        if (!GameManager.Instance.IsGameOver) return;
        this.timer += Time.deltaTime;
        if (this.timer >= this.kOUiTimeOff)
        {
            this.GetUIType(UIType.KO).gameObject.SetActive(false);
        }
        else if (!this.hasShowKOUI)
        {
            this.hasShowKOUI = true;
            this.GetUIType(UIType.KO).gameObject.SetActive(true);
        }
    }
    protected void ShowTimeOverUI()
    {
        if (!GameManager.Instance.IsTimeOver) return;
        this.timer += Time.deltaTime;
        if (this.timer >= this.timeOverUiTimeOff)
        {
            this.GetUIType(UIType.TimeOver).gameObject.SetActive(false);
        }
        else if (!this.hasShowTimeOverUI)
        {
            this.hasShowTimeOverUI = true;
            this.GetUIType(UIType.TimeOver).gameObject.SetActive(true);
        }
    }

    protected void HideAll()
    {
        foreach (var child in this.uiChilds)
        {
            child.gameObject.SetActive(false);
        }
    }
    protected void ShowWinnerUI()
    {
        if (GameManager.Instance.GameOverTimer < GameManager.Instance.GameOverDelay) return;
        if (GameManager.Instance.Winner.StateManager.CurrentState is not IdleCombatState) return;
        if (!this.hasShowWinnerUI)
        {
            this.hasShowWinnerUI = true;
            StartCoroutine(ShowWinnerAfterDelay(1f));
        }
    }

    private IEnumerator ShowWinnerAfterDelay(float delay)
    {
        this.HideAll();
        yield return new WaitForSeconds(delay);
        this.uiChilds[4].gameObject.SetActive(true);
        if (GameManager.Instance.Winner == GameManager.Instance.RedPlayer)
            this.GetUIType(UIType.Winner).RectTransform.anchoredPosition = new Vector2(-560, 165);
        else this.GetUIType(UIType.Winner).RectTransform.anchoredPosition = new Vector2(560, 165);
    }

    public UICtrl GetUIType(UIType uIType)
    {
        return this.uiChilds.Find(e => e.UIType == uIType);
    }

    protected void ShowTimeUI()
    {
        int timeMatch = Mathf.CeilToInt(GameManager.Instance.TimerMatch);
        var clockUI = this.GetUIType(UIType.Clock) as UIClock;
        clockUI.SetNumber(timeMatch);
    }
    protected void ShowHitCountUI()
    {
        if (GameManager.Instance.PlayerIsHit == null) return;
        var hitCountUI = this.GetUIType(UIType.HitCount) as UIHitCount;
        int hitCount = GameManager.Instance.PlayerIsHit.Opponent.CharacterDamageSender.HitCount;
        float comboResetTime = GameManager.Instance.PlayerIsHit.Opponent.CharacterDamageSender.ComboResetTime;
        if (comboResetTime <= 0) hitCountUI.gameObject.SetActive(false);
        else
        {
            if (GameManager.Instance.PlayerIsHit.Opponent == GameManager.Instance.RedPlayer)
                hitCountUI.RectTransform.anchoredPosition = new Vector2(-550, 200);
            else hitCountUI.RectTransform.anchoredPosition = new Vector2(780, 200);
            hitCountUI.SetNumber(hitCount);
            hitCountUI.gameObject.SetActive(true);
        }
    }
}
