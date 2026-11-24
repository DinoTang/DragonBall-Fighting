using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RushAttackWasKnockUpState : CharacterState
{
    protected float knockDuration = 5f;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsKnockUp", true);

        // Đẩy nhân vật lên bằng velocity
        int dir = this.stateManager.CharacterCtrl.CharacterFlip.Direct * -1;
        this.stateManager.CharacterCtrl.Rgb.linearVelocity = new Vector2(10 * dir, 30);

        // Không freeze ở đây nữa
        this.stateManager.CharacterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;

        // Khi tới gần đỉnh bay lên (velocity.y gần 0) thì freeze lại
        if (rb.linearVelocity.y < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockUp", false);
        this.stateManager.CharacterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.stateManager.CharacterCtrl.Rgb.linearVelocity = new Vector2(0, 30);
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 15;
    }
}

