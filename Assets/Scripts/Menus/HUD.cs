using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : BaseMenu
{
    public UILabel targetText;
    public UILabel strokesText;
	public GameObject powerUpsPanel;
	public GameObject storePanel;

	public GameObject playerTurnTexture;


    

	public void GoToMainMenu()
    {
        GameManager.Instance.ConfigureLevelForState(GameManager.GameState.MainMenu);
		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Go To Main Menu");

    }

    void UpdateHUD() {

        GameManager.Instance.gameStats.Round++;
        
		if (targetText == null)
        {
            foreach (UILabel lbl in transform.GetComponentsInChildren<UILabel>())
            {
                if (lbl.name.Contains("Target"))
                {
                    targetText = lbl;
                }
                if (lbl.name.Contains("Round"))
                {
                    lbl.text = GameManager.Instance.gameStats.Round.ToString();
                }
            }
        }
        else
        {
            targetText.text = "1";
        }

        GameObject scoreBoard = GameObject.FindGameObjectWithTag("Scoreboard");
        GameObject chAvatar1 = scoreBoard.transform.FindChild("Character1Photo").gameObject;
		GameObject chAvatar2 = scoreBoard.transform.FindChild("Character2Photo").gameObject;


        if (TheGameManager.Instance.currentActivePlayer.AvatarName == "Taz_Character_Tpose")
        {
            chAvatar1.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/Taz_Photo");
        }
        else if (TheGameManager.Instance.currentActivePlayer.AvatarName == "Preem_Character_Tpose")
        {
            chAvatar1.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/Preem_Photo");
        }
        else if (TheGameManager.Instance.currentActivePlayer.AvatarName == "Savy_Character_Tpose")
        {
            chAvatar1.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/Savy_Photo");
        }
        else if (TheGameManager.Instance.currentActivePlayer.AvatarName == "BenyBond_Character_Tpose")
        {
            chAvatar1.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/BenntBond_Photo");
        }

		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) {

			if (TheGameManager.Instance.currentActivePlayer.AvatarName == "Taz_Character_Tpose")
			{
				chAvatar2.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/Taz_Photo");
			}
			else if (TheGameManager.Instance.currentActivePlayer.AvatarName == "Preem_Character_Tpose")
			{
				chAvatar2.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/Preem_Photo");
			}
			else if (TheGameManager.Instance.currentActivePlayer.AvatarName == "Savy_Character_Tpose")
			{
				chAvatar2.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/Savy_Photo");
			}
			else if (TheGameManager.Instance.currentActivePlayer.AvatarName == "BenyBond_Character_Tpose")
			{
				chAvatar2.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("CharacterTexture/BenntBond_Photo");
			}

		}

        
        Transform leftStats = scoreBoard.transform.GetChild(2);
        Transform rightStats = scoreBoard.transform.GetChild(3);
        int totalLerftScore = 0;
        int totalRightScore = 0;
        foreach (TextMesh lbl in leftStats.GetComponentsInChildren<TextMesh>())
        {
            Debug.Log("-------------------------------------" + lbl.name);
            if (lbl.transform.parent.name.Equals("LeftCount"))
            {

                if (TheGameManager.Instance.players[0].roundScores[int.Parse(lbl.name)] > 0)
                {
                    lbl.text = TheGameManager.Instance.players[0].roundScores[int.Parse(lbl.name)].ToString();
                    totalLerftScore += TheGameManager.Instance.players[0].roundScores[int.Parse(lbl.name)];
                }
                else
                {
                    lbl.text = "";
                }
            }

            if(TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo)
            {
                if (lbl.transform.parent.name.Equals("RightCount"))
                {

                    if (TheGameManager.Instance.players[1].roundScores[int.Parse(lbl.name)] == 1)
                    {
                        lbl.text = "1";
                        totalRightScore++;
                    }
                    else if (TheGameManager.Instance.players[1].roundScores[int.Parse(lbl.name)] == 2)
                    {
                        lbl.text = "0";
                    }
                    else
                    {
                        lbl.text = "";
                    }
                }
            }
            else
            {
                if (lbl.transform.parent.name.Equals("RightCount"))
                {
                    lbl.text = "";
                }
            }
        }

        TextMesh scoreLeftCount = scoreBoard.transform.GetChild(5).GetComponentInChildren<TextMesh>();
        scoreLeftCount.text = totalLerftScore.ToString();
        TextMesh scoreRightCount = scoreBoard.transform.GetChild(6).GetComponentInChildren<TextMesh>();
        scoreRightCount.text = "";
        if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo)
        {
            scoreRightCount.text = totalRightScore.ToString();
        }
    }

    void Start()
    {
        Invoke("UpdateHUD", 0.5f);
		Invoke("playPreShotSound", 1.5f);
		SoundManager.Instance.Play_ArenaSounds();
    }

	void playPreShotSound(){

//		string name = this.gameObject.name;
//		
//		int nameInt=int.Parse (name);
		switch(GameManager.Instance.gameStats.Round){
		case 0:  
			break;
		case 1:
			SoundManager.Instance.Play_pre_swing_1();
			break;
		case 2:  
			SoundManager.Instance.Play_pre_swing_2();
			break;
		case 3:
			SoundManager.Instance.Play_pre_swing_3();
			break;
		case 4:  
			SoundManager.Instance.Play_pre_swing_4();
			break;
		case 5:
			SoundManager.Instance.Play_pre_swing_5();
			break;
		case 6:  
			SoundManager.Instance.Play_pre_swing_6();
			break;
		case 7:  
			SoundManager.Instance.Play_pre_swing_7();
			break;
		case 8:  
			SoundManager.Instance.Play_pre_swing_8();
			break;
		case 9:  
			SoundManager.Instance.Play_pre_swing_9();
			break;
			
		}

	}

    void Update()
    {

        if (strokesText == null)
        {
                foreach (UILabel lbl in transform.GetComponentsInChildren<UILabel>())
                {
                    if (lbl.name.Contains("Shots"))
                    {
                        strokesText = lbl;
                    }
                }
        }
        else
        {
            if (TheGameController.Instance!=null)
            {
                strokesText.text = GameManager.Instance.gameStats.Shots.ToString();

            }
        }
    }

	public void switchTexture(){


		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) {

			if(TheGameManager.Instance.isGameTurnByTurn == false)
			{
				if (TheGameManager.Instance.isPlayer1Turn == true)
						playerTurnTexture.GetComponent<UISprite> ().spriteName = "p1";
				else 
				{
				playerTurnTexture.GetComponent<UISprite> ().spriteName = "p2";

				Invoke ("play2PlayerShot",1.0f); // top time
				Invoke ("play2PlayerShot",2.0f);// 1 sec added to top time
				Invoke ("play2PlayerShot",7.0f);// 6.5 seconds added to top time
				Invoke ("resetTouchControls",12.0f);// 11 seconds added to top time
				Invoke ("play2PlayerShot",12.5f);//11.5 seconds added to top time

				}
			}
			else if(TheGameManager.Instance.isGameTurnByTurn == true)
			{
				if (TheGameManager.Instance.isPlayer1Turn == true)
					playerTurnTexture.GetComponent<UISprite> ().spriteName = "p1";
				else 
				{
					playerTurnTexture.GetComponent<UISprite> ().spriteName = "p2";
				
				}
			}


		}
		else if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Solo)
			playerTurnTexture.SetActive (false);
	
	

	}

	public void play2PlayerShot(){

		GameObject mainCam= GameObject.FindGameObjectWithTag("MainCamera");
		if (mainCam != null)
			mainCam.GetComponent<TouchControls> ().playAutoShot ();
	}

	public void resetTouchControls(){
	
		GameObject mainCam= GameObject.FindGameObjectWithTag("MainCamera");
		if (mainCam != null)
			mainCam.GetComponent<TouchControls> ().resetTouchValues();
	}

	public void PowerupsBtnCallBack()
	{
		powerUpsPanel.SetActive (true);
		Time.timeScale = 0;
	}

	public void StoreBtnCallBack()
	{
		storePanel.GetComponent<TweenPosition> ().PlayForward ();
	}

	public void StoreCrossBtnCallBack()
	{
		storePanel.GetComponent<TweenPosition> ().PlayReverse ();
	}

	public void StorePackage1CallBack()
	{
//		InAppPurchaseX.instance.Purchase100coins ();
	}
	
	public void StorePackage2CallBack()
	{
//		InAppPurchaseX.instance.Purchase1000coins ();
	}
	
	public void StorePackage3CallBack()
	{
//		InAppPurchaseX.instance.Purchase10000coins ();
	}
	
	public void StorePackage4CallBack()
	{
//		InAppPurchaseX.instance.Purchase50000coins ();
	}
}
