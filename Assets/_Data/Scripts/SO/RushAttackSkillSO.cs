using System;
using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/RushAttackSkill")]
public class RushAttackSkillSO : SkillDataSO
{
    [Header("Rush Attack Skill")]
    public AnimationClip RushAttack_Combo;
    public AnimationClip RushAttack_KnockUp;
    public AnimationClip RushAttack_KnockDown;
    public AnimationClip RushAttack_AirFollow;
    public AnimationClip RushAttack_Trail;
    public override State GetSkillState()
    {
        return new RushAttackSkillState();
    }

    public void Execute(CharacterCtrl user, Transform obj, Vector3 p0, Vector3 p1, Vector3 p2, float duration)
    {
        user.StartCoroutine(BezierMove(obj, p0, p1, p2, duration));
    }
    IEnumerator BezierMove(Transform obj, Vector3 p0, Vector3 p1, Vector3 p2, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            // Bezier interpolation
            Vector3 pos = Mathf.Pow(1 - t, 2) * p0 +
                          2 * (1 - t) * t * p1 +
                          Mathf.Pow(t, 2) * p2;

            // Bezier tangent (hướng chuyển động)
            Vector3 tangent = 2 * (1 - t) * (p1 - p0) + 2 * t * (p2 - p1);

            // Set vị trí
            obj.position = pos;

            // Tính khoảng cách tới đích
            float distanceToTarget = Vector3.Distance(pos, p2);

            if (distanceToTarget > 2f) // chưa tới, còn xa thì xoay theo hướng bay
            {
                // float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
                // obj.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else // gần tới -> reset xoay luôn
            {
                obj.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            time += Time.deltaTime;
            yield return null;
        }

        // Kết thúc, đảm bảo đúng đích và xoay đúng
        obj.position = p2;
        obj.rotation = Quaternion.Euler(0f, 0f, 0f);
    }




}