using UnityEngine;
using System.Collections;

public class PuzzleCubeDestinationCollider : MonoBehaviour {
    private GameObject puzzleCube = null;
    public float dragForce = 10F;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (puzzleCube != null)
        {
            puzzleCube.rigidbody.AddForce((gameObject.transform.position - puzzleCube.transform.position) * dragForce);
            ++dragForce;

            if (Mathf.Abs(puzzleCube.transform.position.x - transform.position.x) < 0.5F &&
                Mathf.Abs(puzzleCube.transform.position.y - transform.position.y) < 0.5F &&
                Mathf.Abs(puzzleCube.transform.position.z - transform.position.z) < 0.5F)
            {
                puzzleCube.collider.enabled = false;
                collider.enabled = false;
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PuzzleCube")
        {
            // Only acquire cube if not already acquired
            if (puzzleCube == null &&
                collision.gameObject.GetComponent<PuzzleCubeLock>().TryLock())
                puzzleCube = collision.gameObject;
            Physics.IgnoreCollision(collision.collider, collider);
        }
    }
}
