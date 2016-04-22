using UnityEngine;
using System.Collections;

public enum eGameStates { NONE = 0,	MAINMENU = 1, GAMEPLAY = 2,	GAMEOVER = 3, INCENTIVE = 4 }
public enum eAdsNetwork { NONE = 0, ADMOB = 1, CHARTBOOST = 2, HEYZAP = 3 }
public enum eAdsNetworkType { MONETIZATION = 0, CROSSPROMO = 1 }
public enum eAdsType { BANNER = 0, INTERSTITIAL = 1, VIDEO = 2 }
public enum eBannerAdPosition { TOP = 0, BOTTOM = 1 }

public class AdsManager : MonoBehaviour {

	#region Variables & Constants
	public bool showDebugLogs;
	public eBannerAdPosition bannerAdPosition;
	public BaseAdNetwork[] androidBannerAds;
	public BaseAdNetwork[] androidInterstitialAds;
	public BaseAdNetwork[] iOSBannerAds;
	public BaseAdNetwork[] iOSInterstitialAds;
	public BaseAdNetwork[] WP8BannerAds;
	public BaseAdNetwork[] WP8InterstitialAds;

    public static BaseAdNetwork[] bannerAds;
    public static BaseAdNetwork[] interstitialAds;
	public static eBannerAdPosition bannerAdGravity;
    
	public static eGameStates gameState { get; private set; }
	public static eGameStates lastGameState { get; private set; }
	public static eAdsNetwork activeBannerAd { get; private set; }

	private static bool isAdsFreeEnabled = false;
	private static bool isSuperCrossPromoEnabled = false;
	public static bool isdebugEnabled;

	// persistant singleton
	private static AdsManager _instance;

	private static AdsManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<AdsManager>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	#endregion


	#region Lifecycle methods

