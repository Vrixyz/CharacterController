using UnityEngine;
using System.Collections;

public class PuzzleCubeZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (PuzzleCubeDestination cubeDestination in
                gameObject.GetComponentsInChildren<PuzzleCubeDestination>())
            {
                cubeDestination.firing = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (PuzzleCubeDestination cubeDestination in
                gameObject.GetComponentsInChildren<PuzzleCubeDestination>())
            {
                cubeDestination.firing = false;
            }
        }
    }
}
