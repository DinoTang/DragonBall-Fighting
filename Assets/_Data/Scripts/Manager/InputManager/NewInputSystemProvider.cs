using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Bridge provider for Unity's New Input System.
/// Automatically active when 'com.unity.inputsystem' is installed in the project.
/// </summary>
public class NewInputSystemProvider : IPlayerInputProvider
{
    private readonly int playerIndex;

    public NewInputSystemProvider(int playerIndex = 0)
    {
        this.playerIndex = playerIndex;
    }

#if ENABLE_INPUT_SYSTEM
    private Gamepad GetGamepad()
    {
        var gamepads = Gamepad.all;
        if (playerIndex < gamepads.Count) return gamepads[playerIndex];
        return Gamepad.current;
    }

    public int GetMoveHorizontal()
    {
        var pad = GetGamepad();
        if (pad == null) return 0;
        float x = pad.dpad.x.ReadValue();
        if (Mathf.Abs(x) < 0.2f) x = pad.leftStick.x.ReadValue();
        if (x < -0.5f) return -1;
        if (x > 0.5f) return 1;
        return 0;
    }

    public bool GetJumpDown() => GetGamepad()?.buttonSouth.wasPressedThisFrame ?? false;
    public bool GetBlockDown() => GetGamepad()?.rightShoulder.wasPressedThisFrame ?? false;
    public bool GetBlockUp() => GetGamepad()?.rightShoulder.wasReleasedThisFrame ?? false;
    public bool GetDownDown()
    {
        var pad = GetGamepad();
        if (pad == null) return false;
        return pad.dpad.down.wasPressedThisFrame || pad.leftStick.down.wasPressedThisFrame;
    }
    public bool GetHitDown() => GetGamepad()?.buttonWest.wasPressedThisFrame ?? false;
    public bool GetKickDown() => GetGamepad()?.buttonEast.wasPressedThisFrame ?? false;
    public bool GetChargeKey() => GetGamepad()?.leftShoulder.isPressed ?? false;
    public bool GetChargeKeyUp() => GetGamepad()?.leftShoulder.wasReleasedThisFrame ?? false;
    public bool GetRushAttackDown() => GetGamepad()?.buttonNorth.wasPressedThisFrame ?? false;
    public bool GetKiBlastDown() => GetGamepad()?.rightTrigger.wasPressedThisFrame ?? false;
    public bool GetPowerSurgeDown() => GetGamepad()?.rightStickButton.wasPressedThisFrame ?? false;
    public bool GetSuperDashDown() => GetGamepad()?.leftTrigger.wasPressedThisFrame ?? false;
    public bool GetSuperDashUp() => GetGamepad()?.leftTrigger.wasReleasedThisFrame ?? false;
    public bool GetLeftDown() => GetMoveHorizontal() == -1;
    public bool GetRightDown() => GetMoveHorizontal() == 1;

#else
    // Fallback when Input System package is not yet enabled
    public int GetMoveHorizontal() => 0;
    public bool GetJumpDown() => false;
    public bool GetBlockDown() => false;
    public bool GetBlockUp() => false;
    public bool GetDownDown() => false;
    public bool GetHitDown() => false;
    public bool GetKickDown() => false;
    public bool GetChargeKey() => false;
    public bool GetChargeKeyUp() => false;
    public bool GetRushAttackDown() => false;
    public bool GetKiBlastDown() => false;
    public bool GetPowerSurgeDown() => false;
    public bool GetSuperDashDown() => false;
    public bool GetSuperDashUp() => false;
    public bool GetLeftDown() => false;
    public bool GetRightDown() => false;
#endif
}
