using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

/* This script addes the ability for the player to slide when a button is pressed.*/

public class CharacterSlide : MonoBehaviour {

    public bool SlidingEnabled;

    // private CharacterMotor motor;
    private Animator anim;
    private LTH_ThirdPersonController controller;

    private CapsuleCollider capsule;
    private float _defaultCapsuleHeight;
    private Vector3 _defaultCapsuleCenter;

    public float SlideCapsuleHeight;
    public Vector3 SlideCapsuleCenter;

    public bool IsSliding;

    private bool large;

    

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        controller = GetComponent<LTH_ThirdPersonController>();

       capsule = GetComponent<CapsuleCollider>();
        _defaultCapsuleHeight = capsule.height;
        _defaultCapsuleCenter = capsule.center;
    }

    public void CapsuleLarge()
    {
        IsSliding = false;
      //  large = true;
        //capsule.height = _defaultCapsuleHeight;
        // capsule.center = _defaultCapsuleCenter;
    }

    public void CapsuleSmall()
    {
        large = false;
        capsule.height = SlideCapsuleHeight;
        capsule.center = SlideCapsuleCenter;
    }

    // Update is called once per frame
    void Update() {

        if (large)
        {
            capsule.height = Mathf.Lerp(capsule.height, _defaultCapsuleHeight, Time.deltaTime * 1);
            capsule.center = new Vector3(capsule.center.x, _defaultCapsuleCenter.y - (_defaultCapsuleHeight - capsule.height) * 0.5f, capsule.center.z);
        }
        //Debug.Log(anim.GetFloat("CapsuleHeight"));

        if (SlidingEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Z) && controller.isRunning)
            {
                //Debug.Log("Slide");
                IsSliding = true;
                anim.SetTrigger("Slide");
            }

        }

       /* if (IsAlive)
        {
            capsule.enabled = true;

            var targetHeight = _defaultCapsuleHeight;

            if (_isClimbing && _normalizedClimbTime < 0.5f)
                targetHeight = ClimbSettings.CapsuleHeight;
            else if (_isJumping && _jumpTimer < JumpSettings.HeightDuration)
                targetHeight = JumpSettings.CapsuleHeight;
            else if (_cover.In && !_cover.IsTall && (!IsAiming || IsCornerAiming))
                targetHeight = Mathf.Lerp(CoverSettings.LowCapsuleHeight, targetHeight, _cover.IsTall ? 1.0f : 0.0f);

            capsule.height = Mathf.Lerp(capsule.height, targetHeight, Time.deltaTime * 10);
            capsule.center = new Vector3(capsule.center.x, _defaultCapsuleCenter - (_defaultCapsuleHeight - capsule.height) * 0.5f, capsule.center.z);
        }*/
    }
}
