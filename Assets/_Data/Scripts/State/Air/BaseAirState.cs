using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAirState : CharacterState
{
    protected float speed = 15f;
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.MoveOnAir();
    }
    protected void MoveOnAir()
    {
        if (this.stateManager.IsClone) return;
        float xInput = InputManager.Instance.GetHorizontalOld();
        if (xInput != 0)
        {
            this.stateManager.CharacterCtrl.Rgb.velocity =
            new Vector2(xInput * this.speed, this.stateManager.CharacterCtrl.Rgb.velocity.y);
        }
    }
    protected void BackMainState()
    {
        this.stateManager.SetNextStateToMain();
    }
}
