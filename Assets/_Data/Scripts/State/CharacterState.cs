using UnityEngine;

public class CharacterState : State
{
    protected PlayerType playerType;
    protected CharacterDataSO characterDataSO;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.playerType = this.stateManager.CharacterCtrl.PlayerType;
        this.characterDataSO = this.stateManager.CharacterCtrl.CharacterDataSO;
    }
}