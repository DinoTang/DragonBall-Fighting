using UnityEngine;

public class CharacterState : State
{
    protected PlayerType playerType;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.playerType = this.stateManager.CharacterCtrl.PlayerType;
    }
}