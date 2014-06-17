using UnityEngine;
using System.Collections;

public class ColorGame : MonoBehaviour {

	public GameObject One;
	public GameObject Two;
	public GameObject Three;

	private float timer = 4.0f;

	private bool launchNext = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!launchNext)
		{
			return ;
		}

		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Two.SetActive(true);
            launchNext = false;
        }
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			this.Lauch();
			launchNext = true;
		}
	}


	void Lauch()
	{
		One.SetActive(true);
	}
}
