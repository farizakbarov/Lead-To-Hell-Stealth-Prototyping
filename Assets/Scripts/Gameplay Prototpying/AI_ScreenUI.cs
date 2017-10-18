using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_ScreenUI : MonoBehaviour
{
    public Transform ObjectToTrack;

    //Rect rect = new Rect(0, 0, 300, 100);
    Vector3 offset = new Vector3(0, 1.5f, 0); // height above the target position

    private RectTransform myTransform;
    private Canvas myCanvas;
    //private RectTransform CanvasTransform;

    // Use this for initialization
    void Start()
    {
        myTransform = this.GetComponent<RectTransform>();
        myCanvas = GetComponentInParent<Canvas>();
       // CanvasTransform = myCanvas.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectToTrack != null)
        {
            Vector3 target = GameManager.Singleton.MainPlayerCamera.GetComponent<Camera>().WorldToScreenPoint(ObjectToTrack.position + offset);
            Debug.Log(target);
            myTransform.position = new Vector3(target.x, target.y, 0);
            //Debug.Log(CanvasTransform.rect.width / 2);
           /* Debug.Log(myTransform.localPosition.x);
            if (myTransform.localPosition.x > (CanvasTransform.rect.width /2))
            {
                 myTransform.localPosition = new Vector3((CanvasTransform.rect.width / 2) - (myTransform.rect.width/2), myTransform.localPosition.y, 0);
            }

            if (myTransform.localPosition.x < -(CanvasTransform.rect.width / 2))
            {
                myTransform.localPosition = new Vector3(-(CanvasTransform.rect.width / 2) + (myTransform.rect.width / 2), myTransform.localPosition.y, 0);
            }


            if (myTransform.localPosition.y > (CanvasTransform.rect.height / 2))
            {
                myTransform.localPosition = new Vector3(myTransform.localPosition.x, (CanvasTransform.rect.height / 2) - (myTransform.rect.height / 2), 0);
            }

            if (myTransform.localPosition.y < -(CanvasTransform.rect.height / 2))
            {
                myTransform.localPosition = new Vector3(myTransform.localPosition.x, -(CanvasTransform.rect.height / 2) + (myTransform.rect.height / 2), 0);
            }*/

        }
    }



    /*void OnGUI()
    {
        
        var point = GameManager.Singleton.MainPlayerCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position);
        rect.x = point.x;
        rect.y = Screen.height - point.y - rect.height; // bottom left corner set to the 3D point
        GUI.Label(rect, transform.name); // display its name, or other string
    }*/
}
