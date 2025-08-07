using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttackSkillState : SkillState
{
    protected enum RushAttack_Type { Start, Combo, KnockUp, KnockDown, Follow }
    protected CharacterCtrl self;
    protected float dashRushAttackSpeed = 50f;
    protected Vector2 direction;
    protected RushAttack_Type currentRushAttackType;


    protected Animator selfAnimator;
    protected Animator trailAnimator;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.self = this.stateManager.CharacterCtrl;
        this.self.CharacterDamageReceiver.ToggleIgnoreEnemy(false);
        this.currentRushAttackType = RushAttack_Type.Start;
        this.direction = (this.self.Opponent.transform.position - this.stateManager.transform.position).normalized;

        this.self.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed < 0.2f) return;

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
        this.self.Rgb.isKinematic = false;
        this.self.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected override float GetTotalDuration()
    {
        return 0.5f;
    }

    protected void PlayRushAttackStart()
    {
        if (this.stateManager.mainStateType is AirState) this.self.Rgb.constraints = RigidbodyConstraints2D.FreezePositionY;
        if (this.timed >= this.totalDuration)
        {
            this.self.CharacterAnimEvent.DisableHitbox();
            this.stateManager.SetNextStateToMain();
            return;
        }

        this.self.Rgb.velocity = this.direction * this.dashRushAttackSpeed;
        Debug.LogWarning(this.self.Rgb.velocity);
        if (this.self.CharacterDamageSender.IsHitting)
        {
            this.timed = 0;
            this.self.CharacterDamageSender.SetIsHitting(false);
            this.currentRushAttackType = RushAttack_Type.Combo;
            Debug.LogWarning("Vô RushAttack_Combo");
        }
    }

    protected void PlayRushAttackCombo()
    {

        var rushAttackSkill = this.skill as RushAttackSkillSO;
        string animName = rushAttackSkill.RushAttack_Combo.name;
        var animator = this.self.Animator;
        animator.Play(animName);

        float animTime = rushAttackSkill.RushAttack_Combo.length;
        if (this.timed >= animTime)
        {
            if (this.self.CharacterGroundCollision.IsGround)
                this.currentRushAttackType = RushAttack_Type.KnockUp;
            else
                this.currentRushAttackType = RushAttack_Type.KnockDown;

            this.timed = 0;
        }
    }

    protected void PlayRushAttackKnockUp()
    {
        var rushAttackSkill = this.skill as RushAttackSkillSO;
        string animName = rushAttackSkill.RushAttack_KnockUp.name;
        var animator = this.self.Animator;
        animator.Play(animName);

        if (this.self.CharacterDamageSender.IsKnockUp)
        {
            this.self.Opponent.StateManager.SetNextState(new RushAttackWasKnockUpState());
            this.self.Rgb.velocity = new Vector2(this.direction.x * -1 * 10, 20);
        }


        float animTime = rushAttackSkill.RushAttack_Combo.length;
        if (this.timed >= animTime)
        {
            this.self.Rgb.velocity = new Vector2(0, -10);
            if (!this.self.CharacterGroundCollision.IsGround) return;
            Vector2 newTrailPos = this.stateManager.transform.position;
            newTrailPos.y -= 1.32f;
            this.self.Trail.transform.position = newTrailPos;
            this.self.Trail.transform.localScale = new Vector3(this.direction.x, 1, 1);


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
        // Có thể thêm hiệu ứng rơi, hoặc combo tiếp
    }

    protected void PlayRushAttackFollow()
    {
        var rushAttackSkill = this.skill as RushAttackSkillSO;

        Vector3 start = this.stateManager.transform.position;
        start.x -= 0.5f * this.direction.x; // Lùi nhẹ về phía sau tùy theo hướng

        Vector3 end = this.self.Opponent.transform.position;
        end.x -= this.direction.x * 1f; // Bay lệch nhẹ ra phía sau đối thủ (giống offsetEnd)

        if (this.timed >= 0.1f)
        {
            rushAttackSkill.Execute(this.self, start, end, 0.2f);
        }

        // Khi bay gần tới thì gây hiệu ứng
        float distance = Vector3.Distance(this.stateManager.transform.position, end);
        if (distance <= 1f)
        {
            // this.self.Opponent.StateManager.SetNextState(new AirHurtState());
            this.self.Opponent.StateManager.SetNextStateToMain();
            this.stateManager.SetNextStateToMain();
        }
    }

}
