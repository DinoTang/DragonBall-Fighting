using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillManager : CharacterAbstract
{
    [SerializeField] protected List<SkillWithCooldown> skillList = new();
    [SerializeField] protected SkillDataSO currentSkill;
    public SkillDataSO CurrentSkill { get => currentSkill; set => currentSkill = value; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadSkillListSO();
    }

    protected void LoadSkillListSO()
    {
        if (this.skillList.Count > 0) return;
        List<SkillDataSO> skillList = this.characterCtrl.CharacterDataSO.skillList;
        foreach (var skillData in skillList)
        {
            this.skillList.Add(new SkillWithCooldown
            {
                skillData = skillData,
                cooldown = 0f
            });
        }

        Debug.Log($"{transform.name}: LoadSkillListSO", gameObject);
    }

    protected SkillWithCooldown GetSkill(SkillNameEnum slot)
    {
        return skillList.Find(s => s.skillData.skillName == slot);
    }

    public bool TryUseSkill(SkillNameEnum slot)
    {
        SkillWithCooldown skill = GetSkill(slot);
        if (skill == null || skill.IsOnCooldown)
        {
            Debug.Log($"Skill {slot} đang hồi chiêu");
            return false;
        }

        if (!characterCtrl.CharacterKiManager.CanUseSkill(skill.skillData.kiCost))
        {
            Debug.Log("Không đủ ki");
            return false;
        }


        this.currentSkill = skill.skillData;
        if (this.currentSkill.animationClip != null) characterCtrl.Animator.Play(this.currentSkill.animationClip.name);
        characterCtrl.CharacterKiManager.ConsumeKi(this.currentSkill.kiCost);

        return true;
    }

    public void StartCooldownFor(SkillNameEnum slot)
    {
        SkillWithCooldown skill = GetSkill(slot);
        if (skill != null) skill.StartCooldown();
    }

    protected void Update()
    {
        float dt = Time.deltaTime;
        foreach (var skill in skillList)
        {
            skill.UpdateCooldown(dt);
        }
    }
}
