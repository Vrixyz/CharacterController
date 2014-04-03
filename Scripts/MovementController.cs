using UnityEngine;
using System.Collections;

/// 
/// Handles NavMeshAgent's movement.
/// 
public class MovementController : MonoBehaviour 
{
	private Transform tm = null;
	private NavMeshAgent agent = null;
	
	public Vector3? TargetPosition { get; private set; }
	public Vector3 Velocity { get { return agent.velocity; } }
	
	private float stoppingDistanceSquared = 0.01f; // = 0.1 * 0.1
	
	private Animation animation = null;
	private string idleAnimName = "idle";
	private string walkAnimName = "walk";	
	
	// Use this for initialization
	void Start () 
	{
		this.tm = this.gameObject.GetComponent<Transform>();
		
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		if (agent == null)
		{
			Debug.Log ("Failed to get NavMeshAgent component.");
		}
		
		this.animation = this.gameObject.GetComponent<Animation>();
		if (this.animation != null)
		{
			this.animation[idleAnimName].wrapMode = WrapMode.Loop;
			this.animation[walkAnimName].wrapMode = WrapMode.Loop;
			
			this.animation.CrossFade(idleAnimName);
		}
	}

	/// 
	/// Stop movement if the player is whithin the stoppingDistanceSquared
	/// 
	void Update()
	{
		// more fast than using Vector3.Distance()
		// See the following page for more details:
		//    http://docs.unity3d.com/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html
		var dist = this.agent.destination - this.tm.position;
		if (dist.sqrMagnitude < stoppingDistanceSquared)
		{
			this.StopMove();
		}
	}
	
    // Start to move the agent
	public void StartMove(Vector3 position, float stopDistance)
	{
		this.animation.CrossFade(walkAnimName, 0.2f, PlayMode.StopAll);
		
		TargetPosition = position;
		
		agent.SetDestination(TargetPosition.Value);
	}
	
	// Stop movement of the agent.
	public void StopMove()
	{
		this.animation.CrossFade(idleAnimName, 0.1f, PlayMode.StopAll);
		
		agent.Stop();
	}
}
