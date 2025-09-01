using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UICtrl : BaseBehaviour
{
    [SerializeField] protected UIType uiType;
    public UIType UIType => uiType;
    [SerializeField] protected RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.rectTransform = GetComponent<RectTransform>();
    }
    protected UIType GetName()
    {
        return this.uiType;
    }
}
