using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject Pointer;
    private float maxY = -5.5f;
    private float miniY = -27f;
    private float nextY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Calcule de la position Y avec hauteur Max et Mini
        if (Pointer.transform.position.y > maxY)
        {
            nextY = maxY;
        }
        else if (Pointer.transform.position.y < miniY)
        {
            nextY = miniY;
        }
        else {
            nextY = Pointer.transform.position.y;
        }
        Debug.Log("nextY: " + nextY + " - Y pointer: " + Pointer.transform.position.y);
        transform.position = new Vector3(transform.position.x, nextY, transform.position.z);
    }
}
