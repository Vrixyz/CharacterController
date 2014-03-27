using UnityEngine;
using System.Collections;

public static class Helper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float CameraClamp(float val, float min, float max)
    {
        return (Mathf.Clamp(val, min, max));
    }
}
