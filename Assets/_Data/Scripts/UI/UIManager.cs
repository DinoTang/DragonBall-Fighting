using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseBehaviour
{
    [SerializeField] protected List<RectTransform> uiChilds = new();
    public float kOSymboltimeOff = 3;
    public float timer;

    public bool hasShowKOUI;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIChilds();
    }

    protected void LoadUIChilds()
    {
        if (this.uiChilds.Count > 0) return;

        foreach (RectTransform child in this.transform)
        {
            this.uiChilds.Add(child);
        }
        Debug.Log(transform.name + ": LoadUIChilds");
    }

    protected void Update()
    {
        this.ShowKOUI();
        this.ShowWinnerUI();
    }

    protected void ShowKOUI()
    {
        if (this.hasShowKOUI) return;
        this.timer = GameManager.Instance.GameOverTimer;
        if (this.timer >= this.kOSymboltimeOff) uiChilds[3].gameObject.SetActive(false);
        else if (GameManager.Instance.IsGameOver && !this.hasShowKOUI)
        {
            this.hasShowKOUI = true;
            this.uiChilds[3].gameObject.SetActive(true);
        }
    }

    protected void ShowWinnerUI()
    {
        if (GameManager.Instance.CanShowResult())
        {
            this.HideAll();
            this.uiChilds[4].gameObject.SetActive(true);
        }
    }

    protected void HideAll()
    {
        foreach (var child in this.uiChilds)
        {
            child.gameObject.SetActive(false);
        }
    }
}
