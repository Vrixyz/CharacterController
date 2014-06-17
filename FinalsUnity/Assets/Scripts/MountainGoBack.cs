using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MountainGoBack : MonoBehaviour {
    public List<GameObject> activeThat = new List<GameObject>();
    public GameObject removeThat;
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

            foreach (GameObject g in activeThat) {
                g.GetComponent<ParticleSystem>().Play();
            }
            Destroy(removeThat);
        }
    }
}
