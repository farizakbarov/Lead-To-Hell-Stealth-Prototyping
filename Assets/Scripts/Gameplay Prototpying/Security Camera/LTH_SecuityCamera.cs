using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class LTH_SecuityCamera : MonoBehaviour {

    private TriggerSensor mySensor;
    private CCTVController controller;
    private Light mylight;
    private Color StartColor;
    public Color AlertColor = Color.red;
    public Color DetectColor = Color.yellow;
    public float DetectTime = 2.0f;
    float Type2distance = Mathf.Infinity;
	public float ReturnToIdle = 2.0f;

    GameObject NearestType2;

    private bool TimerFlip = true;

    private bool Alerted;

    private bool SightFlip = true;

    public bool CanSeePlayer;

    // Use this for initialization
    void Start () {
        mySensor = GetComponentInChildren<TriggerSensor>();
        controller = GetComponent<CCTVController>();
        mylight = GetComponentInChildren<Light>();
        StartColor = mylight.color;

        Stealth_GameManager.Singleton.AddSecurityCam(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        if (mySensor.GetVisibility(GameManager.Singleton.Player) > 0.5f && Stealth_GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch)
        {
            CanSeePlayer = true;
            controller.isDynamic = false;
            controller.TrackPlayer = true;
            if (!Alerted)
            {
                mylight.color = DetectColor;
            }
            if (TimerFlip)
            {
                StartCoroutine(WaitAndPrint());
                TimerFlip = false;
            }
            SightFlip = true;
        }
        else
        {
            if (!Alerted)
            {
                
                controller.isDynamic = true;
                controller.TrackPlayer =false;
                mylight.color = StartColor;
                TimerFlip = true;

            }

            //Need to only set PlayerInSight back to false once. not every frame because that interferes with AI detection 
            if (SightFlip)
            {
                //Stealth_GameManager.Singleton.PlayerInSight = false;
                CanSeePlayer = false;
                SightFlip = false;
            }
            
        }

        //when the camera spots the player and is alerted, check to see if the AI it called over has returned back to Idle, if so turn off alerted
        if (Alerted)
        {
			if(NearestType2.GetComponent<PlayMakerFSM>().ActiveStateName == "Patrol" || NearestType2.GetComponent<PlayMakerFSM>().ActiveStateName == "ReturnToSweep" || NearestType2.GetComponent<PlayMakerFSM>().ActiveStateName == "ReturnToStand" || NearestType2.GetComponent<PlayMakerFSM>().ActiveStateName == "Looking For Player")
            {
                Alerted = false;
            }
        }

    }


    //The Timer for when the player is spotted. 
    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(DetectTime);
        if (mySensor.GetVisibility(GameManager.Singleton.Player) > 0.5f)
        {
            mylight.color = AlertColor;
            FindNearestType2();
            Alerted = true;
        }
            
    }


    //function for finding the closest type 2 to call him over
    public void FindNearestType2()
    {

        //loop through all the type 2s in the scene
        foreach (GameObject obj in Stealth_GameManager.Singleton.ListOfType2s)
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
}
