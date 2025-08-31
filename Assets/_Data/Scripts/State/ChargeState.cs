using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsCharge", true);
        ReadyKiChargeFXSpawner.Instance.Spawn(ReadyKiChargeFXSpawner.Instance.nameFX,
        this.stateManager.transform.position);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.stateManager.CharacterCtrl.CharacterKiManager.CurrentKi ==
        this.stateManager.CharacterCtrl.CharacterKiManager.KiMax || GameManager.Instance.IsGameOver)
        {
            // ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Parry.ToString(), this.stateManager.transform.position);
            this.stateManager.SetNextStateToMain();
            return;
        }
        if (InputManager.Instance.ReleaseChargeInput()) this.stateManager.SetNextState(new IdleCombatState());
        // Spawn Smoke Effect 
        this.stateManager.CharacterCtrl.EnableSmokeEffect();

        this.stateManager.CharacterCtrl.CharacterKiManager.ChargeKi();
        CameraManager.Instance.Shake(0.05f, 0.03f);
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsCharge", false);
        this.stateManager.CharacterCtrl.DisableSmokeEffect();
    }

}
