using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

/* This script extends, and customizes the settings for the ThirdPersonController Script to do custom things we want, and easily report what the player is doing
 
It has been abstracted here to it's own script so that it dosn't have to be re-added to ThirdPersonController when an update to that script happens.*/

public class LTH_ThirdPersonController : MonoBehaviour {


    private ThirdPersonController controller;
    private CharacterMotor _motor;

    public float StealthRunSpeed = 1.5f;

    public bool isSneaking = false;
    public bool isRunning;
    public bool isWalking;

    public bool ToggleSneak = true;
    public bool AutoCoverEnabled = true;
    public bool DisableAutoCoverWhenRunning;
    public bool DisableAutoCoverWhenWalking;

    public GameObject PaperThrowable;
    public GameObject FireExtinguisherThrowable;

    private Animator anim;

    // Use this for initialization
    void Start () {
        controller = GetComponent<ThirdPersonController>();
        _motor = GetComponent<CharacterMotor>();
        anim = GetComponent<Animator>();

        PaperThrowable.SetActive(false);
        FireExtinguisherThrowable.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _motor.Grenade.Left = PaperThrowable;
            _motor.Grenade.Right = PaperThrowable;
            FireExtinguisherThrowable.SetActive(false);
            controller.EnableExposionPreview = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _motor.Grenade.Left = FireExtinguisherThrowable;
            _motor.Grenade.Right = FireExtinguisherThrowable;
            PaperThrowable.SetActive(false);
            controller.EnableExposionPreview = true;
        }


        var animatorMovement = anim.deltaPosition / Time.deltaTime;
        var animatorSpeed = animatorMovement.magnitude;

        //if not sneaking, has the run button hold, and is actually moving fast enough, he is running
        if (!isSneaking && Input.GetKey(KeyCode.LeftShift))
        {
            if (animatorSpeed > 3.0)
            {
                isRunning = true;
            }
        }
        else
        {
            isRunning = false;
        }

        //If he's not sneaking or running, he is walking.
        if (!isSneaking && !isRunning)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }


        //the ability to toggle between sneaking and walking/running, instead of holding down the button
        if (ToggleSneak)
        {
            //Toggle
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isSneaking = !isSneaking;
            }

            if (isSneaking)
            {
                _motor.InputCrouch();
            }
        }

        //The ability to move slightly faster when trying to run while sneaking
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_motor.IsCrouching)
            {
                anim.speed = StealthRunSpeed;
            }
        }
        else
        {
            anim.speed = 1f;

        }

        if (!AutoCoverEnabled)
        {
            controller.AutoTakeCover = false;
        }


        //While the player is running, 
        if (isRunning)
        {
            //modify the cover settings so that he can vault over things from a greater distance.
            //Disable auto cover
            if (DisableAutoCoverWhenRunning && !isSneaking)
            {
                //_motor.CoverSettings.IsAuto = false;
                controller.AutoTakeCover = false;
                //_motor.CoverSettings.EnterDistance = _RunningCoverEnter;
            }
        }
        else
        {
            //modify the cover settings so that he enters cover normally
            //_motor.CoverSettings.EnterDistance = _DefaultCoverEnter;
            //enable auto cover
            if (DisableAutoCoverWhenRunning && AutoCoverEnabled)
            {
                controller.AutoTakeCover = true;
            }
        }

        if (isWalking)
        {
            if (DisableAutoCoverWhenWalking)
            {
                controller.AutoTakeCover = false;
            }
        }
    }
}
