using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayMaker;

public class FSM_uGui_button : MonoBehaviour {

    public PlayMakerFSM myFSM;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Distract()
    {
        myFSM.Fsm.Event("DISTRACT");
    }
}