    void Awake()
    {
		isdebugEnabled = showDebugLogs;

		this.LogDebug ("Awake Called!");

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

	// Use this for initialization
	void Start () 
    {
		this.LogDebug ("Start Called!");

		#if UNITY_ANDROID
		bannerAds = androidBannerAds;
		interstitialAds = androidInterstitialAds;
		#elif UNITY_IOS
		bannerAds = iOSBannerAds;
		interstitialAds = iOSInterstitialAds;
		#elif UNITY_WP8
		bannerAds = WP8BannerAds;
		interstitialAds = WP8InterstitialAds;
		#endif
		
		bannerAdGravity = bannerAdPosition;
		isdebugEnabled = showDebugLogs;

		this.SortAdPriorities ();

		AdsManager.CheckIsAdsFreeEnabled ();

		InitAdNetworks ();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

	#endregion

	#region Utility Methods

	public static string GetAdNetworkName(eAdsNetwork adNetwork) {
		string adNetworkName = "Dummy";
		switch (adNetwork) {
		case eAdsNetwork.ADMOB:
			adNetworkName = "AdMob";
			break;
		case eAdsNetwork.HEYZAP:
			adNetworkName = "HeyZap";
			break;
		case eAdsNetwork.CHARTBOOST:
			adNetworkName = "Chartboost";
			break;
		}

		return adNetworkName;
	}

	public static string GetAdTypeName(eAdsType adType) {
		string adTypeName = "Dummy";
		switch (adType) {
		case eAdsType.BANNER:
			adTypeName = "Banner Ad";
			break;
		case eAdsType.INTERSTITIAL:
			adTypeName = "Interstitial Ad";
			break;
		case eAdsType.VIDEO:
			adTypeName = "Video Ad";
			break;
		}
		
		return adTypeName;
	}

	public static string GetGameStateName(eGameStates gameState) {
		string gameStateName = "Dummy";
		switch (gameState) {
		case eGameStates.MAINMENU:
			gameStateName = "Main Menu";
			break;
		case eGameStates.GAMEPLAY:
			gameStateName = "Game Play";
			break;
		case eGameStates.GAMEOVER:
			gameStateName = "Game Over";
			break;
		case eGameStates.INCENTIVE:
			gameStateName = "Incentive";
			break;
		}
		
		return gameStateName;
	}

	// sorting list on base of ad priorities
	private void SortAdPriorities() {
		this.LogDebug ("Sorting Priorities!");
		BaseAdNetwork temp;
		
		// sorting banner priorities
		if (AdsManager.bannerAds.Length > 1) {
			for (int i = 0; i < AdsManager.bannerAds.Length; i++) {
				for (int j = 0; j < AdsManager.bannerAds.Length - 1; j++) {
					if (AdsManager.bannerAds [j].configuration.adPriority > AdsManager.bannerAds [j + 1].configuration.adPriority) {
						temp = AdsManager.bannerAds [j + 1];
						AdsManager.bannerAds [j + 1] = AdsManager.bannerAds [j];
						AdsManager.bannerAds [j] = temp;
					}
				}
			}
		}
		
		// sorting interstitial priorities
		if (AdsManager.interstitialAds.Length > 1) {
			for (int i = 0; i < AdsManager.interstitialAds.Length; i++) {
				for (int j = 0; j < AdsManager.interstitialAds.Length - 1; j++) {
					if (AdsManager.interstitialAds [j].configuration.adPriority > AdsManager.interstitialAds [j + 1].configuration.adPriority) {
						temp = AdsManager.interstitialAds [j + 1];
						AdsManager.interstitialAds [j + 1] = AdsManager.interstitialAds [j];
						AdsManager.interstitialAds [j] = temp;
					}
				}
			}
		}
	}

	public static void CheckIsAdsFreeEnabled() {
		if (PlayerPrefs.GetInt ("AdsManager_IsAdsFree", 0) == 1) {
			AdsManager.IsAdsFree = true;
			LogDebug ("IsAdsFree Enabled!");
		} else {
			AdsManager.IsAdsFree = false;
		}
	}

	public static void LogDebug(string message) {
		if (isdebugEnabled)
			Debug.Log ("AdsManagerX >> " + message);
	}

	private static void LogErrorDebug(string message) {
		if (isdebugEnabled)
			Debug.LogError ("AdsManagerX >> " + message);
	}

	public static bool IsAdsFree {
		get { 
			return isAdsFreeEnabled;
		} 
		set {
			isAdsFreeEnabled = value;
		}
	}

	public static bool IsSuperCrossPromo {
		get { 
			return isSuperCrossPromoEnabled;
		} 
		set {
			isSuperCrossPromoEnabled = value;
		}
	}

	private static void LogMessage(string message) {
		//MainPanel.Instance.ShowMessage (message);
	}

	#endregion

	#region Callback Methods

	public static void ShowGameStateAd(eGameStates gameState)
    {
		LogDebug ("ShowGameStateAd called for State = " + gameState);
		lastGameState = AdsManager.gameState;
		
		AdsManager.gameState = gameState;

		// TODO: implement super crosspromo check

        //show latest ad
        ShowAds();
    }

	public static void SetAdsFreeModeEnabled() {
		if(!AdsManager.IsAdsFree) {
			AdsManager.IsAdsFree = true;
			AdsManager.HideBannerAd ();

			PlayerPrefs.SetInt ("AdsManager_IsAdsFree" , 1);
			PlayerPrefs.Save ();

			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, "Ads Free Mode", "Enabled");

			LogDebug ("IsAdsFree Enabled!");
			LogMessage ("IsAdsFree Enabled!");
		}
	}

	public static void SetInterstitialAdReady(eGameStates gameState, eAdsNetwork adNetwork) {
		for (int i = 0; i < AdsManager.interstitialAds.Length; i++)
		{
			if ((AdsManager.interstitialAds[i].configuration.showThisAdAt == gameState) && (AdsManager.interstitialAds[i].GetAdNetworkInfo() == adNetwork)) {
				LogDebug ("InterstitialAdReady of " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType) + " for State = " + GetGameStateName(AdsManager.interstitialAds[i].configuration.showThisAdAt));
				LogMessage ("InterstitialAdReady of " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType) + " for State = " + GetGameStateName(AdsManager.interstitialAds[i].configuration.showThisAdAt));
				AdsManager.interstitialAds[i].configuration.AdReady = true;
				break;
			} 
		}
	}

	public static void SetBannerAdFailed(eGameStates gameState, eAdsNetwork adNetwork) {
		for (int i = 0; i < AdsManager.bannerAds.Length; i++)
		{
			if ((AdsManager.bannerAds[i].configuration.showThisAdAt == gameState) && (AdsManager.bannerAds[i].GetAdNetworkInfo() == adNetwork)) {
				LogDebug ("BannerAdFailed of " + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType) + " for State = " + GetGameStateName(gameState));
				LogMessage ("BannerAdFailed of " + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType) + " for State = " + GetGameStateName(gameState));
				AdsManager.bannerAds[i].configuration.AdFailedToLoad = true;

				if((i+1) < AdsManager.bannerAds.Length) {
					i += 1;
					AdsManager.bannerAds[i].InitNetwork();
					activeBannerAd = AdsManager.bannerAds[i].GetAdNetworkInfo();

					// log analytics
					if(AdsManager.bannerAds[i].configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
						UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_REQUESTED, GetGameStateName(AdsManager.bannerAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType));
					} else {
						UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_REQUESTED, GetGameStateName(AdsManager.bannerAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType));
					}
				}

				break;
			}
		}
	}

    private static void ShowAds()
    {
		ShowInterstitialAd ();
    }

    private void InitAdNetworks()
    {
		// ads free check
		if (AdsManager.IsAdsFree)
			return;

		LogDebug ("Initializing Ads!");

		int i = 0;
        if (i < AdsManager.bannerAds.Length)
        {
			AdsManager.bannerAds[i].InitNetwork();
			activeBannerAd = AdsManager.bannerAds[i].GetAdNetworkInfo();

			// log analytics
			if(AdsManager.bannerAds[i].configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
				UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_REQUESTED, GetGameStateName(AdsManager.bannerAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType));
			} else {
				UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_REQUESTED, GetGameStateName(AdsManager.bannerAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType));
			}
        }

		for (i = 0; i < AdsManager.interstitialAds.Length; i++)
        {
			LogDebug ("Initializing Interstitial Ads of " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()));
			AdsManager.interstitialAds[i].InitNetwork();

			// log analytics
			if(AdsManager.interstitialAds[i].configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
				UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_REQUESTED, GetGameStateName(AdsManager.interstitialAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType));
			} else {
				UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_REQUESTED, GetGameStateName(AdsManager.interstitialAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType));
			}
        }
    }

    private static void ShowInterstitialAd()
    {
		// ads free check
		if (AdsManager.IsAdsFree)
			return;

		LogDebug ("ShowInterstitialAd called for State = " + AdsManager.gameState);
		int i = 0;

		for (i = 0; i < AdsManager.interstitialAds.Length; i ++)
        {
			if (AdsManager.interstitialAds[i].configuration.showThisAdAt == AdsManager.gameState)
            {
				if(AdsManager.interstitialAds[i].configuration.AdReady){
					AdsManager.interstitialAds[i].ShowAd();
					LogDebug ("ShowInterstitialAd " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType) + " showing for State = " + GetGameStateName(AdsManager.gameState));
					AdsManager.interstitialAds[i].configuration.AdReady = false;

					// log analytics
					if(AdsManager.interstitialAds[i].configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
						UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_SHOWCALL, GetGameStateName(AdsManager.interstitialAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType));
					} else {
						UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_SHOWCALL, GetGameStateName(AdsManager.interstitialAds[i].configuration.showThisAdAt) + " " + GetAdNetworkName(AdsManager.interstitialAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.interstitialAds[i].configuration.adType));
					}
					break;
				} else {
					continue;
				}
            }            
        }

		if (i == AdsManager.interstitialAds.Length)
			LogDebug ("ShowInterstitialAd Ad not ready for State = " + AdsManager.gameState);
    }

    public static void ShowBannerAd()
    {
		// ads free check
		if (AdsManager.IsAdsFree)
			return;

		LogDebug ("ShowBannerAd called for Ad Network = " + GetAdNetworkName(activeBannerAd));

		// hide previous active banner ad
		HideBannerAd ();

		for (int i = 0; i < AdsManager.bannerAds.Length; i++)
        {
			if (AdsManager.bannerAds[i].GetAdNetworkInfo() == activeBannerAd)
            {
				AdsManager.bannerAds[i].ShowAd();
				LogDebug ("ShowBannerAd Showing" + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType));
				break;
            }
        }
    }

    public static void HideBannerAd()
    {
		LogDebug ("HideBannerAd called for Ad Network = " + GetAdNetworkName(activeBannerAd));

		for (int i = 0; i < AdsManager.bannerAds.Length; i++)
        {
			if (AdsManager.bannerAds[i].GetAdNetworkInfo() == activeBannerAd)
            {
				AdsManager.bannerAds[i].HideAd();
				LogDebug ("HideBannerAd hidding" + GetAdNetworkName(AdsManager.bannerAds[i].GetAdNetworkInfo()) + " " + GetAdTypeName(AdsManager.bannerAds[i].configuration.adType));
				break;
            }
        }
    }

	#endregion
}
