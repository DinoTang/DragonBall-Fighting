using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterFlip : CharacterAbstract
{
    [Header("Character Flip")]
    [SerializeField] protected Transform enemyTarget;
    [SerializeField] protected float distanceX;
    public void Flipping()
    {
        this.distanceX = this.enemyTarget.position.x - transform.position.x;
        if (this.distanceX < 0) this.transform.parent.localScale = new Vector3(-1, 1, 1);
        else this.transform.parent.localScale = new Vector3(1, 1, 1);
    }
}

