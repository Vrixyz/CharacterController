using UnityEngine;
using System.Collections;

public class Character_Collision_Manager : MonoBehaviour {
    public float Strength = 30F;
	public bool _diagonale;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	}

   void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "PuzzleCube")
        {
            Rigidbody puzzleCube = hit.rigidbody;

            Vector3 force = gameObject.GetComponent<Character_Motor>().moveVector;
            Vector3 characterDirection = puzzleCube.transform.position - gameObject.transform.position;

            // Only apply to one axis or none
            force.y = 0;
            force = puzzleCube.transform.InverseTransformDirection(force);
			if (!_diagonale)
			{
	            if (Mathf.Abs(characterDirection.x) > Mathf.Abs(characterDirection.z))
	                force.z = 0;
	            else if (Mathf.Abs(characterDirection.x) < Mathf.Abs(characterDirection.z))
	                force.x = 0;
	            else
	                force = Vector3.zero;
			}
			force *= Strength;
            //_puzzleCube.AddForce(force);
            puzzleCube.AddRelativeForce(force);
        }
    }
}
