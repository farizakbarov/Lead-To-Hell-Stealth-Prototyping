using UnityEngine;
using UnityEditor;
using System.Collections;

// Script created by Custom Inspector Generator
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor 
{
	// The target script in serialized and non-serialized form
	GameManager targetScript;
	SerializedObject serializedTargetScript;
	
	// The serialized properties of the target script
	SerializedProperty LTH_QualityData;
	SerializedProperty MainPlayerCamera;
	SerializedProperty CurrentCamera;
	SerializedProperty ListOfCameras;
	SerializedProperty Player;
	SerializedProperty PlayerIsRunning;
	SerializedProperty PlayerIsWalking;
	SerializedProperty PlayerIsNotMoving;
	SerializedProperty PlayerSpeed;
	SerializedProperty Stealth;
	SerializedProperty PlayerController;
	SerializedProperty PlayerStart;
	SerializedProperty Flashlight;
	SerializedProperty FadingEnabled;
	SerializedProperty ScreenFader;
	SerializedProperty FailMsg;
	SerializedProperty GameOverTime;
	SerializedProperty Paused;

	// Initialization
	void OnEnable() {
		// Get a reference to the target script and serialize it
		targetScript = (GameManager)target;
		serializedTargetScript = new SerializedObject(targetScript);
		
		// Find serialized properties
		LTH_QualityData = serializedTargetScript.FindProperty("LTH_QualityData");
		MainPlayerCamera = serializedTargetScript.FindProperty("MainPlayerCamera");
		CurrentCamera = serializedTargetScript.FindProperty("CurrentCamera");
		ListOfCameras = serializedTargetScript.FindProperty("ListOfCameras");
		Player = serializedTargetScript.FindProperty("Player");
		PlayerIsRunning = serializedTargetScript.FindProperty("PlayerIsRunning");
		PlayerIsWalking = serializedTargetScript.FindProperty("PlayerIsWalking");
		PlayerIsNotMoving = serializedTargetScript.FindProperty("PlayerIsNotMoving");
		PlayerSpeed = serializedTargetScript.FindProperty("PlayerSpeed");
		Stealth = serializedTargetScript.FindProperty("Stealth");
		PlayerController = serializedTargetScript.FindProperty("PlayerController");
		PlayerStart = serializedTargetScript.FindProperty("PlayerStart");
		Flashlight = serializedTargetScript.FindProperty("Flashlight");
		FadingEnabled = serializedTargetScript.FindProperty("FadingEnabled");
		ScreenFader = serializedTargetScript.FindProperty("ScreenFader");
		FailMsg = serializedTargetScript.FindProperty("FailMsg");
		GameOverTime = serializedTargetScript.FindProperty("GameOverTime");
		Paused = serializedTargetScript.FindProperty("Paused");
	}
	
	// Drawing the Custom Inspector
    public override void OnInspectorGUI() {
    	// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedTargetScript.Update();
		
		// Draw serialized properties
		EditorGUILayout.PropertyField(LTH_QualityData, new GUIContent("LTH_QualityData"));
        EditorGUILayout.PropertyField(Paused, new GUIContent("Paused"));
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Main Object Referances", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(MainPlayerCamera, new GUIContent("MainPlayerCamera"));
		EditorGUILayout.PropertyField(CurrentCamera, new GUIContent("CurrentCamera"));
		EditorGUILayout.PropertyField(Player, new GUIContent("Player"));
        EditorGUILayout.PropertyField(PlayerStart, new GUIContent("PlayerStart"));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Player States:");
        EditorGUILayout.PropertyField(PlayerIsRunning, new GUIContent("PlayerIsRunning"));
		EditorGUILayout.PropertyField(PlayerIsWalking, new GUIContent("PlayerIsWalking"));
		EditorGUILayout.PropertyField(PlayerIsNotMoving, new GUIContent("PlayerIsNotMoving"));
		EditorGUILayout.PropertyField(PlayerSpeed, new GUIContent("PlayerSpeed"));
		EditorGUILayout.PropertyField(Stealth, new GUIContent("Stealth"));
        EditorGUILayout.PropertyField(Flashlight, new GUIContent("Flashlight"));
        EditorGUILayout.EndVertical();
        //EditorGUILayout.PropertyField(PlayerController, new GUIContent("PlayerController"));

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Screen Fader Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(FadingEnabled, new GUIContent("FadingEnabled"));
		EditorGUILayout.PropertyField(ScreenFader, new GUIContent("ScreenFader"));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("UI", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(FailMsg, new GUIContent("FailMsg"));
		EditorGUILayout.PropertyField(GameOverTime, new GUIContent("GameOverTime"));
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Gameobject lists", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(ListOfCameras, new GUIContent("ListOfCameras"));
        EditorGUILayout.EndVertical();

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedTargetScript.ApplyModifiedProperties();
	}
}



