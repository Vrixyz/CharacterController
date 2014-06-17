using UnityEngine;
using System.Collections;

public class PuzzleCubeDestination : MonoBehaviour {
    private GameObject puzzleCube = null;

    public GameObject heatWavePrefab = null;
    public float dragForce = 10F;
    public bool firing = true;

    public const int waveFrequence = 1500;

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
            Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            HeatWave.Create(gameObject, direction, heatWavePrefab);
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
        firing = true;
    }
}
