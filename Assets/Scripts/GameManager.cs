using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using CoverShooter;
using System;

public class GameManager : MonoBehaviour {


    public LTH_SaveData LTH_GameSettings;
	public LTH_QualitySettings LTH_QualityData;
	
	public static GameManager Singleton;
    public List<Transform> AllAi = new List<Transform>();
    public List<GameObject> ListOfType1s = new List<GameObject>();
	public List<GameObject> ListOfType2s = new List<GameObject>();
    public List<Camera> ListOfCameras = new List<Camera>();
    //public List<Camera> ListOfCameras = new List<Camera>();
    //public List<GameObject> ListOfSecurityCams = new List<GameObject>();

    public GameObject GameplayTimeline;
    public GameObject MainPlayerCamera;
	public GameObject ActivePlayer;
	public GameObject Detective;
	public GameObject Dog;

	/*public float CurrentAlertLevel = 0.0f;
	public float CurrentNoiseLevel = 0.0f;
	public float AlertIncrementAmount = 0.01f;*/
	
	//private UnityEngine.AI.NavMeshAgent PlayerNavAgent;
	//private UnityEngine.AI.NavMeshAgent DogNavAgent;
	
	//private DogAgent DogScript;
	//private FollowerAgent PlayerDogScript;
	
	//private CameraDetect DogCamDetect;
	//private CameraDetect CamDetect;
	
	//private PlayerAgent Agent;
	//private PlayerAgent DogPlayerAgent;

	public bool Stealth;
	
	//public bool CharacterSwitch;

	//public bool EnableAISightSwitch;
	//public bool EnableAIHearing;
    //public bool Stay;

    //public bool PlayerHasBeenSighted;
    public bool PlayerInSight;

    //public bool TrackPlayerMovement;
	public bool PlayerCaught;
    public bool PlayerSafe;
	public bool PlayerIsRunning;
	public GameObject LastSighting;
	//public GameObject AlertBar;
	public bool Flashlight;

	public GameObject FailMsg;
    public float GameOverTime = 5.0f;

    public bool FadingEnabled = true;
	public GameObject ScreenFader;

    public bool Paused;
    /*public bool Quality_aa;
    public bool Quality_LensEffects;
    public bool Quality_Dof;
  public bool Quality_AO;
    public bool Quality_MotionBlur;
    public bool BlackAndWhiteMode;
    public int AA_Type;*/


    public GameObject GameplayUI;

    //public enum Difficulties { Easy, Medium, Hard };

    //public Difficulties Difficulty = Difficulties.Medium;

    //public enum TimeOfDays { Day, Night };

    //public TimeOfDays TimeOfDay = TimeOfDays.Day;

    public float PlayerLighting;


    public float ShadowModifier;
   // public float ShadowBonus = 0.25f;


    public float DifficultyModifier;
   // public float EasyModifier = 0.5f;
   // public float MediumModifier = 1.0f;
   // public float HardModifer = 2.0f;


    public float TimeOfDayModifier;
   // public float DayModifier = 1.0f;
    //public float NightModifer = 0.75f;

    //public float DistanceNearModifier = 2.0f;
   // public float DistanceFarModifier = 0.25f;

    //public bool EnableAIAlertBars;
    //public bool EnableAIStatusIndicators;

    //public bool LastSightingVisible;

    public GameObject GhostParent;
    public GameObject GhostMesh;


