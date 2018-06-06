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

        if (UseAlertBar && Stealth_GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch && GameManager.Singleton.Player != null)
        {
            //find out the distance to the player
            DistanceToPlayer = Vector3.Distance(transform.position, GameManager.Singleton.Player.transform.position);


            //modify the DistanceModifier value based upon how close/far away the player is
            if (DistanceToPlayer <= DistanceClose)
            {
                DistanceModifier = Stealth_GameManager.Singleton.LTH_GameSettings.DistanceNearModifier;
                // Debug.Log("near");
            }
            else if (DistanceToPlayer >= DistnaceFar)
            {
                DistanceModifier = Stealth_GameManager.Singleton.LTH_GameSettings.DistanceFarModifier;
            }
            else
            {
                DistanceModifier = 1;
            }

            //modify the ShadowModifier value based if the player is in shadow or not.
            if (Stealth_GameManager.Singleton.PlayerLighting <= 0.5f)
            {
                Stealth_GameManager.Singleton.ShadowModifier = 1.0f - Stealth_GameManager.Singleton.LTH_GameSettings.ShadowBonus;
            }
            else
            {
                Stealth_GameManager.Singleton.ShadowModifier = 1.0f;
            }

            if(Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty == LTH_SaveData.Difficulties.Easy)
            {
                Stealth_GameManager.Singleton.DifficultyModifier = Stealth_GameManager.Singleton.LTH_GameSettings.EasyModifier;

            }else if(Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty == LTH_SaveData.Difficulties.Medium)
            {
                Stealth_GameManager.Singleton.DifficultyModifier = Stealth_GameManager.Singleton.LTH_GameSettings.MediumModifier;
            }
            else
            {
                Stealth_GameManager.Singleton.DifficultyModifier = Stealth_GameManager.Singleton.LTH_GameSettings.HardModifer;
            }

            if(Stealth_GameManager.Singleton.LTH_GameSettings.TimeOfDay == LTH_SaveData.TimeOfDays.Day)
            {
                Stealth_GameManager.Singleton.TimeOfDayModifier = Stealth_GameManager.Singleton.LTH_GameSettings.DayModifier;
            }
            else
            {
                Stealth_GameManager.Singleton.TimeOfDayModifier = Stealth_GameManager.Singleton.LTH_GameSettings.NightModifer;
            }

            if (MainAIScript != null)
            {
                //If the player is visible, start adding to the alert level + its modifiers
                if (mySensor.GetVisibility(GameManager.Singleton.Player) > 0.5f)
                {
                    MainAIScript.AlertLevel += AlertAdd * DistanceModifier * Stealth_GameManager.Singleton.ShadowModifier * Stealth_GameManager.Singleton.DifficultyModifier * Stealth_GameManager.Singleton.TimeOfDayModifier;
                    Stealth_GameManager.Singleton.PlayerInSight = true;
                }
                else
                {
                    Stealth_GameManager.Singleton.PlayerInSight = false;
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
        if (Stealth_GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch && DectectionOn && !Stealth_GameManager.Singleton.PlayerSafe)
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
                    if (mySensor.GetVisibility(GameManager.Singleton.Player) > 0.5f)
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
        Stealth_GameManager.Singleton.PlayerInSight = false;

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
