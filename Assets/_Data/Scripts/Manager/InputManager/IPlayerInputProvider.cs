public interface IPlayerInputProvider
{
    int GetMoveHorizontal();
    bool GetJumpDown();
    bool GetBlockDown();
    bool GetBlockUp();
    bool GetDownDown();
    bool GetHitDown();
    bool GetKickDown();
    bool GetChargeKey();
    bool GetChargeKeyUp();
    bool GetRushAttackDown();
    bool GetKiBlastDown();
    bool GetPowerSurgeDown();
    bool GetSuperDashDown();
    bool GetSuperDashUp();
    bool GetLeftDown();
    bool GetRightDown();
}
