using UnityEngine;
using System.Collections;

public class ActivateSound : MonoBehaviour {

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
			this.GetComponentInChildren<AudioSource>().mute = false;
			if (!this.GetComponentInChildren<AudioSource>().isPlaying)
			{
				this.GetComponentInChildren<AudioSource>().Play();
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			this.GetComponentInChildren<AudioSource>().mute = true;
		}
	}
}
