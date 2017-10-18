using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using CoverShooter;

public class CinemaMachineCoverSystemStates : MonoBehaviour {



    /*public Vector3 IdleTopOffset;  
    public Vector3 IdleMiddleOffset;   
    public Vector3 IdleBottomOffset;
    

    public Vector3 CrouchTopOffset;
    public Vector3 CrouchMiddleOffset;
    public Vector3 CrouchBottomOffset;*/

    public CharacterMotor Target;

    public Vector3 IdleTopOffset;
    public Vector3 IdleMiddleOffset;
    public Vector3 IdleBottomOffset;

    public Vector3 CrouchTopOffset;
    public Vector3 CrouchMiddleOffset;
    public Vector3 CrouchBottomOffset;

    public float IdleTopRadius;
    public float IdleMiddleRadius;
    public float IdleBottomRadius;

    public float IdleTopHeight;    
    public float IdleMiddleHeight;
    public float IdleBottomHeight;

    public float CrouchTopRadius;
    public float CrouchMiddleRadius;
    public float CrouchBottomRadius;

    public float CrouchTopHeight;
    public float CrouchMiddleHeight;
    public float CrouchBottomHeight;

    private CinemachineFreeLook vcam;

    CinemachineOrbitalTransposer TopRig;
    CinemachineOrbitalTransposer MiddleRig;
    CinemachineOrbitalTransposer BottomRig;

    CinemachineComposer TopAim;
    CinemachineComposer MiddleAim;
    CinemachineComposer BottomAim;


    // Use this for initialization
    void Start () {
        vcam = GetComponent<CinemachineFreeLook>();


        TopAim = vcam.GetRig(0).GetCinemachineComponent<CinemachineComposer>();
        MiddleAim = vcam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        BottomAim = vcam.GetRig(2).GetCinemachineComponent<CinemachineComposer>();

        TopRig = vcam.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>();
        MiddleRig = vcam.GetRig(1).GetCinemachineComponent<CinemachineOrbitalTransposer>();
        BottomRig = vcam.GetRig(2).GetCinemachineComponent<CinemachineOrbitalTransposer>();


    }
	
	// Update is called once per frame
	void Update () {
        if (Target.IsCrouching)
        {
            TopAim.m_TrackedObjectOffset = IdleTopOffset;
            TopRig.m_Radius = CrouchTopRadius;
            TopRig.m_HeightOffset = CrouchTopHeight;

            MiddleAim.m_TrackedObjectOffset = IdleMiddleOffset;

            MiddleRig.m_Radius = CrouchMiddleRadius;
            MiddleRig.m_HeightOffset = CrouchMiddleHeight;

            BottomAim.m_TrackedObjectOffset = IdleBottomOffset;
            BottomRig.m_Radius = CrouchBottomRadius;
            BottomRig.m_HeightOffset = CrouchBottomHeight;

        }
        else
        {
            TopRig.m_Radius = IdleTopRadius;
            TopRig.m_HeightOffset = IdleTopHeight;

            MiddleRig.m_Radius = IdleMiddleRadius;
            MiddleRig.m_HeightOffset = IdleMiddleHeight;

            BottomRig.m_Radius = IdleBottomRadius;
            BottomRig.m_HeightOffset = IdleBottomHeight;
        }
	}
}
