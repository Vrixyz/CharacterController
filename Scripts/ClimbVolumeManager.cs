using UnityEngine;
using System.Collections;

public class ClimbVolumeManager : MonoBehaviour {
    public Transform climbAnchorAdjustment = null;
    public Transform postClimbAdjustment = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Animation_Manager am = other.gameObject.GetComponent<Animation_Manager>();

        if (am != null)
        {
            am.SetClimbVolumeTransform(transform, climbAnchorAdjustment, postClimbAdjustment);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Animation_Manager am = other.gameObject.GetComponent<Animation_Manager>();

        if (am != null)
        {
            am.ClearClimbVolumeTransform(transform);
        }
    }
}
