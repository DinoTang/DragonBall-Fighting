using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Công cụ Editor tự động chuyển đổi các GameObjects tái sử dụng trên Hierarchy thành Prefab.
/// Khi chỉnh sửa 1 Prefab (ví dụ: Hitbox, VFX, Collider, UI...), tất cả các đối tượng trong scene sẽ tự động cập nhật.
/// </summary>
[InitializeOnLoad]
public class PrefabHierarchyConverter : EditorWindow
{
    private const string SESSION_KEY = "DragonBall_Prefabs_AutoConverted_v1";

    static PrefabHierarchyConverter()
    {
        EditorApplication.delayCall += OnEditorReady;
    }

    private static void OnEditorReady()
    {
        if (!SessionState.GetBool(SESSION_KEY, false))
        {
            SessionState.SetBool(SESSION_KEY, true);
            // Tự động kiểm tra và tạo Prefab nếu chưa có
            ConvertAllToPrefabs(false);
        }
    }

    [MenuItem("DragonBall/Prefab Converter Window", priority = 10)]
    public static void ShowWindow()
    {
        var window = GetWindow<PrefabHierarchyConverter>("Prefab Converter");
        window.minSize = new Vector2(420, 360);
        window.Show();
    }

    [MenuItem("DragonBall/Convert Reusable Hierarchy Objects to Prefabs %&p", priority = 11)]
    public static void ConvertAllManual()
    {
        ConvertAllToPrefabs(true);
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        EditorGUILayout.LabelField("DragonBall - Prefab Hierarchy Converter", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Công cụ này đóng gói các GameObjects tái sử dụng trên Hierarchy thành Prefabs.\n" +
            "- Nhân vật (PlayerRed, PlayerBlue)\n" +
            "- Thanh máu & Mana (LifeBar, ManaBar)\n" +
            "- Toàn bộ FX & Kỹ năng trong Spawners\n" +
            "- Môi trường (Walls)\n\n" +
            "Khi đã chuyển thành Prefab, bạn chỉ cần sửa 1 lần trên Prefab là toàn bộ game sẽ tự động áp dụng!",
            MessageType.Info);

        GUILayout.Space(15);
        if (GUILayout.Button("Chuyển đổi TẤT CẢ sang Prefab (Convert All)", GUILayout.Height(36)))
        {
            ConvertAllToPrefabs(true);
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Chỉ chuyển đổi Nhân vật (Characters)", GUILayout.Height(28)))
        {
            ConvertCharacters();
        }

        if (GUILayout.Button("Chỉ chuyển đổi Giao diện (UI LifeBar / ManaBar)", GUILayout.Height(28)))
        {
            ConvertUI();
        }

        if (GUILayout.Button("Chỉ chuyển đổi FX & Kỹ năng (Spawners)", GUILayout.Height(28)))
        {
            ConvertSpawners();
        }
    }

    public static void ConvertAllToPrefabs(bool showDialog)
    {
        int convertedCount = 0;

        EnsureFolderExists("Assets/_Data/Prefabs");
        EnsureFolderExists("Assets/_Data/Prefabs/Characters");
        EnsureFolderExists("Assets/_Data/Prefabs/UI");
        EnsureFolderExists("Assets/_Data/Prefabs/FX");
        EnsureFolderExists("Assets/_Data/Prefabs/Skills");
        EnsureFolderExists("Assets/_Data/Prefabs/Skills/Vegeta");
        EnsureFolderExists("Assets/_Data/Prefabs/Environment");

        convertedCount += ConvertCharactersInternal();
        convertedCount += ConvertUIInternal();
        convertedCount += ConvertSpawnersInternal();
        convertedCount += ConvertEnvironmentInternal();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.isLoaded)
        {
            EditorSceneManager.MarkSceneDirty(activeScene);
            EditorSceneManager.SaveScene(activeScene);
        }

        string msg = $"Đã chuyển đổi và kết nối thành công {convertedCount} đối tượng thành Prefab trong Hierarchy!";
        Debug.Log($"<color=#00FF00><b>[PrefabHierarchyConverter]</b></color> {msg}");

        if (showDialog)
        {
            EditorUtility.DisplayDialog("Thành công", msg, "OK");
        }
    }

    private static void ConvertCharacters()
    {
        EnsureFolderExists("Assets/_Data/Prefabs/Characters");
        int count = ConvertCharactersInternal();
        AssetDatabase.SaveAssets();
        Debug.Log($"[PrefabHierarchyConverter] Đã chuyển đổi {count} nhân vật sang Prefab.");
    }

    private static void ConvertUI()
    {
        EnsureFolderExists("Assets/_Data/Prefabs/UI");
        int count = ConvertUIInternal();
        AssetDatabase.SaveAssets();
        Debug.Log($"[PrefabHierarchyConverter] Đã chuyển đổi {count} UI sang Prefab.");
    }

    private static void ConvertSpawners()
    {
        EnsureFolderExists("Assets/_Data/Prefabs/FX");
        EnsureFolderExists("Assets/_Data/Prefabs/Skills/Vegeta");
        int count = ConvertSpawnersInternal();
        AssetDatabase.SaveAssets();
        Debug.Log($"[PrefabHierarchyConverter] Đã chuyển đổi {count} Spawner FX & Skills sang Prefab.");
    }

