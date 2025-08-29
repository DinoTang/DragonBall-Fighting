using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRedTeamManaBarCtrl : UIManaBarCtrl
{
    protected override void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = FindAnyObjectByType<PlayerRed>().GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
}
