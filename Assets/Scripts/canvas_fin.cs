using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class canvas_fin : MonoBehaviour
{
    public int waitForEnabled;
    private bool returnMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    void Update()
    {
        if (returnMenu == true) {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                //play video et quand fini ->
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(waitForEnabled);

        returnMenu = true;
        GetComponent<Canvas>().enabled = true;
    }
}
