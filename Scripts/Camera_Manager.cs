using UnityEngine;
using System.Collections;

public class Camera_Manager : MonoBehaviour {
    public static Camera_Manager Instance;
    private Transform TargetLookAtTransform = null;
    public Vector2 YLimit = new Vector2(-70, 70);
    public Vector2 ZoomLimit = new Vector2(42, 200);
    private Vector3 DefaultCameraPosition = Vector3.zero;
    private Vector2 MouseZero = Vector2.zero;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        // TODO Find camera new position with clamping
        //Mathf.Clamp(TargetLookAtTransform.localRotation.y, YLimit.x, YLimit.y);
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
