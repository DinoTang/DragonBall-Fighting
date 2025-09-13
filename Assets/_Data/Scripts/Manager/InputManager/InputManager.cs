using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance => instance;
    [Header("Player Inputs")]
    [SerializeField] protected InputSO inputSO;
    [SerializeField] protected PlayerInputConfig playerRedKeys;
    [SerializeField] protected PlayerInputConfig playerBlueKeys;
    protected override void Awake()
    {
        base.Awake();
        if (InputManager.instance != null) Debug.LogWarning("Only 1 InputManager allows to exist");
        instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadInputSO();
        this.LoadPlayerInputConfig();
    }

    protected void LoadInputSO()
    {
        if (this.inputSO != null) return;
        this.inputSO = Resources.Load<InputSO>("Options/InputSO");
        Debug.Log(transform.name + ": LoadAudioSource");
    }

    protected void LoadPlayerInputConfig()
    {
        this.playerRedKeys = CloneConfig(inputSO.playerRedKeys);
        this.playerBlueKeys = CloneConfig(inputSO.playerBlueKeys);
    }

    public bool GetInputChangePlayer1()
    {
        return Input.GetKeyDown(KeyCode.Alpha1);
    }
    public bool GetInputChangePlayer2()
    {
        return Input.GetKeyDown(KeyCode.Alpha2);
    }

    private PlayerInputConfig GetConfig(PlayerType playerType)
    {
        if (playerType as PlayerRed) return this.playerRedKeys;
        else if (playerType as PlayerBlue) return this.playerBlueKeys;

        return null;
    }

    public int GetMoveInput(PlayerType playerType)
    {
        var cfg = GetConfig(playerType);
        if (Input.GetKey(cfg.left)) return -1;
        if (Input.GetKey(cfg.right)) return 1;
        return 0;
    }
    public bool GetJumpInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).up);
    public bool GetBlockInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).block);
    public bool ReleaseBlockHitInput(PlayerType playerType) => Input.GetKeyUp(this.GetConfig(playerType).block);
    public bool GetDownInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).down);
    public bool GetHitInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).punch);
    public bool GetKickInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).kick);
    public bool GetChargeInput(PlayerType playerType) => Input.GetKey(this.GetConfig(playerType).charge);
    public bool ReleaseChargeInput(PlayerType playerType) => Input.GetKeyUp(this.GetConfig(playerType).charge);
    public bool GetRushAttackInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).rushAttack);
    public bool GetKiBlastInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).kiBlast);
    public bool GetPowerSurgeInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).powerSurge);
    public bool GetSuperDashInput(PlayerType playerType) => Input.GetKeyDown(this.GetConfig(playerType).superDash);
    public bool ReleaseSuperDashInput(PlayerType playerType) => Input.GetKeyUp(this.GetConfig(playerType).superDash);

    public bool GetLeftInput()
    {
        return Input.GetKeyDown(KeyCode.A);
    }
    public bool GetRightInput()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
    public bool ReleaseCrouchInput()
    {
        return Input.GetKeyUp(KeyCode.S);
    }
    public float GetHorizontalOld()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public bool GetInputToResetPosition()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }


    private PlayerInputConfig CloneConfig(PlayerInputConfig src)
    {
        var clone = new PlayerInputConfig
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
        return clone;
    }
}
