using System.Collections;
using UnityEngine;

public class PlayerRedHealthBarUI : HealthBarUI
{
    protected override void LoadCharacterCtrl()
    {
        base.LoadCharacterCtrl();
        this.characterCtrl = FindAnyObjectByType<PlayerRed>().GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
}
