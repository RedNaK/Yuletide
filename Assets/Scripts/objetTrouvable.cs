using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetTrouvable : MonoBehaviour
{
    public GameObject drapZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {

        if (drapZone.GetComponent<DropZone>().hasCharacter == true)
        {
            Debug.Log("CLic");
        }
        else
        {
            Debug.Log("CLic PAAAAAASSS");
        }
    }
}
