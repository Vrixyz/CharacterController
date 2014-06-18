using UnityEngine;
using System.Collections;

public class FireBallLauncher : MonoBehaviour {

	public GameObject fireBallPrefab = null;
	public GameObject aim = null;
	public float _startTime = 2.5f;

	public float _time;
	public bool _fast = false;
	private float velocity = 500f;

	// Use this for initialization
	void Start () {
        _time = _startTime;
	}
	
	// Update is called once per frame
	void Update () {
        _time -= Time.deltaTime;

        if (_time <= 0.0f)
        {
           _time = _startTime;
           Create(this.gameObject, fireBallPrefab);
        }
	}


    public void Create(GameObject origin, GameObject prefab)
    {
        GameObject go = (GameObject)Instantiate(prefab);

        if (go != null)
        {
            go.gameObject.tag = "Rock";
            go.transform.position = new Vector3(origin.transform.position.x,
                origin.transform.position.y, origin.transform.position.z);
            //go.rigidbody.AddForce(aim.transform.position - go.transform.position);
			if (_fast)
			{
				go.gameObject.rigidbody.velocity = (aim.gameObject.transform.position - transform.position).normalized * 40.0f;
				//var magnitude = go.rigidbody.velocity.magnitude;
				//if (magnitude != velocity)
				//{
				//	go.rigidbody.velocity = go.rigidbody.velocity * 5.0f;
				//}
    		}
		}
	}
}