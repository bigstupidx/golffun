using UnityEngine;
using System.Collections;

using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.OurUtils;


public class MainMenu  : BaseMenu {

	private bool tweenForward = true;
	private TweenPosition inventoryPosition;
	public GameObject musicSlider;
	public GameObject soundSlider;
	public GameObject dailyBonusPanel;
	public TweenPosition dailyBonusPlaceHolder;
	public GameObject congratulationsPanel;
	public TweenPosition congratulationsPlaceHolder;
	public GameObject GameMana;
	public GameObject leaderboardPanel;
	public GameObject buttonsPanel;
	public GameObject soundsButton;
	string[] LeadScores,UserNames;
	//ILeaderboard leaderboard;

//	public TweenPosition leaderboardPlaceHolder;

	void Start()
	{
	//	SoundManager.Instance.Stop_BGM ();
		//SoundManager.Instance.Play_main_menu ();

	}

	void Update(){
	
	
	
	}
    public void StartGame()
    {
        GameManager.Instance.ConfigureLevelForState(GameManager.GameState.ModeSelection);
		UniversalAnalytics.ActivateStatistics = true;

		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Play Button Clicked",0);
//		gameObject.SetActive (false);
//		GameMana = GameObject.Find("GameManager");
//		GameMana.SetActive (true);
//		SoundManager.Instance.Play_main_menu ();


			
//		PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
//		                                                  {
//			if (success)
//			{
//				Debug.Log("connection to gps succes");
//			}
//			else
//			{
//				Debug.Log("connection to gps failed");
//			}
//		});

	
    }



    void OnEnable()
    {

    }

	public void SettingsBtnPressed(TweenPosition tween)
	{

		int value = PlayerPrefs.GetInt("Sounds"); // 0 is for on 1 is for Off
		
		if (value == 0) 
			soundsButton.GetComponent<UISprite>().spriteName="ui_sound";
		 else 
			soundsButton.GetComponent<UISprite>().spriteName="ui_sound_off";

		


		inventoryPosition = tween;
		if(tweenForward){
			tween.PlayForward();

		}
		else{
			tween.PlayReverse();
		}
		
	}
	public void LeaderboardBtnCallBack()
	{
		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Leader Board Button Clicked");

		if (!leaderboardPanel.activeInHierarchy) {
			leaderboardPanel.SetActive (true);
			buttonsPanel.SetActive (false);
		
			//	leaderboardPlaceHolder.PlayForward ();
		} else {
			leaderboardPanel.SetActive (false);
			buttonsPanel.SetActive (true);

//			PlayGamesPlatform.Instance.LoadScores(
//				GPGSIDs.leaderboard_fungo_golf_3d,
//				LeaderboardStart.PlayerCentered,
//				100,
//				LeaderboardCollection.Public,
//				LeaderboardTimeSpan.AllTime,
//				(data) =>
//				{
////				mStatus = "Leaderboard data valid: " + data.Valid;
////				mStatus += "\n approx:" +data.ApproximateCount + " have " + data.Scores.Length;
//			});
			//	leaderboardPlaceHolder.PlayReverse ();

		}
			if (Social.localUser.authenticated)
			{

			UILabel Rank = GameObject.Find("lbl_Rank").GetComponent<UILabel>();
			Rank.text = "Wait......";
		this.LoadLeaderBoardScores();
//			this.LoadSocialScores();

			Social.ReportScore(500, GPGSIDs.leaderboard_fungo_golf_3d, (bool success) =>

				                   {
					if (success)
					{
					//((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIDs.leaderboard_fungo_golf_3d);



					Rank.text = "Done......";




				//	LoadSocialScores();






				}
			
					else
					{
						//Debug.Log("Login failed for some reason");
					}
			});
		}
			




		//Social.ShowLeaderboardUI();











	}

	public void DailyBonusBtnCallBack()
	{
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Daily Bonus Button Clicked");

		if (!dailyBonusPanel.activeInHierarchy) {
			dailyBonusPanel.SetActive (true);
			dailyBonusPlaceHolder.PlayForward ();
		} else {
			dailyBonusPanel.SetActive (false);
			dailyBonusPlaceHolder.PlayReverse ();

		}
	}

	public void CongratulationsCrossBtnCallBack()
	{
			UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Congratulations Button Clicked");

		congratulationsPanel.SetActive (false);
		congratulationsPlaceHolder.PlayReverse ();
	}
	
	public void setTweenOrder(){
		tweenForward = !tweenForward;
	}

	public void MusicBtnCallBack()
	{
		if (musicSlider.activeInHierarchy) {
			musicSlider.SetActive (false);
		} else {
			musicSlider.SetActive(true);
		}
	}

	public void SoundBtnCallBack()
	{
		int value = PlayerPrefs.GetInt("Sounds"); // 0 is for on 1 is for Off

		if (value == 0) {
			SoundManager.Instance.muteSounds ();
			PlayerPrefs.SetInt("Sounds",1);
			soundsButton.GetComponent<UISprite>().spriteName="ui_sound_off";

		} else {
			SoundManager.Instance.UnMuteSounds ();
			PlayerPrefs.SetInt("Sounds",0);
			soundsButton.GetComponent<UISprite>().spriteName="ui_sound";

		}


//		if (soundSlider.activeInHierarchy) {
//			soundSlider.SetActive (false);
//		} else {
//			soundSlider.SetActive(true);
//		}
	}



