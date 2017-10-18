using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TimeLineTrigger : MonoBehaviour
{

    public PlayableDirector EnterTimeline;
    public bool PlayExitTimeline;
    public PlayableDirector ExitTimeline;
    //public float Threshold = 2.0f;

    public bool SetFogSettings;
    private float StoredStart;
    private float StoredEnd;
    public float NewStart = 5;
    public float NewEnd = 15;

    // Use this for initialization
    void Start()
    {
        // timeline = GetComponent<PlayableDirector>();
        StoredStart = RenderSettings.fogStartDistance;
        StoredEnd = RenderSettings.fogEndDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            GameManager.Singleton.MainPlayerCamera.GetComponent<CinemachineBrain>().enabled = true;
            // GameManager.Singleton.GameplayTimeline.GetComponent<PlayableDirector>().enabled = false;
            EnterTimeline.Play();

            if (SetFogSettings)
            {
                RenderSettings.fogStartDistance = NewStart;
                RenderSettings.fogEndDistance = NewEnd;
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
       /* if (other.gameObject.tag == "Player")
        {
            GameManager.Singleton.GameplayTimeline.GetComponent<PlayableDirector>().enabled = false;

        }*/
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            EnterTimeline.Stop();
            GameManager.Singleton.MainPlayerCamera.GetComponent<CinemachineBrain>().enabled = false;

            if (SetFogSettings)
            {
                RenderSettings.fogStartDistance = StoredStart;
                RenderSettings.fogEndDistance = StoredEnd;
            }

            if (PlayExitTimeline)
            {
                ExitTimeline.Play();

            }
            else
            {
               // GameManager.Singleton.GameplayTimeline.GetComponent<PlayableDirector>().enabled = true;
            }

        }
    }
}
