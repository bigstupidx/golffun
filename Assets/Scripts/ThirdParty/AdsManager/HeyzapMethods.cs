using UnityEngine;
using System.Collections;

public class HeyzapMethods : BaseAdNetwork {

	#region Variables & Constants

	// Your Publisher ID from HeyZap is: b76117f37951f086b58209f94e6ddfc6
	private const string publisherID = "b76117f37951f086b58209f94e6ddfc6";
	private const string AD_TAG = "Heyzap >> ";
	private const string AD_TAG_INC = "HeyzapIncentive >> ";

	#endregion

	#region Lifecycle methods

	// Use this for initialization
	void Start () {
        
	}

	// Update is called once per frame
	void Update () {
	
	}

	#endregion
	
	#region Overriden Callback Methods

	public override void InitNetwork() {
		HeyzapAds.start(publisherID, HeyzapAds.FLAG_DISABLE_AUTOMATIC_FETCHING);

		HZInterstitialAd.AdDisplayListener listener = delegate(string adState, string adTag){
			if ( adState.Equals("show") ) {
				AdsManager.LogDebug(AD_TAG + "show");
				// Do something when the ad shows, like pause your game

				// log analytics
				if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				} else {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				}
			}
			if ( adState.Equals("hide") ) {
				AdsManager.LogDebug(AD_TAG + "hide");
				HZInterstitialAd.fetch();
				// Do something after the ad hides itself
			}
			if ( adState.Equals("click") ) {
				AdsManager.LogDebug(AD_TAG + "click");
				// Do something when an ad is clicked on

				// log analytics
				if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				} else {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				}
			}
			if ( adState.Equals("failed") ) {
				AdsManager.LogDebug(AD_TAG + "failed");
				// Do something when an ad fails to show
			}
			if ( adState.Equals("available") ) {
				AdsManager.LogDebug(AD_TAG + "available");
				// Do something when an ad has successfully been fetched
				AdsManager.SetInterstitialAdReady (configuration.showThisAdAt, this.GetAdNetworkInfo());
			}
			if ( adState.Equals("fetched_failed") ) {
				AdsManager.LogDebug(AD_TAG + "fetched_failed");
				// Do something when an ad did not fetch

				// log analytics
				if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				} else {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				}
			}
			if ( adState.Equals("audio_starting") ) {
				AdsManager.LogDebug(AD_TAG + "audio_starting");
				// The ad being shown will use audio. Mute any background music
			}
			if ( adState.Equals("audio_finished") ) {
				AdsManager.LogDebug(AD_TAG + "audio_finished");
				// The ad being shown has finished using audio.
				// You can resume any background music.
			}
		};
		
		HZInterstitialAd.setDisplayListener(listener);
		
		HZIncentivizedAd.AdDisplayListener IncentivizedListener = delegate(string adState, string adTag){
			if (adState.Equals ("show")) {
				// Do something when the ad shows, like pause your game
				AdsManager.LogDebug(AD_TAG_INC + "show");

				// log analytics
				if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				} else {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				}
			}
			if (adState.Equals ("hide")) {
				// Do something after the ad hides itself
				AdsManager.LogDebug(AD_TAG_INC + "hide");
				HZIncentivizedAd.fetch();
			}
			if (adState.Equals ("click")) {
				// Do something when an ad is clicked on
				AdsManager.LogDebug(AD_TAG_INC + "click");

				// log analytics
				if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				} else {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				}
			}
			if (adState.Equals ("failed")) {
				// Do something when an ad fails to show
				AdsManager.LogDebug(AD_TAG_INC + "failed");
			}
			if (adState.Equals ("available")) {
				// Do something when an ad has successfully been fetched
				AdsManager.LogDebug(AD_TAG_INC + "available");
				AdsManager.SetInterstitialAdReady (configuration.showThisAdAt, this.GetAdNetworkInfo());
			}
			if (adState.Equals ("fetched_failed")) {
				// Do something when an ad did not fetch
				AdsManager.LogDebug(AD_TAG_INC + "fetched_failed");

				// log analytics
				if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				} else {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
				}
			}
			if (adState.Equals ("incentivized_result_complete")) {
				// The user has watched the entire video and should be given a reward.
				AdsManager.LogDebug(AD_TAG_INC + "incentivized_result_complete");
				RewardUser();
			}
			if (adState.Equals ("incentivized_result_incomplete")) {
				// The user did not watch the entire video and should not be given a reward.
				AdsManager.LogDebug(AD_TAG_INC + "incentivized_result_incomplete");
			}
		};
		
		HZIncentivizedAd.setDisplayListener(IncentivizedListener);

		switch (configuration.adType)
		{
		case eAdsType.BANNER:
			AdsManager.LogDebug(AD_TAG + "InitNetwork Banner");
			break;                
		case eAdsType.INTERSTITIAL:
			AdsManager.LogDebug(AD_TAG + "InitNetwork Interstitial");
			HZInterstitialAd.fetch();
			break;                
		case eAdsType.VIDEO:
			AdsManager.LogDebug(AD_TAG + "InitNetwork Video");
			HZIncentivizedAd.fetch();
			break;
		default:
			break;
		}
	}

	public override void ShowAd()
	{
		switch (configuration.adType)
		{
		case eAdsType.BANNER:
			AdsManager.LogDebug(AD_TAG + "ShowAd Banner");
			break;
		case eAdsType.INTERSTITIAL:
			AdsManager.LogDebug(AD_TAG + "ShowAd Interstitial");
			HZInterstitialAd.show();
			break;
		case eAdsType.VIDEO:
			AdsManager.LogDebug(AD_TAG + "ShowAd Video");
			if (HZIncentivizedAd.isAvailable()) {
				HZIncentivizedAd.show();
			}
			break;
		default:
			break;
		}
	}

	public override void HideAd()
	{
		switch (configuration.adType)
		{
		case eAdsType.BANNER:
			break;                
		case eAdsType.INTERSTITIAL:
			break;                
		case eAdsType.VIDEO:
			break;
		default:
			break;
		}
	} 

	public override eAdsNetwork GetAdNetworkInfo() { 
		return eAdsNetwork.HEYZAP;
	}    

	#endregion
	
	#region Utility Methods
	
	public void RewardUser() {
		//UniversalAnalytics.LogEvent(Constants.GA_CATEGORY_TYPE_MONETIZATION, "Incentivized", "500 coins");
		//		int count = PlayerPrefs.GetInt ("VAIndex");
		//		GameManager.Instance.player.UpdatePlayerScore(500);
		//		count--;
		//		PlayerPrefs.SetInt("VAIndex",count);
		//		MakeupScene.isScoreUpdate=true;
		//		DressupScene.isScoreUpdate = true;
		//		LevelSelection.isScoreUpdate = true;
		//		SelectionScene.isScoreUpdate =  true;
	}

	#endregion
}
