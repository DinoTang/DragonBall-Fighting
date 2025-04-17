using System.Collections.Generic;
using UnityEngine;

public class ComboManager : DinoBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected InputBuffer inputBuffer;
    [SerializeField] protected string combo = "";

    protected void Update()
    {
        if (InputManager.Instance.GetDownInput()) this.inputBuffer.AddInput(InputKey.Down);
        if (InputManager.Instance.GetLeftInput()) this.inputBuffer.AddInput(InputKey.Left);
        if (InputManager.Instance.GetRightInput()) this.inputBuffer.AddInput(InputKey.Right);
        if (InputManager.Instance.GetHitInput()) this.inputBuffer.AddInput(InputKey.Hit);
        if (InputManager.Instance.GetKickInput()) this.inputBuffer.AddInput(InputKey.Kick);

        this.combo = this.inputBuffer.GetComboString();

        if (this.combo == "Down+Left+Hit" || this.combo == "Down+Right+Hit")
        {
            this.characterCtrl.StateManager.SetNextState(new Combo1());
            this.inputBuffer.ClearCombo();
        }
        if (this.combo == "Down+Left+Kick" || this.combo == "Down+Right+Kick")
        {
            this.characterCtrl.StateManager.SetNextState(new Combo2());
            this.inputBuffer.ClearCombo();
        }
    }
}
