using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlueTeamManaBarCtrl : UIManaBarCtrl
{
    protected override void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = FindAnyObjectByType<PlayerBlue>().GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
}
