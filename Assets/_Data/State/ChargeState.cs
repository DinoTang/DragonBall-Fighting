using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    public int chargeLevel = 1;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsCharge", true);
        this.animator.SetInteger("Charging", this.chargeLevel);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.ReleaseChargeInput()) this.stateManager.SetNextState(new IdleCombatState());
        if (this.Time >= 2f)
        {
            this.chargeLevel++;
            if (this.chargeLevel == 2)
            {
                this.ChargingLevel();
                return;
            }
            if (this.chargeLevel == 3)
            {
                this.ChargingLevel();
                return;
            }
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsCharge", false);
    }

    protected void ChargingLevel()
    {
        this.animator.SetInteger("Charging", this.chargeLevel);
        this.Time = 0;
    }
}
