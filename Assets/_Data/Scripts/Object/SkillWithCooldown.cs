using UnityEngine;

[System.Serializable]
public class SkillWithCooldown
{
    public SkillDataSO skillData;
    public float cooldown;

    public bool IsOnCooldown => cooldown > 0f;

    public void UpdateCooldown(float dt)
    {
        if (cooldown <= 0) return;
        cooldown -= dt;
        if (cooldown < 0) cooldown = 0;
    }

    public void StartCooldown()
    {
        cooldown = skillData.cooldown;
    }
}
