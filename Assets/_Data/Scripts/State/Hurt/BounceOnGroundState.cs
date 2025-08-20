using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnGroundState : State
{
    private int bounceCount = 0;
    private int maxBounce = 3;
    private float bounceForce = 20f;
    private float damping = 0.8f;
    private bool waitingForNextBounce = false;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsImmortal(true);
        this.animator.SetBool("IsKnockUp", true);
        // this.stateManager.CharacterCtrl.Rgb.gravityScale = 15f;
        this.BounceUp();
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.ToggleIgnoreEnemy(true);

        if (this.stateManager.CharacterCtrl.CharacterCollision.IsGround && !waitingForNextBounce)
        {
            if (this.bounceCount < this.maxBounce)
            {
                this.BounceUp();
                waitingForNextBounce = true;
            }
            else
            {
                this.stateManager.SetNextState(new KnockdownIdleState());
            }
        }

        // Khi đang ở trên không, cho phép bounce lại khi lần sau chạm đất
        if (!this.stateManager.CharacterCtrl.CharacterCollision.IsGround)
        {
            waitingForNextBounce = false;
        }
    }


    private void BounceUp()
    {
        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;
        rb.velocity = new Vector2(0, bounceForce);
        bounceForce *= damping;
        bounceCount++;
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockUp", false);
    }
}
