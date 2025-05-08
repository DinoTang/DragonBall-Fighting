using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterData : CharacterAbstract
{
    [Header("Character Data")]
    [SerializeField] protected SkillDataSO skillDataSO;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillDataSO();
    }

    protected void LoadSkillDataSO()
    {
        if (this.skillDataSO != null) return;
        this.skillDataSO = Resources.Load<SkillDataSO>("SkillData/" + this.characterCtrl.CharacterName.ToString());
        Debug.Log(transform.name + ": LoadSkillDataSO", gameObject);
        Debug.Log(transform.name + ": " + "SkillData/" + this.characterCtrl.CharacterName.ToString(), gameObject);
    }
}

