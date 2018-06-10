using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LTH_PauseDisableEffects : MonoBehaviour {

//    private glareFxChromatic glarescript;
    private PostProcessingBehaviour PP;

	// Use this for initialization
	void Start () {
       // glarescript = GetComponent<glareFxChromatic>();
        PP = GetComponent<PostProcessingBehaviour>();

        GameManager.Singleton.ListOfCameras.Add(this.GetComponent<Camera>());
    }
	
	// Update is called once per frame
	void Update () {

		if (PP != null) {
			if (GameManager.Singleton.LTH_QualityData.Quality_LensEffects) {
				//  glarescript.enabled = true;
				PP.profile.chromaticAberration.enabled = true;
			} else {
				// glarescript.enabled = false;
				PP.profile.chromaticAberration.enabled = false;
			}

			if (GameManager.Singleton.LTH_QualityData.Quality_AO) {
				PP.profile.ambientOcclusion.enabled = true;
			} else {
				PP.profile.ambientOcclusion.enabled = false;
			}

			if (GameManager.Singleton.LTH_QualityData.Quality_Dof) {
				PP.profile.depthOfField.enabled = true;
			} else {
				PP.profile.depthOfField.enabled = false;
			}
			if (GameManager.Singleton.LTH_QualityData.Quality_aa) {
				PP.profile.antialiasing.enabled = true;
			} else {
				PP.profile.antialiasing.enabled = false;
				QualitySettings.antiAliasing = 0;
			}

			if (GameManager.Singleton.LTH_QualityData.Quality_MotionBlur) {
				PP.profile.motionBlur.enabled = true;
			} else {
				PP.profile.motionBlur.enabled = false;
			}

			if (GameManager.Singleton.LTH_QualityData.BlackAndWhiteMode) {
				PP.profile.userLut.enabled = true;
			} else {
				PP.profile.userLut.enabled = false;
			}
		}

    }
}
