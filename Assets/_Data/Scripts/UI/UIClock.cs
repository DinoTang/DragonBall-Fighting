using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIClock : UICtrl
{
    [SerializeField] private List<Sprite> numberSprites; // 0-9
    [SerializeField] private Image tens;
    [SerializeField] private Image ones;

    public void SetNumber(int number)
    {
        number = Mathf.Clamp(number, 0, 999);

        int t = number / 10 % 10;
        int o = number % 10;
        // Gán sprite
        tens.sprite = numberSprites[t];
        ones.sprite = numberSprites[o];
    }
}
