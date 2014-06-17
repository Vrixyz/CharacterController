using UnityEngine;
using System.Collections;

public class Death_Permanent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Die(){
		Destroy (this.gameObject);
	}
}
