﻿using UnityEngine;
using System.Collections;

public class Character_Motor : MonoBehaviour {

    public static Character_Motor Instance;

    public Vector3 moveVector;
    public float VerticalVelocity;
    public float speedLimit = 20;
    public float jumpStrength = 20;
    public float gravityStrength = 5;

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
       // AlignCharacterToCameraDirection();
        ProcessMotion();
    }

    public void ProcessMotion()
    {
        moveVector.Normalize();
        moveVector *= speedLimit;
        moveVector *= Time.deltaTime;
        moveVector.y = VerticalVelocity;
        Slide();
        controller.Move(this.gameObject.transform.TransformDirection(moveVector.x, moveVector.y, moveVector.z));
        ApplyGravity();
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
            print(hitInfo.normal);
            if (hitInfo.normal.y < 0.99F)
            {
                slideVector = hitInfo.normal;
                slideVector.y = -1 + slideVector.y;
                moveVector += slideVector * Time.deltaTime;
            }
        }
    }
}
