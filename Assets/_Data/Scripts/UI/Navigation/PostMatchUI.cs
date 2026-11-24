using UnityEngine;
using UnityEngine.UI;

public class PostMatchUI : MonoBehaviour
{
    [Header("Winner Display")]
    [SerializeField] private Text winnerTitleText;
    [SerializeField] private Text winnerNameText;
    [SerializeField] private Image winnerPortraitImage;
    [SerializeField] private Text victoryConditionText;
    [SerializeField] private Text performanceRankText;

    [Header("Match Statistics")]
    [SerializeField] private Text matchDurationText;
    [SerializeField] private Text remainingHpText;
    [SerializeField] private Text comboScoreText;

    [Header("Action Buttons")]
    [SerializeField] private Button rematchButton;
    [SerializeField] private Button selectFighterButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        if (rematchButton != null) rematchButton.onClick.AddListener(OnRematchClicked);
        if (selectFighterButton != null) selectFighterButton.onClick.AddListener(OnSelectFighterClicked);
        if (mainMenuButton != null) mainMenuButton.onClick.AddListener(OnMainMenuClicked);

        PopulateResults();
    }

    private void PopulateResults()
    {
        var result = GameSession.Instance.LastResult;

        if (winnerTitleText != null)
        {
            winnerTitleText.text = result.IsTimeOver ? "TIME OVER!" : "VICTORY!";
        }

        if (winnerNameText != null)
        {
            winnerNameText.text = $"{result.WinnerTeam} • {result.WinnerDisplayName}";
        }

        if (victoryConditionText != null)
        {
            victoryConditionText.text = result.IsTimeOver ? "DECISION BY HEALTH TIMEOUT" : "DECISIVE K.O. KNOCKOUT!";
        }

        if (performanceRankText != null)
        {
            performanceRankText.text = string.IsNullOrEmpty(result.PerformanceRank) ? "S RANK" : result.PerformanceRank;
        }

        if (winnerPortraitImage != null)
        {
            if (result.WinnerPortrait != null)
            {
                winnerPortraitImage.sprite = result.WinnerPortrait;
            }
            else
            {
                var defaultPortrait = Resources.Load<CharacterDataSO>("CharacterData/VegetaDataSO")?.portrait;
                if (defaultPortrait != null) winnerPortraitImage.sprite = defaultPortrait;
            }
        }

        if (matchDurationText != null)
        {
            matchDurationText.text = $"TIME: {result.MatchDuration:F1}s";
        }

        if (remainingHpText != null)
        {
            float hp = (result.WinnerTeam == "PLAYER 1") ? result.RedHP : result.BlueHP;
            remainingHpText.text = $"HEALTH REMAINING: {hp:F0} HP";
        }

        if (comboScoreText != null)
        {
            comboScoreText.text = $"MAX COMBO: {result.MaxCombo} HITS";
        }

        // Play Win Sound
        if (AudioManager.Instance != null)
        {
            if (result.WinnerTeam == "PLAYER 1")
                AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.RedWin);
            else
                AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.BlueWin);
        }
    }

    private void Update()
    {
        // Keyboard shortcuts
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRematchClicked();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            OnSelectFighterClicked();
        }
        else if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
        {
            OnMainMenuClicked();
        }
    }

    public void OnRematchClicked()
    {
        GameSession.Instance.Rematch();
    }

    public void OnSelectFighterClicked()
    {
        GameSession.Instance.GoToCharacterSelect();
    }

    public void OnMainMenuClicked()
    {
        GameSession.Instance.GoToMainMenu();
    }
}
