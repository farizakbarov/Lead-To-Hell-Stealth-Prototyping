﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

public class LevelStart : MonoBehaviour {

    public GameObject Player;
    private GameObject SpawnedPlayer;

    public GameObject ExplosionPreview;
    public GameObject PathPreview;

	public bool SpawnCamera;
	public GameObject Camera;
	private GameObject SpawnedCamera;

	// Use this for initialization
	void Start () {
        SpawnedPlayer = Instantiate(Player, transform.position, transform.rotation) as GameObject;
        GameManager.Singleton.ActivePlayer = SpawnedPlayer;
        GameManager.Singleton.Detective = SpawnedPlayer;
        SpawnedPlayer.GetComponent<ThirdPersonController>().ExplosionPreview = ExplosionPreview;
        SpawnedPlayer.GetComponent<ThirdPersonController>().PathPreview = PathPreview;

		if(SpawnCamera){
			SpawnedCamera = Instantiate(Camera, transform.position, transform.rotation) as GameObject;
			GameManager.Singleton.MainPlayerCamera = SpawnedCamera;
		}

        GameManager.Singleton.MainPlayerCamera.GetComponent<ThirdPersonCamera>().Target = SpawnedPlayer.GetComponent<CharacterMotor>();
        GameManager.Singleton.MainPlayerCamera.GetComponent<ThirdPersonCamera>().Controller = SpawnedPlayer.GetComponent<ThirdPersonController>();
   

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
