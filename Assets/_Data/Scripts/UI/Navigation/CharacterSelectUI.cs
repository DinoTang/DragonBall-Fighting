using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [System.Serializable]
    public class FighterEntry
    {
        public CharacterEnum characterEnum;
        public string displayName;
        public string title;
        public int power;
        public int speed;
        public int energy;
        public string specialMoves;
        public Color themeColor = Color.yellow;
        public Sprite portrait;
    }

    [Header("Fighter Roster")]
    [SerializeField] private List<FighterEntry> roster = new();

    [Header("P1 Display (Left / Red)")]
    [SerializeField] private Text p1NameText;
    [SerializeField] private Text p1TitleText;
    [SerializeField] private Image p1PortraitImage;
    [SerializeField] private Text p1StatsText;
    [SerializeField] private Text p1SpecialsText;
    [SerializeField] private Text p1StatusBadge;

    [Header("P2 Display (Right / Blue)")]
    [SerializeField] private Text p2NameText;
    [SerializeField] private Text p2TitleText;
    [SerializeField] private Image p2PortraitImage;
    [SerializeField] private Text p2StatsText;
    [SerializeField] private Text p2SpecialsText;
    [SerializeField] private Text p2StatusBadge;

    [Header("Navigation Buttons")]
    [SerializeField] private Button startBattleButton;
    [SerializeField] private Button backButton;

    [Header("Roster Grid")]
    [SerializeField] private Transform rosterContainer;
    [SerializeField] private GameObject rosterCardPrefab;

    private int p1SelectedIndex = 0;
    private int p2SelectedIndex = 0;

    private void Awake()
    {
        InitializeRosterIfEmpty();
    }

    private void Start()
    {
        if (startBattleButton != null) startBattleButton.onClick.AddListener(OnStartBattleClicked);
        if (backButton != null) backButton.onClick.AddListener(OnBackClicked);

        UpdateDisplays();
    }

    private void InitializeRosterIfEmpty()
    {
        if (roster.Count > 0) return;

        Sprite defaultPortrait = Resources.Load<CharacterDataSO>("CharacterData/VegetaDataSO")?.portrait;

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.Vegeta,
            displayName = "VEGETA (SSJ)",
            title = "Prince of All Saiyans",
            power = 90,
            speed = 92,
            energy = 95,
            specialMoves = "• Final Flash (Ultimate Beam)\n• Galick Gun (Straight Beam)\n• Big Bang Attack (Charged Blast)",
            themeColor = new Color(1f, 0.85f, 0.2f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.Goku,
            displayName = "GOKU (SSJ)",
            title = "Legendary Super Saiyan",
            power = 95,
            speed = 88,
            energy = 90,
            specialMoves = "• Super Kamehameha\n• Spirit Bomb (Genki Dama)\n• Instant Transmission Strike",
            themeColor = new Color(1f, 0.5f, 0.1f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.Trunk,
            displayName = "FUTURE TRUNKS",
            title = "Warrior of Hope",
            power = 85,
            speed = 94,
            energy = 88,
            specialMoves = "• Burning Attack\n• Shining Sword Slash\n• Heat Dome Attack",
            themeColor = new Color(0.6f, 0.7f, 1f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.None,
            displayName = "GOHAN (SSJ2)",
            title = "Unleashed Potential",
            power = 96,
            speed = 90,
            energy = 92,
            specialMoves = "• Father-Son Kamehameha\n• Super Masenko\n• Dragon Flight Combo",
            themeColor = new Color(1f, 0.9f, 0.3f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.Cell,
            displayName = "PERFECT CELL",
            title = "The Ultimate Lifeform",
            power = 92,
            speed = 89,
            energy = 94,
            specialMoves = "• Solar Kamehameha\n• Perfect Barrier\n• Death Beam Barrage",
            themeColor = new Color(0.3f, 0.9f, 0.4f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.None,
            displayName = "FRIEZA (FINAL)",
            title = "Emperor of the Universe",
            power = 88,
            speed = 95,
            energy = 96,
            specialMoves = "• Death Ball\n• Death Beam\n• Emperor's Nova",
            themeColor = new Color(0.8f, 0.4f, 1f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.Android_17,
            displayName = "ANDROID 17",
            title = "Cybernetic Ranger",
            power = 86,
            speed = 96,
            energy = 100,
            specialMoves = "• Android Barrier\n• Accel Dance\n• Photon Strike",
            themeColor = new Color(0.2f, 0.8f, 0.9f),
            portrait = defaultPortrait
        });

        roster.Add(new FighterEntry
        {
            characterEnum = CharacterEnum.Android_18,
            displayName = "ANDROID 18",
            title = "Deadly Elegance",
            power = 85,
            speed = 95,
            energy = 100,
            specialMoves = "• Destructo Disc (Kienzan)\n• Infinity Bullet\n• Sadistic 18 Combo",
            themeColor = new Color(1f, 0.4f, 0.7f),
            portrait = defaultPortrait
        });
    }

    private void Update()
    {
        // Keyboard navigation for P1 (A / D or Left / Right)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            p1SelectedIndex = (p1SelectedIndex - 1 + roster.Count) % roster.Count;
            UpdateDisplays();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            p1SelectedIndex = (p1SelectedIndex + 1) % roster.Count;
            UpdateDisplays();
        }

        // Keyboard navigation for P2 (Up / Down)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            p2SelectedIndex = (p2SelectedIndex - 1 + roster.Count) % roster.Count;
            UpdateDisplays();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            p2SelectedIndex = (p2SelectedIndex + 1) % roster.Count;
            UpdateDisplays();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            OnStartBattleClicked();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackClicked();
        }
    }

    public void SelectP1(int index)
    {
        if (index >= 0 && index < roster.Count)
        {
            p1SelectedIndex = index;
            UpdateDisplays();
        }
    }

    public void SelectP2(int index)
    {
        if (index >= 0 && index < roster.Count)
        {
            p2SelectedIndex = index;
            UpdateDisplays();
        }
    }

    private void UpdateDisplays()
    {
        if (roster.Count == 0) return;

        FighterEntry p1 = roster[p1SelectedIndex];
        FighterEntry p2 = roster[p2SelectedIndex];

        // P1 display
        if (p1NameText != null) p1NameText.text = p1.displayName;
        if (p1TitleText != null) p1TitleText.text = p1.title;
        if (p1PortraitImage != null && p1.portrait != null) p1PortraitImage.sprite = p1.portrait;
        if (p1StatsText != null) p1StatsText.text = $"POWER: {p1.power}  •  SPEED: {p1.speed}  •  KI: {p1.energy}";
        if (p1SpecialsText != null) p1SpecialsText.text = p1.specialMoves;
        if (p1StatusBadge != null) p1StatusBadge.text = "[ READY ]";

        // P2 display
        if (p2NameText != null) p2NameText.text = p2.displayName;
        if (p2TitleText != null) p2TitleText.text = p2.title;
        if (p2PortraitImage != null && p2.portrait != null) p2PortraitImage.sprite = p2.portrait;
        if (p2StatsText != null) p2StatsText.text = $"POWER: {p2.power}  •  SPEED: {p2.speed}  •  KI: {p2.energy}";
        if (p2SpecialsText != null) p2SpecialsText.text = p2.specialMoves;
        if (p2StatusBadge != null) p2StatusBadge.text = "[ READY ]";
    }

    public void OnStartBattleClicked()
    {
        if (roster.Count == 0) return;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Fight);
        }

        CharacterEnum red = roster[p1SelectedIndex].characterEnum;
        CharacterEnum blue = roster[p2SelectedIndex].characterEnum;

        // If None, default to Vegeta
        if (red == CharacterEnum.None) red = CharacterEnum.Vegeta;
        if (blue == CharacterEnum.None) blue = CharacterEnum.Vegeta;

        GameSession.Instance.StartBattle(red, blue);
    }

    public void OnBackClicked()
    {
        GameSession.Instance.GoToMainMenu();
    }
}
