using UnityEngine;

public class StateManager : DinoBehaviour
{

    public State mainStateType = new IdleCombatState();

    public State CurrentState { get; set; }
    public State nextState;
    protected override void Awake()
    {
        SetNextStateToMain();
    }

    protected void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
        }

        if (CurrentState != null)
            CurrentState.OnUpdate();
    }

    private void SetState(State _newState)
    {
        nextState = null;
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter();
    }

    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    // private void LateUpdate()
    // {
    //     if (CurrentState != null)
    //         CurrentState.OnLateUpdate();
    // }

    private void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }

}