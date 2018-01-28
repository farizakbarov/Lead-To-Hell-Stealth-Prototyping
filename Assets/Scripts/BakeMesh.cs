using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeMesh : MonoBehaviour {

    [SerializeField]
    SkinnedMeshRenderer m_skinnedMeshRenderer; // Skinned mesh renderer used for baking

    //string frameName = "MyMesh";

    private GameObject Mesh;
    private Transform Parent;
    public Material GhostMaterial;


    bool checkbool;

    bool BeginFade;

    public Color storedColor;

    // Use this for initialization
    void Start () {

        storedColor = GhostMaterial.color;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.Singleton.GhostParent != null)
        {

            if (Input.GetKeyDown(KeyCode.B))
            {
                BakeGhostMesh();
            }
        }


        if (GameManager.Singleton.PlayerInSight != checkbool)
        {
            checkbool = GameManager.Singleton.PlayerInSight;

           // print("my bool has changed to: " + GameManager.Singleton.PlayerInSight);
            if (GameManager.Singleton.PlayerInSight == false)
            {
                if (GameManager.Singleton.LTH_GameSettings.EnableGhostMesh)
                {
                    BakeGhostMesh();
                }
                //do stuff here
            }
        }


        if (BeginFade)
        {
             GhostMaterial.color =  Color.Lerp(GhostMaterial.color, Color.clear, GameManager.Singleton.LTH_GameSettings.GhostMeshFadeOutSpeed * Time.time); ;


            // If the texture is almost clear...
            if (GhostMaterial.color.a <= 0.05f)
            {
                // ... set the colour to clear and disable the GUITexture.
                // myColor = Color.clear;


                // The scene is no longer starting.
                BeginFade = false;
            }
            //  GhostMaterial.color = storedColor;
            // Debug.Log("Fading Started");

        }
    }


    public void BakeGhostMesh()
    {
        Parent = GameManager.Singleton.GhostParent.transform;
        Mesh = GameManager.Singleton.GhostMesh;

        Mesh frameMesh = new Mesh();
        // frameMesh.name = frameName;

        m_skinnedMeshRenderer.BakeMesh(frameMesh);

        // Setup game object to show frame
        // GameObject frameGO = new GameObject(frameName);
        // Mesh.name = frameName;
        Parent.transform.position = transform.position;
        Parent.transform.rotation = transform.rotation;
        Mesh.transform.localRotation = m_skinnedMeshRenderer.transform.localRotation;

        // Setup mesh filter
        MeshFilter meshFilter = Mesh.GetComponent<MeshFilter>();
        meshFilter.mesh = frameMesh;

        // Setup mesh renderer
//        MeshRenderer meshRenderer = Mesh.GetComponent<MeshRenderer>();
        //meshRenderer.sharedMaterials = m_skinnedMeshRenderer.sharedMaterials;
        // meshRenderer.material = GhostMat;
        // StartCoroutine("FadeOut");
    }


    IEnumerator FadeOut()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(GameManager.Singleton.LTH_GameSettings.GhostMeshTimeout);
        //GhostMaterial.color.a = 
        BeginFade = true;
    }
}
