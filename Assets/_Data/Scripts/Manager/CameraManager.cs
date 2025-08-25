using System;
using UnityEngine;

public class CameraManager : BaseBehaviour
{
    [Header("Camera Manager")]
    protected static CameraManager instance;
    public static CameraManager Instance => instance;

    [SerializeField] protected Camera mainCam;
    [SerializeField] protected Vector3 originPos;
    [SerializeField] private Wall leftWall, rightWall;
    public Wall LeftWall => leftWall;
    public Wall RightWall => rightWall;

    [Header("Camera Settings")]
    [SerializeField] protected float followSpeed = 5;

    [SerializeField] protected ZoomSettings zoomSettings;
    [SerializeField] protected CameraBounds bounds;
    public CameraBounds Bounds => bounds;
    protected override void Awake()
    {
        base.Awake();
        if (CameraManager.instance != null) Debug.LogWarning("Only 1 CameraManager allows to exist");
        instance = this;
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.zoomSettings.minZoom = 5.5f;
        this.zoomSettings.maxZoom = 6;
        this.zoomSettings.maxDistance = 5;

        this.bounds.maxX = 5;
        this.bounds.minX = -10;
        this.bounds.maxY = 5.5f;
        this.bounds.minY = 4;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCameraMain();
        this.LoadWall();
    }
    protected void Update()
    {
        this.Zooming();
        this.Following();
        this.ClampCameraBounds();
        this.FollowWalls();
    }
    protected void LoadCameraMain()
    {
        if (this.mainCam != null) return;
        this.mainCam = FindAnyObjectByType<Camera>();
        Debug.Log(transform.name + ": LoadCameraMain");
    }
    protected void LoadWall()
    {
        if (this.leftWall != null && this.rightWall != null) return;
        Wall[] walls = FindObjectsOfType<Wall>();
        if (walls.Length > 2) return;
        this.leftWall = walls[0];
        this.rightWall = walls[1];
        Debug.Log(transform.name + ": LoadWall");
    }
    protected void Following()
    {
        CharacterCtrl player1 = GameManager.Instance.Player1;
        CharacterCtrl player2 = GameManager.Instance.Player2;
        Vector3 midPoint = (player1.transform.position + player2.transform.position) / 2f;
        midPoint.z = -10f;
        this.transform.position = Vector3.Lerp(this.mainCam.transform.position, midPoint, this.followSpeed * Time.deltaTime);
    }

    protected void Zooming()
    {
        CharacterCtrl player1 = GameManager.Instance.Player1;
        CharacterCtrl player2 = GameManager.Instance.Player2;
        float distance = Vector2.Distance(player1.transform.position, player2.transform.position);
        this.mainCam.orthographicSize = Mathf.Lerp(this.zoomSettings.minZoom, this.zoomSettings.maxZoom, distance / this.zoomSettings.maxDistance);
    }
    protected void ClampCameraBounds()
    {
        Vector3 camPos = this.transform.position;
        camPos.x = Mathf.Clamp(camPos.x, this.bounds.minX, this.bounds.maxX);
        camPos.y = Mathf.Clamp(camPos.y, this.bounds.minY, this.bounds.maxY);
        this.transform.position = camPos;
    }
    public void ZoomCameraCutScene(Vector3 position, float targetSize)
    {
        Vector3 newCamPos = position;
        newCamPos.z = -10;
        this.transform.position = newCamPos;
        this.mainCam.orthographicSize = targetSize;
    }

    private void FollowWalls()
    {
        float halfHeight = mainCam.orthographicSize;
        float halfWidth = halfHeight * mainCam.aspect;

        Vector3 camPos = this.transform.position;

        // wall trái theo biên trái camera
        Vector3 newPosLeftWall = new Vector3(camPos.x - halfWidth, camPos.y, 0);
        this.leftWall.transform.position = newPosLeftWall;

        // wall phải theo biên phải camera
        Vector3 newPosRightWall = new Vector3(camPos.x + halfWidth, camPos.y, 0);
        newPosRightWall.x -= 1.5f;
        this.rightWall.transform.position = newPosRightWall;

    }

}