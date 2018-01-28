using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
[CanEditMultipleObjects]

public class GameManagerEditor : Editor {
    // Use this for initialization


    SerializedProperty GameplayTimeline;
    SerializedProperty MainPlayerCamera;
    SerializedProperty ActivePlayer;
    SerializedProperty Detective;
//    SerializedProperty Dog;

    SerializedProperty Stealth;

    SerializedProperty EnableAISightSwitch;
    SerializedProperty EnableAIHearing;

    SerializedProperty PlayerInSight;

    SerializedProperty PlayerCaught;
    SerializedProperty PlayerSafe;
    SerializedProperty PlayerIsRunning;
    SerializedProperty LastSighting;
    SerializedProperty AlertBar;
    SerializedProperty Flashlight;

    SerializedProperty FailMsg;
    SerializedProperty GameOverTime;

    SerializedProperty FadingEnabled;
    SerializedProperty ScreenFader;

    SerializedProperty Paused;
    /*SerializedProperty Quality_aa;
    SerializedProperty Quality_LensEffects;
    SerializedProperty Quality_Dof;
    SerializedProperty Quality_AO;
    SerializedProperty Quality_MotionBlur;
    SerializedProperty BlackAndWhiteMode;
    SerializedProperty AA_Type;*/

    SerializedProperty GameplayUI;


    SerializedProperty AllAi;
    SerializedProperty ListOfType1s;
    SerializedProperty ListOfType2s;
    SerializedProperty ListOfCameras;

    SerializedProperty PlayerLighting;


    //SerializedProperty Difficulty;

    //SerializedProperty TimeOfDay;

    //SerializedProperty ShadowModifier;
    //SerializedProperty ShadowBonus;


    //SerializedProperty DifficultyModifier;
    //SerializedProperty EasyModifier;
    //SerializedProperty MediumModifier;
    //SerializedProperty HardModifer;


   // SerializedProperty TimeOfDayModifier;
   // SerializedProperty DayModifier;
   // SerializedProperty NightModifer;

   // SerializedProperty DistanceNearModifier;
   // SerializedProperty DistanceFarModifier;

	//SerializedProperty EnableAIAlertBars;
	//SerializedProperty EnableAIStatusIndicators;

   //erializedProperty LastSightingVisible;


    SerializedProperty GhostParent;
    SerializedProperty GhostMesh;


    SerializedProperty LTH_GameSettings;

	SerializedProperty LTH_QualityData;



