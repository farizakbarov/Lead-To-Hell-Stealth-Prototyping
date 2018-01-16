using System.Collections;
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

    



    public bool SeekFlip = true;


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

        if (UseAlertBar && GameManager.Singleton.EnableAISightSwitch)
        {
            //find out the distance to the player
            DistanceToPlayer = Vector3.Distance(transform.position, GameManager.Singleton.ActivePlayer.transform.position);


            //modify the DistanceModifier value based upon how close/far away the player is
            if (DistanceToPlayer <= DistanceClose)
            {
                DistanceModifier = GameManager.Singleton.DistanceNearModifier;
                // Debug.Log("near");
            }
            else if (DistanceToPlayer >= DistnaceFar)
            {
                DistanceModifier = GameManager.Singleton.DistanceFarModifier;
            }
            else
            {
                DistanceModifier = 1;
            }

            //modify the ShadowModifier value based if the player is in shadow or not.
            if (GameManager.Singleton.PlayerLighting <= 0.5f)
            {
                GameManager.Singleton.ShadowModifier = 1.0f - GameManager.Singleton.ShadowBonus;
            }
            else
            {
                GameManager.Singleton.ShadowModifier = 1.0f;
            }

            if(GameManager.Singleton.Difficulty == GameManager.Difficulties.Easy)
            {
                GameManager.Singleton.DifficultyModifier = GameManager.Singleton.EasyModifier;

            }else if(GameManager.Singleton.Difficulty == GameManager.Difficulties.Medium)
            {
                GameManager.Singleton.DifficultyModifier = GameManager.Singleton.MediumModifier;
            }
            else
            {
                GameManager.Singleton.DifficultyModifier = GameManager.Singleton.HardModifer;
            }

            if(GameManager.Singleton.TimeOfDay == GameManager.TimeOfDays.Day)
            {
                GameManager.Singleton.TimeOfDayModifier = GameManager.Singleton.DayModifier;
            }
            else
            {
                GameManager.Singleton.TimeOfDayModifier = GameManager.Singleton.NightModifer;
            }

            if (MainAIScript != null)
            {
                //If the player is visible, start adding to the alert level + its modifiers
                if (mySensor.GetVisibility(GameManager.Singleton.ActivePlayer) > 0.5f)
                {
                    MainAIScript.AlertLevel += AlertAdd * DistanceModifier * GameManager.Singleton.ShadowModifier * GameManager.Singleton.DifficultyModifier * GameManager.Singleton.TimeOfDayModifier;
                    GameManager.Singleton.PlayerInSight = true;
                }
                else
                {
                    GameManager.Singleton.PlayerInSight = false;
                    if (myFSM.Fsm.ActiveStateName != "GameOver")
                    {
                        //if the player is not visible, decrease the alert level.
                        if (MainAIScript.AlertLevel > 0)
                        {
                            MainAIScript.AlertLevel -= CoolDown;
                        }
                    }
                }

                if (myFSM.Fsm.ActiveStateName == "Wait - Lost Player")
                {
                    SeekFlip = true;
                }

                //if the alert level reaches 1, he has spotted the player
                if (MainAIScript.AlertLevel >= 1)
                {
                    if (SeekFlip)
                    {
                        myFSM.Fsm.Event("SPOTTED");
                        SeekFlip = false;
                    }
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
                    }


                }
            }

          /*  if (!RearFOV)
            {
                GameManager.Singleton.PlayerInSight = true;
            }*/
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
