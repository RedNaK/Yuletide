﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	/// <summary>
	/// The instance of the GameManager.
	/// </summary>
	private static GameManager _instance;

    /// <summary>
    /// The pause menu script.
    /// </summary>
    private PauseMenuV2 pauseMenu;

	/// <summary>
	/// Whether to show or hide the cursor during gameplay.
	/// </summary>
	public bool showCursorMode;
	/// <summary>
	/// Whether to lock the cursor during gameplay.
	/// </summary>
	public CursorLockMode lockCursorMode;

    /// <summary>
    /// Whether the game was paused.
    /// </summary>
    public bool paused { get; private set; }

    private int launch = 0;
    public bool resetData = false;
    public bool overlayActive = false;
    public int animating = 0;

    /// <summary>
    /// Retrieve the instance of the game manager.
    /// </summary>
    /// <value>The game manager.</value>
    public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.Find("NKGameManager").GetComponent<GameManager>();
			}
			return _instance;
		}
	}

    void Awake()
    {
        if (resetData)
        {
            ResetData();
        }
    }

    // Use this for initialization
    void Start ()
	{
        // The pause menu
        pauseMenu = GetComponent<PauseMenuV2>();

        // Init the cursor behaviour
        Cursor.lockState = lockCursorMode;
        Cursor.visible = showCursorMode;

        paused = false;
        overlayActive = false;
        LoadGame();
    }
	
	// Update is called once per frame
	void Update ()
	{
        // Pause and unpause the game
        if (Input.GetButtonDown("Pause Menu"))
            {
            if (paused)
            {
                hidePauseMenu();
            }
            else
            {
                showPauseMenu();
            }

            // Activate and deactivate game objects
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject gameObject in objects)
            {
                if (paused)
                {
                    gameObject.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    gameObject.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    #if UNITY_WEBPLAYER || UNITY_WEBGL
		    // Go fullscreen
		    if (Input.GetKeyDown(KeyCode.F))
		    {
			    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		    }
    #endif
	}

    /// <summary>
    /// Fired When the player selects the restart button.
    /// </summary>
    public void OnRestartButton()
    {
        ResetData();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Fired when the player selects the back button.
    /// </summary>
    public void OnBackButton()
    {
        hidePauseMenu();
    }

    /// <summary>
    /// Fired when the player selects the quit button.
    /// </summary>
    public void OnQuitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// Show the pause menu and unlock/show cursor.
    /// </summary>
    private void showPauseMenu ()
    {
        pauseMenu.showMenu();
        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        paused = true;
        overlayActive = true;
    }

    /// <summary>
    /// Hide the pause menu and reset cursor to wanted mode.
    /// </summary>
    private void hidePauseMenu()
    {
        pauseMenu.hideMenu();
        Cursor.lockState = lockCursorMode;
        Cursor.visible = showCursorMode;
        paused = false;
        overlayActive = false;
    }

    void OnDestroy()
    {
        SaveGame();
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedLaunch"))
        {
            launch = PlayerPrefs.GetInt("SavedLaunch");
            Debug.Log("Game data loaded! Launch = " + launch);

        }
        else
            Debug.Log("There is no save data!");
    }

    void SaveGame()
    {
        launch++;
        PlayerPrefs.SetInt("SavedLaunch", launch);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    void ResetData()
    {
        PlayerPrefs.DeleteAll();
        launch = 0;
        Debug.Log("Data reset complete");
    }

}