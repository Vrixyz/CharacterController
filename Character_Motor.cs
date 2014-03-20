using UnityEngine;
using System.Collections;

public class Character_Motor : MonoBehaviour {

    static Character_Motor instance;

    public Vector3 moveVector;
    float speedLimit;
    float jumpStrength;
    float gravityStrength;

    CharacterController controller;

    void Awake()
    {
        Character_Motor.instance = this;
    }
    
    // Use this for initialization
	void Start () {
        speedLimit = 20;
        jumpStrength = 20;
        gravityStrength = 5;
        controller = this.gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	    // we don't use this automated update (see ControlledUpdate)
	}

    void ControlledUpdate()
    {
        AlignCharacterToCameraDirection();
        ApplyGravity();
        ProcessMotion();
    }

    void ProcessMotion()
    {
        moveVector.Normalize();
        moveVector *= speedLimit;
        moveVector *= Time.deltaTime;
        // TOCHECK: move the character !
        // TOCHECK: transform to world point ?
        controller.Move(this.gameObject.transform.TransformDirection(moveVector.x, 0, moveVector.z));
    }
    void AlignCharacterToCameraDirection()
    {
        if (moveVector.x != 0 || moveVector.y != 0 || moveVector.z != 0)
        {
            Camera camera = GameObject.Find("CharacterCamera").camera;
            controller.gameObject.transform.rotation = camera.transform.rotation;
            controller.gameObject.transform.rotation.SetLookRotation(moveVector, new Vector3(0,1,0));
            
            // TOCHECK: we're moving, so move the character so it fits the camera angle
        }
    }

    void Jump()
    {
        if (!controller.isGrounded)
            return;
        moveVector.y = jumpStrength;
        return;
    }

    void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            moveVector.y = -1;
            return;
        }
        moveVector.y -= gravityStrength * Time.deltaTime;
    }

}
