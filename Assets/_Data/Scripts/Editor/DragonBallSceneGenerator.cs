using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Tool Editor tự động khởi tạo toàn bộ các scene giao diện cho game Dragon Ball Fighting:
/// 1. SplashScene (Màn hình khởi động, logo, pulse text)
/// 2. MainMenuScene (Màn hình chính, Arcade, Training, Settings Modal, Quit)
/// 3. CharacterSelectScene (Màn hình chọn nhân vật, P1/P2 preview, roster grid, stats)
/// 4. PostMatchScene (Màn hình kết quả, thông báo thắng, chỉ số trận đấu, Rematch, Select, Menu)
/// Đồng thời tự động cập nhật EditorBuildSettings theo đúng thứ tự luồng game.
/// </summary>
public static class DragonBallSceneGenerator
{
    private const string SCENE_DIR = "Assets/Scenes/";
    private const string SPLASH_PATH = SCENE_DIR + "SplashScene.unity";
    private const string MAIN_MENU_PATH = SCENE_DIR + "MainMenuScene.unity";
    private const string CHAR_SELECT_PATH = SCENE_DIR + "CharacterSelectScene.unity";
    private const string BATTLE_PATH = SCENE_DIR + "SampleScene.unity";
    private const string POST_MATCH_PATH = SCENE_DIR + "PostMatchScene.unity";

