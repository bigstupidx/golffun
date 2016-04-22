using UnityEngine;
using System.Collections;


public class BaseItem : MonoBehaviour {

	public string id;
	public string name;
	public string imageName;
	public bool isCurrencyPoint;
	public bool isExperiencePoint;
	public int coin_price;
	
	
	public BaseItem()
	{
		
	}
	
	public BaseItem(string id, string name, string imageName, bool isCurrencyPoint, bool isExperiencePoint, int coin_price)
	{
		this.id = id;
		this.name = name;
		this.imageName = imageName;
		this.isCurrencyPoint = isCurrencyPoint;
		this.isExperiencePoint = isExperiencePoint;
		this.coin_price = coin_price;
	}
	
	public virtual string getAtlasName()
	{
		string path = "";
		return path;
	}
	
	public virtual string getGridImageName()
	{
		return "";
	}
	
	public virtual string getInGameImageName()
	{
		return "";
	}
	
}
