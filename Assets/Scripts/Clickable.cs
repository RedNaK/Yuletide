using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clickable : MonoBehaviour
{
    public bool isCollectible = false;
    public bool isObjetPerso = false;
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
                if (isObjetPerso)
                {
                    aS.volume = 0.5f;
                    aS.PlayOneShot(clicChar);
                }
                else
                {
                    aS.volume = 1f;
                    aS.PlayOneShot(clicObjet);
                }
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                GameManager.instance.objetTrouve();
            }

            if (isCharacter)
            {
                GetComponent<DialoguePersoManager>().click();
            }
        }
        else
        {
            aS.volume = 1f;
            aS.PlayOneShot(clicEmpty);
        }
    }
}
