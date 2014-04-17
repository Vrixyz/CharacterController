using UnityEngine;
using System.Collections;

public class Camera_Manager : MonoBehaviour {
    public static Camera_Manager    Instance;
    private Transform               TargetLookAtTransform = null;
    public Vector2                  YLimit = new Vector2(-70, 70);
    public Vector2                  ZoomLimit = new Vector2(7, 15);
    private Vector3                 DefaultCameraPosition = Vector3.zero;
    private Vector2                 MouseZero = Vector2.zero;
    public float                    zoomDistance = 10f;
    private float                   userZoomDistance;
    public float                    boundUp = 0.7f;
    public float                    boundDown = 45f;
    public float                    mouseSensitivity = 1F;

    private Vector3 _newPosition;

    public float unobstructedSmoothTime = 0.2F;
    public float obstructedSmoothTime = 0.1F;

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
        userZoomDistance = zoomDistance;
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
        InitialCameraPosition();
    }

	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        VerifyMouseInput();

        int obstructedCameraCount = 0;
        bool obstructed = false;
        float currentZoomDistance = zoomDistance;

        zoomDistance = userZoomDistance;
        SmoothCameraAxis();
        ApplyCameraPosition();
        if (ObstructedCameraChecked(0))
        {
            zoomDistance = currentZoomDistance;
            SmoothCameraAxis(true);
            ApplyCameraPosition();
        }
        else if (currentZoomDistance < userZoomDistance)
        {
            zoomDistance = currentZoomDistance + unobstructedSmoothTime;
            SmoothCameraAxis(true);
            ApplyCameraPosition();
        }
        do {
            obstructed = ObstructedCameraChecked(obstructedCameraCount);
            obstructedCameraCount++;
        } while (obstructed);
    }

    void VerifyMouseInput()
    {
        CameraCollisionPointsCheck(TargetLookAtTransform.position, _newPosition);
        if (Input.GetButton("Fire2"))
        {
            SmoothCameraPosition();
            ApplyCameraPosition();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            SmoothCameraAxis(userZoomDistance != zoomDistance);
            ApplyCameraPosition();
            userZoomDistance = zoomDistance;
        }
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

        float x = Mathf.SmoothDamp(0, mouseX * mouseSensitivity,
            ref xVel, smoothTime);
        float y = Mathf.SmoothDamp(0, mouseY * mouseSensitivity,
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

    public void SmoothCameraAxis(bool ignoreLimit = false)
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        zoomDistance = Mathf.SmoothDamp(zoomDistance, zoomDistance - zoom,
            ref zoomVel, smoothTime);
        if (!ignoreLimit)
        {
            zoomDistance = Helper.CameraClamp(zoomDistance, ZoomLimit.x, ZoomLimit.y);
        }
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

    public float CameraCollisionPointsCheck(Vector3 targetLookAt, Vector3 cameraPos)
    {
        // Debug Lines

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


        // Actual Code
        float      closestDistanceToCharacter = -1F;
        RaycastHit hitInfo;

        if (Physics.Linecast(targetLookAt, backBuffer, out hitInfo, ~LayerMask.NameToLayer("Player")))
        {
            closestDistanceToCharacter = hitInfo.distance;
        }
        if (Physics.Linecast(targetLookAt, cpp.UpperRight, out hitInfo, ~LayerMask.NameToLayer("Player")))
        {
            if (closestDistanceToCharacter == -1F || hitInfo.distance < closestDistanceToCharacter)
                closestDistanceToCharacter = hitInfo.distance;
        }
        if (Physics.Linecast(targetLookAt, cpp.UpperLeft, out hitInfo, ~LayerMask.NameToLayer("Player")))
        {
            if (closestDistanceToCharacter == -1F ||  hitInfo.distance < closestDistanceToCharacter)
                closestDistanceToCharacter = hitInfo.distance;
        }
        if (Physics.Linecast(targetLookAt, cpp.LowerLeft, out hitInfo, ~LayerMask.NameToLayer("Player")))
        {
            if (closestDistanceToCharacter == -1F ||  hitInfo.distance < closestDistanceToCharacter)
                closestDistanceToCharacter = hitInfo.distance;
        }
        if (Physics.Linecast(targetLookAt, cpp.LowerRight, out hitInfo, ~LayerMask.NameToLayer("Player")))
        {
            if (closestDistanceToCharacter == -1F ||  hitInfo.distance < closestDistanceToCharacter)
                closestDistanceToCharacter = hitInfo.distance;
        }
        return (closestDistanceToCharacter);
    }

    public bool ObstructedCameraChecked(int obstructedCheckCount)
    {
        bool    cameraObstructionBool = false;
        float   closestDistanceToCharacter = CameraCollisionPointsCheck(TargetLookAtTransform.position, _newPosition);

        if (closestDistanceToCharacter != -1F)
        {
            cameraObstructionBool = true;
            if (obstructedCheckCount < 2)
            {
                //if (zoomDistance - obstructedSmoothTime > closestDistanceToCharacter - Camera.main.nearClipPlane)
                zoomDistance -= obstructedSmoothTime;
            }
            else
            {
                cameraObstructionBool = false;
            }
            SmoothCameraAxis(true);
            ApplyCameraPosition();
        }
        return (cameraObstructionBool);
    }
}