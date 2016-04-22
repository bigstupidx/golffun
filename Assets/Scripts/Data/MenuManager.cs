using UnityEngine;
using System.Collections;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

using GooglePlayGames.OurUtils;

using UnityEngine.UI;
public class MenuManager : Singleton<MenuManager>
{
    public GameObject cam;
	public GameObject gama;

	bool mAuthOnStart = true;
	bool mInMatch = false;
    #region Variables

    public enum Mode {Prefabs, Objects};

//	public Text _log;
//	public UILabel _log;
	public BaseMenu[] menus;
	
	public Mode mode = Mode.Prefabs;
	
	// TODO: change the hashtable to dictionary
	private Hashtable menuTable = null;
	
	private Stack navigationStack = null;
	
	private Hashtable createdMenus = null;
	
	public bool isBackBtnPressed = false;

    #endregion Variables

    #region MonoBehaviour: LifeCycle

    /* 
	 * <summary>
	 * 
	 * </summary>
	 */
	void Awake() 
	{	//	Invoke ("StartLeaderBoard", 0f);
		StartLeaderBoard ();

		GameManager.Instance.GameStateChanged += HandleGameStateChanged;
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//_log.text = "";

//		PlayGamesPlatform.Instance.TurnBased.RegisterMatchDelegate(OnGotMatch);
//		PlayGamesPlatform.Instance.RegisterInvitationDelegate(OnGotInvitation);


		// 1. Initialize and populate the menu hash table
		PopulateMenuHashTable();
		
		// 2. Initialize the navigation stack
		InitializeNavigationStack ();
		
		// 3. Hide all open menus (this is needed in case the developer left some menu open)
		HideAllMenus ();
		
		// 4. Display the initial menu or display the menu at the top of the stack (returning from some other unity scene)
		if (navigationStack.Count == 0) 
		{
			PushMenu (GameManager.Instance.initialState);
		}
		else 
		{
			ShowMenu(GetMenuForState(NavigationStackPeek()));
		}







	//	Social.ShowLeaderboardUI();
	//	Social.ShowLeaderboardUI();

	//	gama.SetActive (true);

	}



	protected void OnGotInvitation(Invitation invitation, bool shouldAutoAccept) {
		if (invitation.InvitationType != Invitation.InvType.TurnBased) {
			// wrong type of invitation!
			return;
		}
		mInMatch = true;
//		gameObject.GetComponent<MainMenuGui>().HandleInvitation(invitation, shouldAutoAccept);
	}
	
