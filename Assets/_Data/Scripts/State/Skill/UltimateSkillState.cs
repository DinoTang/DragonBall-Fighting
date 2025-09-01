using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UltimateSkillState : EnergyProjectileSkillState
{
    protected enum UltimatePhase { StartUp, CutSceneReady, Ready, CutScenePerform, Perform, Finish }
    protected float startUpEnd, cutSceneReadyEnd, readyEnd, cutScenePerformEnd, performEnd, finishEnd;
    protected UltimatePhase currentPhase;
    protected CharacterCtrl self;
    protected UltimateSkillDataSO ultimateSkillDataSO;
    protected float ultimateTime = 0;
    protected float offset = 5f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.self = this.stateManager.CharacterCtrl;
        this.currentPhase = UltimatePhase.StartUp;

        this.startUpEnd = this.ultimateSkillDataSO.animationClip.length;
        this.cutSceneReadyEnd = this.startUpEnd + this.ultimateSkillDataSO.cutSceneReadyUltimateAnim.length;
        this.readyEnd = this.cutSceneReadyEnd + this.ultimateSkillDataSO.readyUltimateAnim.length;
        this.cutScenePerformEnd = this.readyEnd + this.ultimateSkillDataSO.cutScenePerformUltimateAnim.length;
        this.performEnd = this.cutScenePerformEnd + this.ultimateSkillDataSO.duration;
        this.finishEnd = this.performEnd + this.ultimateSkillDataSO.endBeamSkillAnim.length;
    }
    protected override float GetTotalDuration()
    {
        this.ultimateSkillDataSO = this.skill as UltimateSkillDataSO;
        if (ultimateSkillDataSO == null) return 0;
        return base.GetTotalDuration()
        + this.ultimateSkillDataSO.cutSceneReadyUltimateAnim.length
        + this.ultimateSkillDataSO.readyUltimateAnim.length
        + this.ultimateSkillDataSO.cutScenePerformUltimateAnim.length
        + this.ultimateSkillDataSO.duration
        + this.ultimateSkillDataSO.endBeamSkillAnim.length;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.ultimateTime = this.timed;
        switch (this.currentPhase)
        {
            case UltimatePhase.StartUp:
                this.PlayStartUp();
                break;
            case UltimatePhase.CutSceneReady:
                this.PlayCutSceneReady();
                break;
            case UltimatePhase.Ready:
                this.PlayReady();
                break;
            case UltimatePhase.CutScenePerform:
                this.PlayCutScenePerform();
                break;
            case UltimatePhase.Perform:
                this.PlayPerform();
                break;
            case UltimatePhase.Finish:
                this.PlayFinish();
                break;
        }
    }

    protected void PlayStartUp()
    {
        if (this.stateManager.CharacterCtrl.CharacterFlip.DistanceOther <= 4)
        {
            TeleFX_Spawner.Instance.Spawn(TeleFXNameEnum.Vegeta.ToString(), this.stateManager.transform.position, this.stateManager.CharacterCtrl);
            Vector2 newPos = new Vector2(this.stateManager.transform.position.x - this.offset * this.stateManager.transform.localScale.x,
            this.stateManager.transform.position.y);
            this.stateManager.transform.position = newPos;
            TeleFX_Spawner.Instance.Spawn(TeleFXNameEnum.Vegeta.ToString(), this.stateManager.transform.position, this.stateManager.CharacterCtrl);
        }

        if (this.ultimateTime >= this.startUpEnd) this.currentPhase = UltimatePhase.CutSceneReady;

    }

    protected void PlayCutSceneReady()
    {
        CameraManager.Instance.Focus(this.stateManager.transform.position, 2f);
        this.animator.Play(this.ultimateSkillDataSO.cutSceneReadyUltimateAnim.name);
        Vector2 newOpponentPos = this.stateManager.CharacterCtrl.Opponent.transform.position;
        newOpponentPos.y = this.stateManager.transform.position.y;
        this.stateManager.CharacterCtrl.Opponent.transform.position = newOpponentPos;
        if (this.ultimateTime >= this.cutSceneReadyEnd) this.currentPhase = UltimatePhase.Ready;
    }

    protected void PlayReady()
    {
        this.animator.Play(this.ultimateSkillDataSO.readyUltimateAnim.name);
        if (this.ultimateTime >= this.readyEnd) this.currentPhase = UltimatePhase.CutScenePerform;
    }

    protected void PlayCutScenePerform()
    {
        CameraManager.Instance.Focus(this.stateManager.transform.position, 2f);
        this.animator.Play(this.ultimateSkillDataSO.cutScenePerformUltimateAnim.name);
        if (this.ultimateTime >= this.cutScenePerformEnd) this.currentPhase = UltimatePhase.Perform;
    }
    protected void PlayPerform()
    {
        CameraManager.Instance.ResetDefaultCamera();
        this.animator.Play(this.ultimateSkillDataSO.performUltimateAnim.name);
        if (this.ultimateTime >= this.performEnd) this.currentPhase = UltimatePhase.Finish;
    }
    protected void PlayFinish()
    {
        this.animator.Play(this.ultimateSkillDataSO.endBeamSkillAnim.name);
    }
}



