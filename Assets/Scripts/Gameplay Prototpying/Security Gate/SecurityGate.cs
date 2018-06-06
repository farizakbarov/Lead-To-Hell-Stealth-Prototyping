using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGate : MonoBehaviour {

    public bool GateOn = true;
    public float TimeOut = 2.0f;

    float Type2distance = Mathf.Infinity;

    GameObject NearestType2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (GateOn)
            {
                Stealth_GameManager.Singleton.PlayerInSight = true;
                FindNearestType2();
            }
        }

        if(other.tag == "AI")
        {
            GateOn = false;
            StartCoroutine(WaitAndPrint());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GateOn)
            {
                Stealth_GameManager.Singleton.PlayerInSight = false;
            }
        }
    }

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(TimeOut);
        GateOn = true;
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
