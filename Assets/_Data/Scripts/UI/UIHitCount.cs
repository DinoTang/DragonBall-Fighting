using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class UIHitCount : UICtrl
{
    [SerializeField] protected List<Sprite> numberSprites; // 0-9
    [SerializeField] protected Image tens;
    [SerializeField] protected Image ones;

    public virtual void SetNumber(int number)
    {
        int o = number % 10;
        int t = number / 10 % 10;
        // Gán sprite
        ones.sprite = numberSprites[o];
        tens.sprite = numberSprites[t];

        if (t <= 0) this.tens.gameObject.SetActive(false);
        else this.tens.gameObject.SetActive(true);
    }
}
