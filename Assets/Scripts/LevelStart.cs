using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;

public class LevelStart : MonoBehaviour {

    public GameObject Player;
    private GameObject SpawnedPlayer;

	// Use this for initialization
	void Start () {
        SpawnedPlayer = Instantiate(Player, transform.position, transform.rotation) as GameObject;
        GameManager.Singleton.ActivePlayer = SpawnedPlayer;
        GameManager.Singleton.Detective = SpawnedPlayer;
        GameManager.Singleton.MainPlayerCamera.GetComponent<ThirdPersonCamera>().Target = SpawnedPlayer.GetComponent<CharacterMotor>();
        GameManager.Singleton.MainPlayerCamera.GetComponent<ThirdPersonCamera>().Controller = SpawnedPlayer.GetComponent<ThirdPersonController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
