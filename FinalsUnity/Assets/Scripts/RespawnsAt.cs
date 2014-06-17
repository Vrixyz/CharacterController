using UnityEngine;
using System.Collections;

public class RespawnsAt : MonoBehaviour {

    public Transform respawnTransform_Tuto;
    public Transform respawnTransform_1;
    public Transform respawnTransform_2;
	public Transform respawnTransform_3;
	public enum DieZone
	{
        TUTO_ZONE,
		FIRST_ZONE,
		SECOND_ZONE,
		THIRD_ZONE,
	};
	private DieZone currentZone = DieZone.TUTO_ZONE;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y <=
            GameObject.Find("LavaLake").transform.position.y)
        {
            gameObject.SendMessage("Die");
        }
	}

	void SetZone(DieZone zone)
	{
		currentZone = zone;
	}

	void Die() {
		if (currentZone == DieZone.TUTO_ZONE)
		{
			this.gameObject.transform.position = respawnTransform_Tuto.position;
			this.gameObject.transform.rotation = respawnTransform_Tuto.rotation;
		}
		else if (currentZone == DieZone.FIRST_ZONE)
		{
			this.gameObject.transform.position = respawnTransform_1.position;
			this.gameObject.transform.rotation = respawnTransform_1.rotation;
		}
		else if (currentZone == DieZone.SECOND_ZONE)
		{
			this.gameObject.transform.position = respawnTransform_2.position;
			this.gameObject.transform.rotation = respawnTransform_2.rotation;
		}
		else if (currentZone == DieZone.THIRD_ZONE)
		{
			this.gameObject.transform.position = respawnTransform_3.position;
			this.gameObject.transform.rotation = respawnTransform_3.rotation;
		}
	}

}
