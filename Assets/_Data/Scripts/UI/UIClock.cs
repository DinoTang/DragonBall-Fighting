using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIClock : UICtrl
{
    [SerializeField] protected List<Sprite> numberSprites; // 0-9
    [SerializeField] protected Image tens;
    [SerializeField] protected Image ones;

    public virtual void SetNumber(int number)
    {
        int t = number / 10 % 10;
        int o = number % 10;
        // Gán sprite
        tens.sprite = numberSprites[t];
        ones.sprite = numberSprites[o];
    }
}