	void LoadLeaderboardUsers(string[] userIDs)
	{
		//Debug.Log("There was " + userIDs.Length + " user ids");
	
		PlayGamesPlatform.Instance.LoadUsers(userIDs, OnLeaderboardUsersLoaded);

	}
	
	void OnLeaderboardUsersLoaded(IUserProfile[] profiles)
	{
		Debug.Log("There was " + profiles.Length + " profiles loaded");

		UserNames = new string[profiles.Length];
		int i = 0;
		foreach(IUserProfile profile in profiles)
		{
			//Debug.Log("PROFILE NAME = " + profile.userName);

			UserNames[i] = profile.userName;
			int jk = i+1;
			UILabel name = GameObject.Find("LD_Day"+jk).transform.GetChild(0).GetComponent<UILabel>();
			name.text =  profile.userName;
			i++;
		}
	}


			

	public void LoadLeaderBoardScores(){


				
				PlayGamesPlatform.Instance.LoadScores (
					GPGSIDs.leaderboard_fungo_golf_3d,
					LeaderboardStart.PlayerCentered,
					10,
					LeaderboardCollection.Public,
					LeaderboardTimeSpan.AllTime,
					(LeaderboardScoreData data) => {
					
					LeadScores = new string[data.Scores.Length];
					

			string[] Userids = new string[data.Scores.Length];
			Userids[0] = data.Scores[0].userID.ToString();

		
					for(int k = 0; k<=6; k++){

						
						
				if(data.Scores.Length>k){
						//	LeadScores[k] = data.Scores[k].formattedValue.ToString();
					//  data.Scores[k].userID;

							int jk = k+1;
							UILabel days = GameObject.Find("LD_Day"+jk).transform.GetChild(1).GetComponent<UILabel>();
					days.text = data.Scores[k].formattedValue.ToString();

//
					UILabel name = GameObject.Find("LD_Day"+jk).transform.GetChild(0).GetComponent<UILabel>();
					name.text =  data.Scores[k].userID.ToString();
				

					//Userids[k] = data.Scores[k].userID.ToString();
						}
						else{
							int jk = k+1;
							UILabel days = GameObject.Find("LD_Day"+jk).transform.GetChild(1).GetComponent<UILabel>();
							days.text = "NULL";

					UILabel name = GameObject.Find("LD_Day"+jk).transform.GetChild(0).GetComponent<UILabel>();
					name.text =  "NULL";

							
						}
						
						
					}


			LoadLeaderboardUsers(Userids);
			Debug.Log (data.Valid);
			UILabel Rank = GameObject.Find("lbl_Rank").GetComponent<UILabel>();
			Rank.text = data.Scores[0].rank.ToString();
			
		//	Rank.text = data.Scores[0].formattedValue.ToString();
			
			
//			for(int i = 0; i<=5; i++){
//				if(data.Scores[i] != null){
//					LeadScores[i] = data.Scores[i].formattedValue.ToString();
//				}}
			
			
			Debug.Log (data.Id);
			UILabel Wins = GameObject.Find("lbl_Wins").GetComponent<UILabel>();
			Wins.text = data.Id.ToString();
			
			Debug.Log (data.PlayerScore);
			UILabel Losses = GameObject.Find("lbl_Losses").GetComponent<UILabel>();
			Losses.text = data.PlayerScore.userID.ToString();
			
			Debug.Log (data.PlayerScore.userID);
//			UILabel TotalCoins = GameObject.Find("lbl_TotalCoins").GetComponent<UILabel>();
//			TotalCoins.text = data.PlayerScore.userID.ToString();
			
					
			
			
			

		
		});
					




		}

				
		public	void LoadSocialScores(){

		ILeaderboard leaderboard =  PlayGamesPlatform.Instance.CreateLeaderboard();


		leaderboard.id = GPGSIDs.leaderboard_fungo_golf_3d;

		//leaderboard.range = new Range(0, 7); // Here we go


		leaderboard.LoadScores( success => {

				
					
//					for(int i = 0; i<=6; i++){
//						
//						
//						if(scores[i].userID != null){
//							LeadScores[i] =scores[i].userID.ToString();
//							
//							int jk = i+1;
//							UILabel days = GameObject.Find("LD_Day"+jk).transform.GetChild(0).GetComponent<UILabel>();
//							days.text = LeadScores[i];
//						}
//						else{
//							int jk = i+1;
//							UILabel days = GameObject.Find("LD_Day"+jk).transform.GetChild(0).GetComponent<UILabel>();
//							days.text = "NULL";
//							
//						}
						
						
						
						
						
						if(success){
						
						
						
						//	Debug.Log ("Got " + scores.Length + " scores");
			UILabel Wins = GameObject.Find("lbl_TotalCoins").GetComponent<UILabel>();
			Wins.text= "l";
						
				Wins.text = leaderboard.scores.Length.ToString();
			}
			else{
				UILabel Wins = GameObject.Find("lbl_TotalCoins").GetComponent<UILabel>();

				Wins.text= "Fail";

			}
//			foreach (IScore score in scores){
//						
//						
//			
//					//Wins.text = data.Id.ToString();
//						
//					Wins.text = "\t"+"Alex" + score.userID + " " + score.formattedValue + " " + score.date + "\n";}
//													
//						
//												
//									
			});


				
		}
	

	
}
