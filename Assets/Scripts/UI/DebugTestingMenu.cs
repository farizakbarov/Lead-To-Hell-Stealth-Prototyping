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

    public Image InfinateAmmo;

    // Use this for initialization
    void Start()
    {
        if (Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty == LTH_SaveData.Difficulties.Easy)
        {
            Difficulty.value = 0;
        }
        else if (Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty == LTH_SaveData.Difficulties.Medium)
        {
            Difficulty.value = 1;
        }
        else
        {
            Difficulty.value = 2;
        }

        EasyModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.EasyModifier.ToString();
        MediumModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.MediumModifier.ToString();
        HardModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.HardModifer.ToString();
        DistanceFarModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.DistanceFarModifier.ToString();
        DistanceNearModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.DistanceNearModifier.ToString();
        ShadowModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.ShadowBonus.ToString();
        NightModifier.text = Stealth_GameManager.Singleton.LTH_GameSettings.NightModifer.ToString();


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


            Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars = GameManager.Singleton.GetPersistentVar<bool>("AIAlertBars", true);

            Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators = GameManager.Singleton.GetPersistentVar < bool >("AIStatus", true);

            Difficulty.value = GameManager.Singleton.GetPersistentVar<int>("Difficulty", 0);


           GameManager.Singleton.EnableAISightSwitch = GameManager.Singleton.GetPersistentVar<bool>("AISight", true);


            GameManager.Singleton.EnableAIHearing = GameManager.Singleton.GetPersistentVar<bool>("AIHearing", true);




    
            Stealth_GameManager.Singleton.LTH_GameSettings.EasyModifier = GameManager.Singleton.GetPersistentVar<float>("EasyModifier", 0);
        }*/


    }

    // Update is called once per frame
    void Update()
    {

       /* if (Input.GetKeyDown(KeyCode.Escape))
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
        }*/

        if (Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels)
        {
            AIStatusLabelButton.color = Color.green;
        }
        else
        {
            AIStatusLabelButton.color = Color.red;
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.SpottedUI)
        {
            ScreenSightButton.color = Color.green;
        }
        else
        {
            ScreenSightButton.color = Color.red;
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.LightSensorUI)
        {
            LightSensorButton.color = Color.green;
        }
        else
        {
            LightSensorButton.color = Color.red;
        }


        if (Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars)
        {
            AIAlertButton.color = Color.green;
        }
        else
        {
            AIAlertButton.color = Color.red;
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators)
        {
            AIStatusButton.color = Color.green;
        }
        else
        {
            AIStatusButton.color = Color.red;
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch)
        {
            AISight.color = Color.green;
        }
        else
        {
            AISight.color = Color.red;
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIHearing)
        {
            AIHearing.color = Color.green;
        }
        else
        {
            AIHearing.color = Color.red;
        }

        if (GameManager.Singleton.Player != null)
        {
            if (Stealth_GameManager.Singleton.LTH_GameSettings.RollingAbility)
            {
                PlayerRolling.color = Color.green;
            }
            else
            {
                PlayerRolling.color = Color.red;
            }

            if (Stealth_GameManager.Singleton.LTH_GameSettings.SlidingAbility)
            {
                PlayerSlide.color = Color.green;
            }
            else
            {
                PlayerSlide.color = Color.red;
            }

            if (Stealth_GameManager.Singleton.LTH_GameSettings.SneakToggle)
            {
                PlayerStealthToggle.color = Color.green;
            }
            else
            {
                PlayerStealthToggle.color = Color.red;
            }
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.LastSightingVisible)
        {
            LastSightingBox.color = Color.green;
        }
        else
        {
            LastSightingBox.color = Color.red;
        }

        if (Stealth_GameManager.Singleton.LTH_GameSettings.InfinateAmmo)
        {
            InfinateAmmo.color = Color.green;
        }
        else
        {
            InfinateAmmo.color = Color.red;
        }

        
    }

    public void ToggleInfinateAmmo()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.InfinateAmmo = !Stealth_GameManager.Singleton.LTH_GameSettings.InfinateAmmo;
    }

    public void ToggleSlidingAbility()
    {

        Stealth_GameManager.Singleton.LTH_GameSettings.SlidingAbility = !Stealth_GameManager.Singleton.LTH_GameSettings.SlidingAbility;


        GameManager.Singleton.Player.GetComponent<CharacterSlide>().SlidingEnabled = Stealth_GameManager.Singleton.LTH_GameSettings.SlidingAbility;

    }

    public void SneakToggle()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.SneakToggle = !Stealth_GameManager.Singleton.LTH_GameSettings.SneakToggle;


        GameManager.Singleton.Player.GetComponent<LTH_ThirdPersonController>().ToggleSneak = Stealth_GameManager.Singleton.LTH_GameSettings.SneakToggle;

    }



    public void ToggleAIStateLabels()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels = !Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels;


        PlaymakerGUI.drawStateLabels = Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIPlaymakerLabels;

    }

    public void ToggleRollingAblitiy()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.RollingAbility = !Stealth_GameManager.Singleton.LTH_GameSettings.RollingAbility;


        GameManager.Singleton.Player.GetComponent<ThirdPersonController>().EnableRolling = Stealth_GameManager.Singleton.LTH_GameSettings.RollingAbility;
    }



    public void ToggleOnScreenSight()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.SpottedUI = !Stealth_GameManager.Singleton.LTH_GameSettings.SpottedUI;

        OnScreenSight.SetActive(Stealth_GameManager.Singleton.LTH_GameSettings.SpottedUI);

    }

    public void ToggleLightSensorUI()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.LightSensorUI = !Stealth_GameManager.Singleton.LTH_GameSettings.LightSensorUI;

        LightSensor.SetActive(Stealth_GameManager.Singleton.LTH_GameSettings.LightSensorUI);

    }


    public void ToggleAIAlertBars()
    {
        // Debug.Log(GameManager.Singleton.ListOfType2s.Count);

        Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars = !Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIAlertBars;

    }

    public void ToggleAIStatusIndicators()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators = !Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIStatusIndicators;
    }

    public void ChangeDifficulty()
    {
        if (Difficulty.value == 0)
        {
            Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty = LTH_SaveData.Difficulties.Easy;

        }
        else if (Difficulty.value == 1)
        {
            Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty = LTH_SaveData.Difficulties.Medium;
        }
        else
        {
            Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty = LTH_SaveData.Difficulties.Hard;
        }

        int value = Difficulty.value;

        Debug.Log(Stealth_GameManager.Singleton.LTH_GameSettings.Difficulty);
    }

    public void ToggleAISight()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch = !Stealth_GameManager.Singleton.LTH_GameSettings.EnableAISightSwitch;


    }

    public void ToggleAIhearing()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIHearing = !Stealth_GameManager.Singleton.LTH_GameSettings.EnableAIHearing;

    }


    public void SetEasyModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.EasyModifier = float.Parse(EasyModifier.text);

        Debug.Log(Stealth_GameManager.Singleton.LTH_GameSettings.EasyModifier);
    }

    public void SetMediumModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.MediumModifier = float.Parse(MediumModifier.text);

        Debug.Log(Stealth_GameManager.Singleton.LTH_GameSettings.MediumModifier);
    }

    public void SetHardModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.HardModifer = float.Parse(HardModifier.text);

        Debug.Log(Stealth_GameManager.Singleton.LTH_GameSettings.HardModifer);
    }

    public void SetInShadowModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.ShadowBonus = float.Parse(ShadowModifier.text);
    }

    public void SetDisanceNearModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.DistanceNearModifier = float.Parse(DistanceNearModifier.text);

    }
    public void SetDistanceFarModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.DistanceFarModifier = float.Parse(DistanceFarModifier.text);

    }
    public void SetNightModifier()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.NightModifer = float.Parse(NightModifier.text);
    }

    public void ToggleLastSightingVisiblity()
    {
        Stealth_GameManager.Singleton.LTH_GameSettings.LastSightingVisible = !Stealth_GameManager.Singleton.LTH_GameSettings.LastSightingVisible;

        if (Stealth_GameManager.Singleton.LastSighting != null)
        {

            Stealth_GameManager.Singleton.LastSighting.GetComponent<Renderer>().enabled = Stealth_GameManager.Singleton.LTH_GameSettings.LastSightingVisible;

        }


    }


}
