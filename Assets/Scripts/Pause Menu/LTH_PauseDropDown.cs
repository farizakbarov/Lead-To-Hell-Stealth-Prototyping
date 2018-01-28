using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class LTH_PauseDropDown : MonoBehaviour
{

    //This script attaches to the Dropdown UI Gameobjects. It sets the dropdown choices to the current correct settings.

    private Dropdown myDropdown;
    public bool Shadows;
    public bool Textures;
    public bool AO;
    public bool Quality;
    private PostProcessingBehaviour PP;
    public bool AAType;
    public bool AAQuality;

    // Use this for initialization
    void Awake()
    {

        myDropdown = GetComponent<Dropdown>();


    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Singleton.Paused)
        {

            if (Quality)
            {
                int qualityLevel = QualitySettings.GetQualityLevel();
                myDropdown.value = qualityLevel;
            }

            if (AAType)
            {
				if (GameManager.Singleton.LTH_QualityData.Quality_aa)
                {
                    myDropdown.interactable = true;
					if (GameManager.Singleton.LTH_QualityData.AA_Type == 0)
                    {
                        myDropdown.value = 0;
                    }
                    else
                    {
                        myDropdown.value = 1;
                    }
                }
                else
                {
                    myDropdown.interactable = false;
                }
            }

            if (AAQuality)
            {
				if (GameManager.Singleton.LTH_QualityData.Quality_aa && GameManager.Singleton.LTH_QualityData.AA_Type == 0)
                {
                    myDropdown.interactable = true;
                    if (GameManager.Singleton.MainPlayerCamera != null)
                    {
                        PP = GameManager.Singleton.MainPlayerCamera.GetComponent<PostProcessingBehaviour>();
                    }

                    if(PP.profile.antialiasing.settings.fxaaSettings.preset == AntialiasingModel.FxaaPreset.ExtremePerformance)
                    {
                        myDropdown.value = 0;
                    }
                    else if (PP.profile.antialiasing.settings.fxaaSettings.preset == AntialiasingModel.FxaaPreset.Performance)
                    {
                        myDropdown.value = 1;
                    }
                    else if (PP.profile.antialiasing.settings.fxaaSettings.preset == AntialiasingModel.FxaaPreset.Default)
                    {
                        myDropdown.value = 2;
                    }
                    else if (PP.profile.antialiasing.settings.fxaaSettings.preset == AntialiasingModel.FxaaPreset.Quality)
                    {
                        myDropdown.value = 3;
                    }
                    else
                    {
                        myDropdown.value = 4;
                    }

                }
                else
                {
                    myDropdown.interactable = false;
                }

            }




            if (Shadows)
            {
                if (QualitySettings.shadowResolution == ShadowResolution.Low)
                {
                    myDropdown.value = 0;
                }
                else if (QualitySettings.shadowResolution == ShadowResolution.Medium)
                {
                    myDropdown.value = 1;
                }
                else if (QualitySettings.shadowResolution == ShadowResolution.High)
                {
                    myDropdown.value = 2;
                }
                else
                {
                    myDropdown.value = 3;
                }
            }

            if (Textures)
            {
                if (QualitySettings.masterTextureLimit == 0)
                {
                    myDropdown.value = 0;
                }
                else if (QualitySettings.masterTextureLimit == 1)
                {
                    myDropdown.value = 1;
                }
                else
                {
                    myDropdown.value = 2;
                }
            }

            if (AO)
            {
				if (GameManager.Singleton.LTH_QualityData.Quality_AO)
                {
                    myDropdown.interactable = true;

                    if (GameManager.Singleton.MainPlayerCamera != null)
                    {
                        PP = GameManager.Singleton.MainPlayerCamera.GetComponent<PostProcessingBehaviour>();
                    }

                    if (PP.profile.ambientOcclusion.settings.sampleCount == AmbientOcclusionModel.SampleCount.Lowest)
                    {
                        myDropdown.value = 0;
                    }
                    else if (PP.profile.ambientOcclusion.settings.sampleCount == AmbientOcclusionModel.SampleCount.Low)
                    {
                        myDropdown.value = 1;
                    }
                    else if (PP.profile.ambientOcclusion.settings.sampleCount == AmbientOcclusionModel.SampleCount.Medium)
                    {
                        myDropdown.value = 2;
                    }
                    else
                    {
                        myDropdown.value = 3;
                    }
                }
                else
                {
                    myDropdown.interactable = false;
                }
            }
        }
    }
}
