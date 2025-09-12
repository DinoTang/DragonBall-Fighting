using System;
using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/TeleportPunchSkill")]
public class TeleportPunchSkillSO : SkillDataSO
{
    [Header("Teleport Punch Skill")]
    public float offset = 2f;
    public AnimationClip firstHitAnim;
    public AnimationClip secondHitAnim;
    public float waitBetween = 2f;
    public override CharacterState GetSkillState()
    {
        return new TeleportPunchSkillState();
    }
    public void Execute(CharacterCtrl user, CharacterCtrl target)
    {
        user.StartCoroutine(TeleportAndPunch(user, target));
    }

    private IEnumerator TeleportAndPunch(CharacterCtrl user, CharacterCtrl target)
    {
        Vector2 targetPos = target.transform.position;
        float direction = target.transform.localScale.x;

        Vector2 front = targetPos + new Vector2(offset * direction, 0f);
        Vector2 back = targetPos - new Vector2(offset * direction, 0f);

        user.transform.position = front;
        user.Animator.Play(this.firstHitAnim.name);

        yield return new WaitForSeconds(this.waitBetween);

        user.transform.position = back;
        user.Animator.Play(this.secondHitAnim.name);
    }

}