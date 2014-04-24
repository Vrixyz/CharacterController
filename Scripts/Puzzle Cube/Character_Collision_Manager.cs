using UnityEngine;
using System.Collections;

public class Character_Collision_Manager : MonoBehaviour {
    public float Strength = 30F;

    private Rigidbody _puzzleCube = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Once the forces have been reseted by using isKinematic, reset _puzzleCube to null
        if (_puzzleCube != null &&
            _puzzleCube.isKinematic == true)
        {
            _puzzleCube.isKinematic = false;
            _puzzleCube = null;
        }
        // If fire button released, putting rigidbody to kinematic will reset the forces on it
        if (Input.GetButtonUp("Fire1") &&
            _puzzleCube != null)
        {
            _puzzleCube.isKinematic = true;
        }
        // Apply force to rigidbody while fire button is pressed
        if (_puzzleCube != null &&
            Input.GetButton("Fire1"))
        {
            Vector3 force = gameObject.GetComponent<Character_Motor>().moveVector;
            Vector3 characterDirection = _puzzleCube.transform.position - gameObject.transform.position;

            // Only apply to one axis or none
            force.y = 0;
            if (Mathf.Abs(characterDirection.x) > Mathf.Abs(characterDirection.z))
                force.z = 0;
            else if (Mathf.Abs(characterDirection.x) < Mathf.Abs(characterDirection.z))
                force.x = 0;
            else
                force = Vector3.zero;
            force *= Strength;
            _puzzleCube.AddForce(force);
        }
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_puzzleCube == null &&
            hit.gameObject.tag == "PuzzleCube" &&
            Input.GetButtonDown("Fire1"))
        {
            _puzzleCube = hit.rigidbody;
        }
    }

    void OnControllerColliderExit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "PuzzleCube" && 
            _puzzleCube == hit.rigidbody)
        {
           // putting rigidbody to kinematic will reset the forces on it
            _puzzleCube.isKinematic = true;
        }
    }
}
