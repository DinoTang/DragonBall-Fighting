using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SuperDashState : State
{
    protected float speed = 70f;
    protected Vector2 targetLiftPos;
    protected float originalGravity;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsSuperDash", true);

        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;
        this.originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 currentPos = this.stateManager.CharacterCtrl.transform.position;
        this.targetLiftPos = currentPos + Vector2.up;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.ReleaseSuperDashInput())
        {
            this.stateManager.SetNextStateToMain();
        }
        if (this.Timed < 0.35f)
        {
            this.stateManager.transform.position = Vector2.MoveTowards(
                this.stateManager.transform.position,
                this.targetLiftPos,
                5f * Time.deltaTime
            );
            return;
        }
        this.FlightToTarget();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsSuperDash", false);

        // Bật lại trọng lực
        this.stateManager.CharacterCtrl.Rgb.gravityScale = this.originalGravity;
    }


    protected void FlightToTarget()
    {
        Transform self = this.stateManager.CharacterCtrl.transform;
        CharacterCtrl opponent = this.stateManager.CharacterCtrl.Opponent;
        float distance = Vector2.Distance(self.position, opponent.transform.position);
        if (distance <= 2f) this.stateManager.SetNextStateToMain();
        self.position = Vector2.MoveTowards(self.position, opponent.transform.position, this.speed * Time.deltaTime);
    }
}
