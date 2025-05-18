using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : CharacterAbstract
{
    [Header("Skill Manager")]
    [SerializeField] protected List<SkillDataSO> skillList;
    private Dictionary<string, SkillDataSO> skillDict;
    private Dictionary<string, float> cooldowns = new();
    protected override void Awake()
    {
        base.Awake();
        this.skillDict = this.skillList.ToDictionary(s => s.skillName, s => s);
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillListSO();
    }

    protected void LoadSkillListSO()
    {
        if (this.skillList.Count > 0) return;
        SkillDataSO[] skillDataSO = Resources.LoadAll<SkillDataSO>("SkillData/Vegeta");
        skillList.AddRange(skillDataSO);
        Debug.Log(transform.name + ": LoadSkillListSO", gameObject);
    }

    public bool TryUseSkill(string skillName)
    {
        if (!skillDict.ContainsKey(skillName))
        {
            Debug.Log("No have [" + skillName + "] in skillList");
            return false;
        }

        SkillDataSO skill = skillDict[skillName];

        if (cooldowns.ContainsKey(skillName) && Time.time < cooldowns[skillName])
        {
            Debug.Log("Chiêu chưa hồi!");
            return false;
        }

        if (!this.characterCtrl.KiManager.CanUseSkill(skill.kiCost))
        {
            Debug.Log("No enough ki to use skill");
            return false;
        }
        
        this.characterCtrl.Animator.Play(skill.animation.name);
        this.characterCtrl.KiManager.ConsumeKi(skill.kiCost);
        cooldowns[skillName] = Time.time + skill.cooldown;
        return true;
    }
}
