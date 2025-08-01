using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttackSkillState : SkillState
{
    protected enum RushAttack_Type { Start, Combo, KnockUp, KnockDown, Follow }

    protected float dashRushAttackSpeed = 50f;
    protected Vector2 direction;
    protected RushAttack_Type currentRushAttackType;

    // Parabol bay theo follow
    protected float followTimer = 0f;
    protected float followDuration = 0.6f;
    protected float arcHeight = 2f;
    protected Vector3 startPos;
    protected Vector3 endPos;

    protected Animator vegetaAnimator;
    protected Animator trailAnimator;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.currentRushAttackType = RushAttack_Type.Start;
        this.direction = (this.stateManager.CharacterCtrl.Opponent.transform.position - this.stateManager.transform.position).normalized;

        this.stateManager.CharacterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.followTimer = 0f;
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
        this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
        this.stateManager.CharacterCtrl.Rgb.isKinematic = false;
    }

    protected override float GetTotalDuration()
    {
        RushAttackSkillSO rushAttackSkillSO = this.skill as RushAttackSkillSO;
        return 0.5f;
    }

    protected void PlayRushAttackStart()
    {
        if (this.timed >= this.totalDuration)
        {
            this.stateManager.SetNextStateToMain();
            return;
        }

        this.stateManager.CharacterCtrl.Rgb.velocity = this.direction * this.dashRushAttackSpeed;

        if (this.stateManager.CharacterCtrl.CharacterDamageSender.IsHitting)
        {
            this.timed = 0;
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
            this.currentRushAttackType = RushAttack_Type.Combo;
            Debug.LogWarning("Vô RushAttack_Combo");
        }
    }

    protected void PlayRushAttackCombo()
    {
        var rushAttackSkill = this.skill as RushAttackSkillSO;
        string animName = rushAttackSkill.RushAttack_Combo.name;
        var animator = this.stateManager.CharacterCtrl.Animator;
        animator.Play(animName);

        float animTime = rushAttackSkill.RushAttack_Combo.length;
        if (this.timed >= animTime)
        {
            if (this.stateManager.CharacterCtrl.CharacterGroundCollision.IsGround)
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
        var animator = this.stateManager.CharacterCtrl.Animator;
        animator.Play(animName);
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(this.direction.x * -1 * 10, 10);

        float animTime = rushAttackSkill.RushAttack_Combo.length;
        if (this.timed >= animTime)
        {
            this.stateManager.CharacterCtrl.Opponent.StateManager.SetNextState(new RushAttackWasKnockUpState());
            this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, -10);
            if (!this.stateManager.CharacterCtrl.CharacterGroundCollision.IsGround) return;
            Vector2 newTraiPos = this.stateManager.transform.position;
            newTraiPos.y -= 1.32f;
            this.stateManager.CharacterCtrl.Trail.transform.position = newTraiPos;
            this.stateManager.CharacterCtrl.Trail.transform.localScale = new Vector3(this.direction.x, 1, 1);


            this.vegetaAnimator = this.stateManager.CharacterCtrl.Animator;
            this.trailAnimator = this.stateManager.CharacterCtrl.TrailAnimator;

            this.vegetaAnimator.Play(rushAttackSkill.RushAttack_AirFollow.name);
            this.trailAnimator.Play(rushAttackSkill.RushAttack_Trail.name);
            this.currentRushAttackType = RushAttack_Type.Follow;
            this.timed = 0;
        }
    }

    protected void PlayRushAttackKnockDown()
    {
        // Có thể thêm hiệu ứng rơi, hoặc combo tiếp
    }
    private bool hasResetRotation = false;
    protected void PlayRushAttackFollow()
    {
        var rushAttackSkill = this.skill as RushAttackSkillSO;
        // if (!this.stateManager.CharacterCtrl.CharacterGroundCollision.IsGround) return;
        Vector3 start = this.stateManager.transform.position;
        start.x -= 0.5f;
        Vector3 end = this.stateManager.CharacterCtrl.Opponent.transform.position;
        end.x *= this.direction.x;
        int offsetEnd = this.direction.x > 0 ? -1 : 1;
        end.x += offsetEnd;
        Vector3 mid = (start + end) / 2;
        Vector3 control = mid + Vector3.up * 4f;
        if (this.timed >= 0.1)
            rushAttackSkill.Execute(this.stateManager.CharacterCtrl, this.stateManager.transform, start, control, end, 0.4f);


        float distance = Vector3.Distance(this.stateManager.transform.position, end);
        if (distance <= 1.5f && !hasResetRotation)
        {
            this.stateManager.transform.rotation = Quaternion.identity;
            hasResetRotation = true;
        }
        if (distance <= 1f)
        {

            // this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(3, 5);
            this.stateManager.CharacterCtrl.Opponent.StateManager.SetNextState(new AirHurtState());
            this.stateManager.SetNextStateToMain();
        }
    }
}
