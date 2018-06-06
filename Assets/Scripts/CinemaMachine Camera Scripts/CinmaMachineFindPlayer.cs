using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CinmaMachineFindPlayer : MonoBehaviour {

    private CinemachineVirtualCamera cam;
    public bool follow;
    public bool lookat = true;

    // Use this for initialization
    void Start () {
        cam = GetComponent<CinemachineVirtualCamera>();
        if (lookat & cam.LookAt == null)
        {
           cam.LookAt = GameManager.Singleton.Player.transform.Find("PlayerCameraTarget");
        }

        if (follow & cam.Follow == null)
        {
            cam.Follow = GameManager.Singleton.Player.transform.Find("PlayerCameraTarget");
        }


    }
	

}
