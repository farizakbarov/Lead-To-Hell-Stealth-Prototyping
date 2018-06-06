using UnityEngine;
using System.Collections;

public class CCTVController : MonoBehaviour
{

    [Tooltip("CCTV Camera pivot gameobject")]
    public GameObject cameraPivot;
    [Header("Camera Settings")]
    [Tooltip("If true - camera turned on")]
    public bool isEnabled;
    [Tooltip("If true - camera will be turning back and forth between Positive and Negative angles")]
    public bool isDynamic;
    [Tooltip("Maximum negative turn angle from 0 to -90")]
    [Range(0f, -90f)]
    public float negativeAngle;
    [Tooltip("Maximum positive turn angle from 0 to 90")]
    [Range(0f, 90f)]
    public float positiveAngle;
    [Tooltip("Camera turn speed")]
    [Range(0f, 1f)]
    public float turnSpeed;

    public bool TrackPlayer;

    public GameObject Cam;

    private float turnT;
    private int pongSwitch;

  //  private Vector3 StartCamPos;
   // private Quaternion StartCamRot;

    public float speed = 0.1f;
    public bool TrackVertical = true;


    void Start()
    {
        pongSwitch = 1;
       // StartCamPos = Cam.transform.localPosition;
       // StartCamRot = Cam.transform.localRotation;

    }

    void Update()
    {
        if (!TrackPlayer)
        {
            RotationCycle();
            //cameraPivot.transform.localEulerAngles = new Vector3(0, 0, cameraPivot.transform.localEulerAngles.z);
        }
        else
        {

           Quaternion targetRotation = Quaternion.LookRotation((GameManager.Singleton.Player.transform.position + new Vector3(0, 1.5f, 0)) - cameraPivot.transform.position);
            // targetRotation = targetRotation * Quaternion.Euler(-90, 0, 0);

            /*Transform target = GameManager.Singleton.Player.transform;
            target.transform.position = target.transform.position + new Vector3(0,1,0);*/


            cameraPivot.transform.rotation = targetRotation;
            cameraPivot.transform.rotation = cameraPivot.transform.rotation * Quaternion.Euler(-90, 0, 0);

           // cameraPivot.transform.localRotation = Quaternion.Slerp(cameraPivot.transform.localRotation * Quaternion.Euler(-90, 0, 0), targetRotation, speed * Time.time);


                if (TrackVertical)
                {
                    cameraPivot.transform.localEulerAngles = new Vector3(0, cameraPivot.transform.localEulerAngles.y, cameraPivot.transform.localEulerAngles.z);
                }
                else
                {
                    cameraPivot.transform.localEulerAngles = new Vector3(0, 0, cameraPivot.transform.localEulerAngles.z);
                }

            //atargetRotation = Quaternion.Euler(new Vector3(0, 0, targetRotation.eulerAngles.z));

            //cameraPivot.transform.LookAt(GameManager.Singleton.Player.transform);

            // cameraPivot.transform.rotation = targetRotation;

            // cameraPivot.transform.rotation = 

            // Smoothly rotate towards the target point.
            // Cam.transform.rotation = Quaternion.Slerp(Cam.transform.rotation, targetRotation, speed * Time.time);
            // Cam.transform.rotation *= Quaternion.Euler(-90, 0, 0);
            /*Cam.transform.LookAt(GameManager.Singleton.Player.transform);
            Cam.transform.rotation *= Quaternion.Euler(-90, 0, 0);*/
        }
    }

    private void RotationCycle()
    {

        if (isEnabled && isDynamic)
        {
            PingPong();
           cameraPivot.transform.localEulerAngles = Vector3.Slerp(cameraPivot.transform.localPosition, new Vector3(0f, 0f, Mathf.Lerp(negativeAngle, positiveAngle, turnT)), Time.time * 1); 
            
            // cameraPivot.transform.rotation = 

            // Cam.transform.localPosition = Vector3.Slerp(Cam.transform.localPosition, StartCamPos, Time.time * speed);
            //Cam.transform.localRotation = Quaternion.Slerp(Cam.transform.localRotation, StartCamRot, Time.time * speed);
        }


    }

    private void PingPong()
    {
        if (!TrackPlayer)
        {
            if (turnT >= 1f)
            {
                pongSwitch = 2;
            }
            else if (turnT <= 0f)
            {
                pongSwitch = 1;
            }

            switch (pongSwitch)
            {
                case 1:
                    turnT += 1 * Time.deltaTime * turnSpeed;
                    break;
                case 2:
                    turnT -= 1 * Time.deltaTime * turnSpeed;
                    break;
            }
        }
    }

}
