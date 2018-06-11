/* This script accesses the Camera script from the cover shooter, and temporaily overrides the default camera state while the player is inside a safe zone so the camera is in a better postion
 * 
 This is better than editing the cover shooter system to include a new state*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

public class LTH_SafeCamSettings : MonoBehaviour {

    private ThirdPersonCamera CamScript;


    public CameraState SafeState;
    private CameraState StoredState;

    // Use this for initialization
    void Start () {
        CamScript = GetComponent<ThirdPersonCamera>();
        StoredState = CamScript.States.Default;
    }
	
	// Update is called once per frame
	void Update () {
        if (Stealth_GameManager.Singleton.PlayerSafe)
        {
            CamScript.States.Default = SafeState;
        }
        else
        {
            CamScript.States.Default = StoredState;
        }
		
	}
}
