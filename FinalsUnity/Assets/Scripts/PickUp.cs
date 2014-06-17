using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public GameObject diamond;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			diamond.transform.parent = other.transform;
			diamond.transform.localPosition = new Vector3(-0.03f, 0.9f, 1.0f);
			this.gameObject.SetActive(false);
		}
	}
}
