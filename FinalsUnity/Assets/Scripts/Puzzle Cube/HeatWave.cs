using UnityEngine;
using System.Collections;

public class HeatWave : MonoBehaviour {
    private Vector3 _direction = Vector3.zero;
    private float _speed = 4F;
    private const float _rotationSpeed = 200;

    private static ArrayList    _waves = new ArrayList();

	// Use this for initialization
	void Start () {
        _speed = Random.RandomRange(_speed, _speed * 3);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up * _speed * Time.deltaTime * _rotationSpeed, Space.Self);
	}

    public void setDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public static void Create(GameObject origin, Vector3 direction, GameObject prefab)
    {
        GameObject go = (GameObject) Instantiate(prefab);

        if (go != null)
        {
            HeatWave heatWave = go.AddComponent<HeatWave>();
            go.transform.position = new Vector3(origin.transform.position.x,
                origin.transform.position.y - 2, origin.transform.position.z);
            heatWave.setDirection(direction);
            heatWave.transform.parent = origin.transform;
            _waves.Add(go);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("PuzzleCubePlan");

            other.gameObject.SendMessage("Die");
            go.GetComponent<ResetPuzzle>().Reset();
        }
        else if (other.gameObject.GetComponent<PuzzleCubeDestination>() == null &&
            other.isTrigger == false)
        {
            _waves.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public static void Reset()
    {
        for (int i = 0; i < _waves.ToArray().Length; ++i)
        {
            Destroy((GameObject)_waves[i]);
        }
        _waves.Clear();
    }
}
