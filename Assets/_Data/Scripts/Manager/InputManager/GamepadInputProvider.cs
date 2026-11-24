using UnityEngine;

public class GamepadInputProvider : IPlayerInputProvider
{
    private readonly int joystickIndex;
    private readonly string prefix;
    private readonly float deadzone = 0.5f;

    public GamepadInputProvider(int joystickIndex = 1)
    {
        this.joystickIndex = joystickIndex;
        this.prefix = $"joystick {joystickIndex} button ";
    }

    private bool GetButton(int buttonIndex) => Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), $"Joystick{joystickIndex}Button{buttonIndex}"));
    private bool GetButtonDown(int buttonIndex) => Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), $"Joystick{joystickIndex}Button{buttonIndex}"));
    private bool GetButtonUp(int buttonIndex) => Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), $"Joystick{joystickIndex}Button{buttonIndex}"));

    public int GetMoveHorizontal()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        if (horiz < -deadzone) return -1;
        if (horiz > deadzone) return 1;
        return 0;
    }

    public bool GetJumpDown() => GetButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0); // A / Cross
    public bool GetBlockDown() => GetButtonDown(5); // RB / R1
    public bool GetBlockUp() => GetButtonUp(5);
    public bool GetDownDown() => Input.GetAxisRaw("Vertical") < -deadzone;
    public bool GetHitDown() => GetButtonDown(2); // X / Square
    public bool GetKickDown() => GetButtonDown(1); // B / Circle
    public bool GetChargeKey() => GetButton(4); // LB / L1
    public bool GetChargeKeyUp() => GetButtonUp(4);
    public bool GetRushAttackDown() => GetButtonDown(3); // Y / Triangle
    public bool GetKiBlastDown() => GetButtonDown(7); // RT / R2
    public bool GetPowerSurgeDown() => GetButtonDown(9); // Right Stick click
    public bool GetSuperDashDown() => GetButtonDown(6); // LT / L2
    public bool GetSuperDashUp() => GetButtonUp(6);
    public bool GetLeftDown() => GetMoveHorizontal() == -1;
    public bool GetRightDown() => GetMoveHorizontal() == 1;
}
