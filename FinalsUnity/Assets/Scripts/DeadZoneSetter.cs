using UnityEngine;
using System.Collections;

public class DeadZoneSetter : MonoBehaviour {

	public RespawnsAt.DieZone zone;

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
			other.gameObject.SendMessage("SetZone", zone);
		}
	}
}
