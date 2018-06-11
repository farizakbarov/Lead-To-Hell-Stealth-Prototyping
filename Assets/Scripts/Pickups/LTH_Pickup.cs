using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTH_Pickup : MonoBehaviour {


    public bool FireExtinguisher;
    public bool PaperPickup;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (FireExtinguisher)
                {
                    Stealth_GameManager.Singleton.HasFireExtinguisher = true;
                }

                if (PaperPickup)
                {
                    Stealth_GameManager.Singleton.HasPaperThrowable = true;
                }
                //Debug.Log("Pickup");
                
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
