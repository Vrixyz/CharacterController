using UnityEngine;
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
    public float boundDown = 0.4f;

    private Vector3 _newPosition;
    private Vector3 _newRotation;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        // TODO Find camera new position with clamping

        _newPosition = Vector3.zero;
        _newRotation = Vector3.zero;
        DefaultCameraPosition = gameObject.transform.position;
        InitialCameraPosition();
	}

    void InitialCameraPosition()
    {
        gameObject.transform.position = DefaultCameraPosition;
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
            print("Down" +( -zoomDistance * boundDown) + " Up : " + (zoomDistance * boundUp));
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x,
                                                              Mathf.Clamp(Camera.main.transform.localPosition.y, -zoomDistance * boundDown, zoomDistance * boundUp),
                                                              Camera.main.transform.localPosition.z);
            Camera.main.transform.position = (transform.position - TargetLookAtTransform.transform.position).normalized * zoomDistance + TargetLookAtTransform.transform.position;
            
            Camera.main.transform.LookAt(TargetLookAtTransform);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            zoomDistance -= Input.GetAxis("Mouse ScrollWheel");
            zoomDistance = Mathf.Clamp(zoomDistance, 3, 15);
            Camera.main.transform.position = (transform.position - TargetLookAtTransform.transform.position).normalized * zoomDistance + TargetLookAtTransform.transform.position;
        }
    }

    private float xVel = 0F;
    private float yVel = 0F;
    private float smoothTime = 0.1F;
    public void SmoothCameraPosition()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float   x = Mathf.SmoothDamp(Camera.main.transform.position.x, Camera.main.transform.position.x - mouseX,
            ref xVel, smoothTime);
        float y = Mathf.SmoothDamp(Camera.main.transform.position.y, Camera.main.transform.position.y - mouseY,
            ref yVel, smoothTime);

        _newPosition = CreatePositionVector(mouseX, mouseY,  zoomDistance);
    }

    public Vector3 CreatePositionVector(float mouseX, float mouseY, float distance)
    {
        Vector3     positionVec = new Vector3(0, 0, distance);
        Quaternion rotation = Quaternion.Euler(mouseX, mouseY, 0);

        return (Camera.main.gameObject.transform.parent.transform.position + rotation * positionVec);
    }

    public void SmoothCameraAxis()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float x = Mathf.SmoothDamp(Camera.main.transform.rotation.x, Camera.main.transform.rotation.x - mouseX,
            ref xVel, smoothTime);
        float y = Mathf.SmoothDamp(Camera.main.transform.rotation.y, Camera.main.transform.rotation.y - mouseY,
            ref yVel, smoothTime);

        _newRotation = new Vector3(x, y, 0);
    }

    public void ApplyCameraPosition()
    {
        Camera.main.transform.position = _newPosition;
        Camera.main.transform.rotation = Quaternion.Euler(_newRotation);
        Camera.main.transform.LookAt(TargetLookAtTransform);
    }

    public void VerifyUserMouseInput()
    {
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
            print(mainCamera);
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
}
