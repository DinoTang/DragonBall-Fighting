public abstract class State
{
    protected float Time { get; set; }
    protected float Fixedtime { get; set; }
    protected float Latetime { get; set; }

    public virtual void OnEnter() { }

    public virtual void OnUpdate()
    {
        Time += UnityEngine.Time.deltaTime;
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