using UnityEngine;
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
    public bool overlayActive { get; private set; }
    public bool popupActive { get; private set; }

    private int launch = 0;
    public bool resetData = false;
    
    public int animating = 0;

    public GameObject mainpopup;

    public int questID = 0;
    public int questStep = 0;

    private string[,] autorizedObject;
    private string dialogueSuite;
    private Sprite repDialSprite = null;
    private string repDialTitre;
    private string repDialContenu;
    private bool readyToEnd = false;

    public AudioClip dialogueSound;
    public AudioClip fouilleSound;
    private AudioSource aS;

    public Animator fadeInOut;

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
        popupActive = false;
        closePopup();
        LoadGame();

        aS = GetComponent<AudioSource>();

        autorizedObject = new string[5,4];

        autorizedObject[0,0] = "dialogue";
        autorizedObject[0,1] = "lunette";
        autorizedObject[0,2] = "dialogue";
        
        autorizedObject[1,0] = "dialogue";
        autorizedObject[1,1] = "caillou";
        autorizedObject[1,2] = "dialogue";
        autorizedObject[1,3] = "pommedepin";

        autorizedObject[2,0] = "dialogue";
        autorizedObject[2,1] = "peigne";
        autorizedObject[2,2] = "dialogue";
        autorizedObject[2,3] = "fleur";

        autorizedObject[3,0] = "dialogue";
        autorizedObject[3,1] = "cafe";
        autorizedObject[3,2] = "dialogue";
        autorizedObject[3,3] = "gui";

        autorizedObject[4,0] = "dialogue";
        autorizedObject[4,1] = "gateau";
        autorizedObject[4,2] = "dialogue";
        autorizedObject[4,3] = "etoile";
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (overlayActive)
        {
            if (popupActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0)))
            {
                closePopup();
            }
        }
        else
        {
            /*if (Input.GetMouseButtonDown(1))
            {
                setPopupContent(test);
                openPopup();
            }*/

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Clickable"));
                if (hit.collider != null)
                {

                    Clickable cO = hit.collider.gameObject.GetComponent<Clickable>();
                    if (cO != null)
                    {
                        if (cO.isCollectible)
                        {
                            if (autorizedObject[questID, questStep] == hit.collider.gameObject.name)
                            {
                                //Debug.Log("send clic " + hit.collider.gameObject.name);
                                cO.click();
                            }
                            else
                            {
                                //Debug.Log("fouille objet pas encore autorisé");
                                aS.PlayOneShot(fouilleSound);
                            }
                        }
                        else if (cO.isCharacter)
                        {
                            //Debug.Log("send dialogue " + hit.collider.gameObject.name);
                            cO.click();
                        }
                    }
                }
                else
                {
                    /*jouer le son de fouille*/
                    //Debug.Log("fouille");
                    aS.PlayOneShot(fouilleSound);
                }
            }
        }

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

    public void setPopupContent(Sprite sp, string title, string content, bool isRight = false)
    {
        //mainpopup.GetComponentInChildren<Canvas>().enabled = false;
        Image[] imgpopup = mainpopup.GetComponentsInChildren<Image>();
        imgpopup[2].sprite = sp;
        imgpopup[3].sprite = sp;
        imgpopup[2].enabled = !isRight;
        imgpopup[3].enabled = isRight;
        Text[] textpopup = mainpopup.GetComponentsInChildren<Text>();
        textpopup[0].text = title;
        //textpopup[1].text = title;
        //textpopup[0].enabled = !isRight;
        //textpopup[1].enabled = isRight;
        if (content.IndexOf("/") != -1)
        {
            textpopup[1].text = content.Substring(0,content.IndexOf("/")).Replace("\\n", "\n");
            dialogueSuite = content.Substring(content.IndexOf("/")+2).Replace("\\n", "\n");
        }
        else
        {
            textpopup[1].text = content.Replace("\\n", "\n");
            dialogueSuite = null;
        }
    }

    public void openPopup()
    {
        if (overlayActive) { return; }

        mainpopup.GetComponentInChildren<Canvas>().enabled = true;
        overlayActive = true;
        popupActive = true;
        aS.PlayOneShot(dialogueSound);
    }

    public void openPopup(Sprite persoD, string nomPersoD, string dialD)
    {
        if (overlayActive) { return; }

        repDialSprite = persoD;
        repDialTitre = nomPersoD;
        repDialContenu = dialD;

        mainpopup.GetComponentInChildren<Canvas>().enabled = true;
        overlayActive = true;
        popupActive = true;
        aS.PlayOneShot(dialogueSound);
    }

    public void closePopup()
    {
        if (repDialSprite != null)
        {
            setPopupContent(repDialSprite, repDialTitre, repDialContenu, true);
            repDialSprite = null;
        }
        else if (dialogueSuite != null)
        {
            Text[] textpopup = mainpopup.GetComponentsInChildren<Text>();
            textpopup[1].text = dialogueSuite;
            dialogueSuite = null;
        }
        else
        {
            mainpopup.GetComponentInChildren<Canvas>().enabled = false;
            overlayActive = false;
            popupActive = false;
            if (readyToEnd)
            {
                fadeOutToEnd();
                readyToEnd = false; /*on s'assure que ça se déclenche pas 2x*/
            }
        }
    }

    public void objetTrouve()
    {
        nextStep();
    }

    public void dialogueNext()
    {
        nextStep();
    }

    
    public void fadeOutToEnd()
    {
        /*TODO fade out en 3 secondes environ ?*/
        fadeInOut.SetTrigger("toOpacite");
        Invoke("goToFireworks", 2f);
    }
    public void goToFireworks()
    {
        SceneManager.LoadScene(3);
    }

    public void nextStep()
    {       
        questStep++;
        if (questID == 0 && questStep >= 3)
        {
            questID++;
            questStep = 0;
        }
        else if (questID != 0 && questStep >= 4)
        {
            questID++;
            questStep = 0;

            
        }
        else if (questID >= 5 && questStep == 1)
        {
            readyToEnd = true;
            //Debug.Log("launch end game");
        }
        //Debug.Log("quête "+questID+" étape "+questStep);
    }

    /*
     quest ID
    0 = Loutre (Avant, Lunette, Maison, Aide)
    1 = Blaireau
    2 = Hérisson
    3 = Chouette
    4 = Chauve-Souris
    */


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
