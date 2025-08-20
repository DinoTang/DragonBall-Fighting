using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class BasicSkillProjDamageSender : SkillProjDamageSender
{
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is CircleCollider2D circleCollider)
        {
            circleCollider.offset = new Vector2(0.4f, 0);
            circleCollider.radius = 0.3f;
        }
    }
}