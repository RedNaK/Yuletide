﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class DropZone : MonoBehaviour
{

    public bool hasCharacter = false;
    public Light2D lightZone;
    // Start is called before the first frame update
    void Start()
    {
        if(lightZone != null) lightZone.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void charEnteringZone()
    {
        Debug.Log("char entering " + transform.name);
        hasCharacter = true;
        if (lightZone != null) lightZone.enabled = true ;
    }

    public void charExitingZone()
    {
        Debug.Log("char exiting " + transform.name);
        hasCharacter = true;
        if (lightZone != null) lightZone.enabled = false;
    }

    public void dropping(bool isDropping)
    {
        if (isDropping)
        {
            GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        }
    }
}
