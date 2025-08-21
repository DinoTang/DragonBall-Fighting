using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UltimateSkillState : EnergyProjectileSkillState
{
    protected enum UltimateState { Transform, RushAttack, ReadyShoot, StartShoot, End }
    protected UltimateState currentState;
    protected CharacterCtrl self;
    protected UltimateSkillDataSO ultimateSkillDataSO;
    protected float ultimateTime = 0;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.self = this.stateManager.CharacterCtrl;
        this.currentState = UltimateState.Transform;
    }
    protected override float GetTotalDuration()
    {
        this.ultimateSkillDataSO = this.skill as UltimateSkillDataSO;
        if (ultimateSkillDataSO == null) return 0;
        return base.GetTotalDuration() + ultimateSkillDataSO.animationClip.length + ultimateSkillDataSO.duration +
        ultimateSkillDataSO.rushAttackUltimateAnim.length + ultimateSkillDataSO.readyUltimateAnim.length;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.ultimateTime += Time.deltaTime;
        switch (this.currentState)
        {
            case UltimateState.Transform:
                this.PlayTransform();
                break;
            case UltimateState.RushAttack:
                this.PlayRushAttack();
                break;
            case UltimateState.ReadyShoot:
                this.PlayReadyShoot();
                break;
            case UltimateState.StartShoot:
                this.PlayStartShoot();
                break;
            case UltimateState.End:
                this.PlayEnd();
                break;
        }
    }

    protected void PlayTransform()
    {
        if (this.ultimateTime < this.ultimateSkillDataSO.animationClip.length) return;
        this.stateManager.transform.position = this.stateManager.CharacterCtrl.Opponent.FirePoint.transform.position + new Vector3(0, 0.4f);
        this.currentState = UltimateState.RushAttack;
        this.ultimateTime = 0;
    }

    protected void PlayRushAttack()
    {
        string animName = this.ultimateSkillDataSO.rushAttackUltimateAnim.name;
        this.self.Animator.Play(animName);

        if (this.ultimateTime >= this.ultimateSkillDataSO.rushAttackUltimateAnim.length)
        {
            this.stateManager.transform.position =
        this.stateManager.CharacterCtrl.Opponent.transform.position + new Vector3(7.5f * this.stateManager.transform.localScale.x, 7.3f);
            this.currentState = UltimateState.ReadyShoot;
            this.ultimateTime = 0;
        }
    }

    protected void PlayReadyShoot()
    {
        string animName = this.ultimateSkillDataSO.readyUltimateAnim.name;
        this.self.Animator.Play(animName);

        if (this.ultimateTime >= this.ultimateSkillDataSO.readyUltimateAnim.length)
        {
            this.currentState = UltimateState.StartShoot;
            this.ultimateTime = 0;
        }
    }

    protected void PlayStartShoot()
    {
        string animName = this.ultimateSkillDataSO.startUltimateAnim.name;
        this.self.Animator.Play(animName);

        if (this.ultimateTime >= this.ultimateSkillDataSO.duration)
        {
            this.currentState = UltimateState.End;
            this.ultimateTime = 0;
        }
    }

    protected void PlayEnd()
    {
        string animName = this.ultimateSkillDataSO.endBeamSkillAnim.name;
        this.self.Animator.Play(animName);
    }
}



