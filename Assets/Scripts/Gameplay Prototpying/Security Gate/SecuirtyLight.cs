using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuirtyLight : MonoBehaviour {

    public SecurityGate GateScript;
    public Material myMaterial;

	// Use this for initialization
	void Start () {
        myMaterial = GetComponent<Renderer>().material;

    }
	
	// Update is called once per frame
	void Update () {
        if (GateScript.GateOn)
        {
            myMaterial.color = Color.green;
        }
        else{
            myMaterial.color = Color.red;
        }
	}
}
