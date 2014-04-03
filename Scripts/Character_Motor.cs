using UnityEngine;
using System.Collections;

public class Character_Motor : MonoBehaviour {

    public static Character_Motor Instance;

    public Vector3 moveVector;
    public float VerticalVelocity;
    public float jumpStrength = 20;
    public float gravityStrength = 5;

    public float speedLimitFalling = 20F;
    public float speedLimitForward = 20F;
    public float speedLimitBackward = 20F;
    public float speedLimitSliding = 20F;
    public float speedLimitSideway = 20F;

    CharacterController controller;
    public Vector3 slideVector;

    void Awake()
    {
        Character_Motor.Instance = this;
    }
    
    // Use this for initialization
	void Start () {
        controller = this.gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
	    // we don't use this automated update (see ControlledUpdate)
	}

    public void ControlledUpdate()
    {
        ProcessMotion();
    }

    public float SpeedLimit()
    {
        switch (gameObject.GetComponent<Animation_Manager>().characterMotionState) {
            case Animation_Manager.MotionStateList.Forward:
            case Animation_Manager.MotionStateList.LeftForward :
            case Animation_Manager.MotionStateList.RightForward:
                {
                    return speedLimitForward;
                }
            case Animation_Manager.MotionStateList.Backward:
            case Animation_Manager.MotionStateList.LeftBackward:
            case Animation_Manager.MotionStateList.RightBackward:
                {
                    return speedLimitBackward;
                }
            case Animation_Manager.MotionStateList.Left:
            case Animation_Manager.MotionStateList.Right:
                {
                    return speedLimitSideway;
                }
        }
        if (!controller.isGrounded)
            return speedLimitFalling;
        if (slideVector.magnitude > 0)
            return speedLimitSliding;
        return 0F;
    }

    public void ProcessMotion()
    {
        moveVector.Normalize();
        moveVector *= SpeedLimit();
        moveVector *= Time.deltaTime;
        moveVector.y = VerticalVelocity;
        AlignCharacterToCameraDirection();
        moveVector = this.gameObject.transform.TransformDirection(moveVector.x, moveVector.y, moveVector.z);
        Slide();
        controller.Move(moveVector);
        ApplyGravity();
    }

    void AlignCharacterToCameraDirection()
    {
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            Camera_Manager camera = Camera.main.gameObject.GetComponent<Camera_Manager>();
            Quaternion rotation = Camera.main.transform.rotation;
            rotation.x = 0;
            rotation.z = 0;
            controller.gameObject.transform.rotation = rotation;
            controller.gameObject.transform.rotation.SetLookRotation(moveVector, new Vector3(0, 1, 0));
            camera.UpdatePosition();
            camera.ApplyCameraPosition();
        }
    }

    public void Jump()
    {
        if (!controller.isGrounded)
            return;
        moveVector.y = jumpStrength * Time.deltaTime;
        return;
    }

    public void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            moveVector.y = -1;
            return;
        }

        moveVector.y -= gravityStrength * Time.deltaTime;
    }

    public void Slide()
    {
        slideVector = Vector3.zero;
        if (!controller.isGrounded)
        {
            return;
        }
        Vector3 raycastPosition = new Vector3(controller.transform.position.x,
            controller.transform.position.y + 1,
            controller.transform.position.z);

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(raycastPosition, Vector3.down, out hitInfo, 2))
        {
            if (hitInfo.normal.y < 0.99F)
            {
                slideVector = hitInfo.normal;
                slideVector.y = -slideVector.y;
                slideVector *= SpeedLimit();
                if (hitInfo.normal.y < 0.7F)
                {
                    moveVector = slideVector * Time.deltaTime;
                }
                else
                {
                    moveVector += slideVector * Time.deltaTime;
                }
            }
        }
    }
}
