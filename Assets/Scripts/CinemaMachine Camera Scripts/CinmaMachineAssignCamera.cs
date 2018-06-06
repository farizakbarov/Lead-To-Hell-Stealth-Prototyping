using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Cinemachine.Timeline;

public class CinmaMachineAssignCamera : MonoBehaviour
{

    private PlayableDirector director;
    private PropertyName myName;
    public bool AssignMainCamera = true;
    public bool AssignCamToClip;
    public int ClipIndex = 1;
    public bool AssignPlayerCam;
    public Cinemachine.CinemachineVirtualCameraBase NewCam;

   
    int iterator = 0;

    // Use this for initialization
    void Start()
    {
        director = this.GetComponent<PlayableDirector>();

        // director.playableAsset


        //director.SetGenericBinding(GameManager.Singleton.MainPlayerCamera, GameManager.Singleton.MainPlayerCamera);
        foreach (PlayableBinding pb in director.playableAsset.outputs)
        {
            // CinemachineTrack at = pb.sourceObject as CinemachineTrack;

            var tr = pb.sourceObject as CinemachineTrack;

            //  Debug.Log(tr.name);
            if (AssignCamToClip)
            {
                foreach (var clip in tr.GetClips())
                {

                    // CinemachineShot st = t.asset as CinemachineShot;

                    var t = clip.asset as CinemachineShot;
                    if (iterator == ClipIndex - 1)
                    {
                        //  Debug.Log(t.VirtualCamera.exposedName);
                        myName = t.VirtualCamera.exposedName;
                        iterator++;
                    }

                    // director.SetReferenceValue(t.VirtualCamera.exposedName, GameManager.Singleton.MainPlayerCamera.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().gameObject);
                    // st.VirtualCamera.exposedName = UnityEditor.GUID.Generate().ToString();


                    // Debug.Log(st.VirtualCamera.exposedName);
                    // director.SetReferenceValue(t., target.transform);
                    // myName = st.VirtualCamera.exposedName;
                }

                if (AssignPlayerCam)
                {
                    director.SetReferenceValue(myName, Stealth_GameManager.Singleton.GameplayTimeline.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>());
                }
                else
                {
                    director.SetReferenceValue(myName, NewCam);
                }
            }

            if (AssignMainCamera)
            {
                director.SetGenericBinding(tr, GameManager.Singleton.MainPlayerCamera);
            }




        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
