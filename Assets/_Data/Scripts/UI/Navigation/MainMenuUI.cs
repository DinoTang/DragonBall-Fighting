using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button arcadeButton;
    [SerializeField] private Button trainingButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [Header("Settings Modal")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle fpsToggle;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button closeSettingsButton;

    private void Start()
    {
        if (arcadeButton != null) arcadeButton.onClick.AddListener(OnArcadeClicked);
        if (trainingButton != null) trainingButton.onClick.AddListener(OnTrainingClicked);
        if (settingsButton != null) settingsButton.onClick.AddListener(OnSettingsClicked);
        if (quitButton != null) quitButton.onClick.AddListener(OnQuitClicked);
        if (closeSettingsButton != null) closeSettingsButton.onClick.AddListener(OnCloseSettingsClicked);

        if (settingsPanel != null) settingsPanel.SetActive(false);

        // Bind initial settings values
        if (bgmSlider != null)
        {
            bgmSlider.value = GameSession.Instance.BgmVolume;
            bgmSlider.onValueChanged.AddListener(val => GameSession.Instance.BgmVolume = val);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = GameSession.Instance.SfxVolume;
            sfxSlider.onValueChanged.AddListener(val => GameSession.Instance.SfxVolume = val);
        }
        if (fpsToggle != null)
        {
            fpsToggle.isOn = (Application.targetFrameRate == 60);
            fpsToggle.onValueChanged.AddListener(val =>
            {
                Application.targetFrameRate = val ? 60 : -1;
                QualitySettings.vSyncCount = val ? 0 : 0;
            });
        }
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(val => Screen.fullScreen = val);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel != null && settingsPanel.activeSelf)
            {
                OnCloseSettingsClicked();
            }
        }
    }

    public void OnArcadeClicked()
    {
        PlaySound();
        GameSession.Instance.GoToCharacterSelect(GameSession.GameModeType.Versus);
    }

    public void OnTrainingClicked()
    {
        PlaySound();
        GameSession.Instance.GoToCharacterSelect(GameSession.GameModeType.Practice);
    }

    public void OnSettingsClicked()
    {
        PlaySound();
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void OnCloseSettingsClicked()
    {
        PlaySound();
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OnQuitClicked()
    {
        PlaySound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void PlaySound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Ready);
        }
    }
}
