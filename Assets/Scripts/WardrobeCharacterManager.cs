                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         using UnityEngine;
using System.Collections;

public class WardrobeCharacterManager : MonoBehaviour {

	public Texture[] characterTextures;

    public GameObject Taz;
    public GameObject Preem;
    public GameObject Savy;
    public GameObject Beny;

    private GameObject activeChar;
    private Player pleyer;
    private CharacterSelection characterPanel;

	public bool isShoesSelected;
	public bool isCharacterSelected;
	public bool isShirtSelected;
	public bool isTrouserSelected;
	public bool isHatSelected;
	public bool isGlassesSelected;
	public bool isNecklacesSelected;

	

    void Start () {
        Taz.name = "Taz_Character_Tpose";
        Preem.name = "Preem_Character_Tpose";
        Savy.name = "Savy_Character_Tpose";
        Beny.name = "BenyBond_Character_Tpose";
        activeChar = Taz.transform.GetChild(0).gameObject;
        pleyer = new Player(Taz.name);
        TheGameManager.Instance.playerName1 = pleyer.AvatarName;


		Invoke("PopulateCharacterList", 0.5f);
	}

	void Update()
	{
		//	if (nguiCam != null) {
		Ray inputRay;
		RaycastHit hit;
		int layerMask = 1 << LayerMask.NameToLayer ("UI");
		//            Debug.Log(layerMask);
		// pos is the Vector3 representing the screen position of the input
		if (Input.GetMouseButtonDown (0)) {
			inputRay = MenuManager.Instance.cam.GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (inputRay.origin, inputRay.direction, out hit, Mathf.Infinity, layerMask)) {
				
				Debug.Log ("Clicking on UI Element" + hit.collider.name);
				GameObject objName=GameObject.Find (hit.collider.name);
				if(objName == null) return;
				int index=objName.GetComponent<PowerUpGUIHandler>().index;
				Debug.Log("index == "+index);
				onItemSelected(index);
				return;// UI was hit, so don't allow this input to fall through to the gameplay input handler
			}
		}
	}

	void PopulateCharacterList()
	{

		characterPanel = GameObject.FindObjectOfType<CharacterSelection>();

		characterPanel.TazBioCard.SetActive (true);

		if (characterPanel != null)
		{
			foreach (Transform child in characterPanel.itemGrid.transform)
			{
				Destroy(child.gameObject);
			}
			ArrayList playerData = DataProvider.GetPlayerData();
			for (int index = 0; index < 4; index++)
			{
				GameObject item = NGUITools.AddChild(characterPanel.itemGrid, characterPanel.item);
				item.name = (playerData[index] as PlayerData).id;
				PowerUpGUIHandler hnd = item.GetComponent<PowerUpGUIHandler>();
				hnd.characterPic.SetActive(true);
				hnd.isLocked = (playerData[index] as PlayerData).isLocked;
				hnd.itemCost = (playerData[index] as PlayerData).price;
				hnd.index=index;
				hnd.itemType = "character";
				hnd.transform.GetChild(4).GetComponent<UITexture>().mainTexture = characterTextures[index];
				hnd.enabled = true;
				item.transform.localPosition = new Vector3(-1800f, 0f, 0f);
			}
			CancelInvoke("RepositionGrid");
			Invoke("RepositionGrid", 0.5f);
		}
	}

    public void PopulatePlayerList()
    {
        characterPanel = GameObject.FindObjectOfType<CharacterSelection>();
        if (characterPanel != null)
        {
            foreach (Transform child in characterPanel.itemGrid.transform)
            {
                Destroy(child.gameObject);
            }
            ArrayList playerData = DataProvider.GetPlayerData();
            for (int index = 0; index < playerData.Count; index++)
            {

                GameObject item = NGUITools.AddChild(characterPanel.itemGrid, characterPanel.item);
                item.name = index + "_" + pleyer.AvatarName;
                PowerUpGUIHandler hnd = item.GetComponent<PowerUpGUIHandler>();
				hnd.characterPic.SetActive(true);
				if(!isCharacterSelected)
				{
					hnd.transform.GetChild(4).gameObject.SetActive(false);
				}
                hnd.isLocked = (playerData[index] as PlayerData).isLocked;
                hnd.itemCost = (playerData[index] as PlayerData).price;
				hnd.index=index;
                hnd.itemType = "character";
                hnd.enabled = true;
                item.transform.localPosition = new Vector3(-1800f, 0f, 0f);
            }
            CancelInvoke("RepositionGrid");
            Invoke("RepositionGrid", 0.5f);
        }
    }

	public void SwitchCharacter(bool isLeftBtn)
    {
		characterPanel.BenyBioCard.SetActive (false);
		characterPanel.PreemBioCard.SetActive (false);
		characterPanel.SavyBioCard.SetActive (false);
		characterPanel.TazBioCard.SetActive (false);

		if (!isLeftBtn) {
			if (Taz.activeInHierarchy) {
				Preem.SetActive (true);
				Taz.SetActive (false);
				activeChar = Preem.transform.GetChild (0).gameObject;
				pleyer.AvatarName = Preem.name;
				characterPanel.PreemBioCard.SetActive (true);


			} else if (Preem.activeInHierarchy) {
				Savy.SetActive (true);
				Preem.SetActive (false);
				activeChar = Savy.transform.GetChild (0).gameObject;
				pleyer.AvatarName = Savy.name;
				characterPanel.SavyBioCard.SetActive (true);

			} else if (Savy.activeInHierarchy) {
				Beny.SetActive (true);
				Savy.SetActive (false);
				activeChar = Beny.transform.GetChild (0).gameObject;
				pleyer.AvatarName = Beny.name;
				characterPanel.BenyBioCard.SetActive (true);

			} else if (Beny.activeInHierarchy) {
				Taz.SetActive (true);
				Beny.SetActive (false);
				activeChar = Taz.transform.GetChild (0).gameObject;
				pleyer.AvatarName = Taz.name;
				characterPanel.TazBioCard.SetActive (true);

			}
		} else {
			if (Taz.activeInHierarchy)
			{
				Beny.SetActive(true);
				Taz.SetActive(false);
				activeChar = Beny.transform.GetChild(0).gameObject;
				pleyer.AvatarName = Beny.name;
				characterPanel.BenyBioCard.SetActive (true);

			}
			else if (Preem.activeInHierarchy)
			{
				Taz.SetActive(true);
				Preem.SetActive(false);
				activeChar = Taz.transform.GetChild(0).gameObject;
				pleyer.AvatarName = Taz.name;
				characterPanel.TazBioCard.SetActive (true);

			}
			else if (Savy.activeInHierarchy)
			{
				Preem.SetActive(true);
				Savy.SetActive(false);
				activeChar = Preem.transform.GetChild(0).gameObject;
				pleyer.AvatarName = Preem.name;
				characterPanel.PreemBioCard.SetActive (true);

			}
			else if (Beny.activeInHierarchy)
			{
				Savy.SetActive(true);
				Beny.SetActive(false);
				activeChar = Savy.transform.GetChild(0).gameObject;
				pleyer.AvatarName = Savy.name;
				characterPanel.SavyBioCard.SetActive (true);

			}
		}
        
		switchMesh (pleyer.AvatarName);
        TheGameManager.Instance.playerName1 = pleyer.AvatarName;
        //PopulateItems("shirts");
       
    }

    public void PopulateItems(string itemType)
    {

//        return;
        characterPanel = GameObject.FindObjectOfType<CharacterSelection>();
	   SwitchFranela shirts = activeChar.GetComponent<SwitchFranela>();
//		SwitchFranela shirts = characterPanel.dummyObj.GetComponent<SwitchFranela>();

		SwitchPantalon shorts = activeChar.GetComponent<SwitchPantalon>();
		SwitchZapatos shoes = activeChar.GetComponent<SwitchZapatos>();
		SwitchHat hats=activeChar.GetComponent<SwitchHat>();
		SwitchGlasses glasses = activeChar.GetComponent<SwitchGlasses> ();
		SwitchNecklaces necklaces = activeChar.GetComponent<SwitchNecklaces> ();

		int arrayLength = 0;
		if (itemType.Contains("shirts")) {

            Object item = shirts;
			arrayLength=shirts.textures.Length;
        }
        else if (itemType.Contains("shorts"))
        {
            Object item = shorts;
			arrayLength=shorts.textures.Length;
        }
		else if (itemType.Contains("glasses"))
		{
			Object item = glasses;
			arrayLength=glasses.textures.Length;

		}
		else if (itemType.Contains("hats"))
		{
			Object item = hats;
			arrayLength=hats.textures.Length;
		}
		else if (itemType.Contains("necklaces"))
		{
			Object item = necklaces;
			arrayLength=necklaces.gameObjects.Length;
		}
        else{
            Object item = shoes;
			arrayLength=shoes.textures.Length;
        }
        if (characterPanel != null)
        {
            foreach (Transform child in characterPanel.itemGrid.transform)
            {
                Destroy(child.gameObject);
            }

            ArrayList shirtsData = DataProvider.GetPlayerShirtsData(pleyer.AvatarName);

            for (int index = 0; index < arrayLength; index++)
            {

                GameObject item = NGUITools.AddChild(characterPanel.itemGrid, characterPanel.item);
                item.name = index + "_" + pleyer.AvatarName;
                PowerUpGUIHandler hnd = item.GetComponent<PowerUpGUIHandler>();
				hnd.characterPic.SetActive(false);
                hnd.isLocked = (shirtsData[index] as PlayerItems).isLocked;
                hnd.itemCost = (shirtsData[index] as PlayerItems).price;
                hnd.itemType = itemType;
                hnd.enabled = true;
				hnd.index=index;

				GameObject obj = null;
               
                if (itemType.Contains("shirts")) {

					if (pleyer.AvatarName == "Preem_Character_Tpose" ) {
						obj = NGUITools.AddChild(item, characterPanel.prem_shirt);
						obj.transform.localPosition = new Vector3(0,0f,0);
						obj.transform.localScale = new Vector3(92f, 92f, 92f);
						obj.transform.localEulerAngles = new Vector3(0f,180f, 0f);
						
					} else if (pleyer.AvatarName =="Savy_Character_Tpose") {

						obj = NGUITools.AddChild(item, characterPanel.savy_shirt);
						obj.transform.localPosition = new Vector3(0,-80f,0);
						obj.transform.localScale = new Vector3(119f, 119f, 119f);
						obj.transform.localEulerAngles = new Vector3(0f,180f, 0f);
					} else if (pleyer.AvatarName =="BenyBond_Character_Tpose") {

						obj = NGUITools.AddChild(item, characterPanel.beny_shirt);
						obj.transform.localPosition = new Vector3(0,-80f,0);
						obj.transform.localScale = new Vector3(92f, 92f, 92f);
						obj.transform.localEulerAngles = new Vector3(0f,180f, 0f);
					} else if (pleyer.AvatarName =="Taz_Character_Tpose") {

						obj = NGUITools.AddChild(item, characterPanel.taz_shirt);
						obj.transform.localPosition = new Vector3(0,-80f,0);
						obj.transform.localScale = new Vector3(92f, 92f, 92f);
						obj.transform.localEulerAngles = new Vector3(0f,180f, 0f);
					}
                    



                }
                else if (itemType.Contains("shorts"))
                {
                    obj = NGUITools.AddChild(item, characterPanel.pant);
					obj.transform.localPosition = new Vector3(0,0,0);
					obj.transform.localScale = new Vector3(80f, 80f, 80f);
					obj.transform.localEulerAngles = new Vector3(0f,180f, 0f);

                }
				else if (itemType.Contains("hats"))
				{
					obj = NGUITools.AddChild(item, hats.gameObjects[index]);
					obj.transform.localPosition = new Vector3(0,0,0);
					obj.transform.localScale = new Vector3(80f, 80f, 80f);
					obj.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);

				}
				else if (itemType.Contains("glasses"))
				{
					obj = NGUITools.AddChild(item, glasses.gameObjects[index]);
					obj.transform.localPosition = new Vector3(0,0,0);
					obj.transform.localScale = new Vector3(110f, 110f, 110f);
					obj.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
					
				}
				else if (itemType.Contains("necklaces"))
				{
					necklaces.gameObjects[index].SetActive(true);
					obj = NGUITools.AddChild(item, necklaces.gameObjects[index]);
					obj.transform.localPosition = new Vector3(0,0,0);
					obj.transform.localScale = new Vector3(200f, 200f, 200f);
					obj.transform.localEulerAngles = new Vector3(-90f, 180f, 0f);
					
				}

                else{
                    obj = NGUITools.AddChild(item, characterPanel.shoes);
					obj.transform.localPosition = new Vector3(0,0,0);
					obj.transform.localScale = new Vector3(200f, 200f, 200f);
					obj.transform.localEulerAngles = new Vector3(40f,-90f, 0f);


                }
                
				if (itemType.Contains("shorts") && (pleyer.AvatarName =="Preem_Character_Tpose" || pleyer.AvatarName =="BenyBond_Character_Tpose"))
					obj.transform.localPosition = new Vector3(-0f, -110f, 0f);

                item.transform.localPosition = new Vector3(-1800f, 0f, 0f);

                obj.layer = LayerMask.NameToLayer("Items");
                MeshRenderer rend = obj.GetComponent<MeshRenderer>();


				if (itemType.Contains("shirts")) {
					
					rend.material.SetTexture("_MainTex", shirts.textures[index]);
				}
				else if (itemType.Contains("shorts"))
				{
					rend.material.SetTexture("_MainTex", shorts.textures[index]);
				}
				else if (itemType.Contains("hats"))
				{
					rend.material.SetTexture("_MainTex", hats.textures[index]);
				}
				else if (itemType.Contains("glasses"))
				{
					rend.material.SetTexture("_MainTex", glasses.textures[index]);
				}else if(itemType.Contains("necklaces"))
				{
//					rend.material.SetTexture("_MainTex", necklaces.gameObjects[index].GetComponent<MeshRenderer>().material.GetTexture();
					necklaces.gameObjects[index].SetActive(false);
				}
				else
					rend.material.SetTexture("_MainTex", shoes.textures[index]);


//                rend.material.SetTexture("_MainTex", shirts.textures[index]);
                rend.receiveShadows = false;
                rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                CancelInvoke("RepositionGrid");
                Invoke("RepositionGrid", 0.5f);
            }
        }
    }

    public void RepositionGrid() {
        characterPanel.itemGrid.GetComponent<UIGrid>().enabled = true;
        characterPanel.itemGrid.GetComponent<UIGrid>().repositionNow = true;
        characterPanel.itemGrid.GetComponent<UIGrid>().Reposition();
//		characterPanel.scollView.GetComponent<UIScrollView> ().ResetPosition ();



    }


	public void LeftBtnCallBack()
	{
		if (isShoesSelected) {
			switchZapato (true);
		} else if (isShirtSelected) {
			switchFranela (true);
		} else if (isTrouserSelected) {
			switchPantalon (true);
		} else if (isCharacterSelected) {
			SwitchCharacter (true);
		} else if (isHatSelected)
			switchHats (true);
		else if (isGlassesSelected)
			switchGlasses (true);
		else if (isNecklacesSelected)
			switchNecklaces (true);

	}

	public void RightBtnCallBack()
	{
		if (isShoesSelected) {
			switchZapato (false);
		} else if (isShirtSelected) {
			switchFranela (false);
		} else if (isTrouserSelected) {
			switchPantalon (false);
		} else if (isCharacterSelected) {
			SwitchCharacter (false);
		} else if (isHatSelected)
			switchHats (false);
		else if (isGlassesSelected)
			switchGlasses (false);
		else if (isNecklacesSelected)
			switchNecklaces (false);

	}

	public void onItemSelected(int itemIndex){

		if (isShoesSelected) {
			activeChar.GetComponent<SwitchZapatos>().ChangeTexInt(pleyer, false, itemIndex);
		} else if (isShirtSelected) {
			activeChar.GetComponent<SwitchFranela>().ChangeTexInt(pleyer, false, itemIndex);
		} else if (isTrouserSelected) {
			activeChar.GetComponent<SwitchPantalon>().ChangeTexInt(pleyer, false, itemIndex);
		}
		else if(isHatSelected)
			activeChar.GetComponent<SwitchHat>().ChangeTexInt(pleyer, false, itemIndex);
		else if(isGlassesSelected)
			activeChar.GetComponent<SwitchGlasses>().ChangeTexInt(pleyer, false, itemIndex);
		else if(isNecklacesSelected)
			activeChar.GetComponent<SwitchNecklaces>().ChangeTexInt(pleyer, false, itemIndex);
		 else if(isCharacterSelected) {
				SwitchToCharacterIndex(itemIndex);
		}

	}

	public void SwitchToCharacterIndex(int index)
	{

		Taz.SetActive (false);
		Preem.SetActive (false);
		Savy.SetActive (false);
		Beny.SetActive (false);
		characterPanel.BenyBioCard.SetActive (false);
		characterPanel.PreemBioCard.SetActive (false);
		characterPanel.SavyBioCard.SetActive (false);
		characterPanel.TazBioCard.SetActive (false);

				if (index ==1 ) {
					Preem.SetActive (true);
					activeChar = Preem.transform.GetChild (0).gameObject;
					pleyer.AvatarName = Preem.name;
					characterPanel.PreemBioCard.SetActive (true);

				} else if (index ==2) {
					Savy.SetActive (true);
					activeChar = Savy.transform.GetChild (0).gameObject;
					pleyer.AvatarName = Savy.name;
					characterPanel.SavyBioCard.SetActive (true);

				} else if (index ==3) {
					Beny.SetActive (true);
					activeChar = Beny.transform.GetChild (0).gameObject;
					pleyer.AvatarName = Beny.name;
					characterPanel.BenyBioCard.SetActive (true);

				} else if (index ==0) {
					Taz.SetActive (true);
					activeChar = Taz.transform.GetChild (0).gameObject;
					pleyer.AvatarName = Taz.name;
					characterPanel.TazBioCard.SetActive (true);

				}
		Debug.Log ("pleyer.AvatarName " + pleyer.AvatarName);
		switchMesh (pleyer.AvatarName);
	}

	public void switchMesh(string playerName){


		if (playerName=="Preem_Character_Tpose" ) {
			characterPanel.pant=characterPanel.PreemShorts;

		} else if (playerName =="Savy_Character_Tpose") {
			characterPanel.pant=characterPanel.SavyShorts;

		} else if (playerName =="BenyBond_Character_Tpose") {
			characterPanel.pant=characterPanel.BenyShorts;

		} else if (playerName =="Taz_Character_Tpose") {
			characterPanel.pant=characterPanel.TazShorts;

		}

	}

	public void switchHats(bool isLeftBtn)
	{
		activeChar.GetComponent<SwitchHat>().ChangeTex(pleyer, false, isLeftBtn);
	}

    public void switchFranela(bool isLeftBtn)
    {
        activeChar.GetComponent<SwitchFranela>().ChangeTex(pleyer, false, isLeftBtn);
    }

	public void switchPantalon(bool isLeftBtn)
    {
		activeChar.GetComponent<SwitchPantalon>().ChangeTex(pleyer, false, isLeftBtn);
    }

	public void switchGlasses(bool isLeftBtn)
    {
		activeChar.GetComponent<SwitchGlasses>().ChangeTex(pleyer, false, isLeftBtn);
    }

	public void switchNecklaces(bool isLeftBtn)
	{
		activeChar.GetComponent<SwitchNecklaces>().ChangeTex(pleyer, false, isLeftBtn);
	}
	public void switchZapato(bool isLeftBtn)
	{
		activeChar.GetComponent<SwitchZapatos>().ChangeTex(pleyer, false, isLeftBtn);
	}

	public void HomeBtnCallBack()
	{
		GameManager.Instance.ConfigureLevelForState (GameManager.GameState.MainMenu);
	}

	public void ItemSelected(string buttonName)
	{
		switch (buttonName) {
		case "shirt":
		{
			isShirtSelected = true;
			isTrouserSelected = false;
			isShoesSelected = false;
			isCharacterSelected = false;
			isHatSelected=false;
			isGlassesSelected=false;
			isNecklacesSelected=false;
			break;
		}
		case "trouser":
		{
			isShirtSelected = false;
			isTrouserSelected = true;
			isShoesSelected = false;
			isCharacterSelected = false;
			isHatSelected=false;
			isGlassesSelected=false;
			isNecklacesSelected=false;
			break;
		}
		case "shoes":
		{
			isShirtSelected = false;
			isTrouserSelected = false;
			isShoesSelected = true;
			isCharacterSelected = false;
			isHatSelected=false;
			isGlassesSelected=false;
			isNecklacesSelected=false;
			break;
		}
		case "character":
		{
			isShirtSelected = false;
			isTrouserSelected = false;
			isShoesSelected = false;
			isCharacterSelected = true;
			isHatSelected=false;
			isGlassesSelected=false;
			isNecklacesSelected=false;
			Invoke("PopulateCharacterList", 0.5f);
			break;
		}
		case "hats":
		{
			isShirtSelected = false;
			isTrouserSelected = false;
			isShoesSelected = false;
			isCharacterSelected = false;
			isHatSelected=true;
			isGlassesSelected=false;
			isNecklacesSelected=false;
			//	Invoke("PopulateCharacterList", 0.5f);
			break;
		}
		case "glasses":
		{
			isShirtSelected = false;
			isTrouserSelected = false;
			isShoesSelected = false;
			isCharacterSelected = false;
			isHatSelected=false;
			isGlassesSelected=true;
			isNecklacesSelected=false;
			//	Invoke("PopulateCharacterList", 0.5f);
			break;
		}
		case "necklaces":
		{
			isShirtSelected = false;
			isTrouserSelected = false;
			isShoesSelected = false;
			isCharacterSelected = false;
			isHatSelected=false;
			isGlassesSelected=false;
			isNecklacesSelected=true;
			//	Invoke("PopulateCharacterList", 0.5f);
			break;
		}
		default:
			break;
		}
	}
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        