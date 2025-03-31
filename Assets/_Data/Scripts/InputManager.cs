using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : DinoBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (InputManager.instance != null) Debug.LogWarning("Only 1 InputManager allows to exist");
        instance = this;
    }

    public float GetHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetAttackHitInput()
    {
        return Input.GetKeyDown(KeyCode.J);
    }

    public bool GetJumpInput()
    {
        return Input.GetKeyDown(KeyCode.K);
    }
}
