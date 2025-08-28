using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    public bool isFadeOut;
    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }
    
    protected void Update()
    {
        if (this.canvasGroup.alpha == 0) this.isFadeOut = true;
    }
    public void FadeOut()
    {
        StartCoroutine(FadeCanvas(0));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvas(1));
    }

    private IEnumerator FadeCanvas(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;

        // nếu fade xong về 0 thì disable luôn
        if (targetAlpha == 0)
            canvasGroup.gameObject.SetActive(false);
    }
}
