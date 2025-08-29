using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalSkillProjectile : BlastSkillProjectile
{
    protected override void SettingDirect(int side, bool boolean)
    {
        this.direction = new Vector2(side * 2, -1).normalized;
        this.FlipModelX(boolean);
    }
    protected override void OnUpdate()
    {
        this.transform.Translate(this.speed * Time.deltaTime * this.direction);
        float angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
        if (this.direction.x < 0)
        {
            angle += 180f;
        }

        this.model.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
