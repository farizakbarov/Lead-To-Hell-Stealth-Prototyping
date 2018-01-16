using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoverShooter;

public class DebugTestingMenu : MonoBehaviour
{

    public GameObject Menu;
    //private bool paused;

    public GameObject OnScreenSight;
    public GameObject LightSensor;

    public PlayMakerGUI PlaymakerGUI;

    public Image LightSensorButton;

    public Image ScreenSightButton;
    public Image AIAlertButton;
    public Image AIStatusButton;
    public Image AISight;
    public Image AIHearing;

    public Image PlayerRolling;
    public Image PlayerSlide;
    public Image PlayerStealthToggle;

    public Image AIStatusLabelButton;

    public Dropdown Difficulty;


    public InputField EasyModifier;
    public InputField MediumModifier;
    public InputField HardModifier;
    public InputField ShadowModifier;
    public InputField DistanceNearModifier;
    public InputField DistanceFarModifier;
    public InputField NightModifier;

    public Image LastSightingBox;

    // Use this for initialization
    void Start()
    {
        if (GameManager.Singleton.Difficulty == GameManager.Difficulties.Easy)
        {
            Difficulty.value = 0;
        }
        else if (GameManager.Singleton.Difficulty == GameManager.Difficulties.Medium)
        {
            Difficulty.value = 1;
        }
        else
        {
            Difficulty.value = 2;
        }

        EasyModifier.text = GameManager.Singleton.EasyModifier.ToString();
        MediumModifier.text = GameManager.Singleton.MediumModifier.ToString();
        HardModifier.text = GameManager.Singleton.HardModifer.ToString();
        DistanceFarModifier.text = GameManager.Singleton.DistanceFarModifier.ToString();
        DistanceNearModifier.text = GameManager.Singleton.DistanceNearModifier.ToString();
        ShadowModifier.text = GameManager.Singleton.ShadowBonus.ToString();
        NightModifier.text = GameManager.Singleton.NightModifer.ToString();


    }

