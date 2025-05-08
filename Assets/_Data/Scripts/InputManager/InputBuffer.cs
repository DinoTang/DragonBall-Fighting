using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InputBuffer : DinoBehaviour
{
    [SerializeField] protected List<InputKey> inputKeys = new();
    [SerializeField] protected float bufferTime = 0.5f;
    [SerializeField] protected List<float> inputTimes = new();

    public void AddInput(InputKey inputKey)
    {
        this.inputKeys.Add(inputKey);
        this.inputTimes.Add(Time.time);
    }

    public string GetComboString()
    {
        this.RemoveExpireInputs();
        return string.Join("+", this.inputKeys);
    }

    protected void RemoveExpireInputs()
    {
        float nowTime = Time.time;
        for (int i = inputKeys.Count - 1; i >= 0; i--)
        {
            if (nowTime - this.inputTimes[i] > this.bufferTime)
            {
                this.inputTimes.RemoveAt(i);
                this.inputKeys.RemoveAt(i);
            }
        }
    }

    public void ClearCombo()
    {
        this.inputTimes.Clear();
        this.inputKeys.Clear();
    }
}
