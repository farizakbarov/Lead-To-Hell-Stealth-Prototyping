using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]

public class CharacterBase : MonoBehaviour {
	
	[HideInInspector]
	public UnityEngine.AI.NavMeshAgent		agent;
	protected Animator			animator;
	protected Locomotion locomotion;

	[HideInInspector]
	public bool turn;



	public bool LookAt;
	[HideInInspector]
	public GameObject LookAtTarget;
	[HideInInspector]
	public float lookWeight;
	public float lookSmoother = 3f;

    public bool RootMotion = true;
	
	
	public virtual void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = true;
		animator = GetComponent<Animator>();
		locomotion = new Locomotion(animator);		

	}

	public void OnAnimatorIK(){
		animator.SetLookAtWeight(lookWeight);
	}

	public virtual void SetupAgentLocomotion()
	{
		if(agent != null && agent.enabled){
			if (AgentDone())
			{
				if(!turn){
					locomotion.Do(0, 0);
				}
			}
			else
			{
				float speed = agent.desiredVelocity.magnitude;

				Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;

				float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

				locomotion.Do(speed, angle);
			}
		}
	}

	//Sets the navmesh destination to a supplied position
	public virtual void SetDestination(Vector3 Pos){
		agent.destination = Pos;
	}

	public void SetLookAtTarget(GameObject obj){
		animator.SetLookAtPosition(obj.transform.position);
	}

	public void SetLookAtWeight(float weight){
		animator.SetLookAtWeight(weight);
	}

	public void SetRightHandIKWeight(float weight){
		animator.SetIKPositionWeight(AvatarIKGoal.RightHand,weight);
		animator.SetIKRotationWeight(AvatarIKGoal.RightHand,weight);
	}

	public void SetRightHandIKTarget(GameObject obj){
		animator.SetIKPosition(AvatarIKGoal.RightHand,obj.transform.position);
		animator.SetIKRotation(AvatarIKGoal.RightHand,obj.transform.rotation);
	}

	public void SetLeftHandIKWeight(float weight){
		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,weight);
		animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,weight);
	}
	
	public void SetLeftHandIKTarget(GameObject obj){
		animator.SetIKPosition(AvatarIKGoal.LeftHand,obj.transform.position);
		animator.SetIKRotation(AvatarIKGoal.LeftHand,obj.transform.rotation);
	}
	
	public void ChangeSpeed(float speed){
		agent.speed = speed;		
	}
	
	public virtual void OnAnimatorMove()
    {
        if (RootMotion)
        {
            agent.velocity = animator.deltaPosition / Time.deltaTime;
            transform.rotation = animator.rootRotation;
        }
        else
        {
           // agent.velocity = animator.deltaPosition / Time.deltaTime;
            transform.position = agent.nextPosition;
        }
		
    }
	
	protected bool AgentDone()
	{
        if (agent.enabled == true)
        {
            return !agent.pathPending && AgentStopping();
        }
        else
        {
            return false;
        }
    }

	protected bool AgentStopping()
	{
        if (agent.enabled == true)
        {
            return agent.remainingDistance <= agent.stoppingDistance;
        }
        else{
            return false;
        }
	}

	//function for using the Mecanim animations to turn around on the spot to face a direction
	public void TurnOnSpot(Transform TurnTarget){
		turn = true;
		//calulate the angle between the Player and the target
		float angle = Vector3.Angle(this.transform.forward, TurnTarget.forward);

		//find out if we need to turn CW or CCW
		//float direction = AngleDir(this.transform.forward, TurnTarget.forward, Vector3.up);

		float sign = (Vector3.Dot(TurnTarget.forward, this.transform.forward) > 0.0f) ? 1.0f: -1.0f;
		//Debug.Log (angle);
		if(angle > 2){
			locomotion.Do(0,angle * sign);
		}
		else{
			turn = false;
		}

				
	}
	//overloaded method that uses a Vector3 instead of a full transform, however it still uses the Characters Transform.forward so you still need to provide another transform.forward to move to
	public void TurnOnSpot(Vector3 TurnTarget){
		turn = true;
		//calulate the angle between the Player and the target
		float angle = Vector3.Angle(this.transform.forward, TurnTarget);
		
		//find out if we need to turn CW or CCW
		//float direction = AngleDir(this.transform.forward, TurnTarget.forward, Vector3.up);
		
		float sign = (Vector3.Dot(TurnTarget, this.transform.forward) > 0.0f) ? 1.0f: -1.0f;

		if(angle > 2){
			locomotion.Do(0,sign * angle);
		}
		else{
			turn = false;
		}

		
	}

	public virtual void Update(){
//		animator.SetLookAtWeight(lookWeight);	
		if(LookAt){
			if(LookAtTarget == null){
				animator.SetLookAtPosition(transform.Find("Look_At_Target").transform.position);
			}
			else{
				animator.SetLookAtPosition(LookAtTarget.transform.position);
			}
			
			lookWeight = Mathf.Lerp(lookWeight,1f,Time.deltaTime*lookSmoother);
		}
		else{
			lookWeight = Mathf.Lerp(lookWeight,0f,Time.deltaTime*lookSmoother);
		}
	}

}
