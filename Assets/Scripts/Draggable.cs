using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 dragHandle;
    private DropZone[] dropeZones ;
    public DropZone parentDZ;
    private float thisHeight;
    private DropZone nearestDZ;
    private AudioSource aS;

    public AudioClip decoller;
    public AudioClip coller;

    private float preWait;

    public void Start()
    {
        thisHeight = GetComponent<SpriteRenderer>().bounds.size.y/2;
        transform.position = parentDZ.transform.position + new Vector3(0f, thisHeight, 0f);
        dropeZones = GameObject.FindObjectsOfType<DropZone>();
        aS = GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        aS.PlayOneShot(decoller);
        Debug.Log("drag start");
        Plane plane = new Plane(Vector3.forward, transform.position);

        Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            dragHandle = transform.position - (ray.origin + ray.direction * distance);
        }
        nearestDZ = parentDZ;
        parentDZ.dropping(true);
        preWait = Time.time + 0.24f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("on drag");
        // Vector3.up makes it move in the world x/z plane.

        if (preWait > Time.time)
        {
            //return; /*eactiver si possible de gérer souci de je bouge, mais je m'arrête avant que ça ne gère, si souris ne bouge pas, cette fonction n'est pas appelée...*/
        }

        Plane plane = new Plane(Vector3.forward, transform.position);

        Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            transform.position = (ray.origin + ray.direction * distance) + dragHandle;
        }

        Debug.Log("End Drag");
        /*search nearest dropzone and drop on it*/

        float currentNearest = 10000f;
        foreach (DropZone dz in dropeZones)
        {
            /*passer ça en x et y uniquement déjà*/
            float dist = Vector3.Distance(transform.position - new Vector3(0f, thisHeight, 0f), dz.transform.position);
            Debug.Log(dz.name + ": " + dist);
            
            if (dist < 5 && dist < currentNearest)
            {
                currentNearest = dist;
                if(nearestDZ != dz)
                {
                    nearestDZ.dropping(false);
                    nearestDZ = dz;
                    nearestDZ.dropping(true);
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        parentDZ = nearestDZ;
        transform.position = parentDZ.transform.position + new Vector3(0f, thisHeight, 0f);
        aS.PlayOneShot(coller);
    }
}