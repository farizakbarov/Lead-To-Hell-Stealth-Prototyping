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
        if (GameManager.Singleton.LTH_GameSettings.Difficulty == LTH_SaveData.Difficulties.Easy)
        {
            Difficulty.value = 0;
        }
        else if (GameManager.Singleton.LTH_GameSettings.Difficulty == LTH_SaveData.Difficulties.Medium)
        {
            Difficulty.value = 1;
        }
        else
        {
            Difficulty.value = 2;
        }

        EasyModifier.text = GameManager.Singleton.LTH_GameSettings.EasyModifier.ToString();
        MediumModifier.text = GameManager.Singleton.LTH_GameSettings.MediumModifier.ToString();
        HardModifier.text = GameManager.Singleton.LTH_GameSettings.HardModifer.ToString();
        DistanceFarModifier.text = GameManager.Singleton.LTH_GameSettings.DistanceFarModifier.ToString();
        DistanceNearModifier.text = GameManager.Singleton.LTH_GameSettings.DistanceNearModifier.ToString();
        ShadowModifier.text = GameManager.Singleton.LTH_GameSettings.ShadowBonus.ToString();
        NightModifier.text = GameManager.Singleton.LTH_GameSettings.NightModifer.ToString();


      /**  if (GameManager.Singleton.LoadFromPlayerPrefs)
        {

            //if (PlayerPrefs.HasKey("Sliding"))
            // {
            GameManager.Singleton.Detective.GetComponent<CharacterSlide>().SlidingEnabled = GameManager.Singleton.GetPersistentVar<bool>("Sliding", false);
            // }



            GameManager.Singleton.Detective.GetComponent<LTH_ThirdPersonController>().ToggleSneak = GameManager.Singleton.GetPersistentVar<bool>("Sneak", true);


            GameManager.Singleton.Detective.GetComponent<ThirdPersonController>().EnableRolling = GameManager.Singleton.GetPersistentVar<bool>("Rolling", true);



            OnScreenSight.SetActive(GameManager.Singleton.GetPersistentVar<bool>("OnScreenSight", true));

            LightSensor.SetActive(GameManager.Singleton.GetPersistentVar<bool>("LightSensorUI", true));


            GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars = GameManager.Singleton.GetPersistentVar<bool>("AIAlertBars", true);

            GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators = GameManager.Singleton.GetPersistentVar < bool >("AIStatus", true);

            Difficulty.value = GameManager.Singleton.GetPersistentVar<int>("Difficulty", 0);


           GameManager.Singleton.EnableAISightSwitch = GameManager.Singleton.GetPersistentVar<bool>("AISight", true);


            GameManager.Singleton.EnableAIHearing = GameManager.Singleton.GetPersistentVar<bool>("AIHearing", true);




    
            GameManager.Singleton.LTH_GameSettings.EasyModifier = GameManager.Singleton.GetPersistentVar<float>("EasyModifier", 0);
        }*/


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

        if (GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels)
        {
            AIStatusLabelButton.color = Color.green;
        }
        else
        {
            AIStatusLabelButton.color = Color.red;
        }

        if (GameManager.Singleton.LTH_GameSettings.SpottedUI)
        {
            ScreenSightButton.color = Color.green;
        }
        else
        {
            ScreenSightButton.color = Color.red;
        }

        if (GameManager.Singleton.LTH_GameSettings.LightSensorUI)
        {
            LightSensorButton.color = Color.green;
        }
        else
        {
            LightSensorButton.color = Color.red;
        }


        if (GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars)
        {
            AIAlertButton.color = Color.green;
        }
        else
        {
            AIAlertButton.color = Color.red;
        }

        if (GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators)
        {
            AIStatusButton.color = Color.green;
        }
        else
        {
            AIStatusButton.color = Color.red;
        }

        if (GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch)
        {
            AISight.color = Color.green;
        }
        else
        {
            AISight.color = Color.red;
        }

        if (GameManager.Singleton.LTH_GameSettings.EnableAIHearing)
        {
            AIHearing.color = Color.green;
        }
        else
        {
            AIHearing.color = Color.red;
        }

        if (GameManager.Singleton.ActivePlayer != null)
        {
            if (GameManager.Singleton.LTH_GameSettings.RollingAbility)
            {
                PlayerRolling.color = Color.green;
            }
            else
            {
                PlayerRolling.color = Color.red;
            }

            if (GameManager.Singleton.LTH_GameSettings.SlidingAbility)
            {
                PlayerSlide.color = Color.green;
            }
            else
            {
                PlayerSlide.color = Color.red;
            }

            if (GameManager.Singleton.LTH_GameSettings.SneakToggle)
            {
                PlayerStealthToggle.color = Color.green;
            }
            else
            {
                PlayerStealthToggle.color = Color.red;
            }
        }

        if (GameManager.Singleton.LTH_GameSettings.LastSightingVisible)
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

        GameManager.Singleton.LTH_GameSettings.SlidingAbility = !GameManager.Singleton.LTH_GameSettings.SlidingAbility;


        GameManager.Singleton.Detective.GetComponent<CharacterSlide>().SlidingEnabled = GameManager.Singleton.LTH_GameSettings.SlidingAbility;

    }

    public void SneakToggle()
    {
        GameManager.Singleton.LTH_GameSettings.SneakToggle = !GameManager.Singleton.LTH_GameSettings.SneakToggle;


        GameManager.Singleton.Detective.GetComponent<LTH_ThirdPersonController>().ToggleSneak = GameManager.Singleton.LTH_GameSettings.SneakToggle;

    }



    public void ToggleAIStateLabels()
    {
        GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels = !GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels;


        PlaymakerGUI.drawStateLabels = GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels;

    }

    public void ToggleRollingAblitiy()
    {
        GameManager.Singleton.LTH_GameSettings.RollingAbility = !GameManager.Singleton.LTH_GameSettings.RollingAbility;


        GameManager.Singleton.Detective.GetComponent<ThirdPersonController>().EnableRolling = GameManager.Singleton.LTH_GameSettings.RollingAbility;
    }



    public void ToggleOnScreenSight()
    {
        GameManager.Singleton.LTH_GameSettings.SpottedUI = !GameManager.Singleton.LTH_GameSettings.SpottedUI;

        OnScreenSight.SetActive(GameManager.Singleton.LTH_GameSettings.SpottedUI);

    }

    public void ToggleLightSensorUI()
    {
        GameManager.Singleton.LTH_GameSettings.LightSensorUI = !GameManager.Singleton.LTH_GameSettings.LightSensorUI;

        LightSensor.SetActive(GameManager.Singleton.LTH_GameSettings.LightSensorUI);

    }


    public void ToggleAIAlertBars()
    {
        // Debug.Log(GameManager.Singleton.ListOfType2s.Count);

        GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars = !GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars;

    }

    public void ToggleAIStatusIndicators()
    {
        GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators = !GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators;
    }

    public void ChangeDifficulty()
    {
        if (Difficulty.value == 0)
        {
            GameManager.Singleton.LTH_GameSettings.Difficulty = LTH_SaveData.Difficulties.Easy;

        }
        else if (Difficulty.value == 1)
        {
            GameManager.Singleton.LTH_GameSettings.Difficulty = LTH_SaveData.Difficulties.Medium;
        }
        else
        {
            GameManager.Singleton.LTH_GameSettings.Difficulty = LTH_SaveData.Difficulties.Hard;
        }

        int value = Difficulty.value;

        Debug.Log(GameManager.Singleton.LTH_GameSettings.Difficulty);
    }

    public void ToggleAISight()
    {
        GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch = !GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch;


    }

    public void ToggleAIhearing()
    {
        GameManager.Singleton.LTH_GameSettings.EnableAIHearing = !GameManager.Singleton.LTH_GameSettings.EnableAIHearing;

    }


    public void SetEasyModifier()
    {
        GameManager.Singleton.LTH_GameSettings.EasyModifier = float.Parse(EasyModifier.text);

        Debug.Log(GameManager.Singleton.LTH_GameSettings.EasyModifier);
    }

    public void SetMediumModifier()
    {
        GameManager.Singleton.LTH_GameSettings.MediumModifier = float.Parse(MediumModifier.text);

        Debug.Log(GameManager.Singleton.LTH_GameSettings.MediumModifier);
    }

    public void SetHardModifier()
    {
        GameManager.Singleton.LTH_GameSettings.HardModifer = float.Parse(HardModifier.text);

        Debug.Log(GameManager.Singleton.LTH_GameSettings.HardModifer);
    }

    public void SetInShadowModifier()
    {
        GameManager.Singleton.LTH_GameSettings.ShadowBonus = float.Parse(ShadowModifier.text);
    }

    public void SetDisanceNearModifier()
    {
        GameManager.Singleton.LTH_GameSettings.DistanceNearModifier = float.Parse(DistanceNearModifier.text);

    }
    public void SetDistanceFarModifier()
    {
        GameManager.Singleton.LTH_GameSettings.DistanceFarModifier = float.Parse(DistanceFarModifier.text);

    }
    public void SetNightModifier()
    {
        GameManager.Singleton.LTH_GameSettings.NightModifer = float.Parse(NightModifier.text);
    }

    public void ToggleLastSightingVisiblity()
    {
        GameManager.Singleton.LTH_GameSettings.LastSightingVisible = !GameManager.Singleton.LTH_GameSettings.LastSightingVisible;

        if (GameManager.Singleton.LastSighting != null)
        {

            GameManager.Singleton.LastSighting.GetComponent<Renderer>().enabled = GameManager.Singleton.LTH_GameSettings.LastSightingVisible;

        }


    }


}
