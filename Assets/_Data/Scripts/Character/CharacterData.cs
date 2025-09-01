using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterData : CharacterAbstract
{
    [Header("Character Data")]
    [SerializeField] protected CharacterDataSO characterData;
    public CharacterDataSO CharacterDataSO => characterData;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterDataSO();
    }

    protected void LoadCharacterDataSO()
    {
        if (this.characterData != null) return;
        this.characterData = Resources.Load<CharacterDataSO>("CharacterData/" + this.characterCtrl.CharacterName.ToString());
        Debug.Log(transform.name + ": LoadCharacterDataSO", gameObject);
    }
}

