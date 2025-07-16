using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterAbstract
{
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected float distance;
    [SerializeField] protected bool isFacingRight = true;
    public bool IsFacingRight => isFacingRight;
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
        this.distance = this.enemy.transform.position.x - this.transform.position.x;
        if (this.distance < 0)
        {
            this.isFacingRight = false;
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.isFacingRight = true;
            transform.parent.localScale = new Vector3(1, 1, 1);
        }

    }
    protected void Update()
    {
        this.Flipping();
    }

}
