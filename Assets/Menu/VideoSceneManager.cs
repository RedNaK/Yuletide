using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoSceneManager : MonoBehaviour
{


	// Use this for initialization
	void Start()
	{
		Invoke("next", 65f);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
			SceneManager.LoadScene(2);
		}
    }

    // Update is called once per frame
    void next()
	{ 
		//play video et quand fini ->
		SceneManager.LoadScene(2);
	}
}
