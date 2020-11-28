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

	// Update is called once per frame
	void next()
	{ 
		//play video et quand fini ->
		SceneManager.LoadScene(2);
	}
}
