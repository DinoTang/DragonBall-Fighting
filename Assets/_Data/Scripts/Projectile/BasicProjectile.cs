using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : _ProjectileBase
{
    [Header("Basic Projectile")]
    [SerializeField] protected CharacterSelector characterSelector;
    public override string GetName()
    {
        return this.characterSelector.GetName();
    }
    protected void LoadCharSelector()
    {
        if (this.characterSelector != null) return;
        this.characterSelector = GetComponent<CharacterSelector>();
        Debug.Log(transform.name + ": LoadCharSelector", gameObject);
    }
}
