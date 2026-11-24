using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : BaseBehaviour
{
    private static GameSession instance;
    public static GameSession Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameSession>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameSession");
                    instance = go.AddComponent<GameSession>();
                }
            }
            return instance;
        }
    }

    public enum GameModeType
    {
        Versus,
        Arcade,
        Practice
    }

    [Header("Game Mode")]
    public GameModeType CurrentMode = GameModeType.Versus;

    [Header("Selected Fighters")]
    public CharacterEnum RedSelectedCharacter = CharacterEnum.Vegeta;
    public CharacterEnum BlueSelectedCharacter = CharacterEnum.Vegeta;
    public CharacterDataSO RedCharacterDataSO;
    public CharacterDataSO BlueCharacterDataSO;
    public string SelectedStage = "Wasteland Arena";

    [Header("Match Result")]
    public MatchResultData LastResult = new MatchResultData();

    [System.Serializable]
    public class MatchResultData
    {
        public bool HasResult = false;
        public CharacterEnum WinnerPlayer = CharacterEnum.Vegeta;
        public string WinnerTeam = "PLAYER 1";
        public string WinnerDisplayName = "VEGETA (SUPER SAIYAN)";
        public Sprite WinnerPortrait;
        public float RedHP = 100f;
        public float BlueHP = 0f;
        public float MatchDuration = 35.5f;
        public bool IsTimeOver = false;
        public int MaxCombo = 12;
        public string PerformanceRank = "S RANK";
    }

    [Header("Settings")]
    public float BgmVolume = 0.8f;
    public float SfxVolume = 1.0f;
    public int TargetFPS = 60;
    public bool IsFullscreen = true;

    // Constants for Scene Names
    public const string SCENE_SPLASH = "SplashScene";
    public const string SCENE_MAIN_MENU = "MainMenuScene";
    public const string SCENE_CHARACTER_SELECT = "CharacterSelectScene";
    public const string SCENE_BATTLE = "SampleScene";
    public const string SCENE_POST_MATCH = "PostMatchScene";

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDefaultData();
    }

    public void LoadDefaultData()
    {
        if (RedCharacterDataSO == null)
        {
            RedCharacterDataSO = Resources.Load<CharacterDataSO>("CharacterData/VegetaDataSO");
        }
        if (BlueCharacterDataSO == null)
        {
            BlueCharacterDataSO = Resources.Load<CharacterDataSO>("CharacterData/VegetaDataSO");
        }
    }

    public void RecordMatchResult(MatchResultData result)
    {
        this.LastResult = result;
        this.LastResult.HasResult = true;
    }

    public void LoadSceneWithFade(string sceneName, float fadeTime = 0.3f)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, fadeTime));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, float fadeTime)
    {
        if (SceneTransitionFader.Instance != null)
        {
            yield return SceneTransitionFader.Instance.FadeOutCoroutine(fadeTime);
        }

        SceneManager.LoadScene(sceneName);

        // Wait one frame for scene activation
        yield return null;

        if (SceneTransitionFader.Instance != null)
        {
            yield return SceneTransitionFader.Instance.FadeInCoroutine(fadeTime);
        }
    }

    public void GoToSplash() => LoadSceneWithFade(SCENE_SPLASH);
    public void GoToMainMenu() => LoadSceneWithFade(SCENE_MAIN_MENU);
    public void GoToCharacterSelect(GameModeType mode = GameModeType.Versus)
    {
        this.CurrentMode = mode;
        LoadSceneWithFade(SCENE_CHARACTER_SELECT);
    }
    public void StartBattle(CharacterEnum redChar, CharacterEnum blueChar)
    {
        this.RedSelectedCharacter = redChar;
        this.BlueSelectedCharacter = blueChar;
        LoadSceneWithFade(SCENE_BATTLE);
    }
    public void GoToPostMatch() => LoadSceneWithFade(SCENE_POST_MATCH);
    public void Rematch() => LoadSceneWithFade(SCENE_BATTLE);
}
