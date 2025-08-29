using System.Collections.Generic;
using UnityEngine;

public class ManaBarUI : BaseBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected List<RectTransform> manaBars; // 3 thanh mana
    private float fullWidth;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
        this.LoadManaBars();
    }
    protected override void Start()
    {
        if (manaBars.Count > 0)
            fullWidth = manaBars[0].rect.width;
    }

    protected void Update()
    {
        UpdateManaBar();
    }
    protected void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        UIManaBarCtrl uiManaBarCtrl = transform.GetComponentInParent<UIManaBarCtrl>();
        this.characterCtrl = uiManaBarCtrl.CharacterCtrl;
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
    protected void LoadManaBars()
    {
        if (this.manaBars.Count > 0) return;
        foreach (RectTransform child in this.transform.GetChild(0))
        {
            this.manaBars.Add(child);
        }
    }
    protected void UpdateManaBar()
    {
        float currentKi = characterCtrl.CharacterKiManager.CurrentKi;
        float kiMax = characterCtrl.CharacterKiManager.KiMax;
        float kiPerBar = kiMax / manaBars.Count;

        float remainKi = currentKi;

        // Reset tất cả thanh trước
        for (int i = 0; i < manaBars.Count; i++)
        {
            float fill = Mathf.Clamp01(remainKi / kiPerBar);
            float offsetX = (1f - fill) * fullWidth;

            manaBars[i].anchoredPosition = new Vector2(offsetX, manaBars[i].anchoredPosition.y);

            remainKi -= kiPerBar;
        }
    }
}
