using UnityEngine;
using System.Collections;
using Unibill;

public class InAppPurchaseX : MonoBehaviour {

	#region Variables, Constants & Initializers

	public bool isTesting;

	// Use these for ids
	private const string SKU_PURCHASE_100_COINS = "com.jiggygames.fungogolf.100coins";
//	private const string SKU_PURCHASE_100_COINS = "android.test.purchased";
	private const string SKU_PURCHASE_1000_COINS = "com.jiggygames.fungogolf.1000coins";
	private const string SKU_PURCHASE_10000_COINS = "com.jiggygames.fungogolf.10000coins";
	private const string SKU_PURCHASE_50000_COINS = "com.jiggygames.fungogolf.50000coins";

	private const string SKU_PURCHASE_SWORD = "com.divineninja.inappx.sword";
	
	// persistant singleton
	private static InAppPurchaseX _instance;

	#endregion
	
	#region Lifecycle methods

	public static InAppPurchaseX instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<InAppPurchaseX>();

				//Tell unity not to destroy this object when loading a new scene!
				//DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() 
	{
		Debug.Log("Awake Called");

		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void Start ()
	{
		Debug.Log("Start Called");

		if (!Unibiller.Initialised) {
			Unibiller.Initialise ();
		}

		// for testing
		//Unibiller.clearTransactions ();
	}
	
	void OnEnable()
	{
		Debug.Log("OnEnable Called");

		// set callbacks
		if (!Unibiller.Initialised) {
			Unibiller.onBillerReady += OnBillerReady;
			Unibiller.onTransactionsRestored += OnTransactionsRestored;
			Unibiller.onPurchaseComplete += OnPurchaseComplete;
			Unibiller.onPurchaseFailed += OnPurchaseFailed;
			Unibiller.onPurchaseRefunded += OnPurchaseRefunded;
		}
	}
	
	void OnDisable()
	{
		Debug.Log("OnDisable Called");
	}

	#endregion
	
	#region UniBill Callback Methods

	/***************** UniBill Callbacks START *******************/

	private void OnBillerReady(UnibillState state) {
		switch(state) {
			case UnibillState.SUCCESS:
				Debug.Log("Unibill Init SUCCESS");
#if UNITY_ANDROID
				Unibiller.restoreTransactions();
#endif
				break;
			case UnibillState.SUCCESS_WITH_ERRORS:
				Debug.Log("Unibill Init SUCCESS with ERRORS");
#if UNITY_ANDROID
				Unibiller.restoreTransactions();
#endif
				break;
			case UnibillState.CRITICAL_ERROR:
				Debug.Log("Unibill Init ERROR");
				break;
			default:
				break;
		}
	}
	
	private void OnTransactionsRestored(bool isRestored) {
		switch(isRestored){
			case true:
				Debug.Log("Unibill OnTransactionsRestored SUCCESS");
				break;
			case false:
				Debug.Log("Unibill OnTransactionsRestored FAILED");
				break;
			default:
				break;
		}
	}
	
	private void OnPurchaseComplete(PurchasableItem purchase){
		Debug.Log("Unibill OnPurchaseComplete : " + purchase.Id);
		Debug.Log ("GameManager.Instance.gamestats.Coins " + GameManager.Instance.gamestats.Coins);
		switch(purchase.Id){

		case SKU_PURCHASE_100_COINS:
			//MainPanel.Instance.ShowMessage("100 Coins Purchase complete called");
			GameManager.Instance.gamestats.Coins += 100;
			break;
		case SKU_PURCHASE_1000_COINS:
			//MainPanel.Instance.ShowMessage("100 Coins Purchase complete called");
			GameManager.Instance.gamestats.Coins += 1000;
			break;
		case SKU_PURCHASE_10000_COINS:
			//MainPanel.Instance.ShowMessage("100 Coins Purchase complete called");
			GameManager.Instance.gamestats.Coins += 10000;
			break;
		case SKU_PURCHASE_50000_COINS:
			//MainPanel.Instance.ShowMessage("100 Coins Purchase complete called");
			GameManager.Instance.gamestats.Coins += 50000;
			break;
		case SKU_PURCHASE_SWORD:
			//MainPanel.Instance.ShowMessage("Sword Purchase complete called");
			break;
		default:

			break;
		}

		GameObject characterSelection = GameObject.Find("CharacterSelection(Clone)");
		characterSelection.GetComponent<CharacterSelection> ().UpdateScore ();

		PlayerPrefs.Save();
		Debug.Log ("GameManager.Instance.gamestats.Coins " + GameManager.Instance.gamestats.Coins);
	}
		
	private void OnPurchaseFailed(PurchaseFailedEvent e) {
		Debug.Log("Unibill OnPurchaseFailed : " + e.PurchasedItem.Id + " as " + e.Reason);
	}
	
	private void OnPurchaseRefunded(PurchasableItem purchase) {
		Debug.Log("Unibill OnPurchaseRefunded : " + purchase.Id);
	}

	#endregion

	#region Utility Methods 

	private void PurchaseItemForId(string SKU_PURCHASE_ID) {
		if (_instance != null) {
			if(isTesting) {
				this.OnPurchaseComplete(Unibiller.GetPurchasableItemById (SKU_PURCHASE_ID));
			} else {
				Unibiller.initiatePurchase (Unibiller.GetPurchasableItemById (SKU_PURCHASE_ID));
			}
		}
	}

	#endregion

	#region Callback Methods 

	public void Purchase100coins(){
		this.PurchaseItemForId (SKU_PURCHASE_100_COINS);
	}

	public void Purchase1000coins(){
		this.PurchaseItemForId (SKU_PURCHASE_1000_COINS);
	}

	public void Purchase10000coins(){
		this.PurchaseItemForId (SKU_PURCHASE_10000_COINS);
	}

	public void Purchase50000coins(){
		this.PurchaseItemForId (SKU_PURCHASE_50000_COINS);
	}
	
	public void PurchaseSword(){
		this.PurchaseItemForId (SKU_PURCHASE_SWORD);
	}

	public void RestorePurchases() {
		Unibiller.restoreTransactions();
	}

	#endregion
}
