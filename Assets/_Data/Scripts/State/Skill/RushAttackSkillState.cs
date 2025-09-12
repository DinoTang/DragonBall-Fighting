using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttackSkillState : SkillState
{
    protected enum RushAttack_Type { Start, Combo, KnockUp, KnockDown, Follow }
    protected CharacterCtrl self;
    protected float dashRushAttackSpeed = 45f;
    protected RushAttack_Type currentRushAttackType;
    protected int dir;

    protected Animator selfAnimator;
    protected Animator trailAnimator;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.self = this.stateManager.CharacterCtrl;
        this.currentRushAttackType = RushAttack_Type.Start;
        this.self.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.dir = this.self.CharacterFlip.Direct;
        if (this.stateManager.mainStateType is AirState)
            this.self.Rgb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed < 0.1f) return;
        switch (this.currentRushAttackType)
        {
            case RushAttack_Type.Start:
                this.PlayRushAttackStart();
                break;
            case RushAttack_Type.Combo:
                this.PlayRushAttackCombo();
                break;
            case RushAttack_Type.KnockUp:
                this.PlayRushAttackKnockUp();
                break;
            case RushAttack_Type.KnockDown:
                this.PlayRushAttackKnockDown();
                break;
            case RushAttack_Type.Follow:
                this.PlayRushAttackFollow();
                break;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.self.CharacterDamageSender.SetIsHitting(false);

        this.self.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected override float GetTotalDuration()
    {
        return 0.5f;
    }

    protected void PlayRushAttackStart()
    {
        if (this.timed >= this.totalDuration || this.stateManager.CharacterCtrl.Opponent.CharacterDamageReceiver.IsKnockDown)
        {
            this.self.Rgb.velocity = new Vector2(0, this.self.Rgb.velocity.y);
            this.self.CharacterAnimEvent.DisableHitbox();
            this.stateManager.SetNextStateToMain();
            return;
        }

        this.self.Rgb.velocity = new Vector2(1 * this.dir, 0) * this.dashRushAttackSpeed;
        float distanceToAttack = Vector2.Distance(this.self.transform.position, this.self.Opponent.transform.position);


        if (distanceToAttack <= 1.5)
        {
            if (this.stateManager.CharacterCtrl.Opponent.StateManager.CurrentState is KnockdownIdleState)
            {
                this.self.Rgb.velocity = new Vector2(0, this.self.Rgb.velocity.y);
                this.self.CharacterAnimEvent.DisableHitbox();
                this.stateManager.SetNextStateToMain();
                return;
            }
            this.self.CharacterAnimEvent.EnableHitbox();
            if (this.self.Opponent.CharacterDamageReceiver.IsKnockDown) return;

            bool selfGrounded = this.self.CharacterCollision.IsGround;
            bool opponentGrounded = this.self.Opponent.CharacterCollision.IsGround;
            if (!selfGrounded && !opponentGrounded || selfGrounded && !opponentGrounded)
            {
                Vector2 isHitPos = this.stateManager.CharacterCtrl.Opponent.transform.position;
                isHitPos.y = this.stateManager.CharacterCtrl.transform.position.y;
                this.stateManager.CharacterCtrl.Opponent.transform.position = isHitPos;
            }
            else if (!selfGrounded && opponentGrounded)
            {
                Vector2 isHittingPos = this.stateManager.CharacterCtrl.transform.position;
                isHittingPos.y = this.stateManager.CharacterCtrl.Opponent.transform.position.y;
                this.stateManager.CharacterCtrl.transform.position = isHittingPos;
            }

            this.stateManager.CharacterCtrl.Rgb.velocity = Vector2.zero;
            this.stateManager.CharacterCtrl.Opponent.Rgb.velocity = Vector2.zero;
            this.timed = 0;
            this.currentRushAttackType = RushAttack_Type.Combo;
        }
    }
    protected void SetUpIsHitPos()
    {
        Vector2 isHitPos = this.stateManager.CharacterCtrl.Opponent.transform.position;
        isHitPos.x -= this.stateManager.CharacterCtrl.CharacterFlip.Direct * 1.5f;
        this.stateManager.transform.position = isHitPos;
    }
    protected void PlayRushAttackCombo()
    {
        this.SetUpIsHitPos();
        Vector2 pos = this.stateManager.CharacterCtrl.FirePoint.transform.position;
        pos.y += 0.5f;
        CameraManager.Instance.Focus(pos, 3f);
        var rushAttackSkill = this.skill as RushAttackSkillSO;
        string animName = rushAttackSkill.RushAttack_Combo.name;
        var animator = this.self.Animator;
        animator.Play(animName);
        if (this.stateManager.CharacterCtrl.Opponent.CharacterDamageReceiver.isDead) this.stateManager.SetNextStateToMain();



        float animTime = rushAttackSkill.RushAttack_Combo.length;
        if (this.timed >= animTime)
        {
            bool selfGrounded = this.self.CharacterCollision.IsGround;
            bool opponentGrounded = this.self.Opponent.CharacterCollision.IsGround;

            if (selfGrounded && !opponentGrounded)
            {
                this.currentRushAttackType = RushAttack_Type.KnockUp;
            }
            else if (!selfGrounded && opponentGrounded)
            {
                this.currentRushAttackType = RushAttack_Type.KnockUp;
            }
            else if (!selfGrounded && !opponentGrounded)
            {
                this.currentRushAttackType = RushAttack_Type.KnockDown;
            }
            else // Cả 2 đều grounded
            {
                this.currentRushAttackType = RushAttack_Type.KnockUp;
            }


            this.timed = 0;
        }
    }

    protected void PlayRushAttackKnockUp()
    {
        CameraManager.Instance.Focus(this.stateManager.transform.position, 3f);
        var rushAttackSkill = this.skill as RushAttackSkillSO;
        string animName = rushAttackSkill.RushAttack_KnockUp.name;
        var animator = this.self.Animator;
        animator.Play(animName);

        if (this.self.CharacterDamageSender.IsKnockingUp)
        {
            this.self.Opponent.StateManager.SetNextState(new RushAttackWasKnockUpState());
            this.self.Rgb.velocity = new Vector2(this.dir * -1 * 10, 20);
        }


        float animTime = rushAttackSkill.RushAttack_KnockUp.length;
        if (this.timed >= animTime)
        {
            this.self.Rgb.velocity = new Vector2(0, -10);
            if (!this.self.CharacterCollision.IsGround) return;
            Vector2 newTrailPos = this.stateManager.transform.position;
            newTrailPos.y -= 1.32f;
            this.self.Trail.transform.position = newTrailPos;
            this.self.Trail.transform.localScale = new Vector3(this.dir, 1, 1);


            this.selfAnimator = this.self.Animator;
            this.trailAnimator = this.self.TrailAnimator;


            this.selfAnimator.Play(rushAttackSkill.RushAttack_AirFollow.name);
            this.trailAnimator.Play(rushAttackSkill.RushAttack_Trail.name);
            this.currentRushAttackType = RushAttack_Type.Follow;
            this.timed = 0;
        }
    }

    protected void PlayRushAttackKnockDown()
    {
        CameraManager.Instance.ResetDefaultCamera();
        this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsKnockingDown(true);
        var rushAttackSkill = this.skill as RushAttackSkillSO;
        string animName = rushAttackSkill.RushAttack_KnockDown.name;
        var animator = this.self.Animator;
        animator.Play(animName);

        float animTime = rushAttackSkill.RushAttack_KnockDown.length;
        if (this.timed >= animTime)
        {
            this.self.CharacterAnimEvent.DisableHitbox();
            this.stateManager.SetNextStateToMain();
        }
    }

    protected void PlayRushAttackFollow()
    {
        CameraManager.Instance.ResetDefaultCamera();
        var rushAttackSkill = this.skill as RushAttackSkillSO;

        Vector3 start = this.stateManager.transform.position;
        start.x -= 0.5f * this.dir; // Lùi nhẹ về phía sau tùy theo hướng

        Vector3 end = this.self.Opponent.transform.position;
        end.x -= this.dir * 1f; // Bay lệch nhẹ ra phía sau đối thủ (giống offsetEnd)

        if (this.timed >= 0.1f)
        {
            rushAttackSkill.Execute(this.self, start, end, 0.2f);
        }

        // Khi bay gần tới thì gây hiệu ứng
        float distance = Vector3.Distance(this.stateManager.transform.position, end);
        if (distance <= 1f)
        {
            this.self.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
            this.self.Rgb.velocity = new Vector2(0, 30);

            // this.self.Opponent.StateManager.SetNextState(new AirHurtState());
            this.stateManager.CharacterCtrl.CharacterDamageSender.SendDamage(this.stateManager.CharacterCtrl.Opponent.CharacterDamageReceiver);
            this.self.Opponent.StateManager.SetNextStateToMain();
            this.stateManager.SetNextStateToMain();
        }
    }

}
