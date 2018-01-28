using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LTH_SaveData))]
public class LTH_SaveData_Editor : Editor
{
    SerializedProperty Difficulty;
    SerializedProperty EnableAISightSwitch;
    SerializedProperty EnableAIHearing;
    SerializedProperty EnableAIAlertBars;
    SerializedProperty EnableAIStatusIndicators;
    SerializedProperty EnableAIPlaymakerLabels;
    SerializedProperty LastSightingVisible;

    SerializedProperty EnableGhostMesh;

    SerializedProperty ShadowBonus;


    SerializedProperty TimeOfDay;

    SerializedProperty DayModifier;
    SerializedProperty NightModifer;

    SerializedProperty EasyModifier;
    SerializedProperty MediumModifier;
    SerializedProperty HardModifer;

    SerializedProperty DistanceNearModifier;
    SerializedProperty DistanceFarModifier;

    SerializedProperty SlidingAbility;
    SerializedProperty RollingAbility;
    SerializedProperty SneakToggle;

    SerializedProperty LightSensorUI;
    SerializedProperty SpottedUI;


    SerializedProperty GhostMeshTimeout;
    SerializedProperty GhostMeshFadeOutSpeed;


    void OnEnable()
    {

        Difficulty = serializedObject.FindProperty("Difficulty");
        EnableAISightSwitch = serializedObject.FindProperty("EnableAISightSwitch");
        EnableAIHearing = serializedObject.FindProperty("EnableAIHearing");
        EnableAIAlertBars = serializedObject.FindProperty("EnableAIAlertBars");
        EnableAIStatusIndicators = serializedObject.FindProperty("EnableAIStatusIndicators");
        LastSightingVisible = serializedObject.FindProperty("LastSightingVisible");

        ShadowBonus = serializedObject.FindProperty("ShadowBonus");


        TimeOfDay = serializedObject.FindProperty("TimeOfDay");

        DayModifier = serializedObject.FindProperty("DayModifier");
        NightModifer = serializedObject.FindProperty("NightModifer");

        EasyModifier = serializedObject.FindProperty("EasyModifier");
        MediumModifier = serializedObject.FindProperty("MediumModifier");
        HardModifer = serializedObject.FindProperty("HardModifer");

        DistanceNearModifier = serializedObject.FindProperty("DistanceNearModifier");
        DistanceFarModifier = serializedObject.FindProperty("DistanceFarModifier");


         SlidingAbility = serializedObject.FindProperty("SlidingAbility");
         RollingAbility = serializedObject.FindProperty("RollingAbility");
         SneakToggle = serializedObject.FindProperty("SneakToggle");

        EnableAIPlaymakerLabels = serializedObject.FindProperty("EnableAIPlaymakerLabels");

         LightSensorUI = serializedObject.FindProperty("LightSensorUI");
         SpottedUI = serializedObject.FindProperty("SpottedUI");

         GhostMeshTimeout = serializedObject.FindProperty("GhostMeshTimeout");
         GhostMeshFadeOutSpeed = serializedObject.FindProperty("GhostMeshFadeOutSpeed");

         EnableGhostMesh = serializedObject.FindProperty("EnableGhostMesh");

}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //DrawDefaultInspector();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Player Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(SlidingAbility);
        EditorGUILayout.PropertyField(RollingAbility);
        EditorGUILayout.PropertyField(SneakToggle);
        EditorGUILayout.PropertyField(ShadowBonus);
        EditorGUILayout.PropertyField(EnableGhostMesh);
        EditorGUILayout.PropertyField(GhostMeshTimeout);
        EditorGUILayout.PropertyField(GhostMeshFadeOutSpeed);
        EditorGUILayout.PropertyField(LastSightingVisible);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Time of Day:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(TimeOfDay);
        EditorGUILayout.PropertyField(DayModifier);
        EditorGUILayout.PropertyField(NightModifer);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Difficulty:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(Difficulty);
        EditorGUILayout.PropertyField(EasyModifier);
        EditorGUILayout.PropertyField(MediumModifier);
        EditorGUILayout.PropertyField(HardModifer);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("AI:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(EnableAISightSwitch);
        EditorGUILayout.PropertyField(EnableAIHearing);
        EditorGUILayout.PropertyField(DistanceNearModifier);
        EditorGUILayout.PropertyField(DistanceFarModifier);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("UI:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(EnableAIAlertBars);
        EditorGUILayout.PropertyField(EnableAIStatusIndicators);
        EditorGUILayout.PropertyField(EnableAIPlaymakerLabels);
        EditorGUILayout.PropertyField(LightSensorUI);
        EditorGUILayout.PropertyField(SpottedUI);
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
