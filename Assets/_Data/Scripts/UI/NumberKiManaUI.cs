using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberKiManaUI : BaseBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected List<RectTransform> numbersKi = new();
    protected int numberKiCount;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
        this.LoadNumbersKi();
    }
    protected void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        UIManaBarCtrl uiManaBarCtrl = transform.GetComponentInParent<UIManaBarCtrl>();
        this.characterCtrl = uiManaBarCtrl.CharacterCtrl;
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
    protected void LoadNumbersKi()
    {
        if (this.numbersKi.Count > 0) return;

        foreach (RectTransform child in this.transform)
        {
            numbersKi.Add(child);
            child.gameObject.SetActive(false);
        }
        Debug.Log(transform.name + ": LoadNumbersKi");
    }

    protected void Update()
    {
        this.UpdateNumberKi();
    }

    protected void UpdateNumberKi()
    {
        float current = this.characterCtrl.CharacterKiManager.CurrentKi;

        this.numberKiCount = (int)current / 100;

        // reset hết
        for (int i = 0; i < numbersKi.Count; i++)
        {
            numbersKi[i].gameObject.SetActive(i == this.numberKiCount);
        }
    }
}
