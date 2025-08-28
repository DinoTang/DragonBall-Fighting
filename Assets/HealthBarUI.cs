using System.Collections;
using UnityEngine;

public class HealthBarUI : BaseBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected RectTransform immediateHealthBar;
    [SerializeField] protected RectTransform fullHealthBar;
    [SerializeField] protected RectTransform delayedHealthBar;

    private float fullWidth;
    private Vector2 startPos;
    private bool hasLostHealth;

    private float displayedHP;   // máu hiển thị
    private float delayedDisplayedHP;

    [SerializeField] private float immediateSpeed = 15f; // tốc độ tụt thanh xanh
    [SerializeField] private float delayedSpeed = 3f;    // tốc độ tụt thanh đỏ
    [SerializeField] private float delayTime = 2f;       // thời gian chờ trước khi tụt thanh đỏ

    private float damageTimer = 0f; // đếm giây kể từ lúc nhận damage cuối
    protected override void Start()
    {
        this.displayedHP = this.characterCtrl.CharacterDamageReceiver.CurrentHP;
        this.delayedDisplayedHP = this.displayedHP;

        this.fullWidth = this.immediateHealthBar.rect.width;
        this.startPos = this.immediateHealthBar.anchoredPosition;
    }

    void Update()
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
        UpdateBar(immediateHealthBar, displayedHP, max);
        UpdateBar(delayedHealthBar, delayedDisplayedHP, max);
    }
    private void UpdateBar(RectTransform bar, float hp, float max)
    {
        float percent = Mathf.Clamp01(hp / max);
        float offsetX = -(1f - percent) * fullWidth;
        bar.anchoredPosition = new Vector2(startPos.x + offsetX, startPos.y);
    }
}