    private static int ConvertCharactersInternal()
    {
        int count = 0;
        GameObject playerRed = GameObject.Find("PlayerRed");
        if (playerRed != null)
        {
            SaveAndConnectPrefab(playerRed, "Assets/_Data/Prefabs/Characters/PlayerRed.prefab");
            count++;
        }

        GameObject playerBlue = GameObject.Find("PlayerBlue");
        if (playerBlue != null)
        {
            SaveAndConnectPrefab(playerBlue, "Assets/_Data/Prefabs/Characters/PlayerBlue.prefab");
            count++;
        }

        return count;
    }

    private static int ConvertUIInternal()
    {
        int count = 0;
        GameObject p1Life = GameObject.Find("Player1LifeBar");
        if (p1Life != null)
        {
            SaveAndConnectPrefab(p1Life, "Assets/_Data/Prefabs/UI/PlayerLifeBar_Red.prefab");
            count++;
        }

        GameObject p2Life = GameObject.Find("Player2LifeBar");
        if (p2Life != null)
        {
            SaveAndConnectPrefab(p2Life, "Assets/_Data/Prefabs/UI/PlayerLifeBar_Blue.prefab");
            count++;
        }

        GameObject p1Mana = GameObject.Find("Player1ManaBar");
        if (p1Mana != null)
        {
            SaveAndConnectPrefab(p1Mana, "Assets/_Data/Prefabs/UI/PlayerManaBar_Red.prefab");
            count++;
        }

        GameObject p2Mana = GameObject.Find("Player2ManaBar");
        if (p2Mana != null)
        {
            SaveAndConnectPrefab(p2Mana, "Assets/_Data/Prefabs/UI/PlayerManaBar_Blue.prefab");
            count++;
        }

        return count;
    }

    private static int ConvertSpawnersInternal()
    {
        int count = 0;

        // 1. FX Spawners
        GameObject fxSpawnerRoot = GameObject.Find("FXSpawner");
        if (fxSpawnerRoot != null)
        {
            foreach (Transform spawner in fxSpawnerRoot.transform)
            {
                Transform prefabHolder = spawner.Find("Prefab");
                if (prefabHolder == null) continue;

                foreach (Transform child in prefabHolder)
                {
                    string prefabPath = $"Assets/_Data/Prefabs/FX/{child.gameObject.name}.prefab";
                    if (SaveAndConnectPrefab(child.gameObject, prefabPath))
                    {
                        count++;
                    }
                }
            }
        }

        // 2. ShotSkillSpawner
        GameObject skillSpawnerRoot = GameObject.Find("ShotSkillSpawner");
        if (skillSpawnerRoot != null)
        {
            foreach (Transform charFolder in skillSpawnerRoot.transform)
            {
                Transform prefabHolder = charFolder.Find("Prefab");
                if (prefabHolder == null) continue;

                string folderPath = $"Assets/_Data/Prefabs/Skills/{charFolder.gameObject.name}";
                EnsureFolderExists(folderPath);

                foreach (Transform child in prefabHolder)
                {
                    string prefabPath = $"{folderPath}/{child.gameObject.name}.prefab";
                    if (SaveAndConnectPrefab(child.gameObject, prefabPath))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private static int ConvertEnvironmentInternal()
    {
        int count = 0;
        GameObject wall = GameObject.Find("Wall");
        if (wall != null)
        {
            if (SaveAndConnectPrefab(wall, "Assets/_Data/Prefabs/Environment/Wall.prefab"))
            {
                count++;
            }
        }
        return count;
    }

    private static bool SaveAndConnectPrefab(GameObject target, string path)
    {
        if (target == null) return false;

        try
        {
            // Kiểm tra xem đối tượng đã là Prefab Instance được kết nối chuẩn chưa
            PrefabAssetType assetType = PrefabUtility.GetPrefabAssetType(target);
            PrefabInstanceStatus instanceStatus = PrefabUtility.GetPrefabInstanceStatus(target);

            // Nếu chưa kết nối thành Prefab chuẩn trong scene thì lưu và kết nối
            if (instanceStatus != PrefabInstanceStatus.Connected || assetType == PrefabAssetType.MissingAsset)
            {
                PrefabUtility.SaveAsPrefabAssetAndConnect(target, path, InteractionMode.AutomatedAction);
                Debug.Log($"[PrefabHierarchyConverter] Đã lưu & kết nối: {target.name} -> {path}");
                return true;
            }
            else
            {
                // Nếu đã kết nối, cập nhật lại nội dung Prefab asset
                PrefabUtility.SaveAsPrefabAsset(target, path);
                return true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"[PrefabHierarchyConverter] Không thể kết nối {target.name}: {ex.Message}");
            return false;
        }
    }

    private static void EnsureFolderExists(string fullPath)
    {
        if (AssetDatabase.IsValidFolder(fullPath)) return;

        string[] parts = fullPath.Split('/');
        string current = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = current + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(current, parts[i]);
            }
            current = next;
        }
    }
}
