using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.mainStateType = new IdleState();
        this.animator.SetBool("IsIdle", true);
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, this.stateManager.CharacterCtrl.Rgb.velocity.y);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.ToggleIgnoreEnemy(false);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (GameManager.Instance.GameOverTimer == GameManager.Instance.GameOverDelay
        && this.stateManager.CharacterCtrl == GameManager.Instance.Winner)
        {
            this.stateManager.CharacterCtrl.StateManager.SetNextState(new WinState());
        }

        if (GameManager.Instance.GameOverTimer == GameManager.Instance.GameOverDelay
        && this.stateManager.CharacterCtrl != GameManager.Instance.Winner && GameManager.Instance.IsTimeOver)
        {
            this.stateManager.CharacterCtrl.StateManager.SetNextState(new LoseState());
        }

        if (this.stateManager.CharacterCtrl.CharacterCollision.IsGround && this.stateManager.CharacterCtrl.CharacterDamageReceiver.isDead)
        {
            this.stateManager.CharacterCtrl.StateManager.SetNextState(new DeadState());
        }

        if (GameManager.Instance.IsGameOver) return;
        if (this.stateManager.IsClone) return;
        if (GameManager.Instance.MatchState != GameManager.MatchStateEnum.Fight) return;

        this.stateManager.CharacterCtrl.CharacterFlip.Flipping();
        if (InputManager.Instance.GetMoveInput(this.playerType) != 0) this.stateManager.SetNextState(new MoveState());
        if (InputManager.Instance.GetJumpInput(this.playerType)) this.stateManager.SetNextState(new JumpState());
        if (InputManager.Instance.GetBlockInput(this.playerType)) this.stateManager.SetNextState(new BlockState());
        if (InputManager.Instance.GetHitInput(this.playerType)) this.stateManager.SetNextState(new GroundHit1State());
        if (InputManager.Instance.GetKickInput(this.playerType)) this.stateManager.SetNextState(new GroundKick1State());
        if (InputManager.Instance.GetSuperDashInput(this.playerType)) this.stateManager.SetNextState(new SuperDashState());
        if (InputManager.Instance.GetChargeInput(this.playerType))
        {
            if (this.stateManager.CharacterCtrl.CharacterKiManager.CurrentKi ==
            this.stateManager.CharacterCtrl.CharacterKiManager.KiMax) return;

            AudioManager.Instance.PlayCharacterSound(CharacterSoundType.StartCharge);
            this.stateManager.SetNextState(new ChargeState());
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsIdle", false);
    }
}
