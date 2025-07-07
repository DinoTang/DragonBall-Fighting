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
        if (skillList.Count > 0) return;

        SkillDataSO[] loadedSkills = Resources.LoadAll<SkillDataSO>("SkillData/Vegeta");
        foreach (var skillData in loadedSkills)
        {
            skillList.Add(new SkillWithCooldown
            {
                skillData = skillData,
                cooldown = 0f
            });
        }

        Debug.Log($"{transform.name}: LoadSkillListSO", gameObject);
    }

    protected SkillWithCooldown GetSkill(SkillSlot slot)
    {
        return skillList.Find(s => s.skillData.skillName == slot);
    }

    public bool TryUseSkill(SkillSlot slot)
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

        currentSkill = skill.skillData;
        characterCtrl.Animator.Play(currentSkill.animationClip.name);
        characterCtrl.CharacterKiManager.ConsumeKi(currentSkill.kiCost);

        // Hiệu ứng lóe lên khi dùng skill
        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        pos.x -= 1f;
        ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Skill_Cast_Flash.ToString(), pos, this.characterCtrl);
        return true;
    }

    public void StartCooldownFor(SkillSlot slot)
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
