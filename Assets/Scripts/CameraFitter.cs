using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFitter : MonoBehaviour
{
    bool MaintainWidth = true;
    Vector3 CameraPos;
    float DefaultWidth;
    float DefaultHeight;
    float horizontalResolution = 1920f;

    void Start() {
        /*
        CameraPos = Camera.main.transform.position;

        DefaultWidth = Camera.main.orthographicSize * 1920 ;
        DefaultHeight = Camera.main.orthographicSize;
         if (MaintainWidth) {
                Camera.main.orthographicSize = DefaultWidth / Camera.main.aspect;
            }
        Camera.main.transform.position = new Vector3(CameraPos.x, -1 * (DefaultHeight - Camera.main.orthographicSize), CameraPos.z);
        */
        float currentAspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;
    }

    /*
    void OnGUI() {
        float currentAspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;
    }*/
}



