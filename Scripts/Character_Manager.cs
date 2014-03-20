using UnityEngine;
using System.Collections;

public class Character_Manager : MonoBehaviour {

	public static CharacterController Instance;

	public	float	deadZone;

	void Awake()
	{
		Instance = this;
		// Camera_Manager.Instance.CheckCameraExists();
	}

	void Update()
	{
		// Camera_Manager.Instance.CheckCameraExists();

		ControllerInput();

		Character_Motor.Instance.MoveVector = Vector3.zero;
	
		ActionInput();

		Character_Motor.Instance.ApplyGravity();

		Character_Motor.Instance.ProcessMotion();
	
		Character_Motor.Instance.ControlledUpdate();
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
			Character_Motor.Instance.MoveVector += new Vector3(0, 0, -vertical);
		}
	}

	void DelegateJump()
	{
		//Character_Animator.Instance.JumpAnimation();
		Character_Motor.Instance.Jump();
	}

	void ActionInput()
	{
		if (Input.GetButton("Jump"))
			DelegateJump();
	}
}
