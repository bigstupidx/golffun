using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class ChartboostMethods : BaseAdNetwork {

	#region Variables & Constants

	private const string AD_TAG = "Chartboost >> ";
	private const string AD_CROSSPROMO_PLACEMEMT = "cb_end_loop";
	private const string AD_MONETIZATION_PLACEMEMT = "cb_rev_loop";

	#endregion

	#region Lifecycle methods

    // Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

	void OnEnable() {
		// Listen to all impression-related events
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += didDismissInterstitial;
		Chartboost.didCloseInterstitial += didCloseInterstitial;
		Chartboost.didClickInterstitial += didClickInterstitial;
		Chartboost.didCacheInterstitial += didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial += shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial += didDisplayInterstitial;
		Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps += didDismissMoreApps;
		Chartboost.didCloseMoreApps += didCloseMoreApps;
		Chartboost.didClickMoreApps += didClickMoreApps;
		Chartboost.didCacheMoreApps += didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps += shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps += didDisplayMoreApps;
		//Chartboost.didFailToRecordClick += didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo += didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;
		Chartboost.didCacheInPlay += didCacheInPlay;
		Chartboost.didFailToLoadInPlay += didFailToLoadInPlay;
		Chartboost.didPauseClickForConfirmation += didPauseClickForConfirmation;
		Chartboost.willDisplayVideo += willDisplayVideo;
		#if UNITY_IPHONE
		Chartboost.didCompleteAppStoreSheetFlow += didCompleteAppStoreSheetFlow;
		#endif
	}
	
	void OnDisable() {
		// Remove event handlers
		Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= didDismissInterstitial;
		Chartboost.didCloseInterstitial -= didCloseInterstitial;
		Chartboost.didClickInterstitial -= didClickInterstitial;
		Chartboost.didCacheInterstitial -= didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial -= shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial -= didDisplayInterstitial;
		Chartboost.didFailToLoadMoreApps -= didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps -= didDismissMoreApps;
		Chartboost.didCloseMoreApps -= didCloseMoreApps;
		Chartboost.didClickMoreApps -= didClickMoreApps;
		Chartboost.didCacheMoreApps -= didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps -= shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps -= didDisplayMoreApps;
		//Chartboost.didFailToRecordClick -= didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;
		Chartboost.didCacheInPlay -= didCacheInPlay;
		Chartboost.didFailToLoadInPlay -= didFailToLoadInPlay;
		Chartboost.didPauseClickForConfirmation -= didPauseClickForConfirmation;
		Chartboost.willDisplayVideo -= willDisplayVideo;
		#if UNITY_IPHONE
		Chartboost.didCompleteAppStoreSheetFlow -= didCompleteAppStoreSheetFlow;
		#endif
	}

	#endregion
	
	#region Overriden Callback Methods

    public override void InitNetwork()
    {   
		switch (configuration.adType)
		{
		case eAdsType.BANNER:
			AdsManager.LogDebug(AD_TAG + "InitNetwork Banner");
			break;                
		case eAdsType.INTERSTITIAL:
			AdsManager.LogDebug(AD_TAG + "InitNetwork Interstitial");
			if (configuration.adNetworkType == eAdsNetworkType.MONETIZATION) {
				Chartboost.cacheInterstitial(CBLocation.locationFromName(AD_MONETIZATION_PLACEMEMT));
			} else {
				Chartboost.cacheInterstitial(CBLocation.locationFromName(AD_CROSSPROMO_PLACEMEMT));
			}
			#if UNITY_ANDROID
			Chartboost.cacheMoreApps (CBLocation.Default);
			#endif
			break;                
		case eAdsType.VIDEO:
			AdsManager.LogDebug(AD_TAG + "InitNetwork Video");
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
			if (configuration.adNetworkType == eAdsNetworkType.MONETIZATION) {
				Chartboost.showInterstitial(CBLocation.locationFromName(AD_MONETIZATION_PLACEMEMT));
			} else {
				Chartboost.showInterstitial(CBLocation.locationFromName(AD_CROSSPROMO_PLACEMEMT));
			}
			break;                
		case eAdsType.VIDEO:
			AdsManager.LogDebug(AD_TAG + "ShowAd Video");
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
			AdsManager.LogDebug(AD_TAG + "HideAd Banner");
			break;                
		case eAdsType.INTERSTITIAL:
			AdsManager.LogDebug(AD_TAG + "HideAd Interstitial");
			break;                
		case eAdsType.VIDEO:
			AdsManager.LogDebug(AD_TAG + "HideAd Video");
			break;
		default:
			break;
		}
	} 

	public override eAdsNetwork GetAdNetworkInfo() { 
		return eAdsNetwork.CHARTBOOST;
	}

	#endregion
	
	#region callback handlers

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didFailToLoadInterstitial: {0} at location {1}", error, location));

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}
	
	void didDismissInterstitial(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didDismissInterstitial: " + location);
	}
	
	void didCloseInterstitial(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didCloseInterstitial: " + location);
		if (configuration.adNetworkType == eAdsNetworkType.MONETIZATION) {
			Chartboost.cacheInterstitial (CBLocation.locationFromName (AD_MONETIZATION_PLACEMEMT));
		} else {
			Chartboost.cacheInterstitial (CBLocation.locationFromName (AD_CROSSPROMO_PLACEMEMT));
		}
	}
	
	void didClickInterstitial(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didClickInterstitial: " + location);

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}
	
	void didCacheInterstitial(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didCacheInterstitial: " + location);
		AdsManager.SetInterstitialAdReady (configuration.showThisAdAt, this.GetAdNetworkInfo());
	}
	
	bool shouldDisplayInterstitial(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "shouldDisplayInterstitial: " + location);
		return true;
	}
	
	void didDisplayInterstitial(CBLocation location){
		AdsManager.LogDebug(AD_TAG + "didDisplayInterstitial: " + location);

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}
	
	void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didFailToLoadMoreApps: {0} at location: {1}", error, location));
	}
	
	void didDismissMoreApps(CBLocation location) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didDismissMoreApps at location: {0}", location));
	}
	
	void didCloseMoreApps(CBLocation location) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didCloseMoreApps at location: {0}", location));
	}
	
	void didClickMoreApps(CBLocation location) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didClickMoreApps at location: {0}", location));
	}
	
	void didCacheMoreApps(CBLocation location) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didCacheMoreApps at location: {0}", location));
	}
	
	bool shouldDisplayMoreApps(CBLocation location) {
		AdsManager.LogDebug(string.Format(AD_TAG + "shouldDisplayMoreApps at location: {0}", location));
		return true;
	}
	
	void didDisplayMoreApps(CBLocation location){
		AdsManager.LogDebug(AD_TAG + "didDisplayMoreApps: " + location);
	}
	
	void didFailToRecordClick(CBLocation location, CBImpressionError error) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didFailToRecordClick: {0} at location: {1}", error, location));
	}
	
	void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didFailToLoadRewardedVideo: {0} at location {1}", error, location));
	}
	
	void didDismissRewardedVideo(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didDismissRewardedVideo: " + location);
	}
	
	void didCloseRewardedVideo(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didCloseRewardedVideo: " + location);
	}
	
	void didClickRewardedVideo(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didClickRewardedVideo: " + location);
	}
	
	void didCacheRewardedVideo(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didCacheRewardedVideo: " + location);
	}
	
	bool shouldDisplayRewardedVideo(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "shouldDisplayRewardedVideo: " + location);
		return true;
	}
	
	void didCompleteRewardedVideo(CBLocation location, int reward) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
	}
	
	void didDisplayRewardedVideo(CBLocation location){
		AdsManager.LogDebug(AD_TAG + "didDisplayRewardedVideo: " + location);
	}
	
	void didCacheInPlay(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "didCacheInPlay called: "+location);
	}
	
	void didFailToLoadInPlay(CBLocation location, CBImpressionError error) {
		AdsManager.LogDebug(string.Format(AD_TAG + "didFailToLoadInPlay: {0} at location: {1}", error, location));
	}
	
	void didPauseClickForConfirmation() {
		AdsManager.LogDebug(AD_TAG + "didPauseClickForConfirmation called");
	}
	
	void willDisplayVideo(CBLocation location) {
		AdsManager.LogDebug(AD_TAG + "willDisplayVideo: " + location);
	}
	
	#if UNITY_IPHONE
	void didCompleteAppStoreSheetFlow() {
		AdsManager.LogDebug("didCompleteAppStoreSheetFlow");
	}
	#endif

	#endregion
}