    [MenuItem("DragonBall/Open Battle Scene (SampleScene) &#b", priority = 0)]
    public static void OpenBattleScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(BATTLE_PATH);
        }
    }

    [MenuItem("DragonBall/Generate All Game UI Scenes & Setup Build %&g", priority = 1)]
    public static void GenerateAllScenes()
    {
        Debug.Log("[SCENE GENERATOR] Bắt đầu khởi tạo hệ thống giao diện hoàn chỉnh...");

        EnsureSceneDirectory();

        Sprite bgSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Data/Sprites/BackGround/background_1.png");
        Sprite dragonBallSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Data/Sprites/_UI/DragonBall.png");
        Sprite avatarSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Data/Sprites/_UI/Avatar.png");
        Sprite winnerSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/_Data/Sprites/_UI/Winner.png");
        Font defaultFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/TextMesh Pro/Fonts/LiberationSans.ttf") 
                           ?? Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        // 1. Tạo SplashScene
        GenerateSplashScene(dragonBallSprite, defaultFont);

        // 2. Tạo MainMenuScene
        GenerateMainMenuScene(bgSprite, dragonBallSprite, defaultFont);

        // 3. Tạo CharacterSelectScene
        GenerateCharacterSelectScene(bgSprite, avatarSprite, defaultFont);

        // 4. Tạo PostMatchScene
        GeneratePostMatchScene(bgSprite, avatarSprite, winnerSprite, defaultFont);

        // 5. Cấu hình Build Settings
        SetupBuildSettings();

        Debug.Log("[SCENE GENERATOR] Hoàn tất 100% tạo giao diện và cài đặt Build Settings!");
    }

    private static void EnsureSceneDirectory()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
        {
            AssetDatabase.CreateFolder("Assets", "Scenes");
        }
    }

    #region 1. Splash Scene
    private static void GenerateSplashScene(Sprite dragonBallSprite, Font font)
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Camera
        GameObject camGo = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
        Camera cam = camGo.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.04f, 0.03f, 0.08f);
        cam.orthographic = true;
        camGo.transform.position = new Vector3(0, 0, -10);

        // EventSystem
        CreateEventSystem();

        // Canvas Root
        GameObject canvasGo = CreateCanvas("SplashCanvas");
        var splashUI = canvasGo.AddComponent<SplashScreenUI>();

        // Dark Background Image
        GameObject bgGo = CreateUIElement("Background", canvasGo.transform);
        StretchFull(bgGo.GetComponent<RectTransform>());
        Image bgImg = bgGo.AddComponent<Image>();
        bgImg.color = new Color(0.04f, 0.03f, 0.09f, 1f);

        // Logo DragonBall
        if (dragonBallSprite != null)
        {
            GameObject ballGo = CreateUIElement("DragonBallLogo", canvasGo.transform);
            RectTransform ballRt = ballGo.GetComponent<RectTransform>();
            ballRt.anchorMin = new Vector2(0.5f, 0.65f);
            ballRt.anchorMax = new Vector2(0.5f, 0.65f);
            ballRt.sizeDelta = new Vector2(140, 140);
            Image ballImg = ballGo.AddComponent<Image>();
            ballImg.sprite = dragonBallSprite;
            ballImg.preserveAspect = true;
        }

        // Title: DRAGON BALL
        GameObject titleGo = CreateUIElement("TitleText", canvasGo.transform);
        RectTransform titleRt = titleGo.GetComponent<RectTransform>();
        titleRt.anchorMin = new Vector2(0.5f, 0.5f);
        titleRt.anchorMax = new Vector2(0.5f, 0.5f);
        titleRt.sizeDelta = new Vector2(900, 100);
        Text titleTxt = titleGo.AddComponent<Text>();
        titleTxt.font = font;
        titleTxt.text = "DRAGON BALL";
        titleTxt.fontSize = 68;
        titleTxt.alignment = TextAnchor.MiddleCenter;
        titleTxt.color = new Color(1f, 0.85f, 0.1f);
        titleTxt.fontStyle = FontStyle.Bold;

        // Subtitle: SUPER CLASH FIGHTING
        GameObject subGo = CreateUIElement("SubtitleText", canvasGo.transform);
        RectTransform subRt = subGo.GetComponent<RectTransform>();
        subRt.anchorMin = new Vector2(0.5f, 0.42f);
        subRt.anchorMax = new Vector2(0.5f, 0.42f);
        subRt.sizeDelta = new Vector2(800, 50);
        Text subTxt = subGo.AddComponent<Text>();
        subTxt.font = font;
        subTxt.text = "★ SUPER FIGHTING CLASH ★";
        subTxt.fontSize = 26;
        subTxt.alignment = TextAnchor.MiddleCenter;
        subTxt.color = new Color(0f, 0.9f, 1f);
        subTxt.fontStyle = FontStyle.Bold;

        // Pulse Prompt: PRESS ANY KEY TO START
        GameObject promptGo = CreateUIElement("PromptText", canvasGo.transform);
        RectTransform promptRt = promptGo.GetComponent<RectTransform>();
        promptRt.anchorMin = new Vector2(0.5f, 0.22f);
        promptRt.anchorMax = new Vector2(0.5f, 0.22f);
        promptRt.sizeDelta = new Vector2(800, 50);
        Text promptTxt = promptGo.AddComponent<Text>();
        promptTxt.font = font;
        promptTxt.text = "— PRESS ANY KEY OR CLICK TO START —";
        promptTxt.fontSize = 26;
        promptTxt.alignment = TextAnchor.MiddleCenter;
        promptTxt.color = new Color(1f, 0.95f, 0.7f);
        promptTxt.fontStyle = FontStyle.Bold;

        // Invisible Fullscreen Click Button
        GameObject clickBtnGo = CreateUIElement("FullscreenClickButton", canvasGo.transform);
        StretchFull(clickBtnGo.GetComponent<RectTransform>());
        Image clickImg = clickBtnGo.AddComponent<Image>();
        clickImg.color = new Color(0, 0, 0, 0);
        Button clickBtn = clickBtnGo.AddComponent<Button>();

        // Footer
        GameObject footGo = CreateUIElement("FooterText", canvasGo.transform);
        RectTransform footRt = footGo.GetComponent<RectTransform>();
        footRt.anchorMin = new Vector2(0.5f, 0.05f);
        footRt.anchorMax = new Vector2(0.5f, 0.05f);
        footRt.sizeDelta = new Vector2(900, 40);
        Text footTxt = footGo.AddComponent<Text>();
        footTxt.font = font;
        footTxt.text = "FAN TRIBUTE • 60 FPS ULTRA PERFORMANCE • POWERED BY ANTIGRAVITY";
        footTxt.fontSize = 14;
        footTxt.alignment = TextAnchor.MiddleCenter;
        footTxt.color = new Color(0.6f, 0.6f, 0.7f, 0.8f);

        // Bind Serialized Fields on SplashScreenUI
        SerializedObject so = new SerializedObject(splashUI);
        so.FindProperty("startButton").objectReferenceValue = clickBtn;
        so.FindProperty("promptText").objectReferenceValue = promptTxt;
        so.FindProperty("titleText").objectReferenceValue = titleTxt;
        so.ApplyModifiedProperties();

        EditorSceneManager.SaveScene(scene, SPLASH_PATH);
        Debug.Log("[SCENE GENERATOR] Đã tạo SplashScene tại " + SPLASH_PATH);
    }
    #endregion

    #region 2. Main Menu Scene
    private static void GenerateMainMenuScene(Sprite bgSprite, Sprite dragonBallSprite, Font font)
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Camera
        GameObject camGo = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
        Camera cam = camGo.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.05f, 0.04f, 0.09f);
        cam.orthographic = true;
        camGo.transform.position = new Vector3(0, 0, -10);

        CreateEventSystem();

        GameObject canvasGo = CreateCanvas("MainMenuCanvas");
        var menuUI = canvasGo.AddComponent<MainMenuUI>();

        // Background
        GameObject bgGo = CreateUIElement("Background", canvasGo.transform);
        StretchFull(bgGo.GetComponent<RectTransform>());
        Image bgImg = bgGo.AddComponent<Image>();
        if (bgSprite != null)
        {
            bgImg.sprite = bgSprite;
            bgImg.color = new Color(0.4f, 0.45f, 0.6f, 1f); // Stylized tint
        }
        else
        {
            bgImg.color = new Color(0.08f, 0.07f, 0.15f);
        }

        // Dark Vignette
        GameObject vigGo = CreateUIElement("Vignette", canvasGo.transform);
        StretchFull(vigGo.GetComponent<RectTransform>());
        Image vigImg = vigGo.AddComponent<Image>();
        vigImg.color = new Color(0, 0, 0, 0.45f);

        // Title Banner Container
        GameObject headerGo = CreateUIElement("HeaderPanel", canvasGo.transform);
        RectTransform headerRt = headerGo.GetComponent<RectTransform>();
        headerRt.anchorMin = new Vector2(0.5f, 0.75f);
        headerRt.anchorMax = new Vector2(0.5f, 0.95f);
        headerRt.sizeDelta = new Vector2(900, 140);

        GameObject titleGo = CreateUIElement("TitleText", headerGo.transform);
        RectTransform titleRt = titleGo.GetComponent<RectTransform>();
        StretchFull(titleRt);
        Text titleTxt = titleGo.AddComponent<Text>();
        titleTxt.font = font;
        titleTxt.text = "DRAGON BALL: SUPER CLASH";
        titleTxt.fontSize = 54;
        titleTxt.alignment = TextAnchor.MiddleCenter;
        titleTxt.color = new Color(1f, 0.85f, 0.15f);
        titleTxt.fontStyle = FontStyle.Bold;

        // Menu Buttons Panel
        GameObject menuPanelGo = CreateUIElement("MenuButtonsPanel", canvasGo.transform);
        RectTransform menuRt = menuPanelGo.GetComponent<RectTransform>();
        menuRt.anchorMin = new Vector2(0.5f, 0.2f);
        menuRt.anchorMax = new Vector2(0.5f, 0.7f);
        menuRt.sizeDelta = new Vector2(480, 360);

        VerticalLayoutGroup vlg = menuPanelGo.AddComponent<VerticalLayoutGroup>();
        vlg.spacing = 16f;
        vlg.childAlignment = TextAnchor.MiddleCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;

        Button arcadeBtn = CreateStyledButton("ArcadeBtn", menuPanelGo.transform, "⚔️  ARCADE / VERSUS", new Color(0.9f, 0.6f, 0.1f), font, 56);
        Button trainingBtn = CreateStyledButton("TrainingBtn", menuPanelGo.transform, "🥋  TRAINING MODE", new Color(0.15f, 0.65f, 0.9f), font, 56);
        Button settingsBtn = CreateStyledButton("SettingsBtn", menuPanelGo.transform, "⚙️  SETTINGS", new Color(0.6f, 0.35f, 0.8f), font, 56);
        Button quitBtn = CreateStyledButton("QuitBtn", menuPanelGo.transform, "🚪  QUIT GAME", new Color(0.85f, 0.25f, 0.25f), font, 56);

        // Settings Modal
        GameObject settingsModal = CreateSettingsModal(canvasGo.transform, font, out Slider bgmSld, out Slider sfxSld, out Toggle fpsTgl, out Toggle fsTgl, out Button closeSettingsBtn);
        settingsModal.SetActive(false);

        // Footer Help
        GameObject footGo = CreateUIElement("HelpFooter", canvasGo.transform);
        RectTransform footRt = footGo.GetComponent<RectTransform>();
        footRt.anchorMin = new Vector2(0.5f, 0.05f);
        footRt.anchorMax = new Vector2(0.5f, 0.05f);
        footRt.sizeDelta = new Vector2(900, 35);
        Text footTxt = footGo.AddComponent<Text>();
        footTxt.font = font;
        footTxt.text = "[Click or Tab/Enter] Select Menu  •  [ESC] Exit Settings  •  60 FPS Locked";
        footTxt.fontSize = 15;
        footTxt.alignment = TextAnchor.MiddleCenter;
        footTxt.color = new Color(0.7f, 0.7f, 0.8f, 0.8f);

        // Bind Serialized Fields
        SerializedObject so = new SerializedObject(menuUI);
        so.FindProperty("arcadeButton").objectReferenceValue = arcadeBtn;
        so.FindProperty("trainingButton").objectReferenceValue = trainingBtn;
        so.FindProperty("settingsButton").objectReferenceValue = settingsBtn;
        so.FindProperty("quitButton").objectReferenceValue = quitBtn;
        so.FindProperty("settingsPanel").objectReferenceValue = settingsModal;
        so.FindProperty("bgmSlider").objectReferenceValue = bgmSld;
        so.FindProperty("sfxSlider").objectReferenceValue = sfxSld;
        so.FindProperty("fpsToggle").objectReferenceValue = fpsTgl;
        so.FindProperty("fullscreenToggle").objectReferenceValue = fsTgl;
        so.FindProperty("closeSettingsButton").objectReferenceValue = closeSettingsBtn;
        so.ApplyModifiedProperties();

        EditorSceneManager.SaveScene(scene, MAIN_MENU_PATH);
        Debug.Log("[SCENE GENERATOR] Đã tạo MainMenuScene tại " + MAIN_MENU_PATH);
    }

    private static GameObject CreateSettingsModal(Transform parent, Font font, out Slider bgm, out Slider sfx, out Toggle fps, out Toggle fs, out Button closeBtn)
    {
        GameObject modal = CreateUIElement("SettingsModal", parent);
        StretchFull(modal.GetComponent<RectTransform>());

        // Dark blocker
        Image blockImg = modal.AddComponent<Image>();
        blockImg.color = new Color(0, 0, 0, 0.75f);

        // Dialog Box
        GameObject dialog = CreateUIElement("DialogBox", modal.transform);
        RectTransform dRt = dialog.GetComponent<RectTransform>();
        dRt.anchorMin = new Vector2(0.5f, 0.5f);
        dRt.anchorMax = new Vector2(0.5f, 0.5f);
        dRt.sizeDelta = new Vector2(560, 420);
        Image dImg = dialog.AddComponent<Image>();
        dImg.color = new Color(0.12f, 0.11f, 0.2f, 0.98f);

        // Dialog Title
        GameObject tGo = CreateUIElement("Title", dialog.transform);
        RectTransform tRt = tGo.GetComponent<RectTransform>();
        tRt.anchorMin = new Vector2(0.5f, 0.9f);
        tRt.anchorMax = new Vector2(0.5f, 0.9f);
        tRt.sizeDelta = new Vector2(400, 40);
        Text tTxt = tGo.AddComponent<Text>();
        tTxt.font = font;
        tTxt.text = "SETTINGS & OPTIONS";
        tTxt.fontSize = 26;
        tTxt.alignment = TextAnchor.MiddleCenter;
        tTxt.color = new Color(1f, 0.85f, 0.2f);
        tTxt.fontStyle = FontStyle.Bold;

        // Sliders & Toggles Container
        GameObject form = CreateUIElement("Form", dialog.transform);
        RectTransform fRt = form.GetComponent<RectTransform>();
        fRt.anchorMin = new Vector2(0.1f, 0.25f);
        fRt.anchorMax = new Vector2(0.9f, 0.8f);
        fRt.sizeDelta = Vector2.zero;
        VerticalLayoutGroup vlg = form.AddComponent<VerticalLayoutGroup>();
        vlg.spacing = 20f;
        vlg.childAlignment = TextAnchor.UpperCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;

        bgm = CreateSliderRow("BgmRow", form.transform, "BGM Volume", font);
        sfx = CreateSliderRow("SfxRow", form.transform, "SFX Volume", font);
        fps = CreateToggleRow("FpsRow", form.transform, "Target 60 FPS Lock (Smooth)", font);
        fs = CreateToggleRow("FsRow", form.transform, "Fullscreen Mode", font);

        // Close Button
        closeBtn = CreateStyledButton("CloseBtn", dialog.transform, "SAVE & CLOSE", new Color(0.3f, 0.7f, 0.3f), font, 44);
        RectTransform cRt = closeBtn.GetComponent<RectTransform>();
        cRt.anchorMin = new Vector2(0.5f, 0.12f);
        cRt.anchorMax = new Vector2(0.5f, 0.12f);
        cRt.sizeDelta = new Vector2(240, 46);

        return modal;
    }

    private static Slider CreateSliderRow(string name, Transform parent, string label, Font font)
    {
        GameObject row = CreateUIElement(name, parent);
        HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 15f;
        hlg.childControlWidth = false;
        hlg.childControlHeight = false;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        row.GetComponent<RectTransform>().sizeDelta = new Vector2(440, 36);

        GameObject lblGo = CreateUIElement("Label", row.transform);
        lblGo.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        Text lbl = lblGo.AddComponent<Text>();
        lbl.font = font;
        lbl.text = label;
        lbl.fontSize = 16;
        lbl.color = Color.white;
        lbl.alignment = TextAnchor.MiddleLeft;

        GameObject sldGo = CreateUIElement("Slider", row.transform);
        sldGo.GetComponent<RectTransform>().sizeDelta = new Vector2(240, 24);
        Slider sld = sldGo.AddComponent<Slider>();
        sld.minValue = 0f;
        sld.maxValue = 1f;
        sld.value = 1f;

        // Background
        Image sldBg = sldGo.AddComponent<Image>();
        sldBg.color = new Color(0.2f, 0.2f, 0.3f);

        return sld;
    }

    private static Toggle CreateToggleRow(string name, Transform parent, string label, Font font)
    {
        GameObject row = CreateUIElement(name, parent);
        HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 15f;
        hlg.childControlWidth = false;
        hlg.childControlHeight = false;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        row.GetComponent<RectTransform>().sizeDelta = new Vector2(440, 32);

        GameObject boxGo = CreateUIElement("Box", row.transform);
        boxGo.GetComponent<RectTransform>().sizeDelta = new Vector2(24, 24);
        Image boxImg = boxGo.AddComponent<Image>();
        boxImg.color = new Color(0.25f, 0.25f, 0.35f);

        Toggle tgl = row.AddComponent<Toggle>();
        tgl.targetGraphic = boxImg;
        tgl.isOn = true;

        GameObject lblGo = CreateUIElement("Label", row.transform);
        lblGo.GetComponent<RectTransform>().sizeDelta = new Vector2(380, 28);
        Text lbl = lblGo.AddComponent<Text>();
        lbl.font = font;
        lbl.text = label;
        lbl.fontSize = 16;
        lbl.color = Color.white;
        lbl.alignment = TextAnchor.MiddleLeft;

        return tgl;
    }
    #endregion

    #region 3. Character Select Scene
    private static void GenerateCharacterSelectScene(Sprite bgSprite, Sprite avatarSprite, Font font)
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        GameObject camGo = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
        Camera cam = camGo.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.06f, 0.05f, 0.1f);
        cam.orthographic = true;
        camGo.transform.position = new Vector3(0, 0, -10);

        CreateEventSystem();

        GameObject canvasGo = CreateCanvas("CharacterSelectCanvas");
        var csUI = canvasGo.AddComponent<CharacterSelectUI>();

        // Background
        GameObject bgGo = CreateUIElement("Background", canvasGo.transform);
        StretchFull(bgGo.GetComponent<RectTransform>());
        Image bgImg = bgGo.AddComponent<Image>();
        if (bgSprite != null)
        {
            bgImg.sprite = bgSprite;
            bgImg.color = new Color(0.35f, 0.35f, 0.5f, 1f);
        }
        else
        {
            bgImg.color = new Color(0.06f, 0.05f, 0.12f);
        }

        // Header Title
        GameObject headerGo = CreateUIElement("HeaderTitle", canvasGo.transform);
        RectTransform hRt = headerGo.GetComponent<RectTransform>();
        hRt.anchorMin = new Vector2(0.5f, 0.91f);
        hRt.anchorMax = new Vector2(0.5f, 0.91f);
        hRt.sizeDelta = new Vector2(800, 60);
        Text hTxt = headerGo.AddComponent<Text>();
        hTxt.font = font;
        hTxt.text = "SELECT YOUR FIGHTER";
        hTxt.fontSize = 38;
        hTxt.alignment = TextAnchor.MiddleCenter;
        hTxt.color = new Color(1f, 0.85f, 0.1f);
        hTxt.fontStyle = FontStyle.Bold;

        // P1 Panel (Left / Red Team)
        GameObject p1Panel = CreatePlayerCornerPanel("Player1_RedPanel", canvasGo.transform, new Vector2(0.02f, 0.15f), new Vector2(0.28f, 0.86f),
            new Color(0.8f, 0.2f, 0.2f, 0.85f), "PLAYER 1", font, avatarSprite,
            out Text p1Name, out Text p1Title, out Image p1Portrait, out Text p1Stats, out Text p1Specials, out Text p1Status);

        // P2 Panel (Right / Blue Team)
        GameObject p2Panel = CreatePlayerCornerPanel("Player2_BluePanel", canvasGo.transform, new Vector2(0.72f, 0.15f), new Vector2(0.98f, 0.86f),
            new Color(0.2f, 0.4f, 0.85f, 0.85f), "PLAYER 2", font, avatarSprite,
            out Text p2Name, out Text p2Title, out Image p2Portrait, out Text p2Stats, out Text p2Specials, out Text p2Status);

        // Center Roster Box
        GameObject rosterBox = CreateUIElement("RosterBox", canvasGo.transform);
        RectTransform rRt = rosterBox.GetComponent<RectTransform>();
        rRt.anchorMin = new Vector2(0.3f, 0.22f);
        rRt.anchorMax = new Vector2(0.7f, 0.84f);
        rRt.sizeDelta = Vector2.zero;
        Image rBoxImg = rosterBox.AddComponent<Image>();
        rBoxImg.color = new Color(0.08f, 0.08f, 0.14f, 0.88f);

        // Roster Title
        GameObject rTitleGo = CreateUIElement("RosterTitle", rosterBox.transform);
        RectTransform rtRt = rTitleGo.GetComponent<RectTransform>();
        rtRt.anchorMin = new Vector2(0.5f, 0.92f);
        rtRt.anchorMax = new Vector2(0.5f, 0.92f);
        rtRt.sizeDelta = new Vector2(400, 30);
        Text rtTxt = rTitleGo.AddComponent<Text>();
        rtTxt.font = font;
        rtTxt.text = "FIGHTER ROSTER";
        rtTxt.fontSize = 20;
        rtTxt.alignment = TextAnchor.MiddleCenter;
        rtTxt.color = new Color(0.9f, 0.9f, 1f);
        rtTxt.fontStyle = FontStyle.Bold;

        // Grid of 8 Character Buttons
        GameObject gridGo = CreateUIElement("RosterGrid", rosterBox.transform);
        RectTransform gRt = gridGo.GetComponent<RectTransform>();
        gRt.anchorMin = new Vector2(0.05f, 0.08f);
        gRt.anchorMax = new Vector2(0.95f, 0.85f);
        gRt.sizeDelta = Vector2.zero;
        GridLayoutGroup glg = gridGo.AddComponent<GridLayoutGroup>();
        glg.cellSize = new Vector2(160, 95);
        glg.spacing = new Vector2(14, 14);
        glg.childAlignment = TextAnchor.MiddleCenter;

        string[] fighterNames = { "VEGETA (SSJ)", "GOKU (SSJ)", "FUTURE TRUNKS", "GOHAN (SSJ2)", "PERFECT CELL", "FRIEZA (FINAL)", "ANDROID 17", "ANDROID 18" };
        Color[] fighterColors = {
            new Color(0.9f, 0.8f, 0.1f), new Color(1f, 0.5f, 0.1f), new Color(0.4f, 0.7f, 1f), new Color(1f, 0.9f, 0.2f),
            new Color(0.2f, 0.8f, 0.3f), new Color(0.8f, 0.3f, 1f), new Color(0.1f, 0.8f, 0.9f), new Color(1f, 0.4f, 0.7f)
        };

        for (int i = 0; i < fighterNames.Length; i++)
        {
            int index = i;
            Button b = CreateStyledButton($"FighterBtn_{i}", gridGo.transform, fighterNames[i], fighterColors[i], font, 85);
            b.onClick.AddListener(() => csUI.SelectP1(index));
        }

        // Bottom Bar Buttons
        Button startBtn = CreateStyledButton("StartBattleBtn", canvasGo.transform, "⚡ START BATTLE! ⚡", new Color(1f, 0.65f, 0.1f), font, 52);
        RectTransform sbRt = startBtn.GetComponent<RectTransform>();
        sbRt.anchorMin = new Vector2(0.5f, 0.08f);
        sbRt.anchorMax = new Vector2(0.5f, 0.08f);
        sbRt.sizeDelta = new Vector2(360, 54);

        Button backBtn = CreateStyledButton("BackBtn", canvasGo.transform, "◀ BACK TO MENU", new Color(0.5f, 0.5f, 0.6f), font, 40);
        RectTransform bbRt = backBtn.GetComponent<RectTransform>();
        bbRt.anchorMin = new Vector2(0.12f, 0.08f);
        bbRt.anchorMax = new Vector2(0.12f, 0.08f);
        bbRt.sizeDelta = new Vector2(200, 44);

        // Bind Serialized Fields on CharacterSelectUI
        SerializedObject so = new SerializedObject(csUI);
        so.FindProperty("p1NameText").objectReferenceValue = p1Name;
        so.FindProperty("p1TitleText").objectReferenceValue = p1Title;
        so.FindProperty("p1PortraitImage").objectReferenceValue = p1Portrait;
        so.FindProperty("p1StatsText").objectReferenceValue = p1Stats;
        so.FindProperty("p1SpecialsText").objectReferenceValue = p1Specials;
        so.FindProperty("p1StatusBadge").objectReferenceValue = p1Status;

        so.FindProperty("p2NameText").objectReferenceValue = p2Name;
        so.FindProperty("p2TitleText").objectReferenceValue = p2Title;
        so.FindProperty("p2PortraitImage").objectReferenceValue = p2Portrait;
        so.FindProperty("p2StatsText").objectReferenceValue = p2Stats;
        so.FindProperty("p2SpecialsText").objectReferenceValue = p2Specials;
        so.FindProperty("p2StatusBadge").objectReferenceValue = p2Status;

        so.FindProperty("startBattleButton").objectReferenceValue = startBtn;
        so.FindProperty("backButton").objectReferenceValue = backBtn;
        so.ApplyModifiedProperties();

        EditorSceneManager.SaveScene(scene, CHAR_SELECT_PATH);
        Debug.Log("[SCENE GENERATOR] Đã tạo CharacterSelectScene tại " + CHAR_SELECT_PATH);
    }

    private static GameObject CreatePlayerCornerPanel(string name, Transform parent, Vector2 minAnchor, Vector2 maxAnchor,
        Color headerColor, string headerTitle, Font font, Sprite portraitSprite,
        out Text nameTxt, out Text titleTxt, out Image portraitImg, out Text statsTxt, out Text specialsTxt, out Text statusTxt)
    {
        GameObject panel = CreateUIElement(name, parent);
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = minAnchor;
        rt.anchorMax = maxAnchor;
        rt.sizeDelta = Vector2.zero;

        Image panelImg = panel.AddComponent<Image>();
        panelImg.color = new Color(0.08f, 0.08f, 0.14f, 0.9f);

        // Header
        GameObject hGo = CreateUIElement("Header", panel.transform);
        RectTransform hRt = hGo.GetComponent<RectTransform>();
        hRt.anchorMin = new Vector2(0f, 0.9f);
        hRt.anchorMax = new Vector2(1f, 1f);
        hRt.sizeDelta = Vector2.zero;
        Image hImg = hGo.AddComponent<Image>();
        hImg.color = headerColor;

        GameObject hLblGo = CreateUIElement("Title", hGo.transform);
        StretchFull(hLblGo.GetComponent<RectTransform>());
        Text hLbl = hLblGo.AddComponent<Text>();
        hLbl.font = font;
        hLbl.text = headerTitle;
        hLbl.fontSize = 20;
        hLbl.alignment = TextAnchor.MiddleCenter;
        hLbl.color = Color.white;
        hLbl.fontStyle = FontStyle.Bold;

        // Portrait Frame
        GameObject pGo = CreateUIElement("Portrait", panel.transform);
        RectTransform pRt = pGo.GetComponent<RectTransform>();
        pRt.anchorMin = new Vector2(0.5f, 0.65f);
        pRt.anchorMax = new Vector2(0.5f, 0.65f);
        pRt.sizeDelta = new Vector2(150, 150);
        portraitImg = pGo.AddComponent<Image>();
        if (portraitSprite != null) portraitImg.sprite = portraitSprite;
        portraitImg.color = Color.white;
        portraitImg.preserveAspect = true;

        // Name
        GameObject nGo = CreateUIElement("Name", panel.transform);
        RectTransform nRt = nGo.GetComponent<RectTransform>();
        nRt.anchorMin = new Vector2(0.05f, 0.44f);
        nRt.anchorMax = new Vector2(0.95f, 0.52f);
        nRt.sizeDelta = Vector2.zero;
        nameTxt = nGo.AddComponent<Text>();
        nameTxt.font = font;
        nameTxt.text = "VEGETA (SSJ)";
        nameTxt.fontSize = 20;
        nameTxt.alignment = TextAnchor.MiddleCenter;
        nameTxt.color = new Color(1f, 0.9f, 0.3f);
        nameTxt.fontStyle = FontStyle.Bold;

        // Title/Subtitle
        GameObject tGo = CreateUIElement("TitleDesc", panel.transform);
        RectTransform tRt = tGo.GetComponent<RectTransform>();
        tRt.anchorMin = new Vector2(0.05f, 0.38f);
        tRt.anchorMax = new Vector2(0.95f, 0.44f);
        tRt.sizeDelta = Vector2.zero;
        titleTxt = tGo.AddComponent<Text>();
        titleTxt.font = font;
        titleTxt.text = "Prince of All Saiyans";
        titleTxt.fontSize = 14;
        titleTxt.alignment = TextAnchor.MiddleCenter;
        titleTxt.color = new Color(0.8f, 0.8f, 0.9f);
        titleTxt.fontStyle = FontStyle.Italic;

        // Stats
        GameObject sGo = CreateUIElement("Stats", panel.transform);
        RectTransform sRt = sGo.GetComponent<RectTransform>();
        sRt.anchorMin = new Vector2(0.05f, 0.28f);
        sRt.anchorMax = new Vector2(0.95f, 0.36f);
        sRt.sizeDelta = Vector2.zero;
        statsTxt = sGo.AddComponent<Text>();
        statsTxt.font = font;
        statsTxt.text = "POWER: 90 • SPEED: 92 • KI: 95";
        statsTxt.fontSize = 13;
        statsTxt.alignment = TextAnchor.MiddleCenter;
        statsTxt.color = new Color(0.9f, 0.9f, 0.9f);

        // Specials
        GameObject spGo = CreateUIElement("Specials", panel.transform);
        RectTransform spRt = spGo.GetComponent<RectTransform>();
        spRt.anchorMin = new Vector2(0.08f, 0.12f);
        spRt.anchorMax = new Vector2(0.92f, 0.26f);
        spRt.sizeDelta = Vector2.zero;
        specialsTxt = spGo.AddComponent<Text>();
        specialsTxt.font = font;
        specialsTxt.text = "• Final Flash\n• Galick Gun\n• Big Bang Attack";
        specialsTxt.fontSize = 12;
        specialsTxt.alignment = TextAnchor.UpperLeft;
        specialsTxt.color = new Color(0.7f, 0.85f, 1f);

        // Status Badge
        GameObject stGo = CreateUIElement("StatusBadge", panel.transform);
        RectTransform stRt = stGo.GetComponent<RectTransform>();
        stRt.anchorMin = new Vector2(0.1f, 0.03f);
        stRt.anchorMax = new Vector2(0.9f, 0.09f);
        stRt.sizeDelta = Vector2.zero;
        statusTxt = stGo.AddComponent<Text>();
        statusTxt.font = font;
        statusTxt.text = "[ READY ]";
        statusTxt.fontSize = 16;
        statusTxt.alignment = TextAnchor.MiddleCenter;
        statusTxt.color = new Color(0.3f, 1f, 0.4f);
        statusTxt.fontStyle = FontStyle.Bold;

        return panel;
    }
    #endregion

    #region 4. Post Match Scene
    private static void GeneratePostMatchScene(Sprite bgSprite, Sprite avatarSprite, Sprite winnerSprite, Font font)
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        GameObject camGo = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
        Camera cam = camGo.GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.05f, 0.04f, 0.09f);
        cam.orthographic = true;
        camGo.transform.position = new Vector3(0, 0, -10);

        CreateEventSystem();

        GameObject canvasGo = CreateCanvas("PostMatchCanvas");
        var postMatchUI = canvasGo.AddComponent<PostMatchUI>();

        // Background
        GameObject bgGo = CreateUIElement("Background", canvasGo.transform);
        StretchFull(bgGo.GetComponent<RectTransform>());
        Image bgImg = bgGo.AddComponent<Image>();
        if (bgSprite != null)
        {
            bgImg.sprite = bgSprite;
            bgImg.color = new Color(0.25f, 0.25f, 0.4f, 1f);
        }
        else
        {
            bgImg.color = new Color(0.07f, 0.06f, 0.12f);
        }

        // Dark Vignette
        GameObject vigGo = CreateUIElement("Vignette", canvasGo.transform);
        StretchFull(vigGo.GetComponent<RectTransform>());
        Image vigImg = vigGo.AddComponent<Image>();
        vigImg.color = new Color(0, 0, 0, 0.55f);

        // Header VICTORY!
        GameObject headGo = CreateUIElement("VictoryTitle", canvasGo.transform);
        RectTransform hRt = headGo.GetComponent<RectTransform>();
        hRt.anchorMin = new Vector2(0.5f, 0.82f);
        hRt.anchorMax = new Vector2(0.5f, 0.94f);
        hRt.sizeDelta = new Vector2(800, 80);
        Text hTxt = headGo.AddComponent<Text>();
        hTxt.font = font;
        hTxt.text = "VICTORY!";
        hTxt.fontSize = 58;
        hTxt.alignment = TextAnchor.MiddleCenter;
        hTxt.color = new Color(1f, 0.85f, 0.15f);
        hTxt.fontStyle = FontStyle.Bold;

        // Subheader Victory Condition
        GameObject condGo = CreateUIElement("VictoryCondition", canvasGo.transform);
        RectTransform cRt = condGo.GetComponent<RectTransform>();
        cRt.anchorMin = new Vector2(0.5f, 0.77f);
        cRt.anchorMax = new Vector2(0.5f, 0.82f);
        cRt.sizeDelta = new Vector2(600, 35);
        Text condTxt = condGo.AddComponent<Text>();
        condTxt.font = font;
        condTxt.text = "DECISIVE K.O. KNOCKOUT!";
        condTxt.fontSize = 20;
        condTxt.alignment = TextAnchor.MiddleCenter;
        condTxt.color = new Color(0f, 0.9f, 1f);
        condTxt.fontStyle = FontStyle.Bold;

        // Winner Spotlight Card
        GameObject cardGo = CreateUIElement("WinnerCard", canvasGo.transform);
        RectTransform cardRt = cardGo.GetComponent<RectTransform>();
        cardRt.anchorMin = new Vector2(0.25f, 0.28f);
        cardRt.anchorMax = new Vector2(0.75f, 0.74f);
        cardRt.sizeDelta = Vector2.zero;
        Image cardImg = cardGo.AddComponent<Image>();
        cardImg.color = new Color(0.1f, 0.1f, 0.18f, 0.92f);

        // Winner Portrait
        GameObject pGo = CreateUIElement("WinnerPortrait", cardGo.transform);
        RectTransform pRt = pGo.GetComponent<RectTransform>();
        pRt.anchorMin = new Vector2(0.5f, 0.65f);
        pRt.anchorMax = new Vector2(0.5f, 0.65f);
        pRt.sizeDelta = new Vector2(170, 170);
        Image pImg = pGo.AddComponent<Image>();
        if (avatarSprite != null) pImg.sprite = avatarSprite;
        pImg.preserveAspect = true;

        // Winner Name
        GameObject wnGo = CreateUIElement("WinnerName", cardGo.transform);
        RectTransform wnRt = wnGo.GetComponent<RectTransform>();
        wnRt.anchorMin = new Vector2(0.05f, 0.36f);
        wnRt.anchorMax = new Vector2(0.95f, 0.46f);
        wnRt.sizeDelta = Vector2.zero;
        Text wnTxt = wnGo.AddComponent<Text>();
        wnTxt.font = font;
        wnTxt.text = "PLAYER 1 • VEGETA (SUPER SAIYAN)";
        wnTxt.fontSize = 26;
        wnTxt.alignment = TextAnchor.MiddleCenter;
        wnTxt.color = new Color(1f, 0.9f, 0.2f);
        wnTxt.fontStyle = FontStyle.Bold;

        // Rank Badge
        GameObject rankGo = CreateUIElement("RankBadge", cardGo.transform);
        RectTransform rankRt = rankGo.GetComponent<RectTransform>();
        rankRt.anchorMin = new Vector2(0.5f, 0.28f);
        rankRt.anchorMax = new Vector2(0.5f, 0.28f);
        rankRt.sizeDelta = new Vector2(400, 35);
        Text rankTxt = rankGo.AddComponent<Text>();
        rankTxt.font = font;
        rankTxt.text = "★ S RANK - SAIYAN PRIDE! ★";
        rankTxt.fontSize = 20;
        rankTxt.alignment = TextAnchor.MiddleCenter;
        rankTxt.color = new Color(1f, 0.6f, 0.1f);
        rankTxt.fontStyle = FontStyle.Bold;

        // Match Stats Row
        GameObject statsRow = CreateUIElement("StatsRow", cardGo.transform);
        RectTransform srRt = statsRow.GetComponent<RectTransform>();
        srRt.anchorMin = new Vector2(0.05f, 0.05f);
        srRt.anchorMax = new Vector2(0.95f, 0.22f);
        srRt.sizeDelta = Vector2.zero;
        HorizontalLayoutGroup hlg = statsRow.AddComponent<HorizontalLayoutGroup>();
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.spacing = 30f;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;

        GameObject hpGo = CreateUIElement("HpStat", statsRow.transform);
        Text hpTxt = hpGo.AddComponent<Text>();
        hpTxt.font = font;
        hpTxt.text = "HEALTH: 85 HP";
        hpTxt.fontSize = 15;
        hpTxt.alignment = TextAnchor.MiddleCenter;
        hpTxt.color = new Color(0.4f, 1f, 0.5f);

        GameObject timeGo = CreateUIElement("TimeStat", statsRow.transform);
        Text timeTxt = timeGo.AddComponent<Text>();
        timeTxt.font = font;
        timeTxt.text = "TIME: 38.5s";
        timeTxt.fontSize = 15;
        timeTxt.alignment = TextAnchor.MiddleCenter;
        timeTxt.color = Color.white;

        GameObject comboGo = CreateUIElement("ComboStat", statsRow.transform);
        Text comboTxt = comboGo.AddComponent<Text>();
        comboTxt.font = font;
        comboTxt.text = "MAX COMBO: 12 HITS";
        comboTxt.fontSize = 15;
        comboTxt.alignment = TextAnchor.MiddleCenter;
        comboTxt.color = new Color(1f, 0.8f, 0.3f);

        // Action Buttons Row (Bottom)
        GameObject btnRow = CreateUIElement("ButtonRow", canvasGo.transform);
        RectTransform bRt = btnRow.GetComponent<RectTransform>();
        bRt.anchorMin = new Vector2(0.15f, 0.08f);
        bRt.anchorMax = new Vector2(0.85f, 0.2f);
        bRt.sizeDelta = Vector2.zero;
        HorizontalLayoutGroup bHlg = btnRow.AddComponent<HorizontalLayoutGroup>();
        bHlg.childAlignment = TextAnchor.MiddleCenter;
        bHlg.spacing = 30f;
        bHlg.childControlWidth = true;
        bHlg.childControlHeight = true;

        Button rematchBtn = CreateStyledButton("RematchBtn", btnRow.transform, "🔄 REMATCH (R)", new Color(1f, 0.55f, 0.1f), font, 52);
        Button selectBtn = CreateStyledButton("SelectBtn", btnRow.transform, "👥 FIGHTERS (C)", new Color(0.2f, 0.6f, 1f), font, 52);
        Button menuBtn = CreateStyledButton("MenuBtn", btnRow.transform, "🏠 MAIN MENU (M)", new Color(0.5f, 0.5f, 0.6f), font, 52);

        // Bind Serialized Fields on PostMatchUI
        SerializedObject so = new SerializedObject(postMatchUI);
        so.FindProperty("winnerTitleText").objectReferenceValue = hTxt;
        so.FindProperty("winnerNameText").objectReferenceValue = wnTxt;
        so.FindProperty("winnerPortraitImage").objectReferenceValue = pImg;
        so.FindProperty("victoryConditionText").objectReferenceValue = condTxt;
        so.FindProperty("performanceRankText").objectReferenceValue = rankTxt;
        so.FindProperty("matchDurationText").objectReferenceValue = timeTxt;
        so.FindProperty("remainingHpText").objectReferenceValue = hpTxt;
        so.FindProperty("comboScoreText").objectReferenceValue = comboTxt;
        so.FindProperty("rematchButton").objectReferenceValue = rematchBtn;
        so.FindProperty("selectFighterButton").objectReferenceValue = selectBtn;
        so.FindProperty("mainMenuButton").objectReferenceValue = menuBtn;
        so.ApplyModifiedProperties();

        EditorSceneManager.SaveScene(scene, POST_MATCH_PATH);
        Debug.Log("[SCENE GENERATOR] Đã tạo PostMatchScene tại " + POST_MATCH_PATH);
    }
    #endregion

    #region 5. Build Settings Configuration
    public static void SetupBuildSettings()
    {
        var scenes = new List<EditorBuildSettingsScene>
        {
            new EditorBuildSettingsScene(SPLASH_PATH, true),
            new EditorBuildSettingsScene(MAIN_MENU_PATH, true),
            new EditorBuildSettingsScene(CHAR_SELECT_PATH, true),
            new EditorBuildSettingsScene(BATTLE_PATH, true),
            new EditorBuildSettingsScene(POST_MATCH_PATH, true)
        };

        EditorBuildSettings.scenes = scenes.ToArray();
        Debug.Log("[SCENE GENERATOR] Đã cấu hình Build Settings với 5 scene theo đúng thứ tự.");
    }
    #endregion

    #region Helpers
    private static GameObject CreateCanvas(string name)
    {
        GameObject canvasGo = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas c = canvasGo.GetComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler cs = canvasGo.GetComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1920, 1080);
        cs.matchWidthOrHeight = 0.5f;

        return canvasGo;
    }

    private static void CreateEventSystem()
    {
        if (Object.FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
        }
    }

    private static GameObject CreateUIElement(string name, Transform parent)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go;
    }

    private static void StretchFull(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;
    }

    private static Button CreateStyledButton(string name, Transform parent, string label, Color baseColor, Font font, float height = 50f)
    {
        GameObject btnGo = CreateUIElement(name, parent);
        btnGo.GetComponent<RectTransform>().sizeDelta = new Vector2(280, height);

        Image img = btnGo.AddComponent<Image>();
        img.color = baseColor;

        Button btn = btnGo.AddComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.normalColor = baseColor;
        cb.highlightedColor = Color.Lerp(baseColor, Color.white, 0.35f);
        cb.pressedColor = Color.Lerp(baseColor, Color.black, 0.25f);
        cb.selectedColor = cb.highlightedColor;
        btn.colors = cb;

        GameObject lblGo = CreateUIElement("Text", btnGo.transform);
        StretchFull(lblGo.GetComponent<RectTransform>());
        Text lbl = lblGo.AddComponent<Text>();
        lbl.font = font;
        lbl.text = label;
        lbl.fontSize = 20;
        lbl.alignment = TextAnchor.MiddleCenter;
        lbl.color = Color.white;
        lbl.fontStyle = FontStyle.Bold;

        return btn;
    }
    #endregion
}
