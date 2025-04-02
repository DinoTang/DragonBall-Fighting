using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAirState : State
{
    protected float speed = 15f;
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.MoveOnAir();
    }
    protected void MoveOnAir()
    {
        float xInput = InputManager.Instance.GetHorizontal();
        if (xInput != 0)
        {
            this.stateManager.CharacterCtrl.Rgb.velocity =
            new Vector2(xInput * this.speed, this.stateManager.CharacterCtrl.Rgb.velocity.y);
        }
    }
    protected void BackMainState()
    {
        if (this.stateManager.CharacterCtrl.IsGround) this.stateManager.SetNextState(new IdleCombatState());
        else this.stateManager.SetNextState(new AirState());
    }
}
