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
        transform.position = new Vector3(transform.position.x, Pointer.transform.position.y, transform.position.z);
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
        //Debug.Log("nextY: " + nextY + " - Y pointer: " + Pointer.transform.position.y);
        Debug.Log("transform.position:"+ transform.position.y+ " - Pointer.transform.position.y: " + Pointer.transform.position.y);
        Debug.Log(transform.position.y - nextY);
        // if (transform.position.y - nextY > 0.5f || transform.position.y - nextY < -0.5f) {
        //public nextPos = ;
        //transform.position = Vector3.Slerp(transform.position, nextPos + CameraOffset, 0.5f);
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, nextY, 2 * Time.deltaTime), transform.position.z);
       // }
       
    }
}
