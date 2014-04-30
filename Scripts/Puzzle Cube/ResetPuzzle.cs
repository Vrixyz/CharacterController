﻿using UnityEngine;
using System.Collections;

public class ResetPuzzle : MonoBehaviour {
    private ArrayList _transforms = new ArrayList();
    private ArrayList _gameObjects = new ArrayList();

	// Use this for initialization
	void Start () {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                _transforms.Add(child.position);
                _gameObjects.Add(child.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        int i = 0;

        HeatWave.Reset();
        foreach (GameObject child in _gameObjects)
        {
            child.transform.position = ((Vector3)_transforms[i]);
            child.transform.localRotation = Quaternion.identity;

            PuzzleCubeLock  cubeLock = child.gameObject.GetComponent<PuzzleCubeLock>();
            Collider collider = child.gameObject.GetComponent<Collider>();
            PuzzleCubeDestination destination = child.gameObject.GetComponent<PuzzleCubeDestination>();

            if (cubeLock != null)
                cubeLock.Unlock();
            if (collider != null)
                collider.enabled = true;
            if (child.gameObject.rigidbody != null)
            {
                child.gameObject.rigidbody.velocity = Vector3.zero;
                child.gameObject.rigidbody.angularVelocity = Vector3.zero;
                child.gameObject.rigidbody.Sleep();
            }
            if (destination != null)
                destination.Reset();
            ++i;
        }
    }
}
