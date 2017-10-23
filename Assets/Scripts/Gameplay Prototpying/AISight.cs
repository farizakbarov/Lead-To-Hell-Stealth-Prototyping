﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;
using SensorToolkit;

public class AISight : MonoBehaviour
{

    private PlayMakerFSM myFSM;
    public bool DectectionOn = true;
    public bool RearFOV;
    private TriggerSensor mySensor;
    public LTHMoveAnimator MainAIScript;


    public bool UseAlertBar = true;

    public float AlertAdd = 0.01f;
    public float CoolDown = 0.01f;
    private float DistanceToPlayer;
    private float DistanceModifier;
    public float DistanceClose = 5f;
    public float DistnaceFar = 15f;

    private float ShadowModifier;
    public float ShadowBonus = 0.25f;


    // Use this for initialization
    void Start()
    {
        myFSM = transform.GetComponentInParent<PlayMakerFSM>();
        mySensor = GetComponent<TriggerSensor>();


    }

    // Update is called once per frame
    void Update()
    {


        if (MainAIScript != null)
        {

            //Enable the alert bar if the alert level is above 0
            if (MainAIScript.AlertLevel > 0)
            {
                MainAIScript.AlertBar.transform.parent.gameObject.SetActive(true);
            }

            // hide the alert bar if the alert level is 0
            if (MainAIScript.AlertLevel == 0)
            {
                MainAIScript.AlertBar.transform.parent.gameObject.SetActive(false);
            }

        }

        if (UseAlertBar)
        {
            //find out the distance to the player
            DistanceToPlayer = Vector3.Distance(transform.position, GameManager.Singleton.ActivePlayer.transform.position);


            //modify the DistanceModifier value based upon how close/far away the player is
            if (DistanceToPlayer <= DistanceClose)
            {
                DistanceModifier = 1.5f;
                // Debug.Log("near");
            }
            else if (DistanceToPlayer >= DistnaceFar)
            {
                DistanceModifier = 0.25f;
            }
            else
            {
                DistanceModifier = 1;
            }

            //modify the ShadowModifier value based if the player is in shadow or not.
            if (GameManager.Singleton.PlayerLighting <= 0.5f)
            {
                ShadowModifier = 1.0f - ShadowBonus;
            }
            else
            {
                ShadowModifier = 1.0f;
            }


            if (MainAIScript != null)
            {
                //If the player is visible, start adding to the alert level + its modifiers
                if (mySensor.GetVisibility(GameManager.Singleton.ActivePlayer) > 0.5f)
                {
                    MainAIScript.AlertLevel += AlertAdd * DistanceModifier * ShadowModifier;

                }
                else
                {
                    if (myFSM.Fsm.ActiveStateName != "GameOver")
                    {
                        //if the player is not visible, decrease the alert level.
                        if (MainAIScript.AlertLevel > 0)
                        {
                            MainAIScript.AlertLevel -= CoolDown;
                        }
                    }
                }

                //if the alert level reaches 1, he has spotted the player
                if (MainAIScript.AlertLevel >= 1)
                {
                    myFSM.Fsm.Event("SPOTTED");
                }

                if (myFSM.Fsm.ActiveStateName == "Seeking")
                {
                    MainAIScript.AlertLevel = 1;
                }

                if (myFSM.Fsm.ActiveStateName == "HeardPlayer")
                {
                    if (MainAIScript.AlertLevel < 1)
                    {
                        MainAIScript.AlertLevel += AlertAdd;
                    }
                }
            }
        }
    }

    public void ToggleDection(bool value)
    {
        DectectionOn = value;
    }

    public void FoundPlayer()
    {
        if (GameManager.Singleton.EnableAISightSwitch && DectectionOn && !GameManager.Singleton.PlayerSafe)
        {
            if (myFSM != null)
            {
                if (RearFOV)
                {
                    if (myFSM.Fsm.ActiveStateName == "Patrol")
                    {
                        // StartCoroutine(WaitAndPrint());
                        myFSM.Fsm.Event("PLAYERBEHIND");
                    }
                }
                else
                {
                    if (mySensor.GetVisibility(GameManager.Singleton.ActivePlayer) > 0.5f)
                    {
                        if (!UseAlertBar)
                        {
                            myFSM.Fsm.Event("SPOTTED");
                        }
                        //
                        // MainAIScript.AlertLevel += AlertAdd1;

                    }

                }
            }

            if (!RearFOV)
            {
                GameManager.Singleton.PlayerInSight = true;
            }
        }
    }



    public void LostPlayer()
    {
        if (myFSM != null)
        {

            myFSM.Fsm.Event("LOSTPLAYER");

        }
        GameManager.Singleton.PlayerInSight = false;

    }

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(2.0f);
        if (mySensor.GetNearestByTag("Player") != null)
        {
            //Debug.Log("Player Rear Detect");
            myFSM.Fsm.Event("PLAYERBEHIND");
        }

    }
}
