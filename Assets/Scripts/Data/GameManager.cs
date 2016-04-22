using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameManager : Singleton<GameManager> {
	
	public int levelNumber;
	public bool battleToMap = false;
    public GameStats gamestats;
	#region Game States
	
	// An enum with all the possible states of the game. NOTE: depending on the game the game states may change. Please add at the end of the screen.

    public enum GameState { Splash = 0, MainMenu, HUD, ExitPopUp, Scores, CharacterSelection, Inventory, MainMenuQuitPopup, QuitPopup, SettingsPopup,ModeSelection };



//	System.Action<bool> mAuthCallback;
//	bool mAuthOnStart = true;
//	bool mInMatch = false;

	// the current state of the game
	public GameState initialState = GameState.MainMenu;

	public GameState previousState;
    public bool isMissionCompleted = false;
    public GameStats gameStats;
	// the delegate for state change
	public delegate void OnGameStateChange(GameState g);
	public event OnGameStateChange GameStateChanged;
    public UIRoot uiRoot;
	public bool isUIStateDirty = false;

	public bool isGame1Player;


    public GameManager.GameState currentState;
    public string currentLevel;

    public HUD hud()
    {
        HUD[] hud = uiRoot.transform.GetComponentsInChildren<HUD>();
        if (hud.Length > 0)
            return hud[0];
        else
            return null;
    }

	
	void HandleGameStateChanged (GameState g)
	{
		
	}
	
	public void ChangeGameStateTo (GameState g)
	{
		GameStateChanged (g);
        previousState = currentState;
	}
	
	#endregion Game States
	
	
	public ArrayList stagesList;
	#region PurchaseableItems
	
	#endregion PurchaseableItems
	#region AssetLists
	//Stage base target points
	public Dictionary<string,int> targetPointsDictionary;
	#endregion AssetLists
	
	
	
	
	#region Player
	// The Player
	public Player player;
	
	#endregion Player
	
	#region Navigation Manager
	
	public NavigationManager navigationManager = new NavigationManager();
	
	#endregion Navigation Manager
	
	#region Pause
	
	public bool isPaused = false;
	
	// The Sound Manager	
	public bool isSoundPaused;

	#endregion Pause
	
	#region Mono Life Cycle
	
	void Awake() 
	{

		currentState = initialState;
		if(GameObject.FindGameObjectsWithTag("GameManager").Length > 1) 
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
        gamestats = GetComponent<GameStats>();
		GameManager.Instance.GameStateChanged += HandleGameStateChanged;
		GameManager.Instance.isSoundPaused = false;
		
	}
	
	// Use this for initialization
	void Start () 
	{
		//coinsCollected = PlayerPrefs.GetInt("Total Coins");
		makeUIDirtyState();

        Application.LoadLevel(initialState.ToString());
		// authenticate user:
//		Social.localUser.Authenticate((bool success) => {
//			// handle success or failure
//		});

		//// ---------------- Google Play Authentiacation ------------ ///


//		mAuthCallback = (bool success) => {
//		
//		};
		
//		PlayGamesClientConfiguration config = 
//			new PlayGamesClientConfiguration.Builder()
//				.WithInvitationDelegate(OnGotInvitation)
//				.WithMatchDelegate(OnGotMatch)
//				.Build();
//		
//		PlayGamesPlatform.InitializeInstance(config);
//		// make Play Games the default social implementation
//		PlayGamesPlatform.Activate();
		
		// enable debug logs (note: we do this because this is a sample; on your production
		// app, you probably don't want this turned on by default, as it will fill the user's
		// logs with debug info).
	
		

	}
	




	
	// Update is called once per frame
	void Update () 
	{

			Ray inputRay;
			RaycastHit hit;
			int layerMask = 1 << LayerMask.NameToLayer("UI");

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(MenuManager.Instance.NavigationStackPeek() == GameState.MainMenu){
				// Quit Game
				MenuManager.Instance.PushMenu(GameManager.GameState.MainMenuQuitPopup);

			}
			else{
				if(MenuManager.Instance.NavigationStackPeek() == GameState.MainMenuQuitPopup || MenuManager.Instance.NavigationStackPeek() == GameState.QuitPopup)
				{
					return;
				}
				//MenuManager.Instance.PopMenu();
				MenuManager.Instance.PushMenu(GameManager.GameState.QuitPopup);
				//BackToState(MenuManager.Instance.NavigationStackPeek());
			}

		}
	}



	public void makeUIDirtyState()
	{
		isUIStateDirty = true;
		Invoke ("stopUIDirtyState",2.0f);
	}
	private void stopUIDirtyState()
	{
		isUIStateDirty = false;
	}
	
	public void OnDestroy () 
	{
		if(GameObject.FindGameObjectsWithTag("GameManager").Length < 1)
		{
			applicationIsQuitting = true;
		}
	}
	
	
	#endregion Mono Life Cycle

    public void ConfigureLevelForState(GameManager.GameState g)
    {
		if (g == GameManager.GameState.ModeSelection)
		{
			currentState = GameManager.GameState.ModeSelection;
			currentLevel = GameManager.GameState.ModeSelection.ToString();
			SetStateAndLoad(currentLevel, currentState);
		}
		if (g == GameManager.GameState.MainMenuQuitPopup)
		{
			currentState = GameManager.GameState.MainMenu;
			currentLevel = GameManager.GameState.MainMenu.ToString();
			SetStateAndLoad(currentLevel, currentState);
		}
		
		if (g == GameManager.GameState.MainMenu)
        {
            currentState = GameManager.GameState.MainMenu;
            currentLevel = GameManager.GameState.MainMenu.ToString();
            SetStateAndLoad(currentLevel, currentState);
        }

        if (g == GameManager.GameState.Inventory)
        {
            currentState = GameManager.GameState.Inventory;
            currentLevel = GameManager.GameState.MainMenu.ToString();
            SetStateAndLoad(currentLevel, currentState);
        }

        if (g == GameManager.GameState.Scores)
        {
            currentState = GameManager.GameState.Scores;
            currentLevel = GameManager.GameState.MainMenu.ToString();
            SetStateAndLoad(currentLevel, currentState);
        }

        if (g == GameManager.GameState.HUD)
        {
            currentState = GameManager.GameState.HUD;
            currentLevel = GameManager.GameState.HUD.ToString();
            SetStateAndLoad(currentLevel, currentState);
        }
        if (g == GameManager.GameState.CharacterSelection)
        {
            currentState = GameManager.GameState.CharacterSelection;
            currentLevel = GameManager.GameState.CharacterSelection.ToString();
            SetStateAndLoad(currentLevel, currentState);
        }
    }

	public void BackToState(GameManager.GameState g)
	{
			Application.LoadLevel(g.ToString());
	}
	
	
	void SetStateAndLoad(string name, GameManager.GameState state)
    { 
		if (name != "ModeSelection") {

			Application.LoadLevel (name);
		}

//		MenuManager.Instance.PushMenu(currentState);
		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_SCENE, Constants.GA_ACTION_TYPE_SCENESTART,state+ " Started");

        Invoke("PushMenu", 0.1f);
        //MenuManager.Instance.PushMenu(currentState);
       //Invoke("PushMenu", GameObject.FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<ScreenFader>().fadeSpeed);
        Debug.Log("SetStateAndLoad");
    }
    void PushMenu() {
        MenuManager.Instance.PushMenu(currentState);
    }

	public void Destroy()
	{
		SoundManager.Instance.Stop_BGM ();
	}
	
	public void OnDisable() {
		if(SoundManager.Instance != null)
		SoundManager.Instance.Stop_BGM ();
	}

}
