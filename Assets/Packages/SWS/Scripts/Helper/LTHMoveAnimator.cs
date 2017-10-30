﻿/*  This file is part of the "Simple Waypoint System" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using PlayMaker;
using SWS;
using UnityEngine.UI;
#if UNITY_5_5_OR_NEWER
using UnityEngine.AI;
#endif


/// <summary>
/// Mecanim motion animator for movement scripts.
/// Passes speed and direction to the Mecanim controller. 
/// <summary>
/// 

public class LTHMoveAnimator : MonoBehaviour
{
    #region #fields
    //movement script references
    private splineMove sMove;
    //private NavMeshAgent nAgent;
    //Mecanim animator reference
    private Animator animator;
    //cached y-rotation on tweens
    private float lastRotY;
    protected Locomotion locomotion;

    private navMove NavMoveScript;
    private AIDetectionRadius RadiusScript;

    public Transform HomeLocation;
    public float StopSpeed = 1.5f;

    //IK Look at Varibles
    private bool LookAt;
    public GameObject LookAtTarget;
    private float lookWeight;
    public float lookSmoother = 3f;

    public enum AITypes { Type1, Type2 };
    public float RunSpeed = 4.2f;
    public List<GameObject> ListOfSweepDirections = new List<GameObject>();

    private PlayMakerFSM FSM;
    private NavMeshAgent agent;



    private bool MovingToDestination;

    private float AgentSpeed;
    private float AgentStoppingDistance;
    private bool isSeeking;
    private bool TurnOnSpot;
    private GameObject ObjectToTurnTo;

    float Type2distance = Mathf.Infinity;

    GameObject NearestType2;
    private bool Sweeping;
    private bool SweepTurn;
    private int SweepIterator = 0;

    private bool TurnAroundSwitch;
    private float angle;

    Vector3 lastAgentVelocity;
    NavMeshPath lastAgentPath;

    // bool FirstStart = true;

    [HideInInspector]
    public GameObject myIndicator;
    public Vector3 IndicatorOffset;


    public float PointsOfIntrestTimer = 2.0f;
    public List<GameObject> PointsOfInterest = new List<GameObject>();
    private int PointOfIntrestIndex;
    private bool LookingForPlayer;

    Coroutine co;

    private bool MoveFlip;

    Vector3 NextPoint;

    #endregion
    //public GameObject TestObject;
    private bool MatchForward;
    private bool DetectPlayer;


    public Image AlertBar;
    public float AlertLevel;
    //public float AlertLevelPercentage;


    //getting component references
    void Start()
    {
        GameManager.Singleton.AllAi.Add(this.transform);

        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        AgentSpeed = agent.speed;
        AgentStoppingDistance = agent.stoppingDistance;
        locomotion = new Locomotion(animator);

        RadiusScript = GetComponentInChildren<AIDetectionRadius>();
        NavMoveScript = GetComponent<navMove>();

        sMove = GetComponent<splineMove>();
        /* if (!sMove)
             // nAgent = GetComponent<NavMeshAgent>();*/

        FSM = GetComponent<PlayMakerFSM>();




        //Add the AI to the lists in the gamemangaer
        if (FSM != null)
        {
            if (FSM.Fsm.Variables.GetFsmEnum("AI Type").ToString() == "Type1")
            {
                GameManager.Singleton.AddType1(this.gameObject);
            }
            else
            {
                GameManager.Singleton.AddType2(this.gameObject);
            }

            FSM.Fsm.Variables.GetFsmGameObject("Player").Value = GameManager.Singleton.ActivePlayer;
        }

        //InvokeRepeating("IteratePoint", 0.0f, PointsOfIntrestTimer);

        // HomeLocation = FSM.Fsm.Variables.GetFsmGameObject("StartLocation").Value;
    }

    //Function called by the FSM to move the AI back to his home location
    public void ReturnToDestination()
    {
        //for some reason an null error happens with "HomeLocation" when the game starts up, so this first start bypasses it the first time.
        /* if (FirstStart)
         {
             if (FSM != null)
             {
                 FSM.Fsm.Event("ARRIVED");
             }
             FirstStart = false;                
         }
         else
         {
             if (HomeLocation != null)
             {
                 agent.SetDestination(HomeLocation.position);
                 agent.isStopped = false;
                 MovingToDestination = true;
             }
         }*/
        // Debug.Log("Return");
        if (HomeLocation != null)
        {
            agent.SetDestination(HomeLocation.position);
            NextPoint = HomeLocation.position;
            agent.isStopped = false;
            MovingToDestination = true;
        }
    }

    //Function the FSM calls for Running after the Player after he has been spotted
    public void Seek(GameObject location)
    {
        if (location != null)
        {

            TurnOnSpot = false;
            TurnAroundSwitch = false;
            Sweeping = false;
            SweepTurn = false;
            agent.isStopped = false;
            agent.speed = RunSpeed;
            //agent.stoppingDistance = 2;
            MovingToDestination = true;
            isSeeking = true;
        }
    }

    //Function the FSM calls for Running after the Player after he has been spotted
    public void SeekPlayer()
    {
            //Debug.Log("Begin seeking PlaYER");
            TurnOnSpot = false;
            TurnAroundSwitch = false;
            Sweeping = false;
            SweepTurn = false;
            agent.isStopped = false;
            agent.speed = RunSpeed;
            MovingToDestination = true;
            isSeeking = true;
    }

    //FSM calls these functions to begin and stop sweeping
    public void BeginSweeping()
    {
        Sweeping = true;
        SweepTurn = true;
    }

    public void StopSweeping()
    {
        Sweeping = false;
        SweepTurn = false;
    }

    //Function the FSM calls to turn around 180 degress
    public void TurnAround()
    {
        //  Debug.Log("Turn Around Called");
        //if the player is still inside the AI radius, and has not already triggered the Rear FOV Event
        if (RadiusScript.PlayerInRadius)
        {
            NavMoveScript.Pause();
            Sweeping = false;
            SweepTurn = false;
            MovingToDestination = false;
            TurnOnSpot = true;
            TurnAroundSwitch = true;
        }
        else
        {
            FSM.Fsm.Event("LOSTPLAYER");
        }
    }

    //Fuction the FSM calls to turn to match a supplied gameobject
    public void MatchRotation()
    {
        TurnOnSpot = true;
        MatchForward = true;
        ObjectToTurnTo = HomeLocation.gameObject;
    }

    public void TurnToPlayer()
    {
        TurnOnSpot = true;
        MatchForward = false;
        ObjectToTurnTo = GameManager.Singleton.ActivePlayer;
    }

    //function that actually turns the character.
    public void Turn()
    {

        if (ObjectToTurnTo != null)
        {
            if (MatchForward)
            {
                //calulate the angle and Direction between the two objects
                angle = Vector3.Angle(this.transform.forward, ObjectToTurnTo.transform.forward);
                if (Vector3.Cross(this.transform.forward, ObjectToTurnTo.transform.forward).y < 0)
                {
                    angle = -angle;
                }
            }
            else
            {
                Vector3 targetDir = ObjectToTurnTo.transform.position - transform.position;
                angle = Vector3.Angle(targetDir, transform.forward);

                if (Vector3.Cross(this.transform.forward, targetDir).y < 0)
                {
                    angle = -angle;
                }
            }
        }

        //Tell the animator to rotate using the angle
        locomotion.Do(0, angle);

        //When the angle is less than 3, stop turning.
        if (Mathf.Abs(angle) < 10)
        {
            TurnOnSpot = false;
            TurnAroundSwitch = false;
        }
    }

    public void Sweep(GameObject obj)
    {
        if (SweepTurn)
        {
            //calulate the angle and Direction between the two objects
            float angle = Vector3.Angle(this.transform.forward, obj.transform.forward);
            if (Vector3.Cross(this.transform.forward, obj.transform.forward).y < 0)
            {
                angle = -angle;
            }

            //Tell the animator to rotate using the angle
            locomotion.Do(0, angle);

            //When the angle is less than 3, stop turning.
            if (Mathf.Abs(angle) < 10)
            {
                //iterate or reset the iterator so next time AI will rotate to the next direciton
                if (SweepIterator < (ListOfSweepDirections.Count - 1))
                {
                    SweepIterator++;
                }
                else
                {
                    SweepIterator = 0;
                }
            }
            SweepTurn = false;
            FSM.Fsm.Event("DONETURNING");
        }
        else
        {
            locomotion.Do(0, 0);
        }
    }


    public void LookForPlayer()
    {
        if (PointsOfInterest.Count > 0)
        {
            //if the index is higher than the actual amout of points, reset it to the max
            if (PointOfIntrestIndex > PointsOfInterest.Count)
            {
                PointOfIntrestIndex = PointsOfInterest.Count;
            }


            //Move the AI to the first Point of interest.
            if (!MoveFlip)
            {
                agent.isStopped = false;
                MovingToDestination = true;
                NextPoint = PointsOfInterest[0].transform.position;
                MoveFlip = true;
            }
        }
    }

    //function for randomly picking another point of interest to move to
    public void MoveToNextPoint()
    {
        int randomNum = Random.Range(0, PointsOfInterest.Count);
        PointOfIntrestIndex = randomNum;
        NextPoint = PointsOfInterest[PointOfIntrestIndex].transform.position;
        MovingToDestination = true;
    }

    private void Update()
    {

        //AlertLe0velPercentage = AlertLevel / 1.0f;

        if (AlertBar != null)
        {
            AlertBar.fillAmount = AlertLevel;
        }

        if (AlertLevel > 1)
        {
            AlertLevel = 1;
        }

        if (AlertLevel < 0)
        {
            AlertLevel = 0;
        }

        if (MovingToDestination)
        {
            //if the AI is seeking out the Player
            if (isSeeking)
            {
                agent.SetDestination(GameManager.Singleton.LastSighting.transform.position);
                //Debug.Log("Seeking");
            }

            //If the AI is searching around points of interest for the player
            if (LookingForPlayer)
            {
                agent.SetDestination(NextPoint);
            }


            if (AgentDone() && animator.speed < StopSpeed)
            {
                if (!LookingForPlayer)
                {
                   
                    //Debug.Log("aRRIVED");
                    FSM.Fsm.Event("ARRIVED");
                    
                    isSeeking = false;
                    MovingToDestination = false;
                    agent.speed = AgentSpeed;
                    agent.stoppingDistance = AgentStoppingDistance;
                }

                if (LookingForPlayer)
                {
                    Invoke("MoveToNextPoint", PointsOfIntrestTimer);
                    MovingToDestination = false;
                }
            }
        }

        if (TurnOnSpot)
        {
            Turn();
        }
        else if (Sweeping)
        {
            Sweep(ListOfSweepDirections[SweepIterator]);
        }
        else
        {
            SetupAgentLocomotion();
        }

        if (LookAt)
        {
            lookWeight = Mathf.Lerp(lookWeight, 1f, Time.deltaTime * lookSmoother);
        }
        else
        {
            lookWeight = Mathf.Lerp(lookWeight, 0f, Time.deltaTime * lookSmoother);
        }

        if (FSM != null)
        {
            if (FSM.ActiveStateName == "Looking For Player")
            {
                LookForPlayer();
                LookingForPlayer = true;
            }
            else
            {
                LookingForPlayer = false;
                MoveFlip = false;
            }
        }

        if (FSM != null)
        {
            if (myIndicator != null)
            {
                if (FSM.ActiveStateName == "Alert")
                {
                    myIndicator.GetComponent<Image>().color = new Color(1.0F, 0.5F, 0.0F, 1.0F);
                }
                else if (FSM.ActiveStateName == "Seeking")
                {
                    myIndicator.GetComponent<Image>().color = Color.red;
                }
                else if (FSM.ActiveStateName == "Wait - Lost Player" || FSM.ActiveStateName == "Wait - Lost Player 2")
                {
                    myIndicator.GetComponent<Image>().color = Color.gray;
                }
                else if (FSM.ActiveStateName == "Looking For Player")
                {
                    myIndicator.GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    myIndicator.GetComponent<Image>().color = Color.clear;
                }

                //myIndicator.transform.localPosition = myIndicator.transform.localPosition + IndicatorOffset;
            }
        }


    }

    // Function for checking to see if the Agent has finished moving to their destination
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

    //fuction for checking to see if the Agent is about to stop
    protected bool AgentStopping()
    {
        if (agent.enabled == true)
        {
            return agent.remainingDistance <= agent.stoppingDistance;
        }
        else
        {
            return false;
        }
    }

    //Function for checking to see if the player is still in sight, if so he is caught
    void CheckIfCaught()
    {
        
        if (RadiusScript.PlayerInRadius)
        {
            if (GameManager.Singleton.PlayerSafe)
            {
                GameManager.Singleton.ActivePlayer.GetComponent<SafeZone_Detect>().ExitSafeZone();
            }
        }

        //Debug.Log(GameManager.Singleton.PlayerInSight);
        if (GameManager.Singleton.PlayerInSight)
        {
            FSM.Fsm.Event("CAUGHT");

        }
        else
        {
            if (NavMoveScript != null)
            {
                NavMoveScript.Pause();
            }
            agent.isStopped = true;
            
        }
    }


    //Function the FSM calls During the end of the Alert state timer, Checks to see if the player is still in sight or not.
    public void AlertCheck()
    {
        if (GameManager.Singleton.PlayerInSight)
        {
            FSM.Fsm.Event("NOTICED");
        }
        else
        {
            FSM.Fsm.Event("LOSTPLAYER");
        }
    }

    //Function for telling the animator to animate, and making the nav mesh move at the same speed as the animations are moving.
    public virtual void SetupAgentLocomotion()
    {
        if (agent != null && agent.enabled)
        {
            if (AgentDone())
            {
                locomotion.Do(0, 0);
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

    //method override for root motion on the animator
    void OnAnimatorMove()
    {
        //init variables
        float speed = 0f;
        float angle = 0f;

        //calculate variables based on movement script:
        //for splineMove and bezierMove, speed and rotation are regulated by the tween.
        //here we just get the tween's speed and calculate the rotation difference to the last frame.
        //navmesh agents have their own type of movement which has to be calculated separately.
        if (sMove)
        {
            speed = sMove.tween == null || !sMove.tween.IsPlaying() ? 0f : sMove.speed;
            angle = (transform.eulerAngles.y - lastRotY) * 10;
            lastRotY = transform.eulerAngles.y;

            //push variables to the animator with some optional damping
            animator.SetFloat("Speed", speed);
            animator.SetFloat("Direction", angle, 0.15f, Time.deltaTime);
        }
        else
        {
            //speed = nAgent.velocity.magnitude;
            // Vector3 velocity = Quaternion.Inverse(transform.rotation) * nAgent.desiredVelocity;
            //angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

            agent.velocity = animator.deltaPosition / Time.deltaTime;
            transform.rotation = animator.rootRotation;
        }
    }

    //function for finding the closest type 2 to call him over
    public void FindNearestType2()
    {

        //loop through all the type 2s in the scene
        foreach (GameObject obj in GameManager.Singleton.ListOfType2s)
        {
            float distance2 = Vector3.Distance(this.transform.position, obj.transform.position);

            //if the distance to the Type2 is less than the previously registered distance, store it
            if (distance2 < Type2distance)
            {
                NearestType2 = obj;
                Type2distance = distance2;
            }
        }
        // Debug.Log("Nearest Type 2" + NearestType2);
        //tell that Type 2 to seek out the player
        NearestType2.GetComponent<PlayMakerFSM>().Fsm.Event("SPOTTED");
    }


    //Function the FSM calls During the end of the Alert state timer, Checks to see if the player is still in sight or not.
    public void HearingCheck()
    {
        if (DetectPlayer)
        {
            if (GameManager.Singleton.PlayerInSight)
            {
                FSM.Fsm.Event("SPOTTED");
            }
            else
            {
                FSM.Fsm.Event("LOSTPLAYER");
            }
        }
        else
        {
            FSM.Fsm.Event("SPOTTED");
        }
    }

    public void HeardPlayer(bool DetectPlayer1)
    {
        FSM.Fsm.Event("HEARDPLAYER");
        DetectPlayer = DetectPlayer1;

    }


    public void GameOver()
    {
        GameManager.Singleton.GameOver();
    }

    //functions for turning on and setting up the IK look at
    public void TrackPlayer
        ()
    {
        LookAt = true;
        LookAtTarget = GameManager.Singleton.LastSighting;
    }

    public void StopTracking()
    {
        LookAt = false;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (LookAt)
        {
            if (LookAtTarget != null)
            {
                animator.SetLookAtPosition(LookAtTarget.transform.position + new Vector3(0, 1.5f, 0));
            }

        }
        animator.SetLookAtWeight(lookWeight);
    }

    //Function for waypoint system events use to wait at a waypoint
    public void WayPointWait(float time)
    {
        agent.isStopped = true;
        NavMoveScript.Pause();
        StartCoroutine(WaitAndPrint(time));
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // print("WaitAndPrint " + Time.time);
        NavMoveScript.Resume();
        agent.isStopped = false;
    }

    //Function for waypoint system events use to Turn towards a supplied gameobject
    public void WayPointTurn(GameObject obj)
    {
        TurnOnSpot = true;
        ObjectToTurnTo = obj;
    }
}

