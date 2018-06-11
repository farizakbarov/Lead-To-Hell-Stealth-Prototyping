// C# example:
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

//[MenuItem ("Custom/WallHit")]

public class CustomScripts : EditorWindow
{
    bool AllWallHit;

    bool BatchApplyMaterial;
    public Material ReplacementMaterial;

    public Material ReplacementMaterial2;

    bool BatchApplyPrefabChanges;
    public bool RevertPrefab;

    bool RenamePrefab;
    public GameObject[] ArrayOfObjects;
    private int i = 0;

    bool ReplaceGameObjects;
    public GameObject ReplacementPrefab;

    bool SeperateCol;

    private Transform newObject;
    public bool CreatePlaceHolderMesh = true;
    public bool RemoveAnimatorComponent = true;

    private bool MakePrefab;
    public GameObject[] SourceGameObject;
    public Material AssignMaterial;
    public bool LightmapStatic = true;
    public float LightmapScale = 0.5f;
    private GameObject newModel;
    private int i2 = 0;
    private string selectedTag = "Untagged";
    private int selectedLayer = 0;
    private bool SetNotWalkable;
    Vector2 scrolPos;

    bool BatchLightmap;
    private float ScaleInLightmap = 1.0f;

    public bool ResetToZero;


    public static bool WorldSpace;
    private bool CopyPasteTransform;
    public bool list;
    // public bool Rot = true;
    // public bool Scale = true;
    public static Vector3 StoredPosition;
    public static Vector3 StoredRotation;
    public static Vector3 StoredScale;

    public static bool EnablePos = true;
    public static bool EnableRot = true;
    public static bool EnableScale = true;

    static List<string> ListOfNames = new List<string>();
    static List<Vector3> ListOfTransforms = new List<Vector3>();
    static List<Vector3> ListOfRotation = new List<Vector3>();
    static List<Vector3> ListsOfScale = new List<Vector3>();

    bool ReplaceMissingMatieials;

    public SkinnedMeshRenderer MeshToCopy;

    bool CopyPasteSkinMaterials;


    [MenuItem("Window/Custom Scripts")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CustomScripts window = (CustomScripts)EditorWindow.GetWindow(typeof(CustomScripts));
        window.Show();
    }

