using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGroundHitState : BaseGroundAttackState
{
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed > duration * 0.5f && InputManager.Instance.GetHitInput())
        {
            shouldCombo = true;
        }
    }

    protected override void PlayDodgeClip()
    {
        base.PlayDodgeClip();
        AudioManager.Instance.AudioSource.PlayOneShot(
              this.stateManager.CharacterCtrl.CharacterData.CharacterDataSO.dodgeHitClip);
    }
}
