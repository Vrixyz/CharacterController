using UnityEngine;
using System.Collections;

public class SetHeart : MonoBehaviour {

	public	Texture innactive;
	public	Texture active;
	public	GameObject trigger;

	private bool	done = false;

	// Use this for initialization
	void Start () {
		this.renderer.material.mainTexture = innactive;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ActivateCrystal()
	{
		this.renderer.material.mainTexture = active;
	}

    public bool IsDone()
    {
        return (done);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !done)
		{
			for (int i = 0; i < other.transform.childCount; ++i)
			{
				GameObject crystal = other.transform.GetChild(i).gameObject;
				if (crystal == trigger)
				{
					Destroy(crystal);
					ActivateCrystal();
					done = true;
				}
			}
		}
	}
}
