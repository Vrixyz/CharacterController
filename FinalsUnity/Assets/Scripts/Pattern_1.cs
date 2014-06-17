using UnityEngine;
using System.Collections;

public class Pattern_1 : MonoBehaviour {

	private float waitTime = 4f;
	private int blink = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (waitTime < 0)
		{
			Enable();
			waitTime = 0.2f;
			blink--;
		}
		if (blink == 0)
		{
			blink = -1;
			waitTime = 2.0f;
			Enable ();
			Disable();
		}
		if (blink == -2)
		{
			blink = 10;
			waitTime = 4.0f;
			Enable ();
			Disable();
		}
	}

	void Enable()
	{
		for (int i = 1; i < this.transform.childCount; ++i)
		{
			GameObject halo = this.transform.GetChild(i).gameObject;
			halo.renderer.enabled = !halo.renderer.isVisible;
		}
	}

	void Disable()
	{
		for (int i = 1; i < this.transform.childCount; ++i)
		{
			GameObject halo = this.transform.GetChild(i).gameObject;
			halo.gameObject.SetActive(!halo.gameObject.activeInHierarchy);
		}
	}
}
