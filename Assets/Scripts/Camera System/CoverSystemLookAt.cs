using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

public class CoverSystemLookAt : MonoBehaviour {

    public CharacterMotor Target;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var lookPosition = transform.position + transform.forward * 1000;
        Target.SetLookTarget(lookPosition);
    }
}