    // Update is called once per frame
    void Update()
    {

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

        if (PlaymakerGUI.drawStateLabels)
        {
            AIStatusLabelButton.color = Color.green;
        }
        else
        {
            AIStatusLabelButton.color = Color.red;
        }

        if (OnScreenSight.activeSelf)
        {
            ScreenSightButton.color = Color.green;
        }
        else
        {
            ScreenSightButton.color = Color.red;
        }

        if (LightSensor.activeSelf)
        {
            LightSensorButton.color = Color.green;
        }
        else
        {
            LightSensorButton.color = Color.red;
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

        if (GameManager.Singleton.EnableAISightSwitch)
        {
            AISight.color = Color.green;
        }
        else
        {
            AISight.color = Color.red;
        }

        if (GameManager.Singleton.EnableAIHearing)
        {
            AIHearing.color = Color.green;
        }
        else
        {
            AIHearing.color = Color.red;
        }

        if (GameManager.Singleton.Detective.GetComponent<ThirdPersonController>().EnableRolling)
        {
            PlayerRolling.color = Color.green;
        }
        else
        {
            PlayerRolling.color = Color.red;
        }

        if (GameManager.Singleton.Detective.GetComponent<CharacterSlide>().SlidingEnabled)
        {
            PlayerSlide.color = Color.green;
        }
        else
        {
            PlayerSlide.color = Color.red;
        }

        if (GameManager.Singleton.Detective.GetComponent<LTH_ThirdPersonController>().ToggleSneak)
        {
            PlayerStealthToggle.color = Color.green;
        }
        else
        {
            PlayerStealthToggle.color = Color.red;
        }

        if(GameManager.Singleton.LastSightingVisible)
        {
            LastSightingBox.color = Color.green;
        }
        else
        {
            LastSightingBox.color = Color.red;
        }

    }

    public void ToggleSlidingAbility()
    {
        if (GameManager.Singleton.Detective.GetComponent<CharacterSlide>().SlidingEnabled) {
            GameManager.Singleton.Detective.GetComponent<CharacterSlide>().SlidingEnabled = false;
        }
        else{
            GameManager.Singleton.Detective.GetComponent<CharacterSlide>().SlidingEnabled = true;
        }
    }

    public void SneakToggle()
    {
        if (GameManager.Singleton.Detective.GetComponent<LTH_ThirdPersonController>().ToggleSneak)
        {
            GameManager.Singleton.Detective.GetComponent<LTH_ThirdPersonController>().ToggleSneak = false;
        }
        else
        {
            GameManager.Singleton.Detective.GetComponent<LTH_ThirdPersonController>().ToggleSneak = true;
        }
    }



    public void ToggleAIStateLabels()
    {
        if (PlaymakerGUI.drawStateLabels)
        {
            PlaymakerGUI.drawStateLabels = false;
        }
        else
        {
            PlaymakerGUI.drawStateLabels = true;
        }
    }

    public void ToggleRollingAblitiy()
    {
        if (GameManager.Singleton.Detective.GetComponent<ThirdPersonController>().EnableRolling)
        {
            GameManager.Singleton.Detective.GetComponent<ThirdPersonController>().EnableRolling = false;
        }
        else
        {
            GameManager.Singleton.Detective.GetComponent<ThirdPersonController>().EnableRolling = true;
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

    public void ToggleLightSensorUI()
    {
        if (LightSensor.activeSelf)
        {
            LightSensor.SetActive(false);
        }
        else
        {
            LightSensor.SetActive(true);
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

    public void ChangeDifficulty()
    {
        if (Difficulty.value == 0)
        {
            GameManager.Singleton.Difficulty = GameManager.Difficulties.Easy;

        }
        else if (Difficulty.value == 1)
        {
            GameManager.Singleton.Difficulty = GameManager.Difficulties.Medium;
        }
        else
        {
            GameManager.Singleton.Difficulty = GameManager.Difficulties.Hard;
        }
        Debug.Log(GameManager.Singleton.Difficulty);
    }

    public void ToggleAISight()
    {
        GameManager.Singleton.EnableAISightSwitch = !GameManager.Singleton.EnableAISightSwitch;
    }

    public void ToggleAIhearing()
    {
        GameManager.Singleton.EnableAIHearing = !GameManager.Singleton.EnableAIHearing;
    }


    public void SetEasyModifier()
    {
        GameManager.Singleton.EasyModifier = float.Parse(EasyModifier.text);
        Debug.Log(GameManager.Singleton.EasyModifier);
    }

    public void SetMediumModifier()
    {
        GameManager.Singleton.MediumModifier = float.Parse(MediumModifier.text);
        Debug.Log(GameManager.Singleton.MediumModifier);
    }

    public void SetHardModifier()
    {
        GameManager.Singleton.HardModifer = float.Parse(HardModifier.text);
        Debug.Log(GameManager.Singleton.HardModifer);
    }

    public void SetInShadowModifier()
    {
        GameManager.Singleton.ShadowBonus = float.Parse(ShadowModifier.text);
    }

    public void SetDisanceNearModifier()
    {
        GameManager.Singleton.DistanceNearModifier = float.Parse(DistanceNearModifier.text);
    }
    public void SetDistanceFarModifier()
    {
        GameManager.Singleton.DistanceFarModifier = float.Parse(DistanceFarModifier.text);
    }
    public void SetNightModifier()
    {
        GameManager.Singleton.NightModifer = float.Parse(NightModifier.text);
    }

    public void ToggleLastSightingVisiblity()
    {
        GameManager.Singleton.LastSightingVisible = !GameManager.Singleton.LastSightingVisible;

        if (GameManager.Singleton.LastSighting != null)
        {
            if (GameManager.Singleton.LastSightingVisible)
            {
                GameManager.Singleton.LastSighting.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                GameManager.Singleton.LastSighting.GetComponent<Renderer>().enabled = false;
            }
        }

       
    }

    
}
