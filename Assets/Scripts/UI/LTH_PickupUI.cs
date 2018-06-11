using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LTH_PickupUI : MonoBehaviour {

    public bool Paper;
    public bool FireExtinguisher;
    private Text myText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Paper)
        {
            myText.enabled = Stealth_GameManager.Singleton.HasPaperThrowable;
        }

        if (FireExtinguisher)
        {
            myText.enabled = Stealth_GameManager.Singleton.HasFireExtinguisher;
        }


    }
}
