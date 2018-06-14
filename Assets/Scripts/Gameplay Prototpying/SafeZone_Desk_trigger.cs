using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone_Desk_trigger : MonoBehaviour {

    public Transform SafeZoneLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<SafeZone_Detect>().IsInTrigger = true;
            other.GetComponent<SafeZone_Detect>().MoveLocation = SafeZoneLocation;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<SafeZone_Detect>().IsInTrigger = false;
        }

    }
}
