using UnityEngine;
using System.Collections;

public class Character_Manager : MonoBehaviour {

	public static CharacterControllerComponent Instance;

	public float	deadZone;

	void Awake()
	{
		Instance = this;
		// Camera_Manager.Instance.CheckCameraExists();
	}

	void Update()
	{
		// Camera_Manager.Instance.CheckCameraExists();
		ControllerInput();
		Character_Motor.Instance.ControlledUpdate();
		Character_Motor.Instance.MoveVector = Vector3.zero;
	}

	void ControllerInput()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal > deadZone || horizontal < -deadZone)
		{
			Character_Motor.Instance.MoveVector += new Vector3(horizontal, 0, 0);
		}

		if (vertical > deadZone || vertical < -deadzone)
		{
			Character_Motor.Instance.MoveVector += new Vector3(0, -vertical, 0);
		}
	}

	void DelegateJump()
	{
		Character_Motor.Instance.Jump();
	}

	void ActionInput()
	{

	}
}