    void OnEnable()
    {
        GameplayTimeline = serializedObject.FindProperty("GameplayTimeline");
        MainPlayerCamera = serializedObject.FindProperty("MainPlayerCamera");
        ActivePlayer = serializedObject.FindProperty("ActivePlayer");
        Detective = serializedObject.FindProperty("Detective");
        //Dog = serializedObject.FindProperty("Dog");

        Stealth = serializedObject.FindProperty("Stealth");

        //EnableAISightSwitch = serializedObject.FindProperty("EnableAISightSwitch");
        //EnableAIHearing = serializedObject.FindProperty("EnableAIHearing");

        PlayerInSight = serializedObject.FindProperty("PlayerInSight");

        PlayerCaught = serializedObject.FindProperty("PlayerCaught");
        PlayerSafe = serializedObject.FindProperty("PlayerSafe");
        PlayerIsRunning = serializedObject.FindProperty("PlayerIsRunning");
        LastSighting = serializedObject.FindProperty("LastSighting");
        AlertBar = serializedObject.FindProperty("AlertBar");
        Flashlight = serializedObject.FindProperty("Flashlight");

        FailMsg = serializedObject.FindProperty("FailMsg");
        GameOverTime = serializedObject.FindProperty("GameOverTime");

        FadingEnabled = serializedObject.FindProperty("FadingEnabled");
        ScreenFader = serializedObject.FindProperty("ScreenFader");

        Paused = serializedObject.FindProperty("Paused");
        /*Quality_aa = serializedObject.FindProperty("Quality_aa");
        Quality_LensEffects = serializedObject.FindProperty("Quality_LensEffects");
        Quality_Dof = serializedObject.FindProperty("Quality_Dof");
        Quality_AO = serializedObject.FindProperty("Quality_AO");
        Quality_MotionBlur = serializedObject.FindProperty("Quality_MotionBlur");
        BlackAndWhiteMode = serializedObject.FindProperty("BlackAndWhiteMode");
        AA_Type = serializedObject.FindProperty("AA_Type");*/

       AllAi = serializedObject.FindProperty("AllAi");
       ListOfType1s = serializedObject.FindProperty("ListOfType1s");
       ListOfType2s = serializedObject.FindProperty("ListOfType2s");
        ListOfCameras = serializedObject.FindProperty("ListOfCameras");


        GameplayUI = serializedObject.FindProperty("GameplayUI");

        PlayerLighting = serializedObject.FindProperty("PlayerLighting");

       




        GhostParent = serializedObject.FindProperty("GhostParent");
        GhostMesh = serializedObject.FindProperty("GhostMesh");



         LTH_GameSettings = serializedObject.FindProperty("LTH_GameSettings");

		LTH_QualityData = serializedObject.FindProperty("LTH_QualityData");


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //DrawDefaultInspector();
        EditorGUILayout.PropertyField(LTH_GameSettings);
		EditorGUILayout.PropertyField(LTH_QualityData);
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Main Object Referances", EditorStyles.boldLabel);
       // EditorGUILayout.PropertyField(GameplayTimeline);
        EditorGUILayout.PropertyField(MainPlayerCamera);
        EditorGUILayout.PropertyField(ActivePlayer);
        EditorGUILayout.PropertyField(Detective);
      //  EditorGUILayout.PropertyField(Dog);
        EditorGUILayout.PropertyField(LastSighting);
        EditorGUILayout.LabelField("UI:");
        //EditorGUILayout.PropertyField(AlertBar);
        EditorGUILayout.PropertyField(GameplayUI);
        EditorGUILayout.PropertyField(GhostParent);
        EditorGUILayout.PropertyField(GhostMesh);
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Global Gameplay Settings", EditorStyles.boldLabel);



        //EditorGUILayout.PropertyField(TimeOfDay); 
       // EditorGUILayout.PropertyField(Difficulty);
       
        
        EditorGUILayout.LabelField("Player States:");
        EditorGUILayout.PropertyField(Stealth);

        EditorGUILayout.PropertyField(PlayerInSight);

        EditorGUILayout.PropertyField(PlayerCaught);
        EditorGUILayout.PropertyField(PlayerSafe);
        EditorGUILayout.PropertyField(PlayerIsRunning);

        EditorGUILayout.PropertyField(Flashlight);

       // EditorGUILayout.PropertyField(LastSightingVisible);
        //EditorGUILayout.BeginVertical("Box");
        //EditorGUILayout.LabelField("AI:", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(EnableAISightSwitch);
       // EditorGUILayout.PropertyField(EnableAIHearing);
		//EditorGUILayout.PropertyField(EnableAIAlertBars);
		//EditorGUILayout.PropertyField(EnableAIStatusIndicators);

        EditorGUILayout.PropertyField(PlayerLighting);

        
       // EditorGUILayout.LabelField("AI Alert Bar Modifiers", EditorStyles.boldLabel);

       // EditorGUILayout.PropertyField(ShadowBonus);


       // EditorGUILayout.PropertyField(EasyModifier);
       // EditorGUILayout.PropertyField(MediumModifier);
       // EditorGUILayout.PropertyField(HardModifer);


       // EditorGUILayout.PropertyField(DayModifier);
       // EditorGUILayout.PropertyField(NightModifer);
      //  EditorGUILayout.PropertyField(DistanceNearModifier);
        //EditorGUILayout.PropertyField(DistanceFarModifier);
       //EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Game Over Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(FailMsg);
        EditorGUILayout.PropertyField(GameOverTime);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Screen Fader Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(FadingEnabled);
        EditorGUILayout.PropertyField(ScreenFader);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Pause Menu Globals", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(Paused);
        /*EditorGUILayout.PropertyField(Quality_aa);
        EditorGUILayout.PropertyField(Quality_LensEffects);
        EditorGUILayout.PropertyField(Quality_Dof);
        EditorGUILayout.PropertyField(Quality_AO);
        EditorGUILayout.PropertyField(Quality_MotionBlur);
        EditorGUILayout.PropertyField(BlackAndWhiteMode);
        EditorGUILayout.PropertyField(AA_Type);*/
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Gameobject lists", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(AllAi);
        EditorGUILayout.PropertyField(ListOfType1s);
        EditorGUILayout.PropertyField(ListOfType2s);
        EditorGUILayout.PropertyField(ListOfCameras);
        EditorGUILayout.EndVertical();



        serializedObject.ApplyModifiedProperties();
    }
}
