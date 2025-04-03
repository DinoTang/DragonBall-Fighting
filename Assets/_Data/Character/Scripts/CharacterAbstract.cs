using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterAbstract : DinoBehaviour
{
    [Header("Character Abstract")]
    [SerializeField] protected CharacterCtrl characterCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerCtrl();
    }

    protected void LoadPlayerCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }

}

