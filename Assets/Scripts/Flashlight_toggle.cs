using UnityEngine;
using System.Collections;

public class Flashlight_toggle : MonoBehaviour {
	
	public GameObject[] ArrayOfObjects;
	public bool toggle = true;
	public float Volume = 1.0f;
	public AudioClip OnSFX;
	public AudioClip OffSFX;
	
	//public GameObject LightSource;
	//public GameObject VolumeticSource;
	//private Light lightsrc;
	//private 

	// Update is called once per frame
	void Update () {


        GameManager.Singleton.Flashlight = toggle;

	
		if(!toggle){
			foreach (GameObject obj in ArrayOfObjects)
			{
			    obj.GetComponent<Light>().enabled = false;
			}	
		}
		else{
			/*if(GameManager.Singleton.CurrentNoiseLevel < 1.0f){
				GameManager.Singleton.CurrentNoiseLevel += 0.01f;
			}*/

			foreach (GameObject obj in ArrayOfObjects)
			{
			    obj.GetComponent<Light>().enabled = true;
			}
		}

		
		if(Input.GetButtonDown("Flashlight")){
            if (!toggle){
				Debug.Log("Flashlight off");
                if (OffSFX != null)
                {
                    GetComponent<AudioSource>().PlayOneShot(OffSFX, Volume);
                }
				toggle = true;
				foreach (GameObject obj in ArrayOfObjects)
				{
                    obj.GetComponent<Light>().enabled = false;
                }		 
			}
			else{
				Debug.Log("Flashlight On");

                if (OnSFX != null)
                {
                    GetComponent<AudioSource>().PlayOneShot(OnSFX, Volume);
                }
				toggle = false;
				foreach (GameObject obj in ArrayOfObjects)
				{
                    obj.GetComponent<Light>().enabled = true;
                }
			}
			
		}
	
	}
}
