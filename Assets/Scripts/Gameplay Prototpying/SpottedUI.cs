using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpottedUI : MonoBehaviour {

    private Image myImage;

	// Use this for initialization
	void Start () {
        myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Singleton.PlayerInSight)
        {
            myImage.color = Color.green;
        }
        else
        {
            myImage.color = Color.red;
        }
	}

}
