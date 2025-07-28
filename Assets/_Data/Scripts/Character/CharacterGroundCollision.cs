using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(StateManager))]
public class CharacterGroundCollision : CharacterAbstract
{
    [Header("Character Ground Collision")]
    [SerializeField] protected bool isGround;
    public bool IsGround { get => isGround; set => isGround = value; }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        this.HandleGroundCollision(collision);
        this.PreventStandingOnEnemy(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Ground groundCollider = collision.transform.GetComponent<Ground>();
        if (groundCollider == null || this.characterCtrl.StateManager.CurrentState == null) return;
        isGround = false;
    }
    protected void HandleGroundCollision(Collision2D collision)
    {
        Ground groundCollider = collision.transform.GetComponent<Ground>();
        if (groundCollider == null || this.characterCtrl.StateManager.CurrentState == null) return;

        this.isGround = true;
        SmokeLandedFXSpawner.Instance.Spawn(SmokeLandedFXSpawner.Instance.nameFX, transform.position);

        if (!this.CanEnterIdleState()) return;

        this.characterCtrl.StateManager.SetNextState(new IdleCombatState());

        this.characterCtrl.StateManager.CharacterCtrl.Animator.SetBool("IsJump", false);
        this.characterCtrl.StateManager.CanSuperDash = true;
        this.characterCtrl.CharacterDamageSender.SetIsKnockdown(false);
    }

    protected void PreventStandingOnEnemy(Collision2D collision)
    {
        CharacterCtrl opponent = collision.transform.GetComponent<CharacterCtrl>();
        if (opponent != this.characterCtrl.Opponent) return;
        if (opponent.StateManager.CurrentState.GetType() == typeof(KnockDownState)
            || opponent.StateManager.CurrentState.GetType() == typeof(KnockUpState)
            || this.characterCtrl.StateManager.CurrentState.GetType() == typeof(SuperDashState)) return;
        Vector2 selfPos = this.characterCtrl.transform.position;
        Vector2 opponentPos = opponent.transform.position;

        if (selfPos.y > opponentPos.y)
        {
            float dir = this.characterCtrl.CharacterFlip.IsFacingRight ? -1 : 1;
            this.characterCtrl.Rgb.velocity = new Vector2(10f * dir, 5f);
            Debug.LogWarning("Bật ra khỏi enemy!");
        }
    }

    protected bool CanEnterIdleState()
    {
        State currentState = this.characterCtrl.StateManager.CurrentState;
        return !(currentState is IdleCombatState
                || currentState is SuperDashState
                || currentState is KnockDownState
                || currentState is BounceOnGroundState
                || currentState is KnockdownIdleState);
    }
}

