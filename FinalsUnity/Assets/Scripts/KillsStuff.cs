using UnityEngine;
using System.Collections;

public class KillsStuff : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "Player" ||
		    c.gameObject.tag == "PuzzleCube")
		{
			c.gameObject.SendMessage ("Die");
			this.gameObject.transform.parent.gameObject.SendMessage("Die");
		}
	}
}
