using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterAbstract
{
    [SerializeField] protected float distance;
    [SerializeField] protected bool isFacingRight = true;
    public bool IsFacingRight => isFacingRight;

    public void Flipping()
    {
        if (this.characterCtrl.StateManager.CurrentState is KnockdownIdleState
            || this.characterCtrl.StateManager.CurrentState is BounceOnGroundState
            || this.characterCtrl.StateManager.CurrentState is SkillState) return;
        this.distance = this.characterCtrl.Opponent.transform.position.x - this.transform.position.x;
        if (this.distance < 0)
        {
            this.isFacingRight = false;
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.isFacingRight = true;
            transform.parent.localScale = new Vector3(1, 1, 1);
        }

    }
    protected void Update()
    {
        this.Flipping();
    }

}
