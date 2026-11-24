using System.Collections;
using UnityEngine;
public class DiagonalBeamSkillProjDamageSender : BeamSkillDamageSender
{
    protected override void LoadCollider()
    {
        base.LoadCollider();
        this._collider.offset = new Vector2(-0.2f, -0.1f);
        if (this._collider is BoxCollider2D boxCollider)
        {
            boxCollider.size = new Vector2(4f, 4f);
        }
    }
}