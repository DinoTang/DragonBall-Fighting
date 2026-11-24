using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputBuffer : BaseBehaviour
{
    [SerializeField] protected List<InputKey> inputKeys = new();
    [SerializeField] protected float bufferTime = 0.5f;
    [SerializeField] protected List<float> inputTimes = new();

    private string cachedComboString = string.Empty;
    private bool isDirty = false;

    public int Count => inputKeys.Count;
    public bool HasInputs => inputKeys.Count > 0;
    public IReadOnlyList<InputKey> Inputs => inputKeys;

    public void AddInput(InputKey inputKey)
    {
        this.inputKeys.Add(inputKey);
        this.inputTimes.Add(Time.time);
        this.isDirty = true;
    }

    public string GetComboString()
    {
        bool expired = this.RemoveExpireInputs();
        if (expired || isDirty)
        {
            cachedComboString = inputKeys.Count > 0 ? string.Join("+", this.inputKeys) : string.Empty;
            isDirty = false;
        }
        return cachedComboString;
    }

    protected bool RemoveExpireInputs()
    {
        float nowTime = Time.time;
        bool anyRemoved = false;
        for (int i = inputKeys.Count - 1; i >= 0; i--)
        {
            if (nowTime - this.inputTimes[i] > this.bufferTime)
            {
                this.inputTimes.RemoveAt(i);
                this.inputKeys.RemoveAt(i);
                anyRemoved = true;
            }
        }
        return anyRemoved;
    }

    public void ClearCombo()
    {
        this.inputTimes.Clear();
        this.inputKeys.Clear();
        this.cachedComboString = string.Empty;
        this.isDirty = false;
    }
}
