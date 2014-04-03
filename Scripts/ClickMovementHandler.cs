using UnityEngine;
using System.Collections;

/// 
/// ClickMovementHandler class.
/// 
public class ClickMovementHandler : MonoBehaviour 
{
	// assign a player character gameobject.
	public GameObject avatar = null;
	
	// the assigned player character should have MovementController component.
	MovementController moveControl = null;
	
	// Use this for initialization
	void Start () 
	{
		if (avatar)
		{
			moveControl = avatar.gameObject.GetComponent<MovementController>();
			if (moveControl == null)
			{
				Debug.LogError("The gameobject does not have MovementController.");
			}
		}
		else
		{
			Debug.LogError ("");
		}
	}
		
	/// 
	/// LateUpdate is called every frame after all Update functions have been called. 
	/// 
	void LateUpdate()
	{
		InputController.Update();
		
		// Mouse L-Button is down.
		if (InputController.IsLButtonDown)
		{
			// calcuate the position at the mouse is clicked.
		    var ray = Camera.main.ScreenPointToRay(InputController.Position);
		    InputController.CalculateHitPoint(ray);		
					
			if (avatar)
			{
				// move to the given position.
			    moveControl.StartMove(InputController.HitPoint, 1f);
				
			}
			
			InputController.InvalidateLButtonDown();
		}
	}
}
