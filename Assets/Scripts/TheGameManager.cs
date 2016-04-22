using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;

public class TheGameManager : MonoBehaviour {

    public static TheGameManager Instance;

    public int score;
    public int target;
    private int coins = 0;

    public Player[] players;
    public enum GameMode { Solo = 0, Fungo };

    public GameMode gameMode;

    public Player currentActivePlayer;

    public string playerName1;
    public string playerName2;

    public UILabel coinsLabel;

    public GameObject itemsGrid;

	public GameObject normalBat;
    public GameObject itemPowerUpObject;
	public GameObject itemPowerObjectAfterHit;
	public GameObject brokenBat;

	public bool isPlayer1Turn;
	public bool isFirstTurn;

	public bool isGameTurnByTurn;

	void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
			isPlayer1Turn=true;
			isFirstTurn=true;

			PlayerPrefs.SetInt(AvatarName + "textureShirtIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "bumpShirtIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "bumpShortsIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "textureShortsIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "bumpShoesIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "textureShoesIndex", 0);

			PlayerPrefs.SetInt(AvatarName + "textureNecklacesIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "textureGlassesIndex", 0);
			PlayerPrefs.SetInt(AvatarName + "textureHatsIndex", 0);



        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        playerName1 = AvatarName;
        if (coinsLabel!=null)
        {
            coinsLabel.text = Coins.ToString();
        }
        
    }

    public string AvatarName
    {
        get
        {
            if (!PlayerPrefs.HasKey("avatarName"))
            {
                PlayerPrefs.SetString("avatarName", "");
            }
            playerName1 = PlayerPrefs.GetString("avatarName");
            return playerName1;
        }
        set
        {
            playerName1 = value;
            PlayerPrefs.SetString("avatarName", value);
        }
    }

    public int Coins
    {
        get
        {
            if (!PlayerPrefs.HasKey(AvatarName + "_coins"))
            {
                PlayerPrefs.SetInt(AvatarName + "_coins", 100);
            }
            coins = PlayerPrefs.GetInt(AvatarName + "_coins");
            return coins;
        }
        set
        {
            coins = value;
            PlayerPrefs.SetInt(AvatarName + "_coins", value);
        }
    }

    public void StartGameMode(GameMode g)
    {

		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Next Button Clicked");

        gameMode = g;
        if (gameMode == GameMode.Solo)
        {
            players = new Player[1];
            players[0] = new Player(playerName1);
        }

        if (gameMode == GameMode.Fungo)
        {
            players = new Player[2];
            players[0] = new Player(playerName1);
			players[1] = new Player(playerName1);
//			players[1] = new Player(playerName1);
           
        }
        currentActivePlayer = players[0];
        currentActivePlayer.AvatarName = AvatarName;
		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, AvatarName+" Selected");

    }

	public void switchTurn(){

		currentActivePlayer = null;
		if (isPlayer1Turn == true && isFirstTurn == true) {
			isPlayer1Turn = false;
			isFirstTurn = true;
			currentActivePlayer = players [1]; 
		} else if (isPlayer1Turn == false && isFirstTurn == true) {
			isFirstTurn = false;
			isPlayer1Turn = true;
			currentActivePlayer = players [0]; 

		} else if (isPlayer1Turn == true) {
			isPlayer1Turn = false;
			currentActivePlayer = players[1]; 	
		}
		else if( isPlayer1Turn ==false)
		{ isPlayer1Turn=true;
			 currentActivePlayer = players[0]; 	
			}

	}


    public IEnumerator SwitchScene(string scene)
    {
        AsyncOperation async = Application.LoadLevelAsync(scene);
        yield return async;
    }

    public void NewGame()
    {
        score = 0;
        target = 1;
    }

    public void NextLevel()
    {
        if (target != 8)
        {
            target++;
        }
    }

   

}
