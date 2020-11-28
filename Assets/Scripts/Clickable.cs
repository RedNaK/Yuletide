using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clickable : MonoBehaviour
{
    public bool isCollectible = false;
    public bool isCharacter = false;

    private AudioSource aS;

    public AudioClip clicObjet;
    public AudioClip clicChar;
    public AudioClip clicEmpty;

    public DropZone dropZone;

    public void Start() {
        aS = GetComponent<AudioSource>();
    }

    public void click()
    {
        if (dropZone.hasCharacter == true)
        {
            if (isCollectible)
            {
                aS.PlayOneShot(clicObjet);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                GameManager.instance.objetTrouve();
            }

            if (isCharacter)
            {
                aS.PlayOneShot(clicChar);
                GetComponent<DialoguePersoManager>().click();
                GameManager.instance.dialogueLu();
            }
        }
        else
        {
            aS.PlayOneShot(clicEmpty);
        }
    }
}
