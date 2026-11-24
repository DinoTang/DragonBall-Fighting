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

    // Fast O(1) dictionary lookup
    protected readonly Dictionary<UIType, UICtrl> uiDict = new();
    private int lastTimeMatch = -1;
    private int lastHitCount = -1;
    private CharacterCtrl lastHitCountOpponent = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.OnMatchReadyPhase += HandleMatchReadyPhase;
        GameManager.OnMatchFightPhase += HandleMatchFightPhase;
        GameManager.OnMatchFightStarted += HandleMatchFightStarted;
    }

    protected virtual void OnDisable()
    {
        GameManager.OnMatchReadyPhase -= HandleMatchReadyPhase;
        GameManager.OnMatchFightPhase -= HandleMatchFightPhase;
        GameManager.OnMatchFightStarted -= HandleMatchFightStarted;
    }

    private void HandleMatchReadyPhase()
    {
        GetUIType(UIType.RedInfoBar)?.gameObject.SetActive(true);
        GetUIType(UIType.BlueInfoBar)?.gameObject.SetActive(true);
        GetUIType(UIType.Clock)?.gameObject.SetActive(true);
        GetUIType(UIType.Ready)?.gameObject.SetActive(true);
    }

    private void HandleMatchFightPhase()
    {
        GetUIType(UIType.Ready)?.gameObject.SetActive(false);
        GetUIType(UIType.Fight)?.gameObject.SetActive(true);
    }

    private void HandleMatchFightStarted()
    {
        GetUIType(UIType.Fight)?.gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        if (UIManager.instance != null && UIManager.instance != this)
        {
            Debug.LogWarning("Only 1 UIManager allows to exist");
        }
        instance = this;
        this.InitializeUIDict();
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

    protected void InitializeUIDict()
    {
        this.uiDict.Clear();
        foreach (var child in this.uiChilds)
        {
            if (child != null && !this.uiDict.ContainsKey(child.UIType))
            {
                this.uiDict[child.UIType] = child;
            }
        }
    }

    protected void LoadUIChilds()
    {
        if (this.uiChilds.Count > 0) return;

        foreach (RectTransform child in this.transform)
        {
            UICtrl uiCtrlChild = child.GetComponent<UICtrl>();
            if (uiCtrlChild == null || uiCtrlChild is UIFade) continue;
            this.uiChilds.Add(uiCtrlChild);
        }
        this.InitializeUIDict();
    }

    protected void Update()
    {
        if (GameManager.Instance == null) return;
        this.ShowKOUI();
        this.ShowWinnerUI();
        this.ShowTimeOverUI();
        this.ShowTimeUI();
        this.ShowHitCountUI();
    }

    protected void ShowKOUI()
    {
        if (GameManager.Instance.IsTimeOver || !GameManager.Instance.IsGameOver) return;

        this.timer += Time.deltaTime;
        UICtrl koUI = this.GetUIType(UIType.KO);
        if (koUI == null) return;

        if (this.timer >= this.kOUiTimeOff)
        {
            koUI.gameObject.SetActive(false);
        }
        else if (!this.hasShowKOUI)
        {
            this.hasShowKOUI = true;
            koUI.gameObject.SetActive(true);
        }
    }

    protected void ShowTimeOverUI()
    {
        if (!GameManager.Instance.IsTimeOver) return;

        this.timer += Time.deltaTime;
        UICtrl timeOverUI = this.GetUIType(UIType.TimeOver);
        if (timeOverUI == null) return;

        if (this.timer >= this.timeOverUiTimeOff)
        {
            timeOverUI.gameObject.SetActive(false);
        }
        else if (!this.hasShowTimeOverUI)
        {
            this.hasShowTimeOverUI = true;
            timeOverUI.gameObject.SetActive(true);
        }
    }

    protected void HideAll()
    {
        foreach (var child in this.uiChilds)
        {
            if (child != null)
                child.gameObject.SetActive(false);
        }
    }

    protected void ShowWinnerUI()
    {
        if (!GameManager.Instance.IsGameOver) return;
        if (GameManager.Instance.GameOverTimer < GameManager.Instance.GameOverDelay) return;
        if (GameManager.Instance.Winner == null) return;

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

        UICtrl winnerUI = this.GetUIType(UIType.Winner);
        if (winnerUI != null)
        {
            winnerUI.gameObject.SetActive(true);
            if (GameManager.Instance.Winner == GameManager.Instance.RedPlayer)
                winnerUI.RectTransform.anchoredPosition = new Vector2(-560, 165);
            else
                winnerUI.RectTransform.anchoredPosition = new Vector2(560, 165);
        }
    }

    public UICtrl GetUIType(UIType uIType)
    {
        if (this.uiDict.TryGetValue(uIType, out UICtrl ui))
            return ui;

        // Fallback if dictionary wasn't populated
        UICtrl found = this.uiChilds.Find(e => e != null && e.UIType == uIType);
        if (found != null)
        {
            this.uiDict[uIType] = found;
        }
        return found;
    }

    protected void ShowTimeUI()
    {
        int timeMatch = Mathf.CeilToInt(GameManager.Instance.TimerMatch);
        if (timeMatch != this.lastTimeMatch)
        {
            this.lastTimeMatch = timeMatch;
            if (this.GetUIType(UIType.Clock) is UIClock clockUI)
            {
                clockUI.SetNumber(timeMatch);
            }
        }
    }

    protected void ShowHitCountUI()
    {
        if (GameManager.Instance.PlayerIsHit == null) return;
        var hitCountUI = this.GetUIType(UIType.HitCount) as UIHitCount;
        if (hitCountUI == null) return;

        CharacterCtrl opponent = GameManager.Instance.PlayerIsHit.Opponent;
        if (opponent == null || opponent.CharacterDamageSender == null) return;

        int hitCount = opponent.CharacterDamageSender.HitCount;
        float comboResetTime = opponent.CharacterDamageSender.ComboResetTime;

        if (comboResetTime <= 0)
        {
            if (hitCountUI.gameObject.activeSelf)
            {
                hitCountUI.gameObject.SetActive(false);
            }
            this.lastHitCount = -1;
            this.lastHitCountOpponent = null;
        }
        else
        {
            if (!hitCountUI.gameObject.activeSelf)
            {
                hitCountUI.gameObject.SetActive(true);
            }

            if (hitCount != this.lastHitCount || opponent != this.lastHitCountOpponent)
            {
                this.lastHitCount = hitCount;
                this.lastHitCountOpponent = opponent;

                if (opponent == GameManager.Instance.RedPlayer)
                    hitCountUI.RectTransform.anchoredPosition = new Vector2(-550, 200);
                else
                    hitCountUI.RectTransform.anchoredPosition = new Vector2(780, 200);

                hitCountUI.SetNumber(hitCount);
            }
        }
    }
}
