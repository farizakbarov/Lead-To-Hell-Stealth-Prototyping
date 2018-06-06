using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    

    public static GameManager Singleton;

    public LTH_QualitySettings LTH_QualityData;

    /*Main Camera */
    public GameObject MainPlayerCamera;
    public GameObject CurrentCamera;
    public List<Camera> ListOfCameras = new List<Camera>();

    /* The Main Player*/
    public GameObject Player;
    public bool PlayerIsRunning;
    public bool PlayerIsWalking;
    public bool PlayerIsNotMoving;
    public float PlayerSpeed;
    public bool Stealth;
    public Rigidbody PlayerRigidBody;
    public Transform PlayerStart;
    public bool Flashlight;

    /*Fading the screen in and out*/
    public bool FadingEnabled = true;
    public GameObject ScreenFader;
    public GameObject FailMsg;
    public float GameOverTime = 5.0f;


    /*Global Quaility Settings*/
    public bool Paused;

    public void OnEnable()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }

        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("MainCamera"))
        {

            ListOfCameras.Add(fooObj.GetComponent<Camera>());
        }
    }

    // Use this for initialization
    void Start () {

        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            
        }
        else
        {
            
        }

        
    }
	
	// Update is called once per frame
	void Update () {


        
        if(PlayerRigidBody != null){
            PlayerRigidBody = Player.GetComponent<Rigidbody>();
            PlayerSpeed = PlayerRigidBody.velocity.magnitude;
        }

        CurrentCamera = Camera.main.gameObject;

       // Debug.Log(PlayerSpeed);
    }

    public void GameOver()
    {
        if (FadingEnabled)
        {
            if (FailMsg != null)
            {
                FailMsg.SetActive(true);
            }
            StartCoroutine("WaitAndPrint");
        }
    }

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(GameOverTime);
        // print("WaitAndPrint " + Time.time);
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(0);
    }
}
