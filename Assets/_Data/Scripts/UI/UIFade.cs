using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIFade : UICtrl
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvasGroup();
    }
    protected override void Start()
    {
        base.Start();
        this.StartFadeOut();
    }
    protected void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log(transform.name + ": LoadCanvasGroup");
    }
    public void StartFadeOut()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(true);
        StartCoroutine(FadeOutCoroutine());
    }

    private System.Collections.IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(1f);
        float elapsed = 0f;
        float startAlpha = this.canvasGroup.alpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            this.canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        this.canvasGroup.alpha = 0f; // đảm bảo tắt hẳn
        gameObject.SetActive(false); // tuỳ chọn: ẩn luôn object khi fade xong
    }
}
