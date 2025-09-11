using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsKnockBack", true);
        this.stateManager.CharacterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float dir = this.stateManager.CharacterCtrl.CharacterFlip.Direct * -1;
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(dir * 50f, 0);
        if (!this.stateManager.CharacterCtrl.CharacterCollision.IsWall) return;
        AudioManager.Instance.PlayCharacterSound(CharacterSoundType.GroundHit);
        this.stateManager.SetNextState(new FallKnockBackState());
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockBack", false);
        this.stateManager.CharacterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
