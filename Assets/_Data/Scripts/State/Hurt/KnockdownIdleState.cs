using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockdownIdleState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.duration = 1f;
        this.animator.SetBool("IsKnockDownIdle", true);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.ToggleIgnoreEnemy(true);

        if (this.timed >= this.duration)
        {
            if (this.stateManager.CharacterCtrl.CharacterDamageReceiver.isDead)
            {
                this.stateManager.SetNextState(new DeadState());
            }
            else
            {
                this.stateManager.SetNextState(new Tele2());
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockDownIdle", false);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsImmortal(false);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.ToggleIgnoreEnemy(false);
    }


}
