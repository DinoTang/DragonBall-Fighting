using UnityEngine;

public class CameraManager : BaseBehaviour
{
    [Header("Camera Manager")]
    protected static CameraManager instance;
    public static CameraManager Instance => instance;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected Vector3 originPos;
    [SerializeField] protected float originOrthographicSize;
    protected override void Awake()
    {
        base.Awake();
        if (CameraManager.instance != null) Debug.LogWarning("Only 1 CameraManager allows to exist");
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        this.originPos = this.transform.position;
        this.originOrthographicSize = mainCam.orthographicSize;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCameraMain();
    }

    protected void LoadCameraMain()
    {
        if (this.mainCam != null) return;
        this.mainCam = FindAnyObjectByType<Camera>();
        Debug.Log(transform.name + ": LoadCameraMain");
    }

    public void ZoomCameraCutScene(Vector3 position, float targetSize)
    {
        Vector3 newCamPos = position;
        newCamPos.z = -10;
        this.transform.position = newCamPos;
        this.mainCam.orthographicSize = targetSize;
    }

    public void ResetCamPosition()
    {
        this.transform.position = this.originPos;
        this.mainCam.orthographicSize = this.originOrthographicSize;
    }
}