using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour {

    public Transform SafeZoneLocation;
    public bool UseExit;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Stealth_GameManager.Singleton.PlayerSafe = false;
        }
        
    }
}
