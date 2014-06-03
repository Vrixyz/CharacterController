using UnityEngine;
using System.Collections;

public class ClimbVolumeManager : MonoBehaviour {

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
            am.SetClimbVolumeTransform(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Animation_Manager am = other.gameObject.GetComponent<Animation_Manager>();

        if (am != null)
        {
            am.ClearClimbVolumeTransform();
        }
    }
}
