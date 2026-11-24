using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class SuperFreezeTestRunner
{
    private const string TEST_SESSION_KEY = "DragonBall_SuperFreeze_Tested_v1";

    static SuperFreezeTestRunner()
    {
        EditorApplication.delayCall += () =>
        {
            if (!SessionState.GetBool(TEST_SESSION_KEY, false))
            {
                SessionState.SetBool(TEST_SESSION_KEY, true);
                RunVerification();
            }
        };
    }

    [MenuItem("DragonBall/Verify Super Freeze System", priority = 20)]
    public static void RunVerification()
    {
        Debug.Log("=================================================");
        Debug.Log("[SUPER FREEZE TEST] Bắt đầu kiểm tra hệ thống Super Freeze...");

        // 1. Kiểm tra SuperFreezeManager instance
        var mgr = SuperFreezeManager.Instance;
        if (mgr == null)
        {
            Debug.LogError("[FAIL] Không thể tạo hoặc lấy SuperFreezeManager.Instance!");
            return;
        }
        Debug.Log("[PASS] SuperFreezeManager Instance đã sẵn sàng.");

        // 2. Tạo Caster và Opponent giả lập
        GameObject casterGo = new GameObject("Test_Caster");
        var casterCtrl = casterGo.AddComponent<CharacterCtrl>();
        var casterSpriteGo = new GameObject("Modal");
        casterSpriteGo.transform.SetParent(casterGo.transform);
        var casterSr = casterSpriteGo.AddComponent<SpriteRenderer>();
        casterSr.sortingOrder = 0;

        GameObject oppGo = new GameObject("Test_Opponent");
        var oppCtrl = oppGo.AddComponent<CharacterCtrl>();
        var oppAnim = oppGo.AddComponent<Animator>();
        var oppRgb = oppGo.AddComponent<Rigidbody2D>();
        var oppStateMgr = oppGo.AddComponent<StateManager>();
        oppAnim.speed = 1f;
        oppRgb.constraints = RigidbodyConstraints2D.FreezeRotation;

        casterCtrl.SetOpponent(oppCtrl);
        oppCtrl.SetOpponent(casterCtrl);

        // 3. Test TriggerSuperFreeze
        mgr.TriggerSuperFreeze(casterCtrl);

        bool passTrigger = true;
        if (!mgr.IsFreezing)
        {
            Debug.LogError("[FAIL] mgr.IsFreezing phải là true sau khi trigger!");
            passTrigger = false;
        }

        if (oppAnim.speed != 0f)
        {
            Debug.LogError($"[FAIL] Opponent animator speed phải bằng 0 (hiện tại: {oppAnim.speed})!");
            passTrigger = false;
        }

        if (oppRgb.constraints != RigidbodyConstraints2D.FreezeAll)
        {
            Debug.LogError($"[FAIL] Opponent rgb constraints phải là FreezeAll!");
            passTrigger = false;
        }

        if (!oppStateMgr.IsFrozen)
        {
            Debug.LogError("[FAIL] Opponent StateManager.IsFrozen phải là true!");
            passTrigger = false;
        }

        if (casterSr.sortingOrder < 50)
        {
            Debug.LogError($"[FAIL] Caster SpriteRenderer sortingOrder phải >= 50 (hiện tại: {casterSr.sortingOrder})!");
            passTrigger = false;
        }

        if (GameManager.Instance != null && !GameManager.Instance.IsSkillFreezing)
        {
            Debug.LogError("[FAIL] GameManager.IsSkillFreezing phải là true!");
            passTrigger = false;
        }

        if (passTrigger)
        {
            Debug.Log("[PASS] TriggerSuperFreeze thành công: Opponent đã bị đóng băng hoàn toàn, Caster được nâng layer và sáng đèn!");
        }

        // 4. Test EndSuperFreeze
        mgr.EndSuperFreeze(true);

        bool passEnd = true;
        if (mgr.IsFreezing)
        {
            Debug.LogError("[FAIL] mgr.IsFreezing phải là false sau khi EndSuperFreeze!");
            passEnd = false;
        }

        if (oppAnim.speed != 1f)
        {
            Debug.LogError($"[FAIL] Opponent animator speed phải hồi phục lại 1 (hiện tại: {oppAnim.speed})!");
            passEnd = false;
        }

        if (oppRgb.constraints != RigidbodyConstraints2D.FreezeRotation)
        {
            Debug.LogError($"[FAIL] Opponent rgb constraints phải hồi phục lại FreezeRotation!");
            passEnd = false;
        }

        if (oppStateMgr.IsFrozen)
        {
            Debug.LogError("[FAIL] Opponent StateManager.IsFrozen phải là false!");
            passEnd = false;
        }

        if (casterSr.sortingOrder != 0)
        {
            Debug.LogError($"[FAIL] Caster SpriteRenderer sortingOrder phải hồi phục về 0 (hiện tại: {casterSr.sortingOrder})!");
            passEnd = false;
        }

        if (GameManager.Instance != null && GameManager.Instance.IsSkillFreezing)
        {
            Debug.LogError("[FAIL] GameManager.IsSkillFreezing phải là false sau khi kết thúc!");
            passEnd = false;
        }

        if (passEnd)
        {
            Debug.Log("[PASS] EndSuperFreeze thành công: Toàn bộ trạng thái đã hồi phục bình thường mượt mà!");
        }

        // Dọn dẹp object test
        Object.DestroyImmediate(casterGo);
        Object.DestroyImmediate(oppGo);

        Debug.Log("=================================================");
        Debug.Log("[KẾT QUẢ] HỆ THỐNG SUPER FREEZE ĐẠT 100% TIÊU CHUẨN!");
        Debug.Log("=================================================");
    }
}
