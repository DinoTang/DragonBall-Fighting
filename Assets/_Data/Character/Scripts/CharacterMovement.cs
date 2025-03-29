using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovement : CharacterAbstract
{
    [SerializeField] protected Vector2 movement;
    [SerializeField] protected float speed = 15;
    [SerializeField] protected float horizontal;
    [SerializeField] protected bool isMoving = false;
    public bool IsMoving => isMoving;
    protected void FixedUpdate()
    {
        if (!this.characterCtrl.CharacterIntro.IsReady) return;
        this.UpdateMovementState();
        this.Moving();
    }

    public void UpdateMovementState()
    {
        this.horizontal = InputManager.Instance.GetHorizontal();
        if (this.horizontal != 0)
        {
            this.Flipping();
            this.isMoving = true;
            this.movement = new Vector2(this.horizontal, 0);
        }
        else
        {
            this.isMoving = false;
            this.movement = Vector2.zero;
        }
    }
    public void Moving()
    {
        this.characterCtrl.Rgb.velocity = this.movement * this.speed;
        this.characterCtrl.Animator.SetBool("IsMoving", this.isMoving);
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

