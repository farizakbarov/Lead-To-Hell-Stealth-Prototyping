using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;
using SensorToolkit;

public class AISight : MonoBehaviour {

    private PlayMakerFSM myFSM;
    public bool DectectionOn = true;
    public bool RearFOV;
	private TriggerSensor mySensor;


    // Use this for initialization
    void Start () {
        myFSM = transform.GetComponentInParent<PlayMakerFSM>();
		mySensor = GetComponent<TriggerSensor>();


    }
	
	// Update is called once per frame
	void Update () {
		
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
					if (mySensor.GetVisibility (GameManager.Singleton.ActivePlayer) > 0.5f) {
						myFSM.Fsm.Event ("SPOTTED");
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
		if(mySensor.GetNearestByTag("Player") != null)
        {
            //Debug.Log("Player Rear Detect");
            myFSM.Fsm.Event("PLAYERBEHIND");
        }
        
    }
}
