using UnityEngine;
using UnityEngine.UI;

public class SplashScreenUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Text promptText;
    [SerializeField] private Text titleText;

    private bool hasStarted = false;

    private void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartClicked);
        }
    }

    private void Update()
    {
        if (hasStarted) return;

        // Smooth breathing pulse effect on the start prompt
        if (promptText != null)
        {
            float alpha = Mathf.PingPong(Time.unscaledTime * 1.5f, 0.7f) + 0.3f;
            Color c = promptText.color;
            c.a = alpha;
            promptText.color = c;
        }

        // Any key or click triggers transition
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            OnStartClicked();
        }
    }

    public void OnStartClicked()
    {
        if (hasStarted) return;
        hasStarted = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAnnouncerSound(AnnouncerSoundType.Fight);
        }

        GameSession.Instance.GoToMainMenu();
    }
}
