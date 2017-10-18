using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoverShooter;
using PlayMaker;

public class FireExtinguisher : MonoBehaviour {

    public GameObject SmokeExplosion;
    private GameObject Spray;
    public bool SmokeOn;
    private Grenade grenadeScript;
    public GameObject Radius;

    public float Timer = 5f;

    private bool Flip = true;

    private GameObject Explosion;

	// Use this for initialization
	void Start () {
        grenadeScript = GetComponent<Grenade>();

        Spray = this.transform.Find("Extinguisher_Spray").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (grenadeScript._isFlying)
        {

            if (Spray != null)
            {
                Spray.SetActive(true);
            }
            if (grenadeScript.myVelocity < grenadeScript.StoppedThreshold)
            {
                if (Spray != null)
                {
                    Spray.GetComponent<ParticleSystem>().Stop();
                }
                if (Flip)
                {
                    
                    
                    Explosion = Instantiate(SmokeExplosion, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                    SmokeOn = true;
                    Radius.SetActive(true);
                    StartCoroutine(coroutineA());
                    //transform.eulerAngles = new Vector3( this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0);
                    Flip = false;
                }
            }

        }
	}

    IEnumerator coroutineA()
    {
        yield return new WaitForSeconds(Timer);
        SmokeOn = false;
        Radius.SetActive(false);
        // Destroy(Explosion);
        Explosion.GetComponent<ParticleSystem>().Stop();

    }


}
