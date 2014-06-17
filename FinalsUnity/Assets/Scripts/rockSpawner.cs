using UnityEngine;
using System.Collections;

public class rockSpawner : MonoBehaviour {
	public GameObject prefab;
    public GameObject prefab_other;
	public Vector3 direction = new Vector3(500, 0, 0);
	public Vector3 random = new Vector3(0, 250, 250);
	public float cooldownInSeconds = 3;
	private float cooldown;
	// Use this for initialization
	void Start () {
		cooldown = cooldownInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;

		if (cooldown <= 0) {
            GameObject rock = null;
            if (Random.Range(0, 3) == 0)
            {
                rock = (GameObject)GameObject.Instantiate(prefab_other);
            }
            else
			    rock = (GameObject)GameObject.Instantiate(prefab);
			rock.transform.position = this.gameObject.transform.position;
			Vector3 randomForcemodifier = random * (Random.Range(0, 100) / 100F) - random;
			//print(randomForcemodifier);
			rock.rigidbody.AddForce(gameObject.transform.TransformDirection(direction + randomForcemodifier));
			cooldown = cooldownInSeconds;
		}
	}
}
