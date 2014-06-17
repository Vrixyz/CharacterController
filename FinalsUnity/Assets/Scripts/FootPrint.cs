using UnityEngine;
using System.Collections;

public class FootPrint : MonoBehaviour {

	public Object clone;

	// Use this for initialization
	void Start () {
	
	}
	int i = 0;
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position + (Vector3.up * -0.01F);
		if (i++ >= 10 && GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().isGrounded)
		{
			GameObject footprint = (GameObject)GameObject.Instantiate(clone);
			footprint.transform.position = pos;
			footprint.transform.rotation = this.transform.rotation;
			GameObject.Destroy(footprint, 12);
			i = 0;
		}
	}
}
