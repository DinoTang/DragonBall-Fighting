using UnityEngine;

public class KeyboardInputProvider : IPlayerInputProvider
{
    private readonly PlayerInputConfig config;

    public KeyboardInputProvider(PlayerInputConfig config)
    {
        this.config = config ?? new PlayerInputConfig();
    }

    public int GetMoveHorizontal()
    {
        if (Input.GetKey(config.left)) return -1;
        if (Input.GetKey(config.right)) return 1;
        return 0;
    }

    public bool GetJumpDown() => Input.GetKeyDown(config.up);
    public bool GetBlockDown() => Input.GetKeyDown(config.block);
    public bool GetBlockUp() => Input.GetKeyUp(config.block);
    public bool GetDownDown() => Input.GetKeyDown(config.down);
    public bool GetHitDown() => Input.GetKeyDown(config.punch);
    public bool GetKickDown() => Input.GetKeyDown(config.kick);
    public bool GetChargeKey() => Input.GetKey(config.charge);
    public bool GetChargeKeyUp() => Input.GetKeyUp(config.charge);
    public bool GetRushAttackDown() => Input.GetKeyDown(config.rushAttack);
    public bool GetKiBlastDown() => Input.GetKeyDown(config.kiBlast);
    public bool GetPowerSurgeDown() => Input.GetKeyDown(config.powerSurge);
    public bool GetSuperDashDown() => Input.GetKeyDown(config.superDash);
    public bool GetSuperDashUp() => Input.GetKeyUp(config.superDash);
    public bool GetLeftDown() => Input.GetKeyDown(config.left);
    public bool GetRightDown() => Input.GetKeyDown(config.right);
}
