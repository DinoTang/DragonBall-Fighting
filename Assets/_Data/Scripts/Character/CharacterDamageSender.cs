using UnityEngine;
public class CharacterDamageSender : DamageSender
{
    protected override void LoadCollider()
    {
        base.LoadCollider();
        this._collider.enabled = false;
    }
}