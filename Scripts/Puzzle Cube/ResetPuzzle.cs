using UnityEngine;
using System.Collections;

public class ResetPuzzle : MonoBehaviour {
    private ArrayList _transforms;

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            _transforms.Add(child);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        int  i = 0;

        foreach (Transform child in transform)
        {
            child.parent.transform.position = ((Transform)_transforms[i]).position;
            child.parent.transform.rotation = ((Transform)_transforms[i]).rotation;

            PuzzleCubeLock  cubeLock = child.gameObject.GetComponent<PuzzleCubeLock>();

            if (cubeLock != null)
                cubeLock.Unlock();
        }
    }
}
