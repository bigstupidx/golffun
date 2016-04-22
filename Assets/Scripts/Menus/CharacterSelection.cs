using UnityEngine;
using System.Collections;

public class CharacterSelection :  BaseMenu {

	// Use this for initialization
    public GameObject itemGrid;

    public GameObject taz_shirt;
	public GameObject prem_shirt;
	public GameObject savy_shirt;
	public GameObject beny_shirt;
    public GameObject pant;
    public GameObject shoes;
	public GameObject hat;
	public GameObject glasses;
	public GameObject necklace;

//	public GameObject TazShirt;
//	public GameObject PreemShirt;
//	public GameObject SavyShirt;
//	public GameObject BenyShirt;
//
	public GameObject TazShorts;
	public GameObject PreemShorts;
	public GameObject SavyShorts;
	public GameObject BenyShorts;

	public GameObject TazBioCard;
	public GameObject PreemBioCard;
	public GameObject SavyBioCard;
	public GameObject BenyBioCard;

//	public GameObject TazShoes;
//	public GameObject PreemShoes;
//	public GameObject SavyShoes;
//	public GameObject BenyShoes;
    
    public GameObject item;
    public GameObject camera;
    private Camera cam;
    public UIPanel mPanel;
	public UILabel scoreLabel;

	public GameObject storePanel;
	public GameObject scrollBG;
	public GameObject scollView;

	public GameObject dummyObj;

	void Start () {
        cam = camera.GetComponent<Camera>();
//		UpdateScore ();


	}


	void OnEnable()
	{
		UpdateScore ();
	}
    public void LateUpdate()
    {
        Vector3[] corners = mPanel.worldCorners; //cameraContainer is my UISprite. collect the 4 corners of the UISprite: bottom left, top left, top right, bottom right
        Vector3 lowerLeftScreenPoint = UICamera.mainCamera.WorldToScreenPoint(corners[0]); //convert lower left point to screenspace
        Vector3 lowerRightScreenPoint = UICamera.mainCamera.WorldToScreenPoint(corners[3]);//convert lower right point to screenspace
        Vector3 topLeftScreenPoint = UICamera.mainCamera.WorldToScreenPoint(corners[1]); // convert top left point to screenspace*/
        cam.rect = new Rect(  lowerLeftScreenPoint.x / Screen.width, 0f, (lowerRightScreenPoint.x - lowerLeftScreenPoint.x) / Screen.width, 1f);
    }

	public void StoreBtnCallBack()
	{
		storePanel.SetActive (true);
		scrollBG.SetActive (false);
	}

	public void StoreCloseBtnCallBack()
	{
		storePanel.SetActive (false);
		scrollBG.SetActive (true);
	}

	public void StorePackage1CallBack()
	{
		InAppPurchaseX.instance.Purchase100coins ();
	}

	public void StorePackage2CallBack()
	{
		InAppPurchaseX.instance.Purchase1000coins ();
	}

	public void StorePackage3CallBack()
	{
		InAppPurchaseX.instance.Purchase10000coins ();
	}

	public void StorePackage4CallBack()
	{
		InAppPurchaseX.instance.Purchase50000coins ();
	}

	public void UpdateScore(){

	//	scoreLabel.text = GameManager.Instance.gamestats.Coins.ToString ();

	//	scoreLabel.text = GameManager.Instance.gamestats.Coins.ToString ();

	}
}
