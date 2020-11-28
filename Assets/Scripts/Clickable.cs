using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clickable : MonoBehaviour
{
    public bool isCollectible = false;
    public bool isCharacter = false;
    public void click()
    {
        if (isCollectible)
        {
            GetComponent<ObjectTrouvable>().click();
        }

        if (isCharacter)
        {
            GetComponent<DialoguePersoManager>().click();
        }
    }
}
