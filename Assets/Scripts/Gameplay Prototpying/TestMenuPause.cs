using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenuPause : MonoBehaviour {

    public bool Paused;

    public GameObject oldCamera;
    public GameObject NewCamera;
    public GameObject NewCinemaMachineCam;

    // Use this for initialization
    void Start() {

    }

    public void ActivateNewCam(bool value)
    {
        NewCamera.SetActive(value);
        NewCinemaMachineCam.SetActive(value);
        oldCamera.SetActive(!value);
    }

    public void ActivaeOldCam(bool value)
    {
        NewCamera.SetActive(!value);
        NewCinemaMachineCam.SetActive(!value);
        oldCamera.SetActive(value);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }

        if (Paused)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
		
	}
}
