using UnityEngine;

public abstract class State
{
    protected float Timed { get; set; }
    protected float Fixedtime { get; set; }
    protected float Latetime { get; set; }
    protected float duration = 0.3f;
    protected StateManager stateManager;
    protected Animator animator;
    public virtual void OnEnter(StateManager stateManager)
    {
        this.stateManager = stateManager;
        // Debug.Log(this.stateManager);
        this.animator = this.stateManager.CharacterCtrl.Animator;
    }

    public virtual void OnUpdate()
    {
        Timed += UnityEngine.Time.deltaTime;
        // Debug.Log(this.stateManager.CurrentState);
    }

    public virtual void OnFixedUpdate()
    {
        Fixedtime += UnityEngine.Time.deltaTime;
    }
    // public virtual void OnLateUpdate()
    // {
    //     Latetime += UnityEngine.Time.deltaTime;
    // }

    public virtual void OnExit() { }

}