using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : BaseBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected RectTransform delayedHealthBar;
    [SerializeField] protected RectTransform immediateHealthBar;
    [SerializeField] protected RectTransform fullHealthBar;

    protected float fullWidth;
    protected Vector2 startPos;
    protected bool hasLostHealth;

    protected float displayedHP;   // máu hiển thị
    protected float delayedDisplayedHP;

    [SerializeField] protected float immediateSpeed = 15f; // tốc độ tụt thanh xanh
    [SerializeField] protected float delayedSpeed = 10f;    // tốc độ tụt thanh đỏ
    [SerializeField] protected float delayTime = 0.5f;       // thời gian chờ trước khi tụt thanh đỏ

    [SerializeField] protected List<RectTransform> healthBars = new();
    protected float damageTimer = 0f; // đếm giây kể từ lúc nhận damage cuối
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
        this.LoadHealthBars();
    }
    protected virtual void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
    }
    protected override void Start()
    {
        this.displayedHP = this.characterCtrl.CharacterDamageReceiver.CurrentHP;
        this.delayedDisplayedHP = this.displayedHP;

        this.fullWidth = this.immediateHealthBar.rect.width;
        this.startPos = this.immediateHealthBar.anchoredPosition;
    }

    protected void Update()
    {
        this.UpdateHealthBar();
    }

    protected void LoadHealthBars()
    {
        if (this.delayedHealthBar != null && this.immediateHealthBar != null && this.fullHealthBar != null) return;
        this.delayedHealthBar = this.transform.GetChild(0).GetChild(0) as RectTransform;
        this.immediateHealthBar = this.transform.GetChild(0).GetChild(1) as RectTransform;
        this.fullHealthBar = this.transform.GetChild(0).GetChild(2) as RectTransform;
    }

    protected void UpdateHealthBar()
    {
        float current = characterCtrl.CharacterDamageReceiver.CurrentHP;
        float max = characterCtrl.CharacterDamageReceiver.HpMax;
        if (!hasLostHealth && current < max)
        {
            this.fullHealthBar.gameObject.SetActive(false);
            hasLostHealth = true;
        }

        if (current < displayedHP - 0.01f) // ăn damage
            damageTimer = 0f;
        else
            damageTimer += Time.deltaTime;

        displayedHP = Mathf.Lerp(displayedHP, current, Time.deltaTime * immediateSpeed);

        if (damageTimer >= delayTime)
        {
            delayedDisplayedHP = Mathf.Lerp(delayedDisplayedHP, displayedHP, Time.deltaTime * delayedSpeed);
        }

        // Update UI
        this.UpdateBar(immediateHealthBar, displayedHP, max);
        this.UpdateBar(delayedHealthBar, delayedDisplayedHP, max);
    }
    protected void UpdateBar(RectTransform bar, float hp, float max)
    {
        float percent = Mathf.Clamp01(hp / max);
        float offsetX = (1f - percent) * fullWidth;
        bar.anchoredPosition = new Vector2(startPos.x + offsetX, startPos.y);
    }
}
