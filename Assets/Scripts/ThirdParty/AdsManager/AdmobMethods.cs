using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdmobMethods : BaseAdNetwork {

	#region Variables & Constants

	private const string AD_TAG = "AdMob >> ";

    [System.Serializable]
    public class AdMobKeys : System.Object
    {
        public string bannerID;
        public string interstiaialID;
    }

    // name of Ad Network
    public const eAdsNetwork adNetwork = eAdsNetwork.ADMOB;

    // key for android    
    public string androidID;

    // key for iOS    
    public string iOSID;

	// key for WP8
	public string WP8ID;

    // Private methods for banner and interstitial view;
    BannerView bannerView;
    InterstitialAd interstitialAd;

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

	#endregion

	#region Overriden Callback Methods

    public override void InitNetwork()
    {

#if UNITY_ANDROID
        string id = androidID;
#elif UNITY_IPHONE
        string id = iOSID;
#elif UNITY_WP8
		string id = WP8ID;
#endif

        AdRequest request = new AdRequest.Builder().Build();
	//	AdRequest request = new AdRequest.Builder().AddTestDevice("e306e4d1-cb19-4a14-8ba7-6f418dbdaff1").Build();
        AdPosition bannerPosition = AdPosition.Top;

        if (AdsManager.bannerAdGravity == eBannerAdPosition.BOTTOM)
        {
            bannerPosition = AdPosition.Bottom;
        }

        switch (configuration.adType)
        {
            case eAdsType.BANNER:
				AdsManager.LogDebug(AD_TAG + "InitNetwork Banner");

                bannerView = new BannerView(id, AdSize.SmartBanner, bannerPosition);
				// Called when an ad request has successfully loaded.
				bannerView.AdLoaded += BannerAdLoaded;
				// Called when an ad request failed to load.
				bannerView.AdFailedToLoad += BannerAdFailedToLoad;
				// Called when an ad is clicked.
				bannerView.AdOpened += BannerAdOpened;
				// Called when the user is about to return to the app after an ad click.
				bannerView.AdClosing += BannerAdClosing;
				// Called when the user returned from the app after an ad click.
				bannerView.AdClosed += BannerAdClosed;
				// Called when the ad click caused the user to leave the application.
				bannerView.AdLeftApplication += BannerAdLeftApplication;
                
				bannerView.LoadAd(request);
                
                break;
            case eAdsType.INTERSTITIAL:
				AdsManager.LogDebug(AD_TAG + "InitNetwork Interstitial");

                interstitialAd = new InterstitialAd(id);
				// Called when an ad request has successfully loaded.
				interstitialAd.AdLoaded += InterstitialAdLoaded;
				// Called when an ad request failed to load.
				interstitialAd.AdFailedToLoad += InterstitialAdFailedToLoad;
				// Called when an ad is clicked.
				interstitialAd.AdOpened += InterstitialAdOpened;
				// Called when the user is about to return to the app after an ad click.
				interstitialAd.AdClosing += InterstitialAdClosing;
				// Called when the user returned from the app after an ad click.
				interstitialAd.AdClosed += InterstitialAdClosed;
				// Called when the ad click caused the user to leave the application.
				interstitialAd.AdLeftApplication += InterstitialAdLeftApplication;

                interstitialAd.LoadAd(request);

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
			bannerView.Show();
			break;
		case eAdsType.INTERSTITIAL:
			AdsManager.LogDebug(AD_TAG + "ShowAd Interstitial");
			interstitialAd.Show();
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
			bannerView.Hide();
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
		return eAdsNetwork.ADMOB;
	}

	#endregion

	#region Banner callback handlers

	public void BannerAdLeftApplication(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "BannerAdLeftApplication event received.");
		// Handle the ad loaded event.

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}
	
	public void BannerAdClosed(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "BannerAdClosed event received.");
		// Handle the ad loaded event.
	}
	
	public void BannerAdClosing(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "BannerAdClosing event received.");
		// Handle the ad loaded event.
	}
	
	public void BannerAdOpened(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "BannerAdOpened event received.");
		// Handle the ad loaded event.

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}
	
	public void BannerAdLoaded(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "BannerAdLoaded event received.");
		// Handle the ad loaded event.
	}
	
	public void BannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "Banner Failed to load: " + args.Message);
		// Handle the ad failed to load event.
		AdsManager.SetBannerAdFailed (configuration.showThisAdAt, this.GetAdNetworkInfo());

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}

	#endregion

	#region Interstitial callback handlers

	public void InterstitialAdLeftApplication(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "InterstitialAdLeftApplication event received.");
		// Handle the ad loaded event.

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_CLICKED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}

	public void InterstitialAdClosed(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "InterstitialAdClosed event received.");
		// Handle the ad loaded event.
		interstitialAd.LoadAd (new AdRequest.Builder().Build());
	}

	public void InterstitialAdClosing(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "InterstitialAdClosing event received.");
		// Handle the ad loaded event.
	}

	public void InterstitialAdOpened(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "InterstitialAdOpened event received.");
		// Handle the ad loaded event.

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_SERVED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}

	public void InterstitialAdLoaded(object sender, EventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "InterstitialAdLoaded event received.");
		// Handle the ad loaded event.
		AdsManager.SetInterstitialAdReady (configuration.showThisAdAt, this.GetAdNetworkInfo());
	}

	public void InterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		AdsManager.LogDebug(AD_TAG + "Interstitial Failed to load: " + args.Message);
		// Handle the ad failed to load event.

		// log analytics
		if (configuration.adNetworkType == eAdsNetworkType.CROSSPROMO) {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_CROSSPROMO, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		} else {
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_MONETIZATION, Constants.GA_ACTION_TYPE_AD_FAILED, AdsManager.GetGameStateName (configuration.showThisAdAt) + " " + AdsManager.GetAdNetworkName (this.GetAdNetworkInfo ()) + " " + AdsManager.GetAdTypeName (configuration.adType));
		}
	}

	#endregion
}