	protected void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch) {
		mInMatch = true;
//		gameObject.GetComponent<MainMenuGui>().HandleMatchTurn(match, shouldAutoLaunch);
	}

	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	void Update () 
	{
		#if UNITY_ANDROID
		if (Input.GetKeyDown(KeyCode.Escape) && !isBackBtnPressed && !GameManager.Instance.isPaused) 
		{ 
			isBackBtnPressed=true;
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Back Button Clicked");

		}
		
		#endif
//		if (_log != null) {
//			_log.text = Logger.Logs;
//		}
	}
	
	public void OnDestroy () 
	{
	//	gama.SetActive (true);
		//GameManager.Instance.navigationStack = navigationStack;
		if (GameManager.Instance != null)
		{
			if (GameManager.Instance.navigationManager.navigationStacks.ContainsKey(Application.loadedLevelName))
			{
				GameManager.Instance.navigationManager.navigationStacks.Remove(Application.loadedLevelName);
			}
			GameManager.Instance.navigationManager.navigationStacks.Add(Application.loadedLevelName, navigationStack);
		}
	}
	
	#endregion MonoBehaviour: LifeCycle
	
	#region GameManager: GameStateChange Delegate
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	void HandleGameStateChanged (GameManager.GameState g)
	{
		// If we want to do anything on game change
	}
	
	#endregion GameManager: GameStateChange Delegate
	
	#region Menu Navigation Control Logic
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	public void PushMenu(GameManager.GameState g)
	{	
		
		// 1. If the incoming menu is a pop-up dont hide the last menu
		if (GetMenuForState(g).isPopup == false) {
			// 1.1. Hide the menu at the top of the stack
			if (navigationStack.Count != 0)
			{
				HideMenuAtState (NavigationStackPeek ());
			}
		}
		
		// 2. Push the next menu
		navigationStack.Push (g);
		
		// 3. Inform the game manager about the game state change
		InformGameManager (g);
		
		// 4. Show the new menu at the top of the stack
		ShowMenuAtState (g);
		//Debug.Log (g);
		GameManager.Instance.previousState = GameManager.Instance.currentState;
		GameManager.Instance.currentState = g;
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	public void PopMenu()
	{
		// 1. Hide the menu at the top of the stack
		if (navigationStack.Count != 0)
		{
			HideMenuAtState (NavigationStackPeek ());
		}
		
		// 2. Pop the menu from the top of the stack
		navigationStack.Pop ();
		
		// 3. Get the menu at the top of the stack
		GameManager.GameState g = NavigationStackPeek ();
		
		// 4. Inform the game manager about the game state
		InformGameManager (g);
		
		// 5. Show the menu at the top of the stack 
		ShowMenuAtState (g);
		GameManager.Instance.previousState = GameManager.Instance.currentState;
		GameManager.Instance.currentState = g;
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	public void PopMenuToState(GameManager.GameState g)
	{
		// 1. Hide the menu at the top of the stack
		if (navigationStack.Count != 0)
		{
			HideMenuAtState (NavigationStackPeek ());
		}
		
		// 2. Keep popping till the desired menu is reached
		while (NavigationStackPeek() != g)
		{
			navigationStack.Pop ();
			            
			HideMenuAtState (NavigationStackPeek ());
		}
		
		// 3. Inform the game manager about the game state
		InformGameManager (g);
		
		// 4. Show the menu at the top of the stack 
		ShowMenuAtState (g);
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void InformGameManager(GameManager.GameState g)
	{
		GameManager.Instance.ChangeGameStateTo (g);
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void ShowMenuAtState (GameManager.GameState g)
	{
		switch (g) 
		{
		default:
			ShowMenu(GetMenuForState(g));	
			break;
			
		}
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void HideMenuAtState (GameManager.GameState g)
	{
		switch (g) 
		{
		default:
			HideMenu(GetMenuForState(g));		
			break;
			
		}
	}
	
	/* 
	 * <summary>
	 * This method disables all the menues assossiated with menu manager.
	 * </summary>
	 */
	private void HideAllMenus() 
	{
		foreach (DictionaryEntry de in menuTable) 
		{
			BaseMenu bm = de.Value as BaseMenu;
			//bm.gameObject.SetActive(false);
			
			HideMenu(bm);
		}
	}
	
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void ShowMenu(BaseMenu bm) 
	{
		if (mode == Mode.Prefabs)
		{
			BaseMenu tempBaseMenu = createdMenus [bm.state] as BaseMenu;
			if (tempBaseMenu != null)
			{
				tempBaseMenu.MenuWillAppear ();
				tempBaseMenu.gameObject.SetActive (true);
				tempBaseMenu.MenuDidAppear ();
				return; 
			}
			
			
			BaseMenu newBM = BaseMenu.Instantiate (bm) as BaseMenu;
			
			cam = GameObject.FindGameObjectWithTag ("Camera");
			
			newBM.transform.parent = cam.transform;
			
			newBM.transform.localScale = Vector3.one;
			
			newBM.MenuWillAppear ();
			newBM.gameObject.SetActive (true);
			newBM.MenuDidAppear ();
			
			
			createdMenus.Add (bm.state, newBM);
		}
		else if (mode == Mode.Objects)
		{
			bm.MenuWillAppear ();
			bm.gameObject.SetActive (true);
			bm.MenuDidAppear ();
		}
		
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void HideMenu(BaseMenu bm) 
	{
		if (mode == Mode.Prefabs)
		{
			
			BaseMenu previousBM = createdMenus [bm.state] as BaseMenu;
			
			if (previousBM != null) 
			{
				previousBM.MenuWillDisappear ();
				previousBM.gameObject.SetActive (false);
				previousBM.MenuDidDisappear ();
				
				createdMenus.Remove(bm.state);
				
				Destroy(previousBM.gameObject);
			}
		}
		else if (mode == Mode.Objects)
		{
			bm.MenuWillDisappear ();
			bm.gameObject.SetActive (false);
			bm.MenuDidDisappear ();
		}
		
		//Destroy(bm);
		
		
	}
	
	#endregion Menu Navigation Control Logic
	
	#region Initialization 
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void PopulateMenuHashTable() 
	{
		menuTable = new Hashtable ();
		
		createdMenus = new Hashtable ();
		
		for (int i = 0; i < menus.Length; i++)
		{
			
			BaseMenu bm = menus[i];
			Debug.Log("Creating Menu " + bm.state);
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_SCENE, Constants.GA_ACTION_TYPE_SCENESTART, "Main Menu Started");

			menuTable.Add(bm.state, bm);
			bm.gameObject.SetActive(true);
			bm.gameObject.SetActive(false);
		}
		
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private void InitializeNavigationStack()
	{
		if (GameManager.Instance.navigationManager.navigationStacks.ContainsKey(Application.loadedLevelName) == false)
		{
			navigationStack = new Stack ();
		}
		else 
		{
			navigationStack = GameManager.Instance.navigationManager.navigationStacks[Application.loadedLevelName];
			
			GameManager.Instance.navigationManager.navigationStacks.Remove (Application.loadedLevelName);
		}
		
	}
	
	
	#endregion Initialization	
	
	#region Utility Methods
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */
	private BaseMenu GetMenuForState(GameManager.GameState g)
	{
		return menuTable[g] as BaseMenu;
	}
	
	/* 
	 * <summary>
	 * 
	 * </summary>
	 */


	public void StartLeaderBoard(){
	//	_log = GameObject.Find ("DebugLog").GetComponent<UILabel>();

		PlayGamesClientConfiguration config = 
			new PlayGamesClientConfiguration.Builder()
				.WithInvitationDelegate(OnGotInvitation)
				.WithMatchDelegate(OnGotMatch)
				.Build();
		
		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		//	 authenticate user:
//		Social.localUser.Authenticate((bool success) => {
//			if (success)
//			{
//				_log.text="You've successfully logged in";
//			}
//			else
//			{
//				_log.text ="Login failed for some reason";
//			}
//		});


		Social.localUser.Authenticate((bool success) => {
			if (success)
			{
				//_log.text="You've successfully logged in";
				Logger.d("You've successfully logged in");
			}
			else
			{
				//_log.text ="Login failed for some reason";
				Logger.d("Login failed for some reason");
			}
		});


//
//		PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
//		                                                  {
//			if (success)
//			{
//				_log.text="You've successfully logged in";
//			}
//			else
//			{
//				_log.text ="Login failed for some reason";
//			}
//		});
	
	
	
	}

	public void LeaderSuccess(){

	//	_log.text = "LeaderBoard_Success";
	}
	public GameManager.GameState NavigationStackPeek ()
	{
		return (GameManager.GameState)navigationStack.Peek ();
	}
	
	public void PurgeNavigationStack() 
	{
		if (GameManager.Instance.navigationManager.navigationStacks.ContainsKey(Application.loadedLevelName) == false)
		{
			navigationStack.Clear();
		}
		else 
		{
			navigationStack.Clear();
			
			GameManager.Instance.navigationManager.navigationStacks.Remove (Application.loadedLevelName);
		}
	}
	#endregion Utility Methods
	
}
