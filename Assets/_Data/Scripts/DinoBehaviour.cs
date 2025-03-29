using UnityEngine;

public abstract class DinoBehaviour : MonoBehaviour
{
    protected virtual void Start()
    {
        this.LoadComponent();
    }

    protected virtual void Awake()
    {
        this.LoadComponent();
    }

    protected virtual void Reset()
    {
        this.LoadComponent();
        this.ResetValue();
    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void LoadComponent()
    {

    }

    protected virtual void ResetValue()
    {

    }
}
