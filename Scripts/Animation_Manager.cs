using UnityEngine;
using System.Collections;

public class Animation_Manager : MonoBehaviour {

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

    public MotionStateList  characterMotionState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
        print(characterMotionState);
    }
}
