using UnityEngine;
using UnityEditor;
using System.Collections;

// Script created by Custom Inspector Generator
[CustomEditor(typeof(Stealth_GameManager))]
public class Stealth_GameManagerEditor : Editor 
{
	// The target script in serialized and non-serialized form
	Stealth_GameManager targetScript;
	SerializedObject serializedTargetScript;
	
	// The serialized properties of the target script
	SerializedProperty LTH_GameSettings;
	SerializedProperty AllAi;
	SerializedProperty ListOfType1s;
	SerializedProperty ListOfType2s;
	SerializedProperty Dog;
	SerializedProperty GameplayTimeline;
	SerializedProperty PlayerInSight;
	SerializedProperty LastSighting;
	SerializedProperty PlayerCaught;
	SerializedProperty PlayerSafe;
	SerializedProperty GameplayUI;
	SerializedProperty PlayerLighting;
	SerializedProperty ShadowModifier;
	SerializedProperty DifficultyModifier;
	SerializedProperty TimeOfDayModifier;
	SerializedProperty GhostParent;
	SerializedProperty GhostMesh;

    SerializedProperty HasPaperThrowable;
    SerializedProperty HasFireExtinguisher;

    // Initialization
    void OnEnable() {
		// Get a reference to the target script and serialize it
		targetScript = (Stealth_GameManager)target;
		serializedTargetScript = new SerializedObject(targetScript);
		
		// Find serialized properties
		LTH_GameSettings = serializedTargetScript.FindProperty("LTH_GameSettings");
		AllAi = serializedTargetScript.FindProperty("AllAi");
		ListOfType1s = serializedTargetScript.FindProperty("ListOfType1s");
		ListOfType2s = serializedTargetScript.FindProperty("ListOfType2s");
		Dog = serializedTargetScript.FindProperty("Dog");
		GameplayTimeline = serializedTargetScript.FindProperty("GameplayTimeline");
		PlayerInSight = serializedTargetScript.FindProperty("PlayerInSight");
		LastSighting = serializedTargetScript.FindProperty("LastSighting");
		PlayerCaught = serializedTargetScript.FindProperty("PlayerCaught");
		PlayerSafe = serializedTargetScript.FindProperty("PlayerSafe");
		GameplayUI = serializedTargetScript.FindProperty("GameplayUI");
		PlayerLighting = serializedTargetScript.FindProperty("PlayerLighting");
		ShadowModifier = serializedTargetScript.FindProperty("ShadowModifier");
		DifficultyModifier = serializedTargetScript.FindProperty("DifficultyModifier");
		TimeOfDayModifier = serializedTargetScript.FindProperty("TimeOfDayModifier");
		GhostParent = serializedTargetScript.FindProperty("GhostParent");
		GhostMesh = serializedTargetScript.FindProperty("GhostMesh");
         HasPaperThrowable = serializedTargetScript.FindProperty("HasPaperThrowable");
         HasFireExtinguisher = serializedTargetScript.FindProperty("HasFireExtinguisher");
    }
	
	// Drawing the Custom Inspector
    public override void OnInspectorGUI() {
    	// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedTargetScript.Update();
		
		// Draw serialized properties
		EditorGUILayout.PropertyField(LTH_GameSettings, new GUIContent("LTH_GameSettings"));





        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Main Object Referances", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(LastSighting, new GUIContent("LastSighting"));
        EditorGUILayout.PropertyField(Dog, new GUIContent("Dog"));
        EditorGUILayout.PropertyField(GameplayTimeline, new GUIContent("GameplayTimeline"));
        EditorGUILayout.PropertyField(GameplayUI, new GUIContent("GameplayUI"));
        EditorGUILayout.PropertyField(GhostParent, new GUIContent("GhostParent"));
        EditorGUILayout.PropertyField(GhostMesh, new GUIContent("GhostMesh"));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Player States:");
        EditorGUILayout.PropertyField(PlayerInSight, new GUIContent("PlayerInSight"));
        EditorGUILayout.PropertyField(PlayerCaught, new GUIContent("PlayerCaught"));
        EditorGUILayout.PropertyField(PlayerSafe, new GUIContent("PlayerSafe"));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.PropertyField(HasPaperThrowable, new GUIContent("Has Paper Throwable"));
        EditorGUILayout.PropertyField(HasFireExtinguisher, new GUIContent("Has Fire Extinguisher"));
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Gameplay Modifiers", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(PlayerLighting, new GUIContent("PlayerLighting"));
        EditorGUILayout.PropertyField(ShadowModifier, new GUIContent("ShadowModifier"));
        EditorGUILayout.PropertyField(DifficultyModifier, new GUIContent("DifficultyModifier"));
        EditorGUILayout.PropertyField(TimeOfDayModifier, new GUIContent("TimeOfDayModifier"));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Lists", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(AllAi, new GUIContent("AllAi"), true);
        EditorGUILayout.PropertyField(ListOfType1s, new GUIContent("ListOfType1s"), true);
        EditorGUILayout.PropertyField(ListOfType2s, new GUIContent("ListOfType2s"), true);
        EditorGUILayout.EndVertical();

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedTargetScript.ApplyModifiedProperties();
	}
}



