using UnityEngine;
using System.Collections;


using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.OurUtils;

public class Scores : BaseMenu
{
	int totallevels=2;
	public GameObject playerWinTexture;
	public GameObject winScreem;

	int totalP1Scores=0;
	int totalP2Scores=0;


	// Use this for initialization
	void Start () {
		updateRoundScore ();
        foreach (UILabel lbl in transform.GetComponentsInChildren<UILabel>()) {
            if (lbl.name.Contains("TotalGamesPlayed"))
            {
                lbl.text = "Total Games: " + GameManager.Instance.gameStats.TotalGames.ToString();
            }

            if (lbl.name.Contains("Shots"))
            {
                lbl.text = "Shots: " + GameManager.Instance.gameStats.Shots.ToString();
            }

            if (lbl.name.Contains("Round"))
            {
                lbl.text = "Round: " + GameManager.Instance.gameStats.Round.ToString();
            }
        }
		Invoke ("isGameOver", 2.0f);
        Invoke("UpdateHUD", 0.5f);
		Invoke("playApplauseSound", 1.25f);
		winScreem.SetActive (false);
		playerWinTexture.SetActive (false);
	}

	void updateRoundScore(){

		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) {
//			if (TheGameManager.Instance.isPlayer1Turn == true)
//				TheGameManager.Instance.players [0].roundScores [GameManager.Instance.gameStats.Round] = GameManager.Instance.gameStats.Shots;
//			else
//				TheGameManager.Instance.players [1].roundScores [GameManager.Instance.gameStats.Round] = GameManager.Instance.gameStats.Shots;
//			
//		}
//		else
//			TheGameManager.Instance.players [0].roundScores [GameManager.Instance.gameStats.Round] = GameManager.Instance.gameStats.Shots;
		

			if (TheGameManager.Instance.isPlayer1Turn == true)
				TheGameManager.Instance.players [0].roundScores [TheGameManager.Instance.currentActivePlayer.playerRound] = GameManager.Instance.gameStats.Shots;
			else
				TheGameManager.Instance.players [1].roundScores [TheGameManager.Instance.currentActivePlayer.playerRound] = GameManager.Instance.gameStats.Shots;
							
		} else
			TheGameManager.Instance.players [0].roundScores [TheGameManager.Instance.currentActivePlayer.playerRound] = GameManager.Instance.gameStats.Shots;
	

	}

	void playApplauseSound(){

		SoundManager.Instance.Play_applause ();
	}

    void UpdateHUD()
    {
        GameObject scoreBoard =  GameObject.FindGameObjectWithTag("Scoreboard");
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
        }

		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) {

			foreach (TextMesh lbl in rightStats.GetComponentsInChildren<TextMesh>()) {
				Debug.Log ("-------------------------------------" + lbl.name);
				if (lbl.transform.parent.name.Equals ("RightCount")) {
					Debug.Log ("TheGameManager.Instance.players[1].roundScores[int.Parse(lbl.name)] " + TheGameManager.Instance.players [1].roundScores [int.Parse (lbl.name)]);

					if (TheGameManager.Instance.players[1].roundScores[int.Parse(lbl.name)] > 0)
					{
						lbl.text = TheGameManager.Instance.players[1].roundScores[int.Parse(lbl.name)].ToString();
						totalRightScore += TheGameManager.Instance.players[1].roundScores[int.Parse(lbl.name)];
					}
					else
					{
						lbl.text = "";
					}
				}
			}
		}
	
		 totalP1Scores = totalLerftScore;
		 totalP2Scores = totalRightScore;


        TextMesh scoreLeftCount = scoreBoard.transform.GetChild(5).GetComponentInChildren<TextMesh>();
        scoreLeftCount.text = totalLerftScore.ToString();
        TextMesh scoreRightCount = scoreBoard.transform.GetChild(6).GetComponentInChildren<TextMesh>();
        scoreRightCount.text = "";
     
		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo)
        {
            scoreRightCount.text = totalRightScore.ToString();
        }
    }

	// Update is called once per frame
	public void PlayNextLevel () {
		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo)
			TheGameManager.Instance.switchTurn ();

        GameManager.Instance.ConfigureLevelForState(GameManager.GameState.HUD);
        GameManager.Instance.isMissionCompleted = false;
        GameManager.Instance.gameStats.Shots = 0;
        TheGameManager.Instance.Coins += 100;

	}

	public void isGameOver(){
		
		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) {
			if (TheGameManager.Instance.isPlayer1Turn == false) {
				if(TheGameManager.Instance.currentActivePlayer.playerRound>=totallevels){
					if(totalP1Scores>totalP2Scores){
						if (Social.localUser.authenticated)
						{ /// Adding scores to the leaderboard
							Social.ReportScore(totalP1Scores, GPGSIDs.leaderboard_fungo_golf_3d, (bool success) =>
							                   {
								if (success)
								{
									((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIDs.leaderboard_fungo_golf_3d);
								}
								else
								{
									//Debug.Log("Login failed for some reason");
								}
							});}
						switchTexture(true);}
					else
					{switchTexture(false);}
				}
			}
		}
	}

	public void switchTexture(bool isPlayer1Won){

		winScreem.SetActive (true);
		playerWinTexture.SetActive (true);
		if (isPlayer1Won == true) {
			playerWinTexture.GetComponent<UISprite> ().spriteName = "p1";	
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_GAMEOVER, "Player-1 won");
		} else {
			playerWinTexture.GetComponent<UISprite> ().spriteName = "p2";	
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_GAMEOVER, "Player-2 won");
		}
		
		winScreem.SetActive (true);
	}
}
