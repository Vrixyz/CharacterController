using UnityEngine;
using System.Collections;

public class PuzzleCubeDestination : MonoBehaviour {
    private GameObject puzzleCube = null;
    public float dragForce = 10F;
    public bool firing = false;

    public const int waveFrequence = 6000;

    private Vector3[] _directions = new Vector3[]{Vector3.forward,
    Vector3.left,
    Vector3.right,
    Vector3.back
};

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
        if (puzzleCube == null && firing &&
            Random.Range(0, waveFrequence) == 0)
        {
            HeatWave.Create(gameObject, transform.TransformDirection(_directions[Random.Range(0, 4)]));
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PuzzleCube")
        {
            // Only acquire cube if not already acquired
            if (puzzleCube == null &&
                collision.gameObject.GetComponent<PuzzleCubeLock>().TryLock())
            {
                puzzleCube = collision.gameObject;
            }
            Physics.IgnoreCollision(collision.collider, collider);
        }
    }

    public void Reset()
    {
        puzzleCube = null;
        firing = false;
    }
}
