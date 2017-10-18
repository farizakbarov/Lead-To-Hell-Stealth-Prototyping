using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

public class SafeZone_Detect : MonoBehaviour
{

    private RaycastHit[] _raycastHits = new RaycastHit[16];

    private CharacterMotor motor;
    CapsuleCollider capsule;

    public bool FacingSafeZone;

    private int numberOfRaycastHits;

    private BoxCollider col;

    //private Transform Exit;
    public float ExitDistance = 5f;

    //public float speed = 0.05f;

    private bool MoveToPosition;
    private bool MoveToExit;
    private Vector3 pos;

    public bool UseExit;
    private Transform Exit;

    private bool ExitEnabled;

    public float timeTakenDuringLerp = 1f;
    //The Time.time value when we started the interpolation
    private float _timeStartedLerping;

    private Vector3 ExitDirection;

    GameObject Hit;

    // Use this for initialization
    void Start()
    {
        motor = GetComponent<CharacterMotor>();
        capsule = GetComponent<CapsuleCollider>();
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ExitEnabled = true;
    }

    public void ExitSafeZone()
    {

        MoveToExit = true;
        ExitEnabled = false;
        _timeStartedLerping = Time.time;
        ExitDirection = transform.position + transform.forward * ExitDistance;
    }


    // Update is called once per frame
    void Update()
    {
        var numberOfRaycastHits = Physics.RaycastNonAlloc(transform.position + new Vector3(0, capsule.height * 0.1f, 0),
                                   transform.forward,
                                   _raycastHits,
                                   capsule.radius + (motor._cover.In ? float.Epsilon : motor.ObstacleDistance));

        if (numberOfRaycastHits == 0)
        {
            FacingSafeZone = false;
        }

        for (int i = 0; i < numberOfRaycastHits; i++)
        {
            Hit = _raycastHits[i].collider.gameObject;

            if (Hit.layer == 13)
            {
                FacingSafeZone = true;
            }

            if (Input.GetKeyDown(KeyCode.F) && Hit.layer == 13 && !GameManager.Singleton.PlayerSafe)
            {
                GameManager.Singleton.PlayerSafe = true;
                GameManager.Singleton.PlayerInSight = false;
                this.gameObject.layer = 12;
                Physics.IgnoreCollision(this.GetComponent<Collider>(), Hit.gameObject.GetComponent<Collider>());
                Physics.IgnoreCollision(this.GetComponent<Collider>(), Hit.transform.parent.GetComponent<Collider>());


                pos = Hit.transform.GetComponent<SafeZoneTrigger>().SafeZoneLocation.position;
                UseExit = Hit.transform.GetComponent<SafeZoneTrigger>().UseExit;
                Exit = Hit.transform.parent.Find("Exit");
                MoveToPosition = true;
                Debug.Log(Hit.name);
                StartCoroutine(WaitAndPrint(1.0F));
                _timeStartedLerping = Time.time;
            }


        }

        if (MoveToPosition)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            transform.position = Vector3.Lerp(transform.position, pos, percentageComplete);


            if (percentageComplete >= 1.0f)
            {
                MoveToPosition = false;
                percentageComplete = 0;
            }
        }

        if (MoveToExit)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            if (UseExit)
            {
                transform.position = Vector3.Lerp(transform.position, Exit.position, percentageComplete);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, ExitDirection, percentageComplete);
            }

            if (percentageComplete >= 1.0f)
            {
                MoveToExit = false;
                GameManager.Singleton.PlayerSafe = false;
                Physics.IgnoreCollision(this.GetComponent<Collider>(), Hit.gameObject.GetComponent<Collider>(), false);
                Physics.IgnoreCollision(this.GetComponent<Collider>(), Hit.transform.parent.GetComponent<Collider>(), false);
                this.gameObject.layer = 10;
                percentageComplete = 0;
            }
        }

        if (ExitEnabled)
        {
            if (Input.GetKeyDown(KeyCode.F) && GameManager.Singleton.PlayerSafe)
            {
                ExitSafeZone();
            }
        }


    }


}
