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

    [Header("Curve Settings")]
    public AnimationCurve followCurve; // Giá trị Y theo thời gian T (0 → 1)

    [Header("Debug")]
    public Color gizmoColor = Color.yellow;
    public int gizmoResolution = 20;
    public float gizmoHeightScale = 3f;

    public override State GetSkillState()
    {
        return new RushAttackSkillState();
    }


    private bool hasPlayedSound = false;

    public void Execute(CharacterCtrl user, Vector3 start, Vector3 end, float duration)
    {
        if (!hasPlayedSound) // chỉ phát lần đầu
        {
            AudioManager.Instance.AudioSource.PlayOneShot(
                user.CharacterData.CharacterDataSO.groundHitClip);
            hasPlayedSound = true;
        }

        user.StartCoroutine(CurveMove(user, start, end, duration));
    }

    public void ResetSkill()
    {
        hasPlayedSound = false; // reset khi bắt đầu skill mới
    }


    IEnumerator CurveMove(CharacterCtrl obj, Vector3 start, Vector3 end, float duration)
    {
        obj.Rgb.isKinematic = true;
        // obj.Rgb.velocity = Vector2.zero;

        float timer = 0f;
        Vector3 dir = end - start;
        Vector3 flatDir = new Vector3(dir.x, 0, dir.z);

        while (timer < duration)
        {
            if (Vector3.Distance(obj.transform.position, end) <= 1f)
                break;
            float t = timer / duration;
            float heightOffset = followCurve.Evaluate(t) * gizmoHeightScale;
            Vector3 pos = Vector3.Lerp(start, end, t);
            pos.y += heightOffset;
            obj.transform.position = pos;

            timer += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = end;

        obj.Rgb.isKinematic = false;
        // obj.Rgb.velocity = Vector2.zero;
    }
}
