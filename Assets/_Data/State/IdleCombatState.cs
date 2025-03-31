using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCombatState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsHardMode", true);
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, this.stateManager.CharacterCtrl.Rgb.velocity.y);
    }
    public override void OnUpdate()
    {
        if (InputManager.Instance.GetJumpInput()) this.stateManager.SetNextState(new AirState());
        if (InputManager.Instance.GetHorizontal() != 0) this.stateManager.SetNextState(new MovingState());
        if (InputManager.Instance.GetCrouchInput()) this.stateManager.SetNextState(new CrouchState());
        if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new GroundStrongHitState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsHardMode", false);
    }
}
