using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFX : PoolObj
{
    [Header("Explosion")]
    [SerializeField] protected ExplosionFXEnum EFX_Name;
    [SerializeField] protected AudioClip EFXSound;
    public override string GetName()
    {
        return this.EFX_Name.ToString();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (this.EFX_Name == ExplosionFXEnum.Hit_Effect) return;
        AudioManager.Instance.AudioSource.PlayOneShot(EFXSound);
    }
}
