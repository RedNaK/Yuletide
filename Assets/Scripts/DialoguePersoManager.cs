﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePersoManager : MonoBehaviour
{
    public DialoguePerso dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void click()
    {
        Debug.Log("dialogue de " + gameObject.name);    
    }
}
