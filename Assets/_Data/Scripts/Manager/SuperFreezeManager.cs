using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFreezeManager : BaseBehaviour
{
    private static SuperFreezeManager instance;
    public static SuperFreezeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SuperFreezeManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SuperFreezeManager");
                    instance = go.AddComponent<SuperFreezeManager>();
                }
            }
            return instance;
        }
    }

    [Header("Visual Settings")]
    [SerializeField] protected Color darkOverlayColor = new Color(0.01f, 0.01f, 0.03f, 0.72f);
    [SerializeField] protected Color spotlightColor = new Color(1f, 0.98f, 0.85f, 0.6f);
    [SerializeField] protected float fadeInDuration = 0.12f;
    [SerializeField] protected float fadeOutDuration = 0.2f;
    [SerializeField] protected int overlaySortingOrder = 30;
    [SerializeField] protected int spotlightSortingOrder = 45;
    [SerializeField] protected int casterSortingOrderBoost = 50;
    [SerializeField] protected Vector3 spotlightScale = new Vector3(6.5f, 6.5f, 1f);

    [Header("Runtime State")]
    [SerializeField] protected bool isFreezing = false;
    public bool IsFreezing => isFreezing;

    protected CharacterCtrl currentCaster;
    public CharacterCtrl CurrentCaster => currentCaster;

    protected CharacterCtrl currentOpponent;
    public CharacterCtrl CurrentOpponent => currentOpponent;

    protected float originalOpponentAnimatorSpeed = 1f;
    protected RigidbodyConstraints2D originalOpponentConstraints = RigidbodyConstraints2D.FreezeRotation;
    protected Vector2 originalOpponentVelocity = Vector2.zero;

    protected readonly Dictionary<SpriteRenderer, int> casterOriginalSortingOrders = new();

    // Visual overlay GameObjects & SpriteRenderers
    protected GameObject overlayObj;
    protected SpriteRenderer overlaySR;
    protected GameObject spotlightObj;
    protected SpriteRenderer spotlightSR;

    protected Coroutine fadeCoroutine;
    protected float freezeTimer = 0f;
    protected const float MAX_FREEZE_DURATION = 10f; // Safety timeout

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        this.InitializeVisuals();
    }

    protected void InitializeVisuals()
    {
        if (this.overlayObj == null)
        {
            this.overlayObj = new GameObject("SuperFreeze_DarkOverlay");
            this.overlayObj.transform.SetParent(this.transform);
            this.overlaySR = this.overlayObj.AddComponent<SpriteRenderer>();
            this.overlaySR.sprite = CreateSolidWhiteSprite();
            this.overlaySR.sortingOrder = this.overlaySortingOrder;
            this.overlaySR.color = new Color(this.darkOverlayColor.r, this.darkOverlayColor.g, this.darkOverlayColor.b, 0f);
            this.overlayObj.SetActive(false);
        }

        if (this.spotlightObj == null)
        {
            this.spotlightObj = new GameObject("SuperFreeze_SpotlightGlow");
            this.spotlightObj.transform.SetParent(this.transform);
            this.spotlightSR = this.spotlightObj.AddComponent<SpriteRenderer>();
            this.spotlightSR.sprite = CreateRadialGlowSprite();
            this.spotlightSR.sortingOrder = this.spotlightSortingOrder;
            this.spotlightSR.color = new Color(this.spotlightColor.r, this.spotlightColor.g, this.spotlightColor.b, 0f);
            this.spotlightObj.transform.localScale = this.spotlightScale;
            this.spotlightObj.SetActive(false);
        }
    }

    protected Sprite CreateSolidWhiteSprite()
    {
        Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[] { Color.white, Color.white, Color.white, Color.white };
        tex.SetPixels(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 2, 2), new Vector2(0.5f, 0.5f), 1f);
    }

    protected Sprite CreateRadialGlowSprite()
    {
        const int size = 128;
        Texture2D glowTex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Vector2 center = new Vector2(size * 0.5f, size * 0.5f);
        float radius = size * 0.5f;
        Color[] glowPixels = new Color[size * size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center) / radius;
                float alpha = Mathf.Clamp01(1f - dist);
                alpha = Mathf.SmoothStep(0f, 1f, alpha);
                alpha = Mathf.Pow(alpha, 1.4f);
                glowPixels[y * size + x] = new Color(1f, 0.98f, 0.85f, alpha);
            }
        }

        glowTex.SetPixels(glowPixels);
        glowTex.Apply();
        glowTex.filterMode = FilterMode.Bilinear;
        return Sprite.Create(glowTex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100f);
    }

    protected void LateUpdate()
    {
        if (!this.isFreezing && (this.overlayObj == null || !this.overlayObj.activeSelf)) return;

        Camera cam = Camera.main;

        if (cam != null && this.overlayObj != null && this.overlayObj.activeSelf)
        {
            Vector3 camPos = cam.transform.position;
            camPos.z = 0f;
            this.overlayObj.transform.position = camPos;

            float camHeight = cam.orthographicSize * 2f * 2f;
            float camWidth = camHeight * cam.aspect * 1.5f;
            this.overlayObj.transform.localScale = new Vector3(camWidth, camHeight, 1f);
        }

        if (this.currentCaster != null && this.spotlightObj != null && this.spotlightObj.activeSelf)
        {
            Vector3 casterPos = this.currentCaster.transform.position;
            casterPos.y += 0.8f;
            casterPos.z = 0f;
            this.spotlightObj.transform.position = casterPos;
        }

        if (this.isFreezing)
        {
            this.freezeTimer += Time.unscaledDeltaTime;
            if (this.freezeTimer >= MAX_FREEZE_DURATION)
            {
                Debug.LogWarning("SuperFreezeManager: safety timeout reached, ending freeze.");
                this.EndSuperFreeze(false);
            }
        }
    }

    public void TriggerSuperFreeze(CharacterCtrl caster)
    {
        if (caster == null) return;
        if (this.isFreezing && this.currentCaster == caster) return;

        if (this.isFreezing)
        {
            this.EndSuperFreeze(true);
        }

        this.isFreezing = true;
        this.freezeTimer = 0f;
        this.currentCaster = caster;
        this.currentOpponent = caster.Opponent;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsSkillFreezing = true;
        }

        // 1. Freeze Opponent (animation, velocity, constraints, state updates)
        if (this.currentOpponent != null)
        {
            if (this.currentOpponent.Animator != null)
            {
                this.originalOpponentAnimatorSpeed = this.currentOpponent.Animator.speed;
                this.currentOpponent.Animator.speed = 0f;
            }

            if (this.currentOpponent.Rgb != null)
            {
                this.originalOpponentConstraints = this.currentOpponent.Rgb.constraints;
                this.originalOpponentVelocity = this.currentOpponent.Rgb.linearVelocity;
                this.currentOpponent.Rgb.linearVelocity = Vector2.zero;
                this.currentOpponent.Rgb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            if (this.currentOpponent.StateManager != null)
            {
                this.currentOpponent.StateManager.IsFrozen = true;
            }
        }

        // 2. Raise Caster SpriteRenderers above the dark overlay
        this.casterOriginalSortingOrders.Clear();
        SpriteRenderer[] casterRenderers = caster.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in casterRenderers)
        {
            if (sr == null) continue;
            this.casterOriginalSortingOrders[sr] = sr.sortingOrder;
            sr.sortingOrder = this.casterSortingOrderBoost + sr.sortingOrder;
        }

        // 3. Activate visual overlays and smoothly fade in
        if (this.overlayObj != null)
        {
            this.overlayObj.SetActive(true);
        }
        if (this.spotlightObj != null)
        {
            this.spotlightObj.SetActive(true);
            if (this.currentCaster != null)
            {
                Vector3 pos = this.currentCaster.transform.position;
                pos.y += 0.8f;
                pos.z = 0f;
                this.spotlightObj.transform.position = pos;
            }
        }

        if (this.fadeCoroutine != null) StopCoroutine(this.fadeCoroutine);
        this.fadeCoroutine = StartCoroutine(FadeInCoroutine(this.fadeInDuration));
    }

    public void EndSuperFreeze(bool immediate = false)
    {
        if (!this.isFreezing && (this.overlayObj == null || !this.overlayObj.activeSelf)) return;

        this.isFreezing = false;
        this.freezeTimer = 0f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsSkillFreezing = false;
        }

        // 1. Unfreeze Opponent
        if (this.currentOpponent != null)
        {
            if (this.currentOpponent.Animator != null)
            {
                this.currentOpponent.Animator.speed = (this.originalOpponentAnimatorSpeed > 0f) ? this.originalOpponentAnimatorSpeed : 1f;
            }

            if (this.currentOpponent.Rgb != null)
            {
                this.currentOpponent.Rgb.constraints = this.originalOpponentConstraints;
                this.currentOpponent.Rgb.linearVelocity = this.originalOpponentVelocity;
            }

            if (this.currentOpponent.StateManager != null)
            {
                this.currentOpponent.StateManager.IsFrozen = false;
            }
        }

        // 2. Restore Caster Sorting Orders
        foreach (var kvp in this.casterOriginalSortingOrders)
        {
            if (kvp.Key != null)
            {
                kvp.Key.sortingOrder = kvp.Value;
            }
        }
        this.casterOriginalSortingOrders.Clear();

        // 3. Fade Out Overlays
        if (immediate)
        {
            if (this.fadeCoroutine != null) StopCoroutine(this.fadeCoroutine);
            if (this.overlaySR != null)
            {
                Color c = this.overlaySR.color;
                c.a = 0f;
                this.overlaySR.color = c;
            }
            if (this.spotlightSR != null)
            {
                Color c = this.spotlightSR.color;
                c.a = 0f;
                this.spotlightSR.color = c;
            }
            if (this.overlayObj != null) this.overlayObj.SetActive(false);
            if (this.spotlightObj != null) this.spotlightObj.SetActive(false);
            this.currentCaster = null;
            this.currentOpponent = null;
        }
        else
        {
            if (this.fadeCoroutine != null) StopCoroutine(this.fadeCoroutine);
            this.fadeCoroutine = StartCoroutine(FadeOutCoroutine(this.fadeOutDuration));
        }
    }

    public void EndSuperFreezeIfCaster(CharacterCtrl caster)
    {
        if (this.isFreezing && this.currentCaster == caster)
        {
            this.EndSuperFreeze(false);
        }
    }

    protected IEnumerator FadeInCoroutine(float duration)
    {
        float elapsed = 0f;
        Color overlayStart = this.overlaySR != null ? this.overlaySR.color : Color.clear;
        Color overlayTarget = this.darkOverlayColor;

        Color spotlightStart = this.spotlightSR != null ? this.spotlightSR.color : Color.clear;
        Color spotlightTarget = this.spotlightColor;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            if (this.overlaySR != null)
            {
                this.overlaySR.color = Color.Lerp(overlayStart, overlayTarget, t);
            }
            if (this.spotlightSR != null)
            {
                this.spotlightSR.color = Color.Lerp(spotlightStart, spotlightTarget, t);
            }
            yield return null;
        }

        if (this.overlaySR != null) this.overlaySR.color = overlayTarget;
        if (this.spotlightSR != null) this.spotlightSR.color = spotlightTarget;
    }

    protected IEnumerator FadeOutCoroutine(float duration)
    {
        float elapsed = 0f;
        Color overlayStart = this.overlaySR != null ? this.overlaySR.color : Color.clear;
        Color overlayTarget = new Color(this.darkOverlayColor.r, this.darkOverlayColor.g, this.darkOverlayColor.b, 0f);

        Color spotlightStart = this.spotlightSR != null ? this.spotlightSR.color : Color.clear;
        Color spotlightTarget = new Color(this.spotlightColor.r, this.spotlightColor.g, this.spotlightColor.b, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            if (this.overlaySR != null)
            {
                this.overlaySR.color = Color.Lerp(overlayStart, overlayTarget, t);
            }
            if (this.spotlightSR != null)
            {
                this.spotlightSR.color = Color.Lerp(spotlightStart, spotlightTarget, t);
            }
            yield return null;
        }

        if (this.overlaySR != null)
        {
            this.overlaySR.color = overlayTarget;
            this.overlayObj.SetActive(false);
        }
        if (this.spotlightSR != null)
        {
            this.spotlightSR.color = spotlightTarget;
            this.spotlightObj.SetActive(false);
        }
        this.currentCaster = null;
        this.currentOpponent = null;
    }
}
