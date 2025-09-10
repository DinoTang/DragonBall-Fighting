using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(StateManager))]
public class CharacterCollision : CharacterAbstract
{
    [Header("Character Collision")]
    [SerializeField] protected bool isGround;
    public bool IsGround { get => isGround; set => isGround = value; }
    [SerializeField] protected bool isWall;
    public bool IsWall { get => isWall; }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        this.HandleGroundCollision(collision);
        this.HandleWallCollision(collision);
        // this.PreventStandingOnEnemy(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        this.HandleGroundCollisionExit(collision);
        this.HandleWallCollisionExit(collision);
    }
    protected void HandleGroundCollision(Collision2D collision)
    {
        Ground groundCollider = collision.transform.GetComponent<Ground>();
        if (groundCollider == null || this.characterCtrl.StateManager.CurrentState == null) return;

        this.isGround = true;
        SmokeLandedFXSpawner.Instance.Spawn(SmokeLandedFXSpawner.Instance.nameFX, transform.position);

        if (this.characterCtrl.StateManager.CurrentState is BounceOnGroundState)
            AudioManager.Instance.PlayCharacterSound(CharacterSoundType.BounceGroundHit);
        else AudioManager.Instance.PlayCharacterSound(CharacterSoundType.Landing);


        if (!this.CanEnterIdleState()) return;

        this.characterCtrl.StateManager.SetNextState(new IdleCombatState());

        this.characterCtrl.StateManager.CharacterCtrl.Animator.SetBool("IsJump", false);
        this.characterCtrl.StateManager.CanSuperDash = true;
        this.characterCtrl.CharacterDamageSender.SetIsKnockingDown(false);
    }

    protected void HandleGroundCollisionExit(Collision2D collision)
    {
        Ground groundCollider = collision.transform.GetComponent<Ground>();
        if (groundCollider == null || this.characterCtrl.StateManager.CurrentState == null) return;
        isGround = false;
    }

    protected void HandleWallCollision(Collision2D collision)
    {
        Wall wallCollider = collision.transform.GetComponent<Wall>();
        if (wallCollider == null) return;

        this.isWall = true;

    }
    protected void HandleWallCollisionExit(Collision2D collision)
    {
        Wall wallCollider = collision.transform.GetComponent<Wall>();
        if (wallCollider == null) return;

        isWall = false;
    }
    protected void PreventStandingOnEnemy(Collision2D collision)
    {
        CharacterCtrl opponent = collision.transform.GetComponent<CharacterCtrl>();
        if (opponent != this.characterCtrl.Opponent) return;

        if (opponent.StateManager.CurrentState is KnockDownState
            || opponent.StateManager.CurrentState is KnockUpState
            || opponent.StateManager.CurrentState is AirHurtState
            || opponent.StateManager.CurrentState is BounceOnGroundState
            || opponent.StateManager.CurrentState is RushAttackSkillState
            || opponent.StateManager.CurrentState is KnockdownIdleState) return;

        if (this.characterCtrl.StateManager.CurrentState is SuperDashState) return;

        Vector2 selfPos = this.characterCtrl.transform.position;
        Vector2 opponentPos = opponent.transform.position;

        if (selfPos.y > opponentPos.y + 0.3f)
        {
            this.characterCtrl.Rgb.velocity = new Vector2(10f * this.characterCtrl.transform.localScale.x, 0);
            Debug.LogWarning("Bật ra khỏi enemy!");
        }
    }


    protected bool CanEnterIdleState()
    {
        State currentState = this.characterCtrl.StateManager.CurrentState;
        return !(currentState is IdleCombatState
                || currentState is IntroState
                || currentState is SuperDashState
                || currentState is KnockDownState
                || currentState is BounceOnGroundState
                || currentState is KnockdownIdleState
                || currentState is RushAttackWasKnockUpState
                || currentState is RushAttackSkillState
                || currentState is FallKnockBackState
                || currentState is UltimateSkillState);
    }
}

