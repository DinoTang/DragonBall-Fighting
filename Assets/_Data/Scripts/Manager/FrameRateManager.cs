using UnityEngine;

/// <summary>
/// Quản lý và tối ưu hóa tốc độ khung hình (FPS) duy trì ổn định ở mức 60 FPS,
/// đồng thời đồng bộ FixedUpdate (Physics) để đảm bảo game mượt mà, không giật lag.
/// </summary>
public class FrameRateManager : BaseBehaviour
{
    protected static FrameRateManager instance;
    public static FrameRateManager Instance => instance;

    [Header("Frame Rate Settings")]
    [Tooltip("Tốc độ khung hình mục tiêu (Mặc định: 60 FPS)")]
    [SerializeField] private int targetFrameRate = 60;

    [Tooltip("Đồng bộ FixedUpdate timestep theo target FPS (1 / targetFrameRate = 0.0166667s) để chuyển động vật lý mượt mà")]
    [SerializeField] private bool syncPhysicsTimestep = true;

    [Tooltip("Bật/Tắt VSync. Khi khóa 60 FPS, vSyncCount nên đặt = 0 để tránh bị ép theo tần số quét màn hình 144Hz/240Hz")]
    [SerializeField] private bool enableVSync = false;

    [Tooltip("Giữ game tiếp tục chạy ổn định khi mất focus (Alt-Tab hoặc click ngoài cửa sổ)")]
    [SerializeField] private bool runInBackground = true;

    [Header("FPS Counter Overlay (Debug)")]
    [Tooltip("Hiển thị thông số FPS trên màn hình để kiểm tra")]
    [SerializeField] private bool showFpsOverlay = false;

    [Tooltip("Phím tắt để bật/tắt hiển thị FPS (Mặc định: F1)")]
    [SerializeField] private KeyCode toggleFpsKey = KeyCode.F1;

    // Biến đo lường và tính toán FPS
    private float fpsCurrent = 60f;
    private float fpsAccumulator = 0f;
    private int frameCounter = 0;
    private float fpsTimeLeft = 0.5f;
    private const float FPS_UPDATE_INTERVAL = 0.4f;
    private string cachedDisplayString = "FPS: 60.0 (16.7 ms)";
    private GUIStyle cachedStyle;

    /// <summary>
    /// Tự động chạy trước khi bất kỳ Scene nào được load.
    /// Đảm bảo luôn áp dụng 60 FPS ngay từ đầu trong cả Editor và Build.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoInitializeBeforeSceneLoad()
    {
        ApplyFrameRateConfig(60, false, true, true);
    }

    /// <summary>
    /// Đảm bảo luôn có 1 FrameRateManager trong scene để lắng nghe phím tắt và hiển thị HUD debug nếu cần.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void EnsureInstanceExists()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("[FrameRateManager]");
            instance = go.AddComponent<FrameRateManager>();
            DontDestroyOnLoad(go);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        ApplySettings();
    }

    protected override void Start()
    {
        base.Start();
        ApplySettings();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(this.toggleFpsKey))
        {
            this.showFpsOverlay = !this.showFpsOverlay;
        }

        if (this.showFpsOverlay)
        {
            this.CalculateFPS();
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            ApplySettings();
        }
    }

    /// <summary>
    /// Áp dụng các cài đặt FPS, VSync và Physics
    /// </summary>
    public void ApplySettings()
    {
        ApplyFrameRateConfig(this.targetFrameRate, this.enableVSync, this.syncPhysicsTimestep, this.runInBackground);
    }

    /// <summary>
    /// Hàm static thực thi thiết lập cấu hình FPS và VSync cho toàn bộ Engine
    /// </summary>
    public static void ApplyFrameRateConfig(int fps, bool vSync, bool syncPhysics, bool allowBackground)
    {
        // 1. Tắt VSync hoặc bật theo cấu hình (để targetFrameRate hoạt động chuẩn xác)
        QualitySettings.vSyncCount = vSync ? 1 : 0;

        // 2. Khóa tốc độ khung hình mục tiêu ở 60 FPS
        Application.targetFrameRate = fps;

        // 3. Giữ game chạy mượt khi không focus cửa sổ
        Application.runInBackground = allowBackground;

        // 4. Đồng bộ Physics FixedUpdate với FPS hiển thị (60 FPS = 0.0166667s)
        if (syncPhysics && fps > 0)
        {
            Time.fixedDeltaTime = 1.0f / fps;
            Time.maximumDeltaTime = 0.1f; // Ngăn hiện tượng spiral of death nếu lag đột ngột
        }
    }

    public void SetTargetFrameRate(int fps)
    {
        this.targetFrameRate = fps;
        this.ApplySettings();
    }

    public void ToggleFpsOverlay()
    {
        this.showFpsOverlay = !this.showFpsOverlay;
    }

    private void CalculateFPS()
    {
        this.fpsTimeLeft -= Time.unscaledDeltaTime;
        this.fpsAccumulator += 1.0f / Mathf.Max(0.0001f, Time.unscaledDeltaTime);
        this.frameCounter++;

        if (this.fpsTimeLeft <= 0.0f)
        {
            this.fpsCurrent = this.fpsAccumulator / Mathf.Max(1, this.frameCounter);
            this.fpsTimeLeft = FPS_UPDATE_INTERVAL;
            this.fpsAccumulator = 0.0f;
            this.frameCounter = 0;

            float frameMs = 1000.0f / Mathf.Max(1.0f, this.fpsCurrent);
            string vSyncStr = QualitySettings.vSyncCount > 0 ? "ON" : "OFF";
            this.cachedDisplayString = $"FPS: {this.fpsCurrent:F1} ({frameMs:F1} ms) | Target: {Application.targetFrameRate} | VSync: {vSyncStr} | Physics: {1f / Time.fixedDeltaTime:F0}Hz";
        }
    }

    private void OnGUI()
    {
        if (!this.showFpsOverlay) return;

        if (this.cachedStyle == null)
        {
            this.cachedStyle = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 13,
                fontStyle = FontStyle.Bold
            };
        }

        Color textColor;
        if (this.fpsCurrent >= 58f) textColor = new Color(0.2f, 1f, 0.2f);
        else if (this.fpsCurrent >= 48f) textColor = new Color(1f, 0.9f, 0.2f);
        else textColor = new Color(1f, 0.3f, 0.3f);

        this.cachedStyle.normal.textColor = textColor;

        Rect rect = new Rect(12, 12, 360, 28);
        GUI.Box(rect, GUIContent.none);
        GUI.Label(new Rect(rect.x + 8, rect.y + 2, rect.width - 16, rect.height - 4), this.cachedDisplayString, this.cachedStyle);
    }
}
