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
    protected void Update()
    {
        this.TryEnterBlockState();
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
    protected void TryEnterBlockState()
    {
        if (!this.isBlock) return;
        this.characterCtrl.StateManager.SetNextState(new BlockState());
    }
    public override bool OnBlock()
    {
        if (!this.isBlock) return false;
        this.OnParryFX();
        return true;
    }
    protected void OnParryFX()
    {
        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        pos.x += 0.5f;
        ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Parry.ToString(), pos, this.characterCtrl);
    }
}
