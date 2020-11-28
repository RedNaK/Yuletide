using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
