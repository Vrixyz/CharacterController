using UnityEngine;
using System.Collections;

/// 
/// The class which handles inputs.
/// 
public static class InputController 
{
    public static bool IsLButtonDown { get; private set; }
	public static bool IsRButtonDown { get; private set; }
	
	public static Vector2 Position { get; private set; }
	public static Vector3 HitPoint { get; private set; }
	
	private static Vector2? ClickedPosition;
	
	private static bool previousLButtonDown;
	private static bool previousRButtonDown;
	
	static InputController()
	{
		// center position on the screen.
		Position = new Vector2(Screen.width, Screen.height) * 0.5f;
	}
	
	public static void InvalidateLButtonDown() 
	{ 
		IsLButtonDown = false; 
	}
	
	public static void InvalidateRButtonDown() 
	{ 
		IsRButtonDown = false; 
	}
	
	private static void Drag()
	{
		;
	}
	
	/// 
	/// Calcuate hit point from the given ray.
	/// 
	public static void CalculateHitPoint(Ray ray)
	{
		RaycastHit hit;
		
		HitPoint = Vector3.zero; 
		
		var ignoreMask = 0x00000004;
		if (Physics.Raycast(ray, out hit, float.MaxValue, ~(ignoreMask)))
		{
			HitPoint = hit.point;
			
			//Debug.Log ("HitPoint:" + HitPoint.ToString());
		}
		else
		{
			float d;
			Plane p = new Plane(Vector3.up, 0); 
			
			if (p.Raycast(ray, out d))
				HitPoint = ray.origin + ray.direction * d;
			
			//Debug.Log ("HitPoint:" + HitPoint.ToString());
		}
		
	}
	
	/// 
	/// This should be called within any update() funtion.
	/// 
	public static void Update()
	{
		
		Position = Input.mousePosition;
		IsLButtonDown = false;
		
		// left mouse down.
		if (Input.GetMouseButton(0))
		{
			if (ClickedPosition == null)
			{
				ClickedPosition = Position;
			}
			else
			{
				// mouse l-button is dowdn on the previous update then move cursor now 
				// which means it starts to drag.
                Drag();
			}
		}
		else 
		{
			// mouse l-button is down on the previous update and does not move the cursor
			// so ClickedPoisition is not nuill
			if (ClickedPosition != null)
			{
				// mouse l-button is down.
				IsLButtonDown = true;
			}
			
			ClickedPosition = null;
		}
		
		IsRButtonDown = Input.GetMouseButton(1) && !previousRButtonDown;
		
		previousLButtonDown = Input.GetMouseButton(0);
		previousRButtonDown = Input.GetMouseButton(1);
	}
}
