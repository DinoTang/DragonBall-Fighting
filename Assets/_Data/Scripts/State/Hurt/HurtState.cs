using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsHurt", true);
        int indexRand = Random.Range(0, 3);
        this.animator.SetInteger("HurtCounter", indexRand);
        this.stateManager.CharacterCtrl.CharacterModalCtrl.Sprite.sortingOrder = -1;
        this.stateManager.CharacterCtrl.Opponent.CharacterModalCtrl.Sprite.sortingOrder = 0;
        GameManager.Instance.PlayerIsHit = this.stateManager.CharacterCtrl;
        CameraManager.Instance.Shake(0.05f, 0.05f);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= this.duration)
        {
            this.stateManager.SetNextStateToMain();
        }
        if (this.stateManager.CharacterCtrl.CharacterDamageReceiver.isDead) this.stateManager.SetNextState(new HurtDeadState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsHurt(false);
    }
}