    void OnGUI()
    {
        // batch apply materials		

        EditorGUILayout.Space();
        scrolPos = EditorGUILayout.BeginScrollView(scrolPos, GUILayout.Height(position.height));
        EditorGUILayout.BeginVertical("box");
        BatchApplyMaterial = EditorGUILayout.Foldout(BatchApplyMaterial, "Batch Apply Materials");

        if (BatchApplyMaterial)
        {
            EditorGUILayout.HelpBox("Script batch applying a material to selected objects", MessageType.Info);


            EditorGUILayout.Space();

            ReplacementMaterial = EditorGUILayout.ObjectField("Replacement Material", ReplacementMaterial, typeof(Material), true) as Material;

            if (GUILayout.Button("Apply Material to Selected Scene Objects"))
            {
                BatchApplyMaterials();
            }

            if (GUILayout.Button("Apply Material to Selected Assets"))
            {
                BatchApplyMaterialAsset();
            }

        }
        EditorGUILayout.EndVertical();
        //Bach Apply Prefab

        EditorGUILayout.Space();


        EditorGUILayout.BeginVertical("box");
        BatchLightmap = EditorGUILayout.Foldout(BatchLightmap, "Batch Lightmap");

        if (BatchLightmap)
        {
            ScaleInLightmap = EditorGUILayout.FloatField("Lightmap Scale", ScaleInLightmap);
            if (GUILayout.Button("Thunderbirds Are Go!"))
            {
                BatchLightmapScale();
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();


        EditorGUILayout.BeginVertical("box");
        BatchApplyPrefabChanges = EditorGUILayout.Foldout(BatchApplyPrefabChanges, "Batch Apply Prefab Changes");

        if (BatchApplyPrefabChanges)
        {
            EditorGUILayout.HelpBox("Script for batch Applying changes to Selected Prefabs", MessageType.Info);


            EditorGUILayout.Space();

            BatchApplyPrefabChanges = EditorGUILayout.Toggle("Revert Prefabs", BatchApplyPrefabChanges);
            if (GUILayout.Button("Do it for me."))
            {
                BatchApplyPrefab();
            }


        }
        EditorGUILayout.EndVertical();
        //Bach Apply Prefab

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        ReplaceGameObjects = EditorGUILayout.Foldout(ReplaceGameObjects, "Replace GameObjects");

        if (ReplaceGameObjects)
        {
            EditorGUILayout.HelpBox("Script for replacing selected objects in the scene with a Prefab.", MessageType.Info);
            EditorGUILayout.Space();
            ReplacementPrefab = EditorGUILayout.ObjectField("Replacement GameObject", ReplacementPrefab, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("Thunderbirds Are Go!"))
            {
                ReplaceG();
            }
        }
        EditorGUILayout.EndVertical();
        ///Add Wall Hit

        EditorGUILayout.Space();


        /* EditorGUILayout.BeginVertical("box");
         RenamePrefab = EditorGUILayout.Foldout(RenamePrefab, "Rename Model Prefabs");

         if (RenamePrefab)
         {
             EditorGUILayout.HelpBox("Batch renaming prefabs to be their mesh's name + '_pref'", MessageType.Info);
             EditorGUILayout.Space();
             if (GUILayout.Button("Transform And Roll Out!"))
             {
                 RenameModelPrefab();
             }
         }
         EditorGUILayout.EndVertical();

         EditorGUILayout.Space();*/

        EditorGUILayout.BeginVertical("box");
        MakePrefab = EditorGUILayout.Foldout(MakePrefab, "Convert To Prefab");

        if (MakePrefab)
        {
            //
            //SourceGameObject = EditorGUILayout.ObjectField("Source GameObject",SourceGameObject, typeof(GameObject), true) as GameObject;
            SourceGameObject = Selection.gameObjects;
            AssignMaterial = EditorGUILayout.ObjectField("Material", AssignMaterial, typeof(Material), true) as Material;
            LightmapStatic = EditorGUILayout.Toggle("Static", LightmapStatic);
            LightmapScale = EditorGUILayout.FloatField("Lightmap Scale", LightmapScale);
            ResetToZero = EditorGUILayout.Toggle("Reset Rotation to Zero", ResetToZero);
            SetNotWalkable = EditorGUILayout.Toggle("Navmesh Not Walkable", SetNotWalkable);
            /*selectedTag = EditorGUI.TagField(
				new Rect(3,3,position.width/2 - 6, 20),
				"New Tag:",
				selectedTag);*/
            selectedTag = EditorGUILayout.TagField("New Tag:", selectedTag);
            selectedLayer = EditorGUILayout.LayerField("Layer:", selectedLayer);



            if (GUILayout.Button("Make It So"))
            {
                CreatePrefab();
            }

        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();


        /* EditorGUILayout.BeginVertical("box");
         AllWallHit = EditorGUILayout.Foldout(AllWallHit, "Add Wall Hit Settings");

         if (AllWallHit)
         {
             EditorGUILayout.HelpBox("Tool for Applting settins to objects so that the player collides with them", MessageType.Info);


             EditorGUILayout.Space();

             if (GUILayout.Button("Apply"))
             {
                 WallhitSettings();
             }


         }
         EditorGUILayout.EndVertical();
         EditorGUILayout.Space();*/


        /*EditorGUILayout.BeginVertical("box");
        SeperateCol = EditorGUILayout.Foldout(SeperateCol, "Seperate Collision");

        if (SeperateCol)
        {
            EditorGUILayout.HelpBox("Tool for Spliting a Gameobject's mesh and collision", MessageType.Info);
            CreatePlaceHolderMesh = EditorGUILayout.Toggle("Create PlaceHolder Mesh", CreatePlaceHolderMesh);
            RemoveAnimatorComponent = EditorGUILayout.Toggle("Remove Animator Component", RemoveAnimatorComponent);
            EditorGUILayout.Space();

            if (GUILayout.Button("Apply"))
            {
                GUILayout.FlexibleSpace();
                Seperate();
            }


        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();*/



        EditorGUILayout.BeginVertical("box");


        CopyPasteTransform = EditorGUILayout.Foldout(CopyPasteTransform, "Copy Paste Transform");

        if (CopyPasteTransform)
        {
            WorldSpace = EditorGUILayout.Toggle("World Space", WorldSpace);
            EditorGUILayout.BeginHorizontal();
            EnablePos = EditorGUILayout.Toggle("Position", EnablePos);
            EnableRot = EditorGUILayout.Toggle("Rotation", EnableRot);
            EnableScale = EditorGUILayout.Toggle("Scale", EnableScale);


            EditorGUILayout.EndHorizontal();
            StoredPosition = EditorGUILayout.Vector3Field("Positon:", StoredPosition);

            StoredRotation = EditorGUILayout.Vector3Field("Rotation", StoredRotation);
            StoredScale = EditorGUILayout.Vector3Field("Scale:", StoredScale);



            if (GUILayout.Button("Copy Single"))
            {
                GUILayout.FlexibleSpace();
                CopySingle();
            }

            if (GUILayout.Button("Paste Single"))
            {
                GUILayout.FlexibleSpace();
                PasteSingle();
            }

            EditorGUILayout.Space();


            EditorGUILayout.LabelField("Num of Objects ", ListOfNames.Count.ToString());


            list = EditorGUILayout.Foldout(list, "List Of Objects");

            if (list)
            {
                for (int i = 0; i < ListOfNames.Count; i++)
                {
                    EditorGUILayout.LabelField(ListOfNames[i]);
                }
            }

            if (GUILayout.Button("Mass Copy"))
            {
                GUILayout.FlexibleSpace();
                CopyTransform();
            }

            if (GUILayout.Button("Mass Paste"))
            {
                GUILayout.FlexibleSpace();
                PasteTransform();
            }


        }



        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");
        ReplaceMissingMatieials = EditorGUILayout.Foldout(ReplaceMissingMatieials, "Replace Missing Materials");

        if (ReplaceMissingMatieials)
        {
            ReplacementMaterial2 = EditorGUILayout.ObjectField("Replacement Material", ReplacementMaterial2, typeof(Material), true) as Material;
            if (GUILayout.Button("Replace Missing Materials"))
            {
                GUILayout.FlexibleSpace();
                PrintMaterial();
            }

            if (GUILayout.Button("Select Gameobjects with Missing Materials"))
            {
                GUILayout.FlexibleSpace();
                SelectAllMissingMaterial();
            }


            if (GUILayout.Button("Revert Materials to Prefab"))
            {
                GUILayout.FlexibleSpace();
                RevertMaterial();
            }

        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();



        EditorGUILayout.BeginVertical("box");
        CopyPasteSkinMaterials = EditorGUILayout.Foldout(CopyPasteSkinMaterials, "Copy Paste Skinned Mesh Materials");
        if (CopyPasteSkinMaterials)
        {
            
            MeshToCopy = EditorGUILayout.ObjectField("Skinned Mesh To Copy", MeshToCopy, typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;

            if (GUILayout.Button("Choose Selected Mesh"))
            {
                MeshToCopy = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>();
            }

            if (GUILayout.Button("Paste Materials"))
            {
                Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials = MeshToCopy.sharedMaterials;
            }
            
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        if (GUILayout.Button("Revert Prefab Values"))
        {
            GUILayout.FlexibleSpace();
            RevertToPrefab();
        }

        EditorGUILayout.Space();


        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
        EditorGUILayout.EndScrollView();
    }

    [MenuItem("Custom/Copy Paste Transform/Mass Transform Copy %#c")]
    static void CopyTransform()
    {
        var obj = Selection.gameObjects;

        if (obj == null)
        {
            Debug.Log("Please Select a Gameobject in the Scene");
            return;
        }


        /*StoredPosition = obj.transform.localPosition;
        StoredRotation = obj.transform.localEulerAngles;
        StoredScale = obj.transform.localScale;*/

        ListOfNames.Clear();
        ListOfTransforms.Clear();
        ListOfRotation.Clear();
        ListsOfScale.Clear();

        foreach (GameObject g in obj)
        {
            if (!WorldSpace)
            {
                ListOfNames.Add(g.name);
                ListOfTransforms.Add(g.transform.localPosition);
                ListOfRotation.Add(g.transform.localEulerAngles);
                ListsOfScale.Add(g.transform.localScale);
            }
            else
            {
                ListOfNames.Add(g.name);
                ListOfTransforms.Add(g.transform.position);
                ListOfRotation.Add(g.transform.eulerAngles);
                ListsOfScale.Add(g.transform.lossyScale);
            }
        }

    }

    [MenuItem("Custom/Copy Paste Transform/Mass Transform Paste %#v")]
    static void PasteTransform()
    {
        var obj = Selection.gameObjects;

        Undo.RecordObjects(obj, "Mass Paste Transform");

        if (obj == null)
        {
            Debug.Log("Please Select a Gameobject in the Scene");
            return;
        }

        foreach (GameObject g in obj)
        {
            for (int i = 0; i < ListOfNames.Count; i++)
            {
                if (g.name == ListOfNames[i])
                {

                    if (!WorldSpace)
                    {
                        if (EnablePos)
                        {
                            g.transform.localPosition = ListOfTransforms[i];
                        }
                        if (EnableRot)
                        {
                            g.transform.localEulerAngles = ListOfRotation[i];
                        }
                        if (EnableScale)
                        {
                            g.transform.localScale = ListsOfScale[i];
                        }
                    }
                    else
                    {
                        if (EnablePos)
                        {
                            g.transform.position = ListOfTransforms[i];
                        }
                        if (EnableRot)
                        {
                            g.transform.eulerAngles = ListOfRotation[i];
                        }
                        if (EnableScale)
                        {
                            g.transform.localScale = ListsOfScale[i];
                        }
                    }
                }
            }
        }

        /*obj.transform.localPosition = StoredPosition;
        obj.transform.localEulerAngles = StoredRotation;
        obj.transform.localScale = StoredScale;*/
    }

    void selectByMaterial()
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            //doSomethingToHierarchy(gameObject);
        }
    }

    void PrintMaterial()
    {
        var obj = Selection.activeGameObject;
        if (obj.GetComponent<Renderer>().sharedMaterial == null)
        {
            Debug.Log("No Material");
        }
        else
        {

        }
        //Debug.Log(obj.GetComponent<Renderer>().sharedMaterial.name);
    }

    [MenuItem("Custom/Copy Paste Transform/Copy Single Transform #c")]
    static void CopySingle()
    {
        var obj = Selection.activeGameObject;


        if (obj == null)
        {
            Debug.Log("Please Select a Gameobject in the Scene");
            return;
        }


        if (!WorldSpace)
        {
            StoredPosition = obj.transform.localPosition;
            StoredRotation = obj.transform.localEulerAngles;
            StoredScale = obj.transform.localScale;
        }
        else
        {
            StoredPosition = obj.transform.position;
            StoredRotation = obj.transform.eulerAngles;
            StoredScale = obj.transform.lossyScale;
        }

    }

    [MenuItem("Custom/Copy Paste Transform/Paste Single Transform #v")]
    static void PasteSingle()
    {
        var obj = Selection.activeGameObject;
        Undo.RecordObject(obj, "Paste Transform");

        if (obj == null)
        {
            Debug.Log("Please Select a Gameobject in the Scene");
            return;
        }
        if (!WorldSpace)
        {
            if (EnablePos)
            {
                obj.transform.localPosition = StoredPosition;

            }
            if (EnableRot)
            {
                obj.transform.localEulerAngles = StoredRotation;
            }
            if (EnableScale)
            {
                obj.transform.localScale = StoredScale;
            }
        }
        else
        {
            if (EnablePos)
            {
                obj.transform.position = StoredPosition;
            }
            if (EnableRot)
            {
                obj.transform.eulerAngles = StoredRotation;
            }
            if (EnableScale)
            {
                obj.transform.localScale = StoredScale;
            }
        }
    }



    //Array function for actually creating the arrays
    void WallhitSettings()
    {
        var obj = Selection.gameObjects;

        if (obj == null)
        {
            Debug.Log("Please Select a Gameobject in the Scene");
            return;
        }

        foreach (GameObject g in obj)
        {
            g.tag = "WallHit";
            g.AddComponent<Rigidbody>();
            Rigidbody r = g.GetComponent<Rigidbody>();
            r.useGravity = false;
            r.isKinematic = false;
            r.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            r.constraints = RigidbodyConstraints.FreezeAll;

            Animator a = g.GetComponent<Animator>();
            if (a != null)
            {
                DestroyImmediate(a);
            }

            PrefabUtility.ReplacePrefab(g, PrefabUtility.GetPrefabParent(g), ReplacePrefabOptions.ConnectToPrefab);
        }

    }


    void BatchLightmapScale()
    {

        foreach (Transform t in Selection.transforms)
        {
            Undo.RegisterCompleteObjectUndo(t.gameObject, "Batch Scale In Lightmap");
            if (t.GetComponent<Renderer>() != null)
            {
                // t.GetComponent<Renderer>().material = ReplacementMaterial;
                SetScaleInLightmap(t.GetComponent<Renderer>(), ScaleInLightmap);


            }

            Component[] AllOfDem;
            AllOfDem = t.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer ren in AllOfDem)
            {
                SetScaleInLightmap(ren, ScaleInLightmap);
            }

        }
    }

    void BatchApplyMaterials()
    {

        if (ReplacementMaterial == null)
        {
            Debug.Log("Please assign a material to use");
        }
        else
        {
            foreach (Transform t in Selection.transforms)
            {
                Undo.RegisterCompleteObjectUndo(t.gameObject, "Batch Material");
                if (t.GetComponent<Renderer>() != null)
                {
                    t.GetComponent<Renderer>().material = ReplacementMaterial;
                }

                Component[] AllOfDem;
                AllOfDem = t.gameObject.GetComponentsInChildren<Renderer>();

                foreach (Renderer ren in AllOfDem)
                {
                    if (ren.gameObject.name.Contains("V-Light"))
                    {
                        //nope
                    }
                    else
                    {
                        ren.material = ReplacementMaterial;
                    }

                }


                /* foreach (Transform child in t)
                 {
                     if (child.GetComponent<Renderer>() != null)
                     {
                         child.GetComponent<Renderer>().material = ReplacementMaterial;
                     }

                     foreach (Transform child2 in child)
                     {
                         if (child2.GetComponent<Renderer>() != null)
                         {
                             child2.GetComponent<Renderer>().material = ReplacementMaterial;
                         }
                     }
                 }*/

            }
        }
    }

    void RevertToPrefab()
    {

        foreach (GameObject g in Selection.gameObjects)
        {
            PrefabUtility.RevertPrefabInstance(g);

            Component[] AllOfDem;
            AllOfDem = g.GetComponentsInChildren<Renderer>();

            foreach (Renderer ren in AllOfDem)
            {
                if (ren.gameObject.name.Contains("V-Light"))
                {
                    //nope
                }
                else
                {
                    PrefabUtility.RevertPrefabInstance(ren.gameObject);
                }

            }
        }

        //ren.materials;
        //SerializedObject obj = new SerializedObject(ren);

        //SerializedProperty sp = obj.FindProperty("Materials");

        //Debug.Log(sp.name);

        //sp.prefabOverride = false;
        //sp.serializedObject.ApplyModifiedProperties();
    }


    void BatchApplyMaterialAsset()
    {
        foreach (Object o in Selection.objects)
        {
            if (o.GetType() == typeof(GameObject))
            {
                GameObject test = o as GameObject;
                if (test.GetComponent<Renderer>() != null)
                {
                    test.GetComponent<Renderer>().material = ReplacementMaterial;
                }

                Component[] AllOfDem;
                AllOfDem = test.gameObject.GetComponentsInChildren<Renderer>();

                foreach (Renderer ren in AllOfDem)
                {
                    ren.material = ReplacementMaterial;
                }
            }
        }
    }


    void BatchApplyPrefab()
    {
        foreach (GameObject t in Selection.gameObjects)

        {
            if (RevertPrefab)
            {
                PrefabUtility.RevertPrefabInstance(t);
            }
            else
            {
                PrefabUtility.ReplacePrefab(t, PrefabUtility.GetPrefabParent(t), ReplacePrefabOptions.ConnectToPrefab);
            }
        }
    }

    void RenameModelPrefab()
    {
        for (i = 0; i < ArrayOfObjects.Length; i++)
        {
            //GameObject loopedGameobject = ArrayOfObjects[i] as GameObject;
            string NewName = ArrayOfObjects[i].GetComponent<MeshFilter>().sharedMesh.name + "_pref";
            //ArrayOfObjects[i].name = NewName + "_pref";
            Debug.Log(NewName);
            string AssetPath = AssetDatabase.GetAssetPath(ArrayOfObjects[i]);
            AssetDatabase.RenameAsset(AssetPath, NewName);

        }
    }

    void ReplaceAssetMaterial()
    {
        for (i = 0; i < ArrayOfObjects.Length; i++)
        {
            //GameObject loopedGameobject = ArrayOfObjects[i] as GameObject;
            string NewName = ArrayOfObjects[i].GetComponent<MeshFilter>().sharedMesh.name + "_pref";
            //ArrayOfObjects[i].name = NewName + "_pref";
            Debug.Log(NewName);
            string AssetPath = AssetDatabase.GetAssetPath(ArrayOfObjects[i]);
            AssetDatabase.RenameAsset(AssetPath, NewName);

        }
    }

    void ReplaceG()
    {
        foreach (Transform t in Selection.transforms)
        {
            Undo.RegisterCompleteObjectUndo(t.gameObject, "Replaced GameObjects");
            GameObject newObject = PrefabUtility.InstantiatePrefab(ReplacementPrefab) as GameObject;
            Transform newT = newObject.transform;
            newT.position = t.position;
            newT.rotation = t.rotation;
            newT.localScale = t.localScale;
            newT.parent = t.parent;
        }

        foreach (GameObject go in Selection.gameObjects)
        {
            DestroyImmediate(go);
        }
    }

    void CreatePrefab()
    {
        for (i2 = 0; i2 < SourceGameObject.Length; i2++)
        {
            {
                newModel = SourceGameObject[i2];

                if (AssignMaterial != null)
                {
                    newModel.GetComponent<Renderer>().material = AssignMaterial;
                }

                //get the asset's file path + its name and extention
                string myPath = AssetDatabase.GetAssetPath(SourceGameObject[i2]);

                //remove FBX extention
                var idx = myPath.LastIndexOf(".FBX");
                if (idx > -1)
                    myPath = myPath.Remove(idx);

                idx = myPath.LastIndexOf(".fbx");
                if (idx > -1)
                    myPath = myPath.Remove(idx);

                //replace the Art folder with the prefabs folder.
                string newPath = myPath.Replace("Art", "Prefabs");

                //create another copy of the file path.
                string CheckPath = newPath;

                //remove the file name from the path so all we have is the directory
                idx = CheckPath.LastIndexOf(newModel.name);
                if (idx > -1)
                    CheckPath = CheckPath.Remove(idx);

                if (!Directory.Exists(CheckPath))
                {
                    //if it doesn't, create it
                    Directory.CreateDirectory(CheckPath);
                }

                //add the naming convention and the new extension	
                newPath = newPath + "_pref.prefab";

                if (LightmapStatic)
                {
                    GameObjectUtility.SetStaticEditorFlags(newModel, StaticEditorFlags.LightmapStatic);
                    GameObjectUtility.SetStaticEditorFlags(newModel, StaticEditorFlags.BatchingStatic);
                    GameObjectUtility.SetStaticEditorFlags(newModel, StaticEditorFlags.NavigationStatic);
                    SetScaleInLightmap(newModel.GetComponent<Renderer>(), LightmapScale);
                }
                if (ResetToZero)
                {
                    newModel.transform.rotation = Quaternion.identity;
                }

                /* if (SetNotWalkable)
                 {
                     int layer = GameObjectUtility.GetNavMeshLayerFromName("Not Walkable");

                     //GameObject g = Selection.activeGameObject;

                     GameObjectUtility.SetNavMeshLayer(newModel, layer);
                 }*/

                newModel.tag = selectedTag;
                newModel.layer = selectedLayer;

                //create a new Prefab
                PrefabUtility.CreatePrefab(newPath, newModel);
            }
        }
    }

    public static void SetScaleInLightmap(Renderer renderer, float scaleInLightmap)

    {

        SerializedObject serializedObject = new SerializedObject(renderer);

        SerializedProperty scaleInLightmapProperty = serializedObject.FindProperty("m_ScaleInLightmap");

        scaleInLightmapProperty.floatValue = scaleInLightmap;

        serializedObject.ApplyModifiedProperties();

    }

    void RevertMaterial()
    {
        foreach (GameObject t in Selection.gameObjects)

        {

            if (t.GetComponent<MeshRenderer>() != null)
            {
                SerializedObject serObj = new SerializedObject(t.GetComponent<MeshRenderer>());

                /*SerializedProperty it = myProp.Copy();
                while (it.Next(true))
                { // or NextVisible, also, the bool argument specifies whether to enter on children or not
                    Debug.Log(it.name);
                }*/

                SerializedProperty sp = serObj.FindProperty("m_Materials");
                /* 
                while (sp.NextVisible(true))
                { // or NextVisible, also, the bool argument specifies whether to enter on children or not
                    Debug.Log(sp.name);
                }*/


                if (sp != null)
                {
                    sp.prefabOverride = false;
                    sp.serializedObject.ApplyModifiedProperties();
                }

                if (sp != null)
                {
                    sp.prefabOverride = false;
                    sp.serializedObject.ApplyModifiedProperties();
                }
            }
        }


    }


    void SelectAllMissingMaterial()
    {

        List<GameObject> newSelection = new List<GameObject>();

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.GetComponent<Renderer>() != null)
            {
                if (g.GetComponent<Renderer>().sharedMaterial == null)
                {
                    newSelection.Add(g);
                }
            }
        }

        Selection.objects = newSelection.ToArray();
    }

    void Seperate()
    {
        foreach (Transform t in Selection.transforms)

        {

            //Undo.RegisterUndo(t,"Seperated Collisions" + t.name);
            Undo.RegisterCompleteObjectUndo(t.gameObject, "Seperated Collision");

            //Clone the gameobject
            newObject = Instantiate(t, t.transform.position, t.transform.rotation) as Transform;

            Undo.RegisterCreatedObjectUndo(newObject, "Seperated Collision");

            if (CreatePlaceHolderMesh)
            {
                //temporary create a Box to soruce the Mesh from
                GameObject newMesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //change the cloned objects mesh to the box
                newObject.GetComponent<MeshFilter>().sharedMesh = newMesh.GetComponent<MeshFilter>().sharedMesh;
                //put the cloned object on the hidden layer so the camera can't see it.
                newObject.gameObject.layer = 8;
                //get rid of the temporary box
                DestroyImmediate(newMesh);
            }
            else
            {
                //just get rid of the Mesh Filter and renderer components.
                DestroyImmediate(newObject.GetComponent(typeof(MeshFilter)));
                DestroyImmediate(newObject.GetComponent(typeof(MeshRenderer)));
            }

            if (RemoveAnimatorComponent)
            {
                //remove the animator components
                DestroyImmediate(newObject.GetComponent(typeof(Animator)));
                DestroyImmediate(t.GetComponent(typeof(Animator)));
            }

            //get rid of the collision on the source object.
            DestroyImmediate(t.GetComponent(typeof(BoxCollider)));

            //parent the cloned collision to the source object.
            newObject.transform.parent = t.transform;

        }
    }

}