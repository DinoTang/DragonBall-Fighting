using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : DinoBehaviour
{
    [SerializeField] protected int size = 4;
    public int Size => size;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.LoadSize();
    }

    protected void LoadSize()
    {
        this.transform.localScale = Vector3.one * this.size;
    }
}
