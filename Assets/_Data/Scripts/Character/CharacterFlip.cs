using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterAbstract
{
    [Header("Character Flip")]
    [SerializeField] protected float distance;
    public float Distance => distance;
    [SerializeField] protected float distanceOther;
    public float DistanceOther => distanceOther;
    [SerializeField] protected int direct;
    public int Direct => direct;
    [SerializeField] protected bool isFacingRight = true;
    public bool IsFacingRight => isFacingRight;

    public void Flipping()
    {
        this.direct = this.isFacingRight ? 1 : -1;

        if (this.characterCtrl.StateManager.CurrentState is KnockdownIdleState
            || this.characterCtrl.StateManager.CurrentState is BounceOnGroundState
            || this.characterCtrl.StateManager.CurrentState is BeamSkillState
            || this.characterCtrl.StateManager.CurrentState is BurstSkillState
            || this.characterCtrl.StateManager.CurrentState is RushAttackSkillState) return;
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
        if (InputManager.Instance.GetInputToResetPosition())
        {
            this.characterCtrl.transform.position = new Vector2(-2, -1);
            this.characterCtrl.Opponent.transform.position = new Vector2(2, -1);
        }
        this.Flipping();
        this.distanceOther = Vector2.Distance(this.transform.position, this.characterCtrl.Opponent.transform.position);
    }

}
