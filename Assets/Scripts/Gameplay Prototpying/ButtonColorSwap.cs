using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;
using UnityEngine.UI;

public class ButtonColorSwap : MonoBehaviour {

    public Color OnColor = Color.green;
    public Color OffColor = Color.red;

    public bool SneakButton;
    public bool CoverButton;
    public bool DisableCoverRun;
    public bool DisableCoverWalk;

    private LTH_ThirdPersonController controller;
    private Image myButton;

	// Use this for initialization
	void Start () {
        controller = GameManager.Singleton.ActivePlayer.GetComponent<LTH_ThirdPersonController>();
        myButton = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (SneakButton)
        {
            if (controller.ToggleSneak)
            {
                myButton.color = OnColor;
            }
            else
            {
                myButton.color = OffColor;
            }
        }

        if (CoverButton)
        {
            if (controller.AutoCoverEnabled)
            {
                myButton.color = OnColor;
            }
            else
            {
                myButton.color = OffColor;
            }
        }

        if (DisableCoverRun)
        {
            if (controller.DisableAutoCoverWhenRunning)
            {
                myButton.color = OnColor;
            }
            else
            {
                myButton.color = OffColor;
            }
        }

        if (DisableCoverWalk)
        {
            if (controller.DisableAutoCoverWhenWalking)
            {
                myButton.color = OnColor;
            }
            else
            {
                myButton.color = OffColor;
            }
        }
    }
}
