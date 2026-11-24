using UnityEngine;

public class StateManager : BaseBehaviour
{
    [Header("State Manager")]
    [SerializeField] protected CharacterCtrl characterCtrl;
    public CharacterCtrl CharacterCtrl => characterCtrl;
    [SerializeField] protected bool isClone = false;
    public bool IsClone { get => isClone; set => isClone = value; }
    [SerializeField] protected bool canSuperDash = false;
    public bool CanSuperDash { get => canSuperDash; set => canSuperDash = value; }
    [Header("Super Freeze")]
    [SerializeField] protected bool isFrozen = false;
    public bool IsFrozen { get => isFrozen; set => isFrozen = value; }
    
    public CharacterState mainStateType;
    public CharacterState CurrentState { get; set; }
    public CharacterState nextState;

    // Cached states to eliminate per-frame GC allocations
    protected IdleState cachedIdleState;
    protected AirState cachedAirState;

    public IdleState CachedIdleState => cachedIdleState ??= new IdleState();
    public AirState CachedAirState => cachedAirState ??= new AirState();

    protected override void Awake()
    {
        base.Awake();
        this.cachedIdleState = new IdleState();
        this.cachedAirState = new AirState();
    }

    protected override void Start()
    {
        base.Start();
        this.mainStateType = this.cachedIdleState;
        this.SetState(this.mainStateType);
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
    }

    protected void Update()
    {
        if (this.isFrozen) return;

        if (this.characterCtrl != null && this.characterCtrl.CharacterCollision != null)
        {
            if (this.characterCtrl.CharacterCollision.IsGround)
            {
                this.canSuperDash = true;
                if (this.mainStateType != this.cachedIdleState)
                {
                    this.mainStateType = this.cachedIdleState;
                }
            }
            else if (this.CurrentState is not SuperDashState)
            {
                if (this.mainStateType != this.cachedAirState)
                {
                    this.mainStateType = this.cachedAirState;
                }
            }
        }

        if (nextState != null)
        {
            SetState(nextState);
        }

        CurrentState?.OnUpdate();
    }

    private void SetState(CharacterState _newState)
    {
        nextState = null;
        CurrentState?.OnExit();
        CurrentState = _newState;
        CurrentState?.OnEnter(this);
    }

    public void SetNextState(CharacterState _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    private void FixedUpdate()
    {
        if (this.isFrozen) return;
        if (CurrentState != null)
            CurrentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }
}
