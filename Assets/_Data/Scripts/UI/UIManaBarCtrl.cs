using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManaBarCtrl : BaseBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    public CharacterCtrl CharacterCtrl => characterCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
    }
    protected virtual void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
    }
}
