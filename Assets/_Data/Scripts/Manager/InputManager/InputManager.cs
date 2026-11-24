using System;
using UnityEngine;

public class InputManager : BaseBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance => instance;

    [Header("Player Inputs")]
    [SerializeField] protected InputSO inputSO;
    [SerializeField] protected PlayerInputConfig playerRedKeys;
    [SerializeField] protected PlayerInputConfig playerBlueKeys;

    private IPlayerInputProvider redInputProvider;
    private IPlayerInputProvider blueInputProvider;

    public IPlayerInputProvider RedInputProvider => redInputProvider;
    public IPlayerInputProvider BlueInputProvider => blueInputProvider;

    protected override void Awake()
    {
        base.Awake();
        if (InputManager.instance != null && InputManager.instance != this)
        {
            Debug.LogWarning("Only 1 InputManager allows to exist");
        }
        instance = this;
        this.InitializeProviders();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadInputSO();
        this.LoadPlayerInputConfig();
        this.InitializeProviders();
    }

    protected void InitializeProviders()
    {
        if (this.redInputProvider == null && this.playerRedKeys != null)
        {
            this.redInputProvider = new KeyboardInputProvider(this.playerRedKeys);
        }
        if (this.blueInputProvider == null && this.playerBlueKeys != null)
        {
            this.blueInputProvider = new KeyboardInputProvider(this.playerBlueKeys);
        }
    }

    public void SetPlayerInputProvider(PlayerType playerType, IPlayerInputProvider provider)
    {
        if (playerType is PlayerRed) this.redInputProvider = provider;
        else if (playerType is PlayerBlue) this.blueInputProvider = provider;
    }

    public void SetPlayerDevice(PlayerType playerType, InputDeviceType deviceType, int joystickIndex = 1)
    {
        IPlayerInputProvider provider = deviceType switch
        {
            InputDeviceType.Gamepad => new GamepadInputProvider(joystickIndex),
            InputDeviceType.NewInputSystem => new NewInputSystemProvider(joystickIndex - 1),
            _ => new KeyboardInputProvider(playerType is PlayerRed ? playerRedKeys : playerBlueKeys)
        };
        SetPlayerInputProvider(playerType, provider);
    }

    protected void LoadInputSO()
    {
        if (this.inputSO != null) return;
        this.inputSO = Resources.Load<InputSO>("Options/InputSO");
    }

    protected void LoadPlayerInputConfig()
    {
        if (this.inputSO == null) return;
        this.playerRedKeys = CloneConfig(this.inputSO.playerRedKeys);
        this.playerBlueKeys = CloneConfig(this.inputSO.playerBlueKeys);
    }

    public bool GetInputChangePlayer1() => Input.GetKeyDown(KeyCode.Alpha1);
    public bool GetInputChangePlayer2() => Input.GetKeyDown(KeyCode.Alpha2);

    public PlayerInputConfig GetConfig(PlayerType playerType)
    {
        if (playerType is PlayerRed) return this.playerRedKeys;
        if (playerType is PlayerBlue) return this.playerBlueKeys;
        return null;
    }

    public IPlayerInputProvider GetProvider(PlayerType playerType)
    {
        if (playerType is PlayerRed) return this.redInputProvider;
        if (playerType is PlayerBlue) return this.blueInputProvider;
        return null;
    }

    public int GetMoveInput(PlayerType playerType) => GetProvider(playerType)?.GetMoveHorizontal() ?? 0;
    public bool GetJumpInput(PlayerType playerType) => GetProvider(playerType)?.GetJumpDown() ?? false;
    public bool GetBlockInput(PlayerType playerType) => GetProvider(playerType)?.GetBlockDown() ?? false;
    public bool ReleaseBlockHitInput(PlayerType playerType) => GetProvider(playerType)?.GetBlockUp() ?? false;
    public bool GetDownInput(PlayerType playerType) => GetProvider(playerType)?.GetDownDown() ?? false;
    public bool GetHitInput(PlayerType playerType) => GetProvider(playerType)?.GetHitDown() ?? false;
    public bool GetKickInput(PlayerType playerType) => GetProvider(playerType)?.GetKickDown() ?? false;
    public bool GetChargeInput(PlayerType playerType) => GetProvider(playerType)?.GetChargeKey() ?? false;
    public bool ReleaseChargeInput(PlayerType playerType) => GetProvider(playerType)?.GetChargeKeyUp() ?? false;
    public bool GetRushAttackInput(PlayerType playerType) => GetProvider(playerType)?.GetRushAttackDown() ?? false;
    public bool GetKiBlastInput(PlayerType playerType) => GetProvider(playerType)?.GetKiBlastDown() ?? false;
    public bool GetPowerSurgeInput(PlayerType playerType) => GetProvider(playerType)?.GetPowerSurgeDown() ?? false;
    public bool GetSuperDashInput(PlayerType playerType) => GetProvider(playerType)?.GetSuperDashDown() ?? false;
    public bool ReleaseSuperDashInput(PlayerType playerType) => GetProvider(playerType)?.GetSuperDashUp() ?? false;
    public bool GetLeftInput(PlayerType playerType) => GetProvider(playerType)?.GetLeftDown() ?? false;
    public bool GetRightInput(PlayerType playerType) => GetProvider(playerType)?.GetRightDown() ?? false;

    public bool GetLeftInput() => Input.GetKeyDown(KeyCode.A);
    public bool GetRightInput() => Input.GetKeyDown(KeyCode.D);
    public bool ReleaseCrouchInput() => Input.GetKeyUp(KeyCode.S);
    public float GetHorizontalOld() => Input.GetAxisRaw("Horizontal");
    public bool GetInputToResetPosition() => Input.GetKeyDown(KeyCode.Space);

    private PlayerInputConfig CloneConfig(PlayerInputConfig src)
    {
        if (src == null) return new PlayerInputConfig();
        return new PlayerInputConfig
        {
            up = src.up,
            down = src.down,
            left = src.left,
            right = src.right,
            punch = src.punch,
            kick = src.kick,
            block = src.block,
            charge = src.charge,
            rushAttack = src.rushAttack,
            kiBlast = src.kiBlast,
            powerSurge = src.powerSurge,
            superDash = src.superDash
        };
    }
}