    public void OnEnable(){
		if(Singleton == null){
			Singleton = this;
		}


        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("MainCamera"))
        {

            ListOfCameras.Add(fooObj.GetComponent<Camera>());
        }
       // Debug.Log(ListOfCameras.Count);
    }
	
	public void AddType1(GameObject t){
		if(!ListOfType1s.Contains(t))
		{
			ListOfType1s.Add(t);
		}
	}

	public void AddType2(GameObject t){
		if(!ListOfType2s.Contains(t))
		{
			ListOfType2s.Add(t);
		}
	}

	
	/*public void RemoveType1(NPCAgent t){
		ListOfType1s.Remove(t);		
	}
	
	public void RemoveType2(NPCAgent t){
		ListOfType2s.Remove(t);		
	}*/



    public void GameOver()
    {
        if (FadingEnabled)
        {
            ActivePlayer.GetComponent<CharacterMotor>().IsAlive = false;
			if (FailMsg != null) {
				FailMsg.SetActive (true);
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


    // Use this for initialization
    void Start () {

		//AlertBar = GameObject.FindGameObjectWithTag("AlertBar");
		Detective = GameObject.FindGameObjectWithTag("Player");
		//Dog = GameObject.FindGameObjectWithTag("Dog");


		if(Detective != null){
			//PlayerNavAgent = Detective.GetComponent<UnityEngine.AI.NavMeshAgent>();
			/*PlayerDogScript = Detective.GetComponent<FollowerAgent>();
			CamDetect = Detective.GetComponent<CameraDetect>();		
			Agent = Detective.GetComponent<PlayerAgent>();*/
		}

		if(Dog != null){
			//DogNavAgent = Dog.GetComponent<UnityEngine.AI.NavMeshAgent>();
			/*DogScript = Dog.GetComponent<DogAgent>();
			DogCamDetect = Dog.GetComponent<CameraDetect>();
			DogPlayerAgent = Dog.GetComponent<PlayerAgent>();*/
		}
	

		if(Detective != null){
			ActivePlayer = Detective;
		}else{
			ActivePlayer = Dog;
		}

		LastSighting = GameObject.Find("LastSighting");

		if(LastSighting == null){
			LastSighting = new GameObject("LastSighting");
		}




		/*if(FailMsg == null){
			FailMsg = new GameObject("Failmsg");
			FailMsg.AddComponent<GUITexture>();
			Texture2D failtex = Resources.Load("GUI/CaughtMsg", typeof(Texture2D)) as Texture2D;
			FailMsg.GetComponent<GUITexture>().texture = failtex;
			FailMsg.GetComponent<GUITexture>().pixelInset = new Rect(-failtex.width/2,-failtex.height/2,failtex.width, failtex.height);
			FailMsg.transform.position = new Vector3(0.5f, 0.5f, 0);
			FailMsg.transform.localScale = Vector3.zero;
			FailMsg.SetActive(false);
		}*/
		/*if(ActivePlayer == Player){
			DisableAISight();
		}*/
	}

	/*void DisableAISight(){
		foreach(NPCAgent g in ListOfType1s){
			g.DisableSight();
		}
		foreach(NPCAgent g in ListOfType2s){
			g.DisableSight();
		}
	}

	void EnableAISight(){
		foreach(NPCAgent g in ListOfType1s){
			g.EnableSight();
		}
		foreach(NPCAgent g in ListOfType2s){
			g.EnableSight();
		}
	}*/

	/*void awake(){
		if(ActivePlayer == Player){
			DisableAISight();
		}
	}*/

	//function for finding the closest type 2 to call him over
	/*public NPCAgent FindNearestType2(){
		NPCAgent NearestType2 = null;
		float Type2distance = Mathf.Infinity; 
		//loop through all the type 2s in the scene
		foreach(NPCAgent obj in ListOfType2s){
			float distance2 =Vector3.Distance(this.transform.position, obj.transform.position);
			//if the distance to the Type2 is less than the previously registered distance, store it
			if(distance2 < Type2distance){
				NearestType2 = obj;
				Type2distance = distance2;
			}
		}
		//tell that Type 2 to seek out the player
		return NearestType2;
	}*/
	
	// Update is called once per frame
	void Update () {



        if (PlayerInSight)
        {
            LastSighting.transform.position = ActivePlayer.transform.position;
        }

		if(Input.GetKeyDown(KeyCode.Keypad1)){
            LTH_GameSettings.EnableAISightSwitch = !LTH_GameSettings.EnableAISightSwitch;
		}

		/*if(PlayerCaught && FailMsg !=null){
			FailMsg.SetActive(true);
			Detective.GetComponent<PlayerController>().DisableInput=true;
		}*/

		/*if(!PlayerHasBeenSighted && GameManager.Singleton.AlertBar != null){
			if(CurrentAlertLevel > 0.0f){
				GameManager.Singleton.AlertBar.GetComponent<GUITexture>().color = Color.white;
				CurrentAlertLevel -= 0.01f;
			}
		}else{
			if(CurrentAlertLevel < 1.0f){

				CurrentAlertLevel += AlertIncrementAmount;
			}
		}*/

		if(Dog != null){
			/*if(Input.GetButtonDown("Stay")){
				Stay = !Stay;
				Debug.Log("Stay: " + Stay);
			}*/
			
			/*if(Stay){			
				if(CharacterSwitch){
					DogScript.following = false;
				}else{
					PlayerDogScript.following = false;
				}
			}
			else{
				if(CharacterSwitch){
					PlayerDogScript.following = true;
				}else{
					DogScript.following = true;
				}
			}*/
			
			/*if(Input.GetButtonDown("Switch")){
				CharacterSwitch = !CharacterSwitch;
				
				if(CharacterSwitch){
					if(EnableAISightSwitch){
						EnableAISight();
					}
					//Disable the Dog's Navmesh
					DogNavAgent.enabled = false;
					
					//Disable the script that makes the Dog Follow
					DogScript.enabled = false;
					
					//Enable the scripts that gives you control of the Dog
					DogCamDetect.enabled = true;				
					DogPlayerAgent.enabled = true;
					
					//Set the camera's target to the Dog
					CameraScript.target = Dog.transform.Find("PlayerCameraTarget").gameObject.transform;
					
					//Turn on the Player's Navmesh and set his stopping distance
					PlayerNavAgent.enabled = true;
					PlayerNavAgent.stoppingDistance = 2.0f;
					
					//Enable the follow script on the player
					PlayerDogScript.enabled = true;
					//Disable the scripts to give you control of the Player
					CamDetect.enabled = false;
					Agent.enabled = false;

					ActivePlayer = Dog;
				}
				else{
					if(EnableAISightSwitch){
						DisableAISight();
					}

					//Enable the Dog's Navmesh and set its stopping distance
					DogNavAgent.enabled = true;
					DogNavAgent.stoppingDistance = 2.0f;
					
					//Enable the Script that make the Dog Follow
					DogScript.enabled = true;
					
					//disable the scripts that gives you control of the Dog
					DogPlayerAgent.enabled = false;
					DogCamDetect.enabled = false;
					
					//Set the camera's target to the Player
					CameraScript.target = Detective.transform.Find("PlayerCameraTarget").gameObject.transform;
					
					//Turn off the Player's Navmesh and reset his stopping distance
					PlayerNavAgent.stoppingDistance = 0.5f;
					PlayerNavAgent.enabled = false;
					
					//Disable the follow script on the player
					PlayerDogScript.enabled = false;
					//Enable the scripts to give you control of the Player
					CamDetect.enabled = true;
					Agent.enabled = true;

					ActivePlayer = Detective;
				}
			}*/
		}
		
	}

    /// <summary>
    /// Sets <variable> to <value> based on its type T and additionally
    /// stores it in the registry or plist using key <varName>
    /// </summary>
    /// <auth: Isaac Dart (isaac@mantle.tech) >
    public void SetPersistentVar<T>(string varName, ref T variable)
    {

            //variable = value;

            Type varType = variable.GetType();
            if (varType == typeof(int))
            {
                int intVal = Convert.ToInt32(variable);
                PlayerPrefs.SetInt("i_" + varName, intVal);
            }
            else if (varType == typeof(bool))
            {
                int intVal = Convert.ToInt32(variable);
                PlayerPrefs.SetInt("b_" + varName, intVal);
            }
            else if (varType == typeof(float))
            {
                float floatVal = (float)(Convert.ToDouble(variable));
                PlayerPrefs.SetFloat("f_" + varName, floatVal);
            }
            else
            {
                string stringVal = Convert.ToString(variable);
                PlayerPrefs.SetString("s_" + varName, stringVal);
            }

           // Debug.Log("Saved: " + varName + ", Value: " + variable);
            PlayerPrefs.Save();
        

    }

    /// <summary>
    /// Returns a value of type T from the registry or plist using <varName>
    /// </summary>
    /// <auth: Isaac Dart (isaac@mantle.tech) >
    public T GetPersistentVar<T>(string varName, T defaultValue)
    {

            T variable = defaultValue;


            Type varType = variable.GetType();
            if (varType == typeof(int))
            {
                int defaultIntVal = Convert.ToInt32(defaultValue);
                int intVal = PlayerPrefs.GetInt("i_" + varName, defaultIntVal);
                variable = (T)Convert.ChangeType(intVal, varType);
            }
            else if (varType == typeof(bool))
            {
                int defaultIntVal = Convert.ToInt32(defaultValue);
                int intVal = PlayerPrefs.GetInt("b_" + varName, defaultIntVal);
                bool boolVal = intVal != 0;
                variable = (T)Convert.ChangeType(boolVal, varType);
            }
            else if (varType == typeof(float))
            {
                float defaultFloatVal = (float)(Convert.ToDouble(defaultValue));
                float floatVal = PlayerPrefs.GetFloat("f_" + varName, defaultFloatVal);
                variable = (T)Convert.ChangeType(floatVal, varType);
            }
            else
            {
                string defaultStringVal = Convert.ToString(defaultValue);
                string stringVal = PlayerPrefs.GetString("s_" + varName, defaultStringVal);
                variable = (T)Convert.ChangeType(stringVal, varType);
            }
        

            return variable;
        }

            
    

}
