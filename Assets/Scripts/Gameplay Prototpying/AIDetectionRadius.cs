using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;
using SWS;
using CoverShooter;
using UnityEngine.AI;

public class AIDetectionRadius : MonoBehaviour
{

    private PlayMakerFSM myFSM;
    public bool PlayerInRadius;
    public bool UseFOVSight = true;
    public TriggerSensor mySensor;
    private LTHMoveAnimator MoveScript;
    private SphereCollider col;

    public float ThrowableDetectionThreshold1 = 3.0f;

    public bool LastSightingInRadius;

    NavMeshAgent nav;

    // Use this for initialization
    void Start()
    {
       myFSM = transform.parent.GetComponent<PlayMakerFSM>();
        MoveScript = transform.parent.GetComponent<LTHMoveAnimator>();
        nav = transform.parent.GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInRadius = true;
        }

        if(other.tag == "PointOfInterest")
        {
            MoveScript.PointsOfInterest.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (UseFOVSight)
            {
                if (myFSM.ActiveStateName != "Distracted")
                {
                    //if the player is visble by their FOV sensor, they are spotted
                    if (mySensor.GetVisibility(GameManager.Singleton.ActivePlayer) > 0.5f)
                    //if(mySensor.IsDetected(GameManager.Singleton.ActivePlayer))
                    {
                        // Debug.Log("Spotted");
                        GameManager.Singleton.PlayerInSight = true;
                    }
                    else
                    {
                        GameManager.Singleton.PlayerInSight = false;
                    }
                }
            }

            //Debug.Log(CalculatePathLength(GameManager.Singleton.ActivePlayer.transform.position));

            if (GameManager.Singleton.LTH_GameSettings.EnableAIHearing) {
                if (myFSM != null)
                {
                    if (GameManager.Singleton.PlayerIsRunning && myFSM.ActiveStateName != "Seeking" && myFSM.ActiveStateName != "Distracted")
                    {
                       
                        if (CalculatePathLength(GameManager.Singleton.ActivePlayer.transform.position) <= col.radius)
                        {
                            Debug.Log("Heard Player");
                            GameManager.Singleton.PlayerInSight = true;
                            MoveScript.HeardPlayer(true);
                        }
                    }
                }
			}
        }

        if (other.tag == "Throwable")
        {
            if (myFSM != null)
            {
                //ignore the thrown object if already seeking out the player
                if (myFSM.ActiveStateName != "Seeking")
                {
                    //Detect the thrown object while it is moving.
                    if (other.GetComponent<Grenade>().myVelocity > ThrowableDetectionThreshold1)
                    {
                        GameManager.Singleton.LastSighting.transform.position = other.transform.position;
                        MoveScript.HeardPlayer(false);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if the player exits the Radius, he is out of sight.
        if (other.tag == "Player")
        {
            GameManager.Singleton.PlayerInSight = false;
            //other.GetComponent<BakeMesh>().BakeGhostMesh();
            PlayerInRadius = false;
        }

        if (other.tag == "PointOfInterest")
        {
            MoveScript.PointsOfInterest.Remove(other.gameObject);
        }
    }

    float CalculatePathLength(Vector3 TargetPos)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(TargetPos, path);
        }

        Vector3[] allWaypoints = new Vector3[path.corners.Length + 2];

        allWaypoints[0] = transform.position;
        allWaypoints[allWaypoints.Length - 1] = TargetPos;

        for (int i=0; i<path.corners.Length; i++)
        {
            allWaypoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWaypoints.Length -1; i++)
        {
            pathLength += Vector3.Distance(allWaypoints[i], allWaypoints[i + 1]);
        }

        return pathLength;

    }
}


