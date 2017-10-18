using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour {

    private bool value;
    private Toggle myToggle;

    public enum choices
    {
        Sneak,
        Cover,
        DisableCoverRun,
    }

    public choices choice = choices.Sneak;
    
    
    // Use this for initialization
    void Start () {

        myToggle = GetComponent<Toggle>();
    }
	
	// Update is called once per frame
	void Update () {


        if (choice == choices.DisableCoverRun){
            value = GameManager.Singleton.ActivePlayer.GetComponent<LTH_ThirdPersonController>().DisableAutoCoverWhenRunning;
        }else if (choice == choices.Cover)
        {
            value = GameManager.Singleton.ActivePlayer.GetComponent<LTH_ThirdPersonController>().AutoCoverEnabled;
        }
        else
        {
            value = GameManager.Singleton.ActivePlayer.GetComponent<LTH_ThirdPersonController>().ToggleSneak;
        }
        
        myToggle.isOn = value;

    }
}
