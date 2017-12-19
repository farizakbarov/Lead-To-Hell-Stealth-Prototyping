using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTestingMenu : MonoBehaviour {

    public GameObject Menu;
    //private bool paused;

    public GameObject OnScreenSight;

    public Image ScreenSightButton;
    public Image AIAlertButton;
    public Image AIStatusButton;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Singleton.Paused = !GameManager.Singleton.Paused;
        }

        if (GameManager.Singleton.Paused)
        {
            Time.timeScale = 0;
            Menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Menu.SetActive(false);
        }


        if (OnScreenSight.activeSelf)
        {
            ScreenSightButton.color = Color.green;
        }
        else
        {
            ScreenSightButton.color = Color.red;
        }


        if (GameManager.Singleton.EnableAIAlertBars)
        {
            AIAlertButton.color = Color.green;
        }
        else
        {
            AIAlertButton.color = Color.red;
        }

        if (GameManager.Singleton.EnableAIStatusIndicators)
        {
            AIStatusButton.color = Color.green;
        }
        else
        {
            AIStatusButton.color = Color.red;
        }

    }


    public void ToggleOnScreenSight()
    {
        if (OnScreenSight.activeSelf == true)
        {
            OnScreenSight.SetActive(false);
        }
        else
        {
            OnScreenSight.SetActive(true);
        }
    }


    public void ToggleAIAlertBars()
    {
        // Debug.Log(GameManager.Singleton.ListOfType2s.Count);

        GameManager.Singleton.EnableAIAlertBars = !GameManager.Singleton.EnableAIAlertBars;
    }

    public void ToggleAIStatusIndicators()
    {
        GameManager.Singleton.EnableAIStatusIndicators = !GameManager.Singleton.EnableAIStatusIndicators;
    }
}
