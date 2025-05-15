using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotFX : PoolObj
{
    [SerializeField] protected CharacterEnum characterEnum;
    [SerializeField] protected Model model;
    public override string GetName()
    {
        return this.characterEnum.ToString();
    }
    
    public void Init(CharacterCtrl owner)
    {
        if (owner.transform.localScale.x == -1)
        {
            this.model.transform.localScale = new Vector3(-1, 1, 1) * this.model.Size;
        }
        else
        {
            this.model.transform.localScale = new Vector3(1, 1, 1) * this.model.Size;
        }
    }
}
