using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SuperDashState : CharacterState
{
    protected float speed = 70f;
    protected Vector2 targetLiftPos;
    protected float originalGravity;
    protected float knockbackForceX = 15;
    protected float knockbackForceY = 20;
    protected float hitKnockbackForceY = 40;
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
        if (InputManager.Instance.ReleaseSuperDashInput(this.playerType)) this.stateManager.SetNextStateToMain();

        if (this.timed < 0.35f)
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
        if (this.stateManager.mainStateType is AirState) return;
        Transform self = this.stateManager.transform;

        self.position = Vector2.MoveTowards(
            self.position,
            this.targetLiftPos,
            5f * Time.deltaTime
        );
    }
    protected void DashToOpponent()
    {
        this.stateManager.CharacterCtrl.CharacterCollision.IsGround = false;
        CharacterCtrl opponent = this.stateManager.CharacterCtrl.Opponent;
        CharacterCtrl self = this.stateManager.CharacterCtrl;

        if (opponent == null)
        {
            Debug.LogWarning($"{this.GetType().Name}: Không tìm thấy đối phương.");
            this.stateManager.SetNextStateToMain();
            return;
        }

        if (!this.TryHandleDashHit(self, opponent)) return;
        this.Dashing(self, opponent);
    }

    protected bool TryHandleDashHit(CharacterCtrl self, CharacterCtrl opponent)
    {
        float distance = Vector2.Distance(self.transform.position, opponent.transform.position);
        if (distance <= 1.5f)
        {
            int dir = this.stateManager.CharacterCtrl.CharacterFlip.Direct;
            if (opponent.CharacterDamageReceiver.IsBlock || opponent.StateManager.CurrentState is KnockdownIdleState)
            {
                this.HandleBlockedDashHit(dir);
            }
            else
            {
                this.HandleSuccessfulDashHit(self, opponent);
            }
            this.stateManager.CanSuperDash = false;
            this.stateManager.SetNextState(new AirState());
            return false;
        }
        return true;
    }

    protected void HandleBlockedDashHit(int dir)
    {
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.OnParryFX();
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(-dir * this.knockbackForceX, this.knockbackForceY);
    }

    protected void HandleSuccessfulDashHit(CharacterCtrl self, CharacterCtrl opponent)
    {
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, this.hitKnockbackForceY);
        float heightOffset = self.transform.position.y - opponent.transform.position.y;
        float finalKnockbackY = this.hitKnockbackForceY + heightOffset * 1.2f; // nhân nhẹ để tạo lực bật cao hơn nếu chênh

        bool bonusForceCondition = this.stateManager.mainStateType.GetType() == typeof(IdleState);
        float bonusForce = bonusForceCondition ? 2 : 3;
        opponent.Rgb.velocity = new Vector2(0, finalKnockbackY + bonusForce);

        opponent.StateManager.SetNextState(new KnockUpState());
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
