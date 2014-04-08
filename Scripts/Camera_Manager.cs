﻿using UnityEngine;
using System.Collections;

public class Camera_Manager : MonoBehaviour {
    public static Camera_Manager Instance;
    private Transform TargetLookAtTransform = null;
    public Vector2 YLimit = new Vector2(-70, 70);
    public Vector2 ZoomLimit = new Vector2(42, 200);
    private Vector3 DefaultCameraPosition = Vector3.zero;
    private Vector2 MouseZero = Vector2.zero;
    public float zoomDistance = 10f;
    public float boundUp = 0.7f;
    public float boundDown = 45f;

    private Vector3 _newPosition;
    private Vector3 _newRotation;

    private float xVel = 0F;
    private float yVel = 0F;
    private float zoomVel = 0F;
    private float smoothTime = 0.0001F;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        _newPosition = Vector3.zero;
        _newRotation = Vector3.zero;
        DefaultCameraPosition = gameObject.transform.localPosition;
        InitialCameraPosition();
	}

    public void InitialCameraPosition()
    {
        gameObject.transform.localPosition = DefaultCameraPosition;
    }

    void InitialCameraParameters()
    {
        _newPosition = Vector3.zero;
        _newRotation = Vector3.zero;
        InitialCameraPosition();
    }

	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        VerifyMouseInput();
    }

    void VerifyMouseInput()
    {
        if (Input.GetButton("Fire2"))
        {
            SmoothCameraPosition();
            //CameraCollisionPointsCheck(TargetLookAtTransform.position, _newPosition);
            ApplyCameraPosition();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            SmoothCameraAxis();
            ApplyCameraPosition();
        }
        CameraCollisionPointsCheck(TargetLookAtTransform.position, _newPosition);
    }
    
    public void UpdatePosition()
    {
        VerifyUserMouseInput();
        _newPosition = new Vector3(oldmouseX, oldmouseY, zoomDistance);
        _newPosition = CreatePositionVector(oldmouseX, oldmouseY, _newPosition);
    }

    public void SmoothCameraPosition()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float x = Mathf.SmoothDamp(0, mouseX,
            ref xVel, smoothTime);
        float y = Mathf.SmoothDamp(0, mouseY,
            ref yVel, smoothTime);
        oldmouseX += x;
        oldmouseY += y;

     
        UpdatePosition();
    }
    
    public float oldmouseX = 180;
    public float oldmouseY = -45;

    public Vector3 CreatePositionVector(float mouseX, float mouseY, Vector3 position)
    {
        Vector3 positionVec = new Vector3(0, 0, position.z);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);

        return (TargetLookAtTransform.transform.position + (rotation * positionVec));
    }

    public void SmoothCameraAxis()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        zoomDistance = Mathf.SmoothDamp(zoomDistance, zoomDistance - zoom,
            ref zoomVel, smoothTime);
        zoomDistance = Helper.CameraClamp(zoomDistance, 7, 15);
        _newPosition = new Vector3(oldmouseX, oldmouseY, zoomDistance);
        _newPosition = CreatePositionVector(oldmouseX, oldmouseY, _newPosition);
    }

    public void ApplyCameraPosition()
    {
        Camera.main.transform.position = _newPosition;
        Camera.main.transform.LookAt(TargetLookAtTransform);
    }

    public void VerifyUserMouseInput()
    {
        oldmouseY = Helper.CameraClamp(oldmouseY, -boundDown, boundUp);
    }

    public static void InitialCameraCheck()
    {
        Camera mainCamera = Camera.main;
        GameObject targetLookAt = GameObject.Find("targetLookAt");

        if (targetLookAt == null)
        {
            targetLookAt = new GameObject();
            targetLookAt.name = "targetLookAt";
            
            targetLookAt.transform.parent = Character_Manager.Instance.gameObject.transform;
            targetLookAt.transform.localPosition = Vector3.zero;
        }
        if (mainCamera == null)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Camera>();
            mainCamera = gameObject.GetComponent<Camera>();
            mainCamera.name = "Main Camera";
            mainCamera.tag = "MainCamera";
            mainCamera.transform.parent = Character_Manager.Instance.gameObject.transform;
            mainCamera.transform.localPosition = Vector3.zero;
        }
        if (mainCamera.gameObject.GetComponent<Camera_Manager>() == null)
        {
            mainCamera.gameObject.AddComponent<Camera_Manager>();
        }
        mainCamera.gameObject.GetComponent<Camera_Manager>().TargetLookAtTransform = targetLookAt.transform;
    }

    public void CameraCollisionPointsCheck(Vector3 targetLookAt, Vector3 cameraPos)
    {
        Helper.ClipPlaneStruct cpp = Helper.FindNearClipPlanePositions();

        Vector3 backBuffer = Vector3.zero;
        backBuffer.z -= Camera.main.nearClipPlane;
        backBuffer = transform.TransformPoint(backBuffer);

        Debug.DrawLine(backBuffer, targetLookAt, Color.red);

        Debug.DrawLine(cpp.UpperLeft, cpp.UpperRight, Color.white);
        Debug.DrawLine(cpp.UpperRight, cpp.LowerRight, Color.white);
        Debug.DrawLine(cpp.LowerLeft, cpp.LowerRight, Color.white);
        Debug.DrawLine(cpp.LowerLeft, cpp.UpperLeft, Color.white);
        
        Debug.DrawLine(cpp.UpperLeft, targetLookAt, Color.white);
        Debug.DrawLine(cpp.UpperRight, targetLookAt, Color.white);
        Debug.DrawLine(cpp.LowerLeft, targetLookAt, Color.white);
        Debug.DrawLine(cpp.LowerRight, targetLookAt, Color.white);
    }
}
