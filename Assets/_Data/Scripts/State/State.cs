using UnityEngine;

public abstract class State
{
    protected float timed;
    protected float fixedTime;
    protected float lateTime;
    protected float duration = 0.3f;
    protected StateManager stateManager;
    protected Animator animator;
    public virtual void OnEnter(StateManager stateManager)
    {
        this.stateManager = stateManager;
        // Debug.Log(this.stateManager);
        this.animator = this.stateManager.CharacterCtrl.Animator;
        Debug.LogWarning(this.stateManager.transform.name + " " + this.timed + ": " + this.stateManager.CurrentState);
    }

    public virtual void OnUpdate()
    {
        this.timed += UnityEngine.Time.deltaTime;
        // Debug.Log(this.stateManager.transform.name + " " + this.timed + ": " + this.stateManager.CurrentState);
        // Debug.Log(this.stateManager.transform.name + ": " + this.stateManager.mainStateType);
    }

    public virtual void OnFixedUpdate()
    {
        this.fixedTime += UnityEngine.Time.fixedDeltaTime;
    }
    public virtual void OnLateUpdate()
    {
        this.lateTime += UnityEngine.Time.deltaTime;
    }

    public virtual void OnExit() { }

}