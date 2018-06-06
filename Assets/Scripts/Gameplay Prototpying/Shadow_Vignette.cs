using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Shadow_Vignette : MonoBehaviour {

	public PostProcessingProfile ppProfile;
	private LightSensor mySensor;
	public float Intensity = 0.45f;
	public float Speed = 1.0f;

	private float NewIntensity;

	bool InShadow;

	public float DelayTime = 1.0f;
	bool EnableTransistion;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Singleton.Player != null) {
			mySensor = GameManager.Singleton.Player.GetComponent<LightSensor> ();
		
			VignetteModel.Settings VignetteSettings = ppProfile.vignette.settings;

			if (mySensor.LightingTotal <= 0.9f) {
				InShadow = false;
				StartCoroutine ("StartTransistion");
			} else {
				InShadow = true;
				StartCoroutine ("StartTransistion");
			}

			VignetteSettings.intensity = NewIntensity;

			ppProfile.vignette.settings = VignetteSettings;

			if (EnableTransistion) {
				if (InShadow) {
					if (NewIntensity > 0) {
						NewIntensity -= 0.01f * Speed;
					}

					if (NewIntensity < 0) {
						NewIntensity = 0;
						EnableTransistion = false;
					}
				
				} else {
					if (NewIntensity < Intensity) {
						NewIntensity += 0.01f * Speed;
					}

					if (NewIntensity > Intensity) {
						NewIntensity = Intensity;
						EnableTransistion = false;
					}
				}
			}
		}
	}

	IEnumerator StartTransistion() {
		yield return new WaitForSeconds(DelayTime);
		EnableTransistion = true;
	}
}
