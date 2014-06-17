using UnityEngine;
using System.Collections;

public class PuzzleCubeLock : MonoBehaviour {
    private bool _isLocked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool TryLock()
    {
        if (_isLocked)
            return (false);
        _isLocked = true;
        return (true);
    }

    public void Unlock()
    {
        _isLocked = false;
    }
}
