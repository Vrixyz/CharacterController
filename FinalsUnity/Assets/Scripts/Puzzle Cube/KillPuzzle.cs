using UnityEngine;
using System.Collections;

public class KillPuzzle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<ResetPuzzle>().Goal();
        }
    }
}
