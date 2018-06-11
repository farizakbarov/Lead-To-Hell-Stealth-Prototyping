using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTH_FireExtinguisher_Pickup : MonoBehaviour {

 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                //Debug.Log("Pickup");
                Stealth_GameManager.Singleton.HasFireExtinguisher = true;
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }


}
