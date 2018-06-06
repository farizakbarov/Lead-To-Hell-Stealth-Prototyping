using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using CoverShooter;

public class CinemaMachineInvertedControls : MonoBehaviour
{


    private CinemachineFreeLook cam;
    public string Axis1 = "Mouse X";
    public string Axis2 = "Mouse Y";
    public bool InvertedX = true;
    public bool InvertedY = true;

    private float value1;
    private float value2;
    //public GameObject Player;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        cam.m_HeadingBias = GameManager.Singleton.Player.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        value1 = Input.GetAxis(Axis1);
        value2 = Input.GetAxis(Axis2);
        if (InvertedX)
        {
            cam.m_XAxis.m_InputAxisValue = -value1;
        }
        else
        {
            cam.m_XAxis.m_InputAxisValue = value1;
        }

        if (InvertedY)
        {
            cam.m_YAxis.m_InputAxisValue = -value2;
        }
        else
        {
            cam.m_YAxis.m_InputAxisValue = value2;
        }
    }
}
