using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponent();
    }

    protected virtual void Start()
    {
        // Initialization logic after components are loaded in Awake
    }

    protected virtual void Reset()
    {
        this.ResetValue();
        this.LoadComponent();
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
