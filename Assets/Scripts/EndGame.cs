using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private bool finish = false;
    public FeuArtificeManager[] fs;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("theend", 100f);
    }

    // Update is called once per frame
    void theend()
    {
        finish = true;
        foreach (FeuArtificeManager f in fs)
        {
            f.wait = -1;
        }

    }

    private void Update()
    {
        if (finish && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }
}
