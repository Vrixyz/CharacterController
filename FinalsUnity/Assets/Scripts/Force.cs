using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {

    public Vector3 force = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
        this.gameObject.rigidbody.AddForce(force);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
