using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{

    //public bool FadingEnabled;
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    public bool sceneStarting = true;
    public bool sceneEnding = false;// Whether or not the scene is still fading in.
    private Image myImage;
    private Color myColor;


    void Awake()
    {

        // Set the texture so that it is the the size of the screen and covers it.
        //GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        myImage = GetComponent<Image>();

        if (GameManager.Singleton.FadingEnabled)
        {
            if (sceneStarting)
            {
                GetComponent<Image>().color = Color.black;
            }
        }
        else
        {

            GetComponent<Image>().color = Color.clear;
        }
    }


    void Update()
    {
        if (GameManager.Singleton.FadingEnabled)
        {

            if (sceneStarting)
            {
                StartScene();

            }
            if (sceneEnding)
            {
                SceneEnd();
            }
        }
    }


    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.

        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.black, fadeSpeed * Time.time);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        //  myColor = Color.Lerp(myColor, Color.black, fadeSpeed * Time.deltaTime);
        myImage.CrossFadeAlpha(1.0f, fadeSpeed, false);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        // myImage.CrossFadeAlpha(0.01f, fadeSpeed, false);

        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.clear, fadeSpeed * Time.time);

        // If the texture is almost clear...
        if (GetComponent<Image>().color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            // myColor = Color.clear;
            GetComponent<Image>().color = Color.clear;
            myImage.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }

    void SceneEnd()
    {
        FadeToBlack();

        if (GetComponent<Image>().color.a >= 0.95f)
        {
            // ... set the colour to clear and disable the GUITexture.
            // myColor = Color.clear;
            GetComponent<Image>().color = Color.black;

            // The scene is no longer starting.
            sceneEnding = false;

            Debug.Log("Load Next Level");
        }

    }


    public void EndScene()
    {
        // Make sure the texture is enabled.
        myImage.enabled = true;

        // Start fading towards black.
        //FadeToBlack();
        sceneEnding = true;

        // If the screen is almost black...
    }
}