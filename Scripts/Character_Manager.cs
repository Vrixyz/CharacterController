using UnityEngine;
using System.Collections;

public class Character_Manager : MonoBehaviour {

	public static Character_Manager Instance;

	public	float	deadZone;

	void Awake()
	{
		Instance = this;
		Camera_Manager.InitialCameraCheck();
	}

	void Update()
	{
		// Camera_Manager.Instance.CheckCameraExists();

//<<<<<<< HEAD
        Character_Motor.Instance.moveVector = new Vector3(0,  Character_Motor.Instance.moveVector.y, 0);

        ActionInput();
        ControllerInput();

		//Character_Motor.Instance.ProcessMotion();
//=======
//        ControllerInput();

//        Character_Motor.Instance.moveVector = Vector3.zero;
	
//        ActionInput();

//        Character_Motor.Instance.ApplyGravity();

//        Character_Motor.Instance.ProcessMotion();
//>>>>>>> 8ce8792d9b67235787e2aa31b6c8e97385d0f6e5
	
		Character_Motor.Instance.ControlledUpdate();
	}

	void ControllerInput()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal > deadZone || horizontal < -deadZone)
		{
			Character_Motor.Instance.moveVector += new Vector3(horizontal, 0, 0);
		}

		if (vertical > deadZone || vertical < -deadZone)
		{
//<<<<<<< HEAD
			Character_Motor.Instance.moveVector += new Vector3(0, 0, vertical);
//=======
//            Character_Motor.Instance.moveVector += new Vector3(0, 0, -vertical);
//>>>>>>> 8ce8792d9b67235787e2aa31b6c8e97385d0f6e5
		}
        Character_Motor.Instance.VerticalVelocity = Character_Motor.Instance.moveVector.y; 
	}

	void DelegateJump()
	{
		//Character_Animator.Instance.JumpAnimation();
		Character_Motor.Instance.Jump();
	}

	void ActionInput()
	{
		if (Input.GetButtonDown("Jump"))
			DelegateJump();
	}
}
