using UnityEngine;
using System.Collections;

public class BaseAdNetwork : MonoBehaviour 
{
    // The public properties of the network
    public AdNetworkInfo configuration;

    public virtual void InitNetwork() { }
    public virtual void ShowAd() { }
    public virtual void HideAd() { }

	public virtual eAdsNetwork GetAdNetworkInfo() { 
		return eAdsNetwork.NONE; 
	}
}
