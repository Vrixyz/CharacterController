using UnityEngine;
using System.Collections;

public class ResetPuzzle : MonoBehaviour {
    private ArrayList _positions = new ArrayList();
    private ArrayList _rotations = new ArrayList();
    private ArrayList _gameObjects = new ArrayList();
    private bool _puzzleOver = false;

	// Use this for initialization
	void Start () {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                _positions.Add(child.position);
                _rotations.Add(child.rotation);
                _gameObjects.Add(child.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Goal()
    {
        _puzzleOver = true;
        HeatWave.Reset();
        foreach (GameObject child in _gameObjects)
        {
            if (child.gameObject.rigidbody != null)
            {
                Collider collider = child.gameObject.GetComponent<Collider>();

                collider.enabled = false;
            }
        }
    }

    public void Reset()
    {
        if (_puzzleOver)
            return;

        int i = 0;

        HeatWave.Reset();
        foreach (GameObject child in _gameObjects)
        {
            child.transform.position = ((Vector3)_positions[i]);
            child.transform.rotation = ((Quaternion)_rotations[i]);

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
