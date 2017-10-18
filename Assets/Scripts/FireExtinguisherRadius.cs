using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;

public class FireExtinguisherRadius : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
       // Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "AI")
        {
            
            if (other.GetComponent<PlayMakerFSM>().ActiveStateName != "Distracted")
            {
                other.GetComponent<PlayMakerFSM>().Fsm.Event("DISTRACT");
            }
        }
    }
}
