using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class BigBangAttackDamageSender : BlastSkillDamageSender
{
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is CircleCollider2D circleCollider)
        {
            circleCollider.offset = new Vector2(0.4f, 0);
            circleCollider.radius = 1.5f;
        }
    }
}