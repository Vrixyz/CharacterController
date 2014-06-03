using UnityEngine;
using System.Collections;

public class Animation_Manager : MonoBehaviour {
    public static Animation_Manager Instance;

    private Transform climbVolumeTransform = null;

    public void Awake()
    {
        Instance = this;
    }

    public void SetClimbVolumeTransform(Transform climbTransform)
    {
        climbVolumeTransform = climbTransform;
        Character_Manager.Instance.isClimbing = true;
    }

    public void ClearClimbVolumeTransform()
    {
        climbVolumeTransform = null;
        Character_Manager.Instance.isClimbing = false;
    }

    public enum MotionStateList
    {
        Stationary = 0,
        Forward = 1,
        Backward = 2,
        Left = 4,
        Right = 8,
        LeftForward = 1 | 4,
        RightForward = 1 | 8,
        LeftBackward = 2 | 4,
        RightBackward = 2 | 8
    };

    public enum AnimationStateList
    {
        Stationary = 0,
        Jumping,
        Falling,
        Landing,
        Using,
        Climbing,
        Sliding,
        Forward,
        Backward,
        Left,
        Right,
        LeftForward,
        RightForward,
        LeftBackward,
        RightBackward,
        Dead
    }

    public MotionStateList  characterMotionState;
    public AnimationStateList characterAnimationState;
    public AnimationStateList previousCharacterAnimationState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CurrentAnimationState();
        ProcessAnimationState();
	}

    public void CurrentAnimationState()
    {
        if (characterAnimationState == AnimationStateList.Dead)
            return;
        if (!gameObject.GetComponent<CharacterController>().isGrounded &&
            characterAnimationState != AnimationStateList.Jumping &&
            characterAnimationState != AnimationStateList.Falling)
        {
            previousCharacterAnimationState = characterAnimationState;
            characterAnimationState = AnimationStateList.Falling;
            FireFallAnimationState();
        }
        if (gameObject.GetComponent<CharacterController>().isGrounded &&
            (characterAnimationState != AnimationStateList.Jumping && // other stuf implying moving
            characterAnimationState != AnimationStateList.Landing &&
            characterAnimationState != AnimationStateList.Climbing &&
            characterAnimationState != AnimationStateList.Using &&
            characterAnimationState != AnimationStateList.Sliding))
        {
            previousCharacterAnimationState = characterAnimationState;
            switch (characterMotionState)
            {
                case MotionStateList.Stationary:
                    characterAnimationState = AnimationStateList.Stationary;
                    break;
                case MotionStateList.Forward:
                    characterAnimationState = AnimationStateList.Forward;
                    break;
                case MotionStateList.Backward:
                    characterAnimationState = AnimationStateList.Backward;
                    break;
                case MotionStateList.Left:
                    characterAnimationState = AnimationStateList.Left;
                    break;
                case MotionStateList.LeftForward:
                    characterAnimationState = AnimationStateList.LeftForward;
                    break;
                case MotionStateList.Right:
                    characterAnimationState = AnimationStateList.Right;
                    break;
                case MotionStateList.RightForward:
                    characterAnimationState = AnimationStateList.RightForward;
                    break;
                case MotionStateList.LeftBackward:
                    characterAnimationState = AnimationStateList.LeftBackward;
                    break;
                case MotionStateList.RightBackward:
                    characterAnimationState = AnimationStateList.RightBackward;
                    break;
            }
        }
    }

    public void ProcessAnimationState()
    {
        switch (characterAnimationState)
        {
            case AnimationStateList.Stationary:
                AnimationAfterIdleState();
                break;
            case AnimationStateList.Forward:
                AnimationAfterRunState();
                break;
            case AnimationStateList.Backward:
                AnimationAfterRunBackwardsState();
                break;
            case AnimationStateList.Left:
            case AnimationStateList.LeftForward:
                AnimationAfterStrafeRunLeftState();
                break;
            case AnimationStateList.Right:
            case AnimationStateList.RightForward:
                AnimationAfterStrafeRunRightState();
                break;
            case AnimationStateList.LeftBackward:
                AnimationAfterStrafeBackwardsLeftState();
                break;
            case AnimationStateList.RightBackward:
                AnimationAfterStrafeBackwardsRightState();
                break;
            case AnimationStateList.Using:
                AnimationAfterUsingState();
                break;
            case AnimationStateList.Sliding:
                AnimationAfterSlidingState();
                break;
            case AnimationStateList.Jumping:
                AnimationAfterJumpState();
                break;
            case AnimationStateList.Falling:
                AnimationAfterFallState();
                break;
            case AnimationStateList.Landing:
                AnimationAfterLandState();
                break;
        }
    }

    public void AnimationAfterSlidingState()
    {
        if (!gameObject.GetComponent<Character_Motor>().isSliding)
        {
            GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Idle");
        } else if (!GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.IsPlaying("Run")) {
            GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Run");
        }
    }

    public void AnimationAfterUsingState()
    {
        if (!GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.IsPlaying("RunJump"))
        {
            previousCharacterAnimationState = characterAnimationState;
            characterAnimationState = AnimationStateList.Stationary;
        }
    }

    public void AnimationAfterIdleState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Idle");
    }
    public void AnimationAfterRunState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Run");
    }
    public void AnimationAfterRunBackwardsState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("RunBackwards");
    }
    public void AnimationAfterStrafeRunLeftState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("StrafeRunLeft");
    }
    public void AnimationAfterStrafeRunRightState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("StrafeRunRight");
    }
    public void AnimationAfterStrafeBackwardsLeftState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("StrafeBackLeft");
    }
    public void AnimationAfterStrafeBackwardsRightState()
    {
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("StrafeBackRight");
    }
    public void AnimationAfterJumpState()
    {
        if (gameObject.GetComponent<CharacterController>().isGrounded)
        {

            if (previousCharacterAnimationState == AnimationStateList.Forward)
            {
                GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Run"); // RunLand
            }
            else
            {
                GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Idle"); // JumpLand
            }
            characterAnimationState = AnimationStateList.Landing;
        }
        else if (!GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.IsPlaying("RunJump"))
        {
            characterAnimationState = AnimationStateList.Falling;
            GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Fall"); // Fall (should be looped)
        }
    }
    public void AnimationAfterFallState()
    {
        if (gameObject.GetComponent<CharacterController>().isGrounded)
        {
            if (previousCharacterAnimationState == AnimationStateList.Forward)
            {
                GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Run"); // RunLand
            }
            else
            {
                GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("StrafeBackLeft"); // JumpLand
            }
            characterAnimationState = AnimationStateList.Landing;
        }
    }
    public void AnimationAfterLandState()
    {
        if (previousCharacterAnimationState == AnimationStateList.Forward)
        {
            if (!GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.IsPlaying("Run")) // RunLand
            {
                characterAnimationState = AnimationStateList.Forward;
                GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.Play("Run");
            }
        }
        else if (!GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.IsPlaying("StrafeBackLeft")) // JumpLand
        {
            characterAnimationState = AnimationStateList.Stationary;
            GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.Play("Idle");
        }
    }

    public void FireUseAnimationState()
    {
        previousCharacterAnimationState = characterAnimationState;
        characterAnimationState = AnimationStateList.Using;
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("RunJump"); // Use
    }

    public void FireSlideAnimationState()
    {
        previousCharacterAnimationState = characterAnimationState;
        characterAnimationState = AnimationStateList.Sliding;
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Run"); // Slide
    }

    public void FireJumpAnimationState()
    {
        if (!gameObject.GetComponent<CharacterController>().isGrounded ||
            IsDead() || characterAnimationState == AnimationStateList.Jumping)
            return;
        previousCharacterAnimationState = characterAnimationState;
        characterAnimationState = AnimationStateList.Jumping;
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("RunJump");
    }

    public void FireClimbAnimationState()
    {
        if (!gameObject.GetComponent<CharacterController>().isGrounded ||
            IsDead() || climbVolumeTransform == null)
            return;
        print("" + climbVolumeTransform.rotation.y + "//" + transform.rotation.y + " = " + Mathf.Abs(Mathf.Abs(climbVolumeTransform.rotation.y - transform.rotation.y)));
        if (Mathf.Abs(Mathf.Abs(climbVolumeTransform.rotation.y - transform.rotation.y)) > (Mathf.PI / 6F))
            Character_Manager.Instance.DelegateJump();
        else
        {
            previousCharacterAnimationState = characterAnimationState;
            characterAnimationState = AnimationStateList.Climbing;
            GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("StrafeRunRight"); // Climb
        }
    }

    public void FireFallAnimationState()
    {
        if (IsDead())
            return;
        previousCharacterAnimationState = characterAnimationState;
        characterAnimationState = AnimationStateList.Falling;
        GameObject.FindGameObjectWithTag("AnimatedPlayer").animation.CrossFade("Fall");
    }

    public void CurrentMotionState()
    {
        bool front = false;
        bool back = false;
        bool left = false;
        bool right = false;

        Vector3    moveVector = gameObject.GetComponent<Character_Motor>().moveVector;
        front = moveVector.z > 0F;
        back = moveVector.z < 0F;
        right = moveVector.x > 0F;
        left = moveVector.x < 0F;

        characterMotionState = (front ? MotionStateList.Forward : 0) |
            (back ? MotionStateList.Backward : 0) |
            (left ? MotionStateList.Left : 0) |
            (right ? MotionStateList.Right : 0);
    }

    public bool IsDead()
    {
        return (characterAnimationState == AnimationStateList.Dead);
    }
}
