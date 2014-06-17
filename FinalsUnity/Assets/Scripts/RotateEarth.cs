using UnityEngine;
using System.Collections;

public class RotateEarth : MonoBehaviour {

	public float rotateSpeed = 2f;
	
	Transform _t;
	
	void Start() {
		_t = transform;	
	}
	
	void Update () {
		_t.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);
	}
}
