using System.Collections;
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

    public bool SpawnGhostMesh;
    public GameObject GhostMeshPrefab;
    private GameObject SpawnedGhostMesh;

    public bool SpawnUI;
    public GameObject UIPrefab;
    private GameObject SpawnedUI;

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

        if (SpawnGhostMesh)
        {
            SpawnedGhostMesh = Instantiate(GhostMeshPrefab, transform.position + new Vector3(0, -100, 0), transform.rotation) as GameObject;
            GameManager.Singleton.GhostParent = SpawnedGhostMesh;
            GameManager.Singleton.GhostMesh = SpawnedGhostMesh.transform.GetChild(0).gameObject;
        }

        if (SpawnUI)
        {
            SpawnedUI = Instantiate(UIPrefab, transform.position, transform.rotation) as GameObject;
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
