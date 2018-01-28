using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LTH_GameSettings", menuName = "LTH Game Settings/LTH Game Settings Asset", order = 1)]
public class LTH_SaveData : ScriptableObject
{
    public string objectName = "LTH_SaveData";
    public enum Difficulties { Easy, Medium, Hard };
    public Difficulties Difficulty = Difficulties.Medium;
    public bool EnableAISightSwitch = true;
    public bool EnableAIHearing = true;
    public bool EnableAIAlertBars = true;
    public bool EnableAIStatusIndicators = true;
    public bool EnableAIPlaymakerLabels = true;
    public bool LastSightingVisible = true;
    public bool EnableGhostMesh = true;
    public float GhostMeshTimeout = 3.0f;
    public float GhostMeshFadeOutSpeed = 2.0f;

    public float ShadowBonus = 0.25f;

    public bool SlidingAbility = true;
    public bool RollingAbility = true;
    public bool SneakToggle;


    public enum TimeOfDays { Day, Night };
    public TimeOfDays TimeOfDay = TimeOfDays.Day;

    public float DayModifier = 1.0f;
    public float NightModifer = 0.75f;

    public float EasyModifier = 0.5f;
    public float MediumModifier = 1.0f;
    public float HardModifer = 2.0f;

    public float DistanceNearModifier = 2.0f;
    public float DistanceFarModifier = 0.25f;


    public bool LightSensorUI = true;
    public bool SpottedUI = true;





}
