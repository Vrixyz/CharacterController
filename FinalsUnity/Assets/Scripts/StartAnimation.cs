using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {

	public GameObject animation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		animation.SetActive(true);
		if (animation.particleSystem)
			animation.particleSystem.Play();
		if (animation.animation)
			animation.animation.Play();
	}
}
