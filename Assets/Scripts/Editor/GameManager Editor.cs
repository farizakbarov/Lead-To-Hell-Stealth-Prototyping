﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
[CanEditMultipleObjects]

public class GameManagerEditor : Editor {
    // Use this for initialization


    SerializedProperty GameplayTimeline;
    SerializedProperty MainPlayerCamera;
    SerializedProperty ActivePlayer;
    SerializedProperty Detective;
    SerializedProperty Dog;

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
    SerializedProperty Quality_aa;
    SerializedProperty Quality_LensEffects;
    SerializedProperty Quality_Dof;
    SerializedProperty Quality_AO;
    SerializedProperty Quality_MotionBlur;
    SerializedProperty BlackAndWhiteMode;
    SerializedProperty AA_Type;

    SerializedProperty GameplayUI;


    SerializedProperty AllAi;
    SerializedProperty ListOfType1s;
    SerializedProperty ListOfType2s;
    SerializedProperty ListOfCameras;

    SerializedProperty PlayerLighting;


    SerializedProperty Difficulty;

    SerializedProperty TimeOfDay;



    void OnEnable()
    {
        GameplayTimeline = serializedObject.FindProperty("GameplayTimeline");
        MainPlayerCamera = serializedObject.FindProperty("MainPlayerCamera");
        ActivePlayer = serializedObject.FindProperty("ActivePlayer");
        Detective = serializedObject.FindProperty("Detective");
        Dog = serializedObject.FindProperty("Dog");

        Stealth = serializedObject.FindProperty("Stealth");

        EnableAISightSwitch = serializedObject.FindProperty("EnableAISightSwitch");
        EnableAIHearing = serializedObject.FindProperty("EnableAIHearing");

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
        Quality_aa = serializedObject.FindProperty("Quality_aa");
        Quality_LensEffects = serializedObject.FindProperty("Quality_LensEffects");
        Quality_Dof = serializedObject.FindProperty("Quality_Dof");
        Quality_AO = serializedObject.FindProperty("Quality_AO");
        Quality_MotionBlur = serializedObject.FindProperty("Quality_MotionBlur");
        BlackAndWhiteMode = serializedObject.FindProperty("BlackAndWhiteMode");
        AA_Type = serializedObject.FindProperty("AA_Type");

       AllAi = serializedObject.FindProperty("AllAi");
       ListOfType1s = serializedObject.FindProperty("ListOfType1s");
       ListOfType2s = serializedObject.FindProperty("ListOfType2s");
        ListOfCameras = serializedObject.FindProperty("ListOfCameras");


        GameplayUI = serializedObject.FindProperty("GameplayUI");

        PlayerLighting = serializedObject.FindProperty("PlayerLighting");

        Difficulty = serializedObject.FindProperty("Difficulty");

        TimeOfDay = serializedObject.FindProperty("TimeOfDay");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //DrawDefaultInspector();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Main Object Referances", EditorStyles.boldLabel);
       // EditorGUILayout.PropertyField(GameplayTimeline);
        EditorGUILayout.PropertyField(MainPlayerCamera);
        EditorGUILayout.PropertyField(ActivePlayer);
        EditorGUILayout.PropertyField(Detective);
        EditorGUILayout.PropertyField(Dog);
        EditorGUILayout.PropertyField(LastSighting);
        EditorGUILayout.LabelField("UI:");
        //EditorGUILayout.PropertyField(AlertBar);
        EditorGUILayout.PropertyField(GameplayUI);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.PropertyField(TimeOfDay); 
        EditorGUILayout.PropertyField(Difficulty);
        EditorGUILayout.LabelField("Global Gameplay Settings", EditorStyles.boldLabel);
        
        EditorGUILayout.LabelField("Player States:");
        EditorGUILayout.PropertyField(Stealth);

        EditorGUILayout.PropertyField(PlayerInSight);

        EditorGUILayout.PropertyField(PlayerCaught);
        EditorGUILayout.PropertyField(PlayerSafe);
        EditorGUILayout.PropertyField(PlayerIsRunning);

        EditorGUILayout.PropertyField(Flashlight);



        EditorGUILayout.LabelField("AI:");
        EditorGUILayout.PropertyField(EnableAISightSwitch);
        EditorGUILayout.PropertyField(EnableAIHearing);

        EditorGUILayout.PropertyField(PlayerLighting);
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
        EditorGUILayout.PropertyField(Quality_aa);
        EditorGUILayout.PropertyField(Quality_LensEffects);
        EditorGUILayout.PropertyField(Quality_Dof);
        EditorGUILayout.PropertyField(Quality_AO);
        EditorGUILayout.PropertyField(Quality_MotionBlur);
        EditorGUILayout.PropertyField(BlackAndWhiteMode);
        EditorGUILayout.PropertyField(AA_Type);
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