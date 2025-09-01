using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (InputManager.instance != null) Debug.LogWarning("Only 1 InputManager allows to exist");
        instance = this;
    }
    protected void Update()
    {
        if (this.GetInputChangePlayer1())
        {
            GameManager.Instance.RedPlayer.StateManager.IsClone = false;
            GameManager.Instance.BluePlayer.StateManager.IsClone = true;
            Debug.LogWarning("Player1");
        }
        if (this.GetInputChangePlayer2())
        {
            GameManager.Instance.BluePlayer.StateManager.IsClone = false;
            GameManager.Instance.RedPlayer.StateManager.IsClone = true;
            Debug.LogWarning("Player2");
        }
    }

    public bool GetInputChangePlayer1()
    {
        return Input.GetKeyDown(KeyCode.Alpha1);
    }
    public bool GetInputChangePlayer2()
    {
        return Input.GetKeyDown(KeyCode.Alpha2);
    }


    public float GetHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public bool GetInputToResetPosition()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool GetLeftInput()
    {
        return Input.GetKeyDown(KeyCode.A);
    }
    public bool GetRightInput()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
    public bool GetHitInput()
    {
        return Input.GetKeyDown(KeyCode.J);
    }
    public bool GetUInput()
    {
        return Input.GetKeyDown(KeyCode.U);
    }
    public bool GetKickInput()
    {
        return Input.GetKeyDown(KeyCode.K);
    }
    public bool GetUpInput()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
    public bool GetDownInput()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
    public bool GetBlockInput()
    {
        return Input.GetKey(KeyCode.S);
    }
    public bool ReleaseCrouchInput()
    {
        return Input.GetKeyUp(KeyCode.S);
    }
    public bool GetChargeInput()
    {
        return Input.GetKey(KeyCode.L);
    }
    public bool ReleaseChargeInput()
    {
        return Input.GetKeyUp(KeyCode.L);
    }
    public bool GetBlockHitInput()
    {
        return Input.GetKey(KeyCode.S);
    }
    public bool ReleaseBlockHitInput()
    {
        return Input.GetKeyUp(KeyCode.S);
    }
    public bool GetPowerFiredInput()
    {
        return Input.GetKeyDown(KeyCode.I);
    }
    public bool GetLauncherInput()
    {
        return Input.GetKeyDown(KeyCode.O);
    }
    public bool GetSuperDashInput()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
    public bool ReleaseSuperDashInput()
    {
        return Input.GetKeyUp(KeyCode.F);
    }
}
