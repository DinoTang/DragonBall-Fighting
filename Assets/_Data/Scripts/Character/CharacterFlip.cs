using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterAbstract
{
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected float distance;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnemy();
    }
    protected override void Start()
    {
        base.Start();
        this.Flipping();
    }
    protected void LoadEnemy()
    {
        if (this.enemy != null) return;
        this.enemy = FindAnyObjectByType<Enemy>();
        Debug.Log(transform.name + ": LoadEnemy", gameObject);
    }
    public void Flipping()
    {
        this.distance = this.transform.position.x - this.enemy.transform.position.x;
        if (this.distance < 0) transform.parent.localScale = new Vector3(1, 1, 1);
        else transform.parent.localScale = new Vector3(-1, 1, 1);
    }
    protected void Update()
    {
        this.Flipping();
    }

}
