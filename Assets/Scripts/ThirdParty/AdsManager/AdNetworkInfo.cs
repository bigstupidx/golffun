using UnityEngine;
using System.Collections;

[System.Serializable]
public class AdNetworkInfo : System.Object {
	
    public eGameStates showThisAdAt;
	public eAdsNetworkType adNetworkType;
    public eAdsType adType;
	public int adPriority;
	private bool adReady = false;
	private bool adFailedToLoad = false;

	public bool AdReady {
		get { 
			return adReady;
		} 
		set {
			adReady = value;
		}
	}

	public bool AdFailedToLoad {
		get { 
			return adFailedToLoad;
		} 
		set {
			adFailedToLoad = value;
		}
	}
}
