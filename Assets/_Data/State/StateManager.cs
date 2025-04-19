using UnityEngine;

public class StateManager : DinoBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected bool isClone = false;
    public bool IsClone => isClone;
    public CharacterCtrl CharacterCtrl => characterCtrl;
    public State mainStateType;

    public State CurrentState { get; set; }
    public State nextState;
    protected override void Start()
    {
        base.Start();
        this.mainStateType = new IdleCombatState();
        this.SetState(mainStateType);
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
    }
    protected void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = GetComponent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl", gameObject);
    }
    protected void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
        }

        CurrentState?.OnUpdate();
    }

    private void SetState(State _newState)
    {
        nextState = null;
        CurrentState?.OnExit();
        CurrentState = _newState;
        CurrentState.OnEnter(this);
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
    public void StopAddVelocity()
    {
        this.characterCtrl.Rgb.velocity = Vector2.zero;
    }
}