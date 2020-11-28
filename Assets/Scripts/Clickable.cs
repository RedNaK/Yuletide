using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clickable : MonoBehaviour
{
    public bool isCollectible = false;
    public bool isCharacter = false;

    public DropZone dropZone;
    public void click()
    {
        if (dropZone.hasCharacter == true)
        {
            if (isCollectible)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                GameManager.instance.objetTrouve();
            }

            if (isCharacter)
            {
                GetComponent<DialoguePersoManager>().click();
                GameManager.instance.dialogueLu();
            }
        }
    }
}
