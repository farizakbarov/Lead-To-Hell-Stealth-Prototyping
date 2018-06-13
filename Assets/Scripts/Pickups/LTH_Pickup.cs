using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTH_Pickup : MonoBehaviour {

    public enum PickupTypes { Paper, FireExtinguisher, TrashBin };

    public PickupTypes myType;

    public bool Respawn;
    public float RespawnTime = 15;

    private bool PickupActive = true;


    //public bool PickipActive = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Example()
    {
       // print(Time.time);
        yield return new WaitForSeconds(RespawnTime);
        PickupActive = true;
        this.transform.parent.gameObject.GetComponent<Renderer>().enabled = true;
        // print(Time.time);
    }

    private void OnTriggerStay(Collider other)
    {
        if (PickupActive)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetButtonDown("Interact"))
                {


                    if (myType == PickupTypes.TrashBin)
                    {
                        GameManager.Singleton.Player.GetComponent<Animator>().SetTrigger("Pickup");
                        Stealth_GameManager.Singleton.HasPaperThrowable = true;
                    }
                    else
                    {
                        if (myType == PickupTypes.FireExtinguisher)
                        {
                            Stealth_GameManager.Singleton.HasFireExtinguisher = true;
                        }

                        if (myType == PickupTypes.Paper)
                        {
                            Stealth_GameManager.Singleton.HasPaperThrowable = true;
                        }
                        if (Respawn)
                        {
                            StartCoroutine(Example());
                        }

                        this.transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
                        PickupActive = false;
                    }
                    //Debug.Log("Pickup");




                }
            }
        }
    }
}
