﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LTH_PauseSetBool : MonoBehaviour {

    //THis script attaches to the TOggle UI gameobjects, it sets the value to the current value

    public bool AA;
    public bool AO;
    public bool LensEffects;
    public bool DOF;
    public bool MB;
    public bool BlackAndWhite;
    private Toggle myToggle;

	// Use this for initialization
	void Start () {
        myToggle = GetComponent<Toggle>();

    }
	
	// Update is called once per frame
	void Update () {
        if (AA)
        {
			myToggle.isOn = GameManager.Singleton.LTH_QualityData.Quality_aa;
        }
        if (AO)
        {
			myToggle.isOn = GameManager.Singleton.LTH_QualityData.Quality_AO;
        }
        if (LensEffects)
        {
			myToggle.isOn = GameManager.Singleton.LTH_QualityData.Quality_LensEffects;
        }
        if (DOF)
        {
			myToggle.isOn = GameManager.Singleton.LTH_QualityData.Quality_Dof;
        }
        if (MB)
        {
			myToggle.isOn = GameManager.Singleton.LTH_QualityData.Quality_MotionBlur;
        }
        if (BlackAndWhite)
        {
			myToggle.isOn = GameManager.Singleton.LTH_QualityData.BlackAndWhiteMode;
        }
    }
}