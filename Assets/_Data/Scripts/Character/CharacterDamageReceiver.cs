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
        // this.TryEnterBlockState();
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
        if (this.characterCtrl.StateManager.CurrentState is KnockBackState) return;
        if (this.characterCtrl.StateManager.mainStateType is AirState)
            this.characterCtrl.StateManager.SetNextState(new AirHurtState());
        else this.characterCtrl.StateManager.SetNextState(new HurtState());
        this.characterCtrl.CharacterKiManager.GainKi(2);
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
    public void OnParryFX()
    {
        Vector2 pos = this.characterCtrl.transform.position;
        pos.x += this.characterCtrl.CharacterFlip.Direct * 0.5f;
        pos.y -= 0.5f;
        ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Parry.ToString(), pos, this.characterCtrl);
    }

    public void ToggleIgnoreEnemy(bool ignore)
    {
        Collider2D playerCol = this.collide;
        Collider2D enemyCol = this.characterCtrl.Opponent.CharacterDamageReceiver.collide;

        Physics2D.IgnoreCollision(playerCol, enemyCol, ignore);
    }

}
