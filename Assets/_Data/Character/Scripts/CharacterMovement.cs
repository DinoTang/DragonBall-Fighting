using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovement : CharacterAbstract
{
    [SerializeField] protected Vector2 movement;
    [SerializeField] protected float speed = 15;
    [SerializeField] protected float horizontal;
    protected void FixedUpdate()
    {
        if (!this.characterCtrl.CharacterIntro.IsReady) return;
        if (this.characterCtrl.CharacterAttack.IsAttack) return;
        this.UpdateMovementState();
        this.Moving();
    }

    public void UpdateMovementState()
    {
        this.horizontal = InputManager.Instance.GetHorizontal();
        if (this.horizontal != 0)
        {
            this.Flipping();
            this.movement = new Vector2(this.horizontal, 0);
            this.characterCtrl.StateManager.SetNextState(new MovingState());
        }
        else if (this.characterCtrl.StateManager.CurrentState.GetType() == typeof(MovingState))
        {
            this.movement = Vector2.zero;
            this.characterCtrl.StateManager.SetNextState(new IdleCombatState());
        }
    }
    public void Moving()
    {
        this.characterCtrl.Rgb.velocity = this.movement * this.speed;
    }

    public void Flipping()
    {
        if (this.horizontal > 0)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
        }
        else transform.parent.localScale = new Vector3(-1, 1, 1);
    }
}

