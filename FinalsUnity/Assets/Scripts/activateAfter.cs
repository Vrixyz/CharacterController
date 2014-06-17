using UnityEngine;
using System.Collections;

public class activateAfter : MonoBehaviour {
    public float secondsBeforeActivate = 0;
    public GameObject objectToActivate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (secondsBeforeActivate > 0)
        {
            secondsBeforeActivate -= Time.deltaTime;
            return;
        }
        objectToActivate.SetActive(true);
        Destroy(this);
	}
}
