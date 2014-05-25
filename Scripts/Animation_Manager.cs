using UnityEngine;
using System.Collections;

public class Animation_Manager : MonoBehaviour {
    public static Animation_Manager Instance;

    public void Awake()
    {
        Instance = this;
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
        Dead = 0,
        Jumping,
        Falling,
        Landing,
        Using,
        Climbing,
        Standing,
        Stationary,
        Forward,
        Backward,
        Left,
        Right,
        LeftForward,
        RightForward,
        LeftBackward,
        RightBackward
    }

    public MotionStateList  characterMotionState;
    public AnimationStateList characterAnimationState;

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
            (characterAnimationState != AnimationStateList.Jumping ||
            characterAnimationState != AnimationStateList.Falling))
        {
            characterAnimationState = AnimationStateList.Falling;
            // Do Falling
        }
        if (gameObject.GetComponent<CharacterController>().isGrounded &&
            (characterAnimationState != AnimationStateList.Jumping || // other stuf implying moving
            characterAnimationState != AnimationStateList.Landing ||
            characterAnimationState != AnimationStateList.Climbing ||
            characterAnimationState != AnimationStateList.Using))
        {
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
}
