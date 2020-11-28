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
    private bool isDragging;
    private PointerEventData lastEventData ;

    public float rateL = 3f;

    public void Start()
    {
        thisHeight = GetComponent<SpriteRenderer>().bounds.size.y/2;
        transform.position = parentDZ.transform.position + new Vector3(0f, thisHeight, 0f);
        parentDZ.dropping(true);
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
        isDragging = true;
        parentDZ.charExitingZone();
        foreach (DropZone dz in dropeZones)
        {
            dz.startDragging();
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        lastEventData = eventData;
    }

    void Update()
    {
        /*reactiver si possible de gérer souci de je bouge, mais je m'arrête avant que ça ne gère, si souris ne bouge pas, cette fonction n'est pas appelée...*/
        /*
        if (preWait > Time.time)
        {
            return; 
        }
        */
       
        if (!isDragging) { return; }

        Plane plane = new Plane(Vector3.forward, transform.position);
        Ray ray = lastEventData.pressEventCamera.ScreenPointToRay(lastEventData.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 newPos = (ray.origin + ray.direction * distance) + dragHandle;
            if (Mathf.Abs(newPos.y - transform.position.y) > 0.1f)
            {
                transform.position = new Vector3 (Mathf.Lerp(transform.position.x, newPos.x, rateL * Time.deltaTime), Mathf.Lerp(transform.position.y, newPos.y, rateL * Time.deltaTime), transform.position.z);
            }
        }

        /*search nearest dropzone and drop on it*/

        float currentNearest = 10000f;
        foreach (DropZone dz in dropeZones)
        {
            float dist = Vector3.Distance(transform.position - new Vector3(0f, thisHeight, 0f), dz.transform.position);
            Debug.Log(dz.name + ": " + dist);

            if (dist < 5 && dist < currentNearest)
            {
                currentNearest = dist;
                if (nearestDZ != dz)
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
        parentDZ.charEnteringZone();
        aS.PlayOneShot(coller);
        isDragging = false;
        foreach (DropZone dz in dropeZones)
        {
            dz.stopDragging();
        }
    }
}