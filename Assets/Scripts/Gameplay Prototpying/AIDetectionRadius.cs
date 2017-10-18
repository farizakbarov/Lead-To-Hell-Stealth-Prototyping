using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;
using SWS;
using CoverShooter;

public class AIDetectionRadius : MonoBehaviour
{

    private PlayMakerFSM myFSM;
    public bool PlayerInRadius;
    public bool UseFOVSight = true;
    public TriggerSensor mySensor;
    private LTHMoveAnimator MoveScript;

    public float ThrowableDetectionThreshold1 = 3.0f;

    public bool Testing;

    public bool LastSightingInRadius;

    // Use this for initialization
    void Start()
    {
       myFSM = transform.parent.GetComponent<PlayMakerFSM>();
        MoveScript = transform.parent.GetComponent<LTHMoveAnimator>();

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

			if (GameManager.Singleton.EnableAIHearing) {
                if (myFSM != null)
                {
                    if (GameManager.Singleton.PlayerIsRunning && myFSM.ActiveStateName != "Seeking" && myFSM.ActiveStateName != "Distracted")
                    {
                        //Debug.Log ("Heard Player");
                        GameManager.Singleton.PlayerInSight = true;
                        MoveScript.HeardPlayer(true);
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
            PlayerInRadius = false;
        }

        if (other.tag == "PointOfInterest")
        {
            MoveScript.PointsOfInterest.Remove(other.gameObject);
        }
    }
}
