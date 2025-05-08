using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterDamageReceiver : DamageReceiver
{
    [Header("Character Damage Receiver")]
    [SerializeField] protected CharacterCtrl characterCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
    }
    protected void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
    protected override void OnHurt()
    {
        base.OnHurt();
        if (this.characterCtrl.StateManager.CurrentState.GetType() == typeof(Hurt1State) ||
        this.characterCtrl.StateManager.CurrentState.GetType() == typeof(Hurt2State)) return;
        this.characterCtrl.StateManager.SetNextState(new Hurt1State());
    }
}
