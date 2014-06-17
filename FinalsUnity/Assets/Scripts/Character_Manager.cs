using UnityEngine;
using System.Collections;

public class Character_Manager : MonoBehaviour {

	public static Character_Manager Instance;

	public	float	deadZone;

	private bool jumping = false;

	void Awake()
	{
		Instance = this;
		Camera_Manager.InitialCameraCheck();
	}

	void Update()
	{
		// Camera_Manager.Instance.CheckCameraExists();

        Character_Motor.Instance.moveVector = new Vector3(0,  Character_Motor.Instance.moveVector.y, 0);

        ActionInput();
        ControllerInput();

		Character_Motor.Instance.ControlledUpdate();
	}

	void FixedUpdate()
	{
		if (jumping)
		{
			DelegateJump();
			jumping = false;
		}
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
			Character_Motor.Instance.moveVector += new Vector3(0, 0, vertical);
		}
        Character_Motor.Instance.VerticalVelocity = Character_Motor.Instance.moveVector.y;
        gameObject.GetComponent<Animation_Manager>().CurrentMotionState();
	}

	void DelegateJump()
	{
		//Character_Animator.Instance.JumpAnimation();
		Character_Motor.Instance.Jump();
	}

	void ActionInput()
	{
		if (Input.GetButtonDown("Jump"))
			jumping = true;
	}
}
