using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;

public class LTH_PauseMenu : MonoBehaviour
{

    // public bool Paused;
    public bool GraphicsMenu;
    public GameObject MainMenuParent;
    public GameObject GraphicsMenuParent;
    public GameObject ShadowDropdown;
    public GameObject TextureDropdown;
    public GameObject AODropdown;
    public GameObject QualityDropdown;
    public GameObject AATypeDropdown;
    public GameObject AAQualityDropdown;

    // Use this for initialization
    void Start()
    {

        MainMenuParent.SetActive(false);
        GraphicsMenuParent.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        //check if pause button (escape key) is pressed
        if (Input.GetKeyDown("escape") || Input.GetButtonDown("Start"))
        {

            //check if game is already paused		
            if (GameManager.Singleton.Paused == true)
            {
                //unpause the game
                GameManager.Singleton.Paused = false;
                Time.timeScale = 1;
                AudioListener.volume = 1;
                MainMenuParent.SetActive(false);
                GraphicsMenuParent.SetActive(false);
                //Screen.showCursor = false;	

                /*for(var cam in ListOfPhysics){
                    cam.GetComponent(InteractiveCloth).enabled = true;					
                } */

            }

            //else if game isn't paused, then pause it
            else if (GameManager.Singleton.Paused == false)
            {
                GameManager.Singleton.Paused = true;
                AudioListener.volume = 0;
                Time.timeScale = 0;
                MainMenuParent.SetActive(true);
                GraphicsMenuParent.SetActive(false);
                /*for(var cam in ListOfPhysics){
                    //cam.SetActive (false);	
                    cam.GetComponent(InteractiveCloth).enabled = false;				
                } */
                //Screen.showCursor = true;
            }
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        GameManager.Singleton.Paused = false;
        MainMenuParent.SetActive(false);
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void ShowGraphicsMenu()
    {
        MainMenuParent.SetActive(false);
        GraphicsMenuParent.SetActive(true);
    }

    public void HideGraphicsMenu()
    {
        MainMenuParent.SetActive(true);
        GraphicsMenuParent.SetActive(false);
    }

    public void Toggle_AA()
    {
		GameManager.Singleton.LTH_QualityData.Quality_aa = !GameManager.Singleton.LTH_QualityData.Quality_aa;
    }

    public void Toggle_Lens()
    {
		GameManager.Singleton.LTH_QualityData.Quality_LensEffects = !GameManager.Singleton.LTH_QualityData.Quality_LensEffects;
    }

    public void Toggle_Dof()
    {
		GameManager.Singleton.LTH_QualityData.Quality_Dof = !GameManager.Singleton.LTH_QualityData.Quality_Dof;
    }

    public void Toggle_MB()
    {
		GameManager.Singleton.LTH_QualityData.Quality_MotionBlur = !GameManager.Singleton.LTH_QualityData.Quality_MotionBlur;
    }

    public void Toggle_AO()
    {
		GameManager.Singleton.LTH_QualityData.Quality_AO = !GameManager.Singleton.LTH_QualityData.Quality_AO;
    }

    public void ToggleBlackAndWhite()
    {
		GameManager.Singleton.LTH_QualityData.BlackAndWhiteMode = !GameManager.Singleton.LTH_QualityData.BlackAndWhiteMode;
    }

    public void SetShadowQuality()
    {

        if (ShadowDropdown.GetComponent<Dropdown>().value == 0)
        {
            QualitySettings.shadowResolution = ShadowResolution.Low;
        }
        else if (ShadowDropdown.GetComponent<Dropdown>().value == 1)
        {
            QualitySettings.shadowResolution = ShadowResolution.Medium;
        }
        else if (ShadowDropdown.GetComponent<Dropdown>().value == 2)
        {
            QualitySettings.shadowResolution = ShadowResolution.High;
        }
        else
        {
            QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
        }
    }

    public void SetTextureQuality()
    {
        if (TextureDropdown.GetComponent<Dropdown>().value == 0)
        {
            QualitySettings.masterTextureLimit = 0;
        }
        else if (TextureDropdown.GetComponent<Dropdown>().value == 1)
        {
            QualitySettings.masterTextureLimit = 1;
        }
        else
        {
            QualitySettings.masterTextureLimit = 2;
        }
    }


    public void SetAOQuality()
    {
        PostProcessingBehaviour PP = GameManager.Singleton.MainPlayerCamera.GetComponent<PostProcessingBehaviour>();
        AmbientOcclusionModel.Settings settings = PP.profile.ambientOcclusion.settings;

        if (AODropdown.GetComponent<Dropdown>().value == 0)
        {

            settings.sampleCount = AmbientOcclusionModel.SampleCount.Lowest;
            PP.profile.ambientOcclusion.settings = settings;
        }

        else if (AODropdown.GetComponent<Dropdown>().value == 1)
        {
            settings.sampleCount = AmbientOcclusionModel.SampleCount.Low;
            PP.profile.ambientOcclusion.settings = settings;
        }
        else if (AODropdown.GetComponent<Dropdown>().value == 2)
        {
            settings.sampleCount = AmbientOcclusionModel.SampleCount.Medium;
            PP.profile.ambientOcclusion.settings = settings;
        }
        else
        {
            settings.sampleCount = AmbientOcclusionModel.SampleCount.High;
            PP.profile.ambientOcclusion.settings = settings;
        }


    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(QualityDropdown.GetComponent<Dropdown>().value);
    }

    public void SetGI()
    {
        //  Lightmapping.realtimeGI = !Lightmapping.realtimeGI;
        // Lightmapping.giWorkflowMode = 
    }

    public void SetAAType()
    {
        PostProcessingBehaviour PP = GameManager.Singleton.MainPlayerCamera.GetComponent<PostProcessingBehaviour>();
        AntialiasingModel.Settings settings = PP.profile.antialiasing.settings;

        //Debug.Log(AATypeDropdown.GetComponent<Dropdown>().value);

        if (AATypeDropdown.GetComponent<Dropdown>().value == 0)
        {
            Debug.Log("FXAA");
			GameManager.Singleton.LTH_QualityData.AA_Type = 0;
            //Set the settings in the Post Process Asset
            PP.profile.antialiasing.enabled = true;
            settings.method = AntialiasingModel.Method.Fxaa;
            PP.profile.antialiasing.settings = settings;

            //DIsable MSAA
            QualitySettings.antiAliasing = 0;
            GameManager.Singleton.MainPlayerCamera.GetComponent<Camera>().allowMSAA = false;
        }
        else if (AATypeDropdown.GetComponent<Dropdown>().value == 1)
        {
            Debug.Log("TXAA");
            //set the settings in the post process asset.
			GameManager.Singleton.LTH_QualityData.AA_Type = 1;
            PP.profile.antialiasing.enabled = true;
            settings.method = AntialiasingModel.Method.Taa;
            PP.profile.antialiasing.settings = settings;
            //disable MSAA
            QualitySettings.antiAliasing = 0;
            GameManager.Singleton.MainPlayerCamera.GetComponent<Camera>().allowMSAA = false;
        }
        else if (AATypeDropdown.GetComponent<Dropdown>().value == 2)
        {
            Debug.Log("MSAA");
            //TUrn off the AA settings in the Post Process Asset
			GameManager.Singleton.LTH_QualityData.AA_Type = 2;
            PP.profile.antialiasing.enabled = false;
            //Exable MSAA
            QualitySettings.antiAliasing = 2;
            GameManager.Singleton.MainPlayerCamera.GetComponent<Camera>().allowMSAA = true;
        }

    }

    public void SetAAQuality()
    {
        PostProcessingBehaviour PP = GameManager.Singleton.MainPlayerCamera.GetComponent<PostProcessingBehaviour>();
        AntialiasingModel.Settings settings = PP.profile.antialiasing.settings;


        if (AAQualityDropdown.GetComponent<Dropdown>().value == 0)
        {
            settings.fxaaSettings.preset = AntialiasingModel.FxaaPreset.ExtremePerformance;
        }
        else if (AAQualityDropdown.GetComponent<Dropdown>().value == 1)
        {
            settings.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Performance;
        }
        else if (AAQualityDropdown.GetComponent<Dropdown>().value == 2)
        {
            settings.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Default;
        }
        else if (AAQualityDropdown.GetComponent<Dropdown>().value == 3)
        {
            settings.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Quality;
        }
        else
        {
            settings.fxaaSettings.preset = AntialiasingModel.FxaaPreset.ExtremeQuality;
        }
        PP.profile.antialiasing.settings = settings;
    }

}
