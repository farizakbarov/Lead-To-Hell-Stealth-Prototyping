using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stealth_GameManager : MonoBehaviour {

    public static Stealth_GameManager Singleton;

    public LTH_SaveData LTH_GameSettings;

    public List<Transform> AllAi = new List<Transform>();
    public List<GameObject> ListOfType1s = new List<GameObject>();
    public List<GameObject> ListOfType2s = new List<GameObject>();

    public GameObject Dog;
    public GameObject GameplayTimeline;

    public bool PlayerInSight;
    public GameObject LastSighting;

    public bool PlayerCaught;
    public bool PlayerSafe;
    public GameObject GameplayUI;

    public float PlayerLighting;


    public float ShadowModifier;
    // public float ShadowBonus = 0.25f;
    public float DifficultyModifier;

    public float TimeOfDayModifier;
    public GameObject GhostParent;
    public GameObject GhostMesh;

    public bool HasPaperThrowable;
    public bool HasFireExtinguisher;
    public bool PaperReady;
    public bool FireExtinguisherReady;


    public void OnEnable()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void AddType1(GameObject t)
    {
        if (!ListOfType1s.Contains(t))
        {
            ListOfType1s.Add(t);
        }
    }

    public void AddType2(GameObject t)
    {
        if (!ListOfType2s.Contains(t))
        {
            ListOfType2s.Add(t);
        }
    }

    //loop through all the AI in the scene, If any of them can see the player, return true, if none can see him return false
    private bool IsPlayerInSight()
    {
        for (int i = 0; i < AllAi.Count; ++i)
        {
            if (AllAi[i].GetComponent<LTHMoveAnimator>().CanSeePlayer == true)
            {
                return true;
            }
        }

        return false;
    }


    // Use this for initialization
    void Start () {
        LastSighting = GameObject.Find("LastSighting");

        if (LastSighting == null)
        {
            LastSighting = new GameObject("LastSighting");
        }
    }
	
	// Update is called once per frame
	void Update () {

        PlayerInSight = IsPlayerInSight();

        if (PlayerInSight && LastSighting != null)
        {
            LastSighting.transform.position = GameManager.Singleton.Player.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            LTH_GameSettings.EnableAISightSwitch = !LTH_GameSettings.EnableAISightSwitch;
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
