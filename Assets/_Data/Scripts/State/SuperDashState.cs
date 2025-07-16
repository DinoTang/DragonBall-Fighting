using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SuperDashState : State
{
    protected float speed = 70f;
    protected Vector2 targetLiftPos;
    protected float originalGravity;
    protected float knockbackForceX = 20;
    protected float knockbackForceY = 40;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsSuperDash", true);
        this.ResetGravityAndVelocity();
        this.SetupPosition();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.ReleaseSuperDashInput()) this.stateManager.SetNextStateToMain();

        if (this.Timed < 0.35f)
        {
            this.HandleLift();
            return;
        }
        this.DashToOpponent();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsSuperDash", false);

        // Bật lại trọng lực
        this.stateManager.CharacterCtrl.Rgb.gravityScale = this.originalGravity;
    }

    protected void ResetGravityAndVelocity()
    {
        // Tắt trọng lực và quán tính trước đó 
        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;
        this.originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;


    }
    protected void SetupPosition()
    {
        // SetUp vị trí 
        Vector2 currentPos = this.stateManager.CharacterCtrl.transform.position;
        this.targetLiftPos = currentPos + new Vector2(0f, 0.5f);
    }
    protected void HandleLift()
    {
        if (!this.stateManager.CharacterCtrl.IsGround) return;

        Transform self = this.stateManager.transform;
        self.position = Vector2.MoveTowards(
            self.position,
            this.targetLiftPos,
            5f * Time.deltaTime
        );
    }
    protected void DashToOpponent()
    {
        CharacterCtrl opponent = this.stateManager.CharacterCtrl.Opponent;
        CharacterCtrl self = this.stateManager.CharacterCtrl;

        if (opponent == null)
        {
            Debug.LogWarning($"{this.GetType().Name}: Không tìm thấy đối phương.");
            this.stateManager.SetNextStateToMain();
            return;
        }

        float distance = Vector2.Distance(self.transform.position, opponent.transform.position);
        if (distance <= 2f)
        {
            if (opponent.CharacterDamageReceiver.IsBlock)
            {
                float dir = this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight ? 1 : -1;
                this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(-dir * this.knockbackForceX, this.knockbackForceY);
                Debug.Log(this.stateManager.CharacterCtrl.Rgb.velocity);
            }


            // this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, 40f);
            // opponent.Rgb.velocity = new Vector2(0, 40f);
            this.stateManager.SetNextState(new AirState());
            return;
        }

        this.Dashing(self, opponent);
    }

    protected void Dashing(CharacterCtrl self, CharacterCtrl opponent)
    {
        self.transform.position = Vector2.MoveTowards(
            self.transform.position,
            opponent.transform.position,
            this.speed * Time.deltaTime
        );
    }
}
