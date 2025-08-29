using System.Collections;
using UnityEngine;

public class PlayerBlueHealthBarUI : HealthBarUI
{
    protected override void LoadCharacterCtrl()
    {
        base.LoadCharacterCtrl();
        this.characterCtrl = FindAnyObjectByType<PlayerBlue>().GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
}
