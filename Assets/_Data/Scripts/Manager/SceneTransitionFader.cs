using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionFader : MonoBehaviour
{
    private static SceneTransitionFader instance;
    public static SceneTransitionFader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SceneTransitionFader>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SceneTransitionFader");
                    instance = go.AddComponent<SceneTransitionFader>();
                }
            }
            return instance;
        }
    }

    private Canvas faderCanvas;
    private Image faderImage;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeFader();
    }

    private void InitializeFader()
    {
        if (this.faderCanvas != null) return;

        this.faderCanvas = gameObject.AddComponent<Canvas>();
        this.faderCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        this.faderCanvas.sortingOrder = 9999;

        gameObject.AddComponent<CanvasScaler>();

        GameObject imgGo = new GameObject("FaderImage");
        imgGo.transform.SetParent(this.transform, false);
        RectTransform rt = imgGo.AddComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;

        this.faderImage = imgGo.AddComponent<Image>();
        this.faderImage.color = new Color(0, 0, 0, 0);
        this.faderImage.raycastTarget = false;
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        InitializeFader();
        this.faderImage.raycastTarget = true;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            this.faderImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
        this.faderImage.color = new Color(0, 0, 0, 1);
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        InitializeFader();
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            this.faderImage.color = new Color(0, 0, 0, 1f - t);
            yield return null;
        }
        this.faderImage.color = new Color(0, 0, 0, 0);
        this.faderImage.raycastTarget = false;
    }
}
