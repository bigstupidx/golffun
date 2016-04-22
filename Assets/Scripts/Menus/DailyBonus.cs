using UnityEngine;
using System.Collections;
using System;

public class DailyBonus : Singleton<DailyBonus> {

	private const string GAME_LAST_OPENED = "DBGLOpened";
	private const string Daily_Bonus_Index = "DBIndex";
	
	public GameObject[] unlockedArrows;
	public GameObject congratulation_popUp;
	public int[] daily_coins;
	public UILabel congratulation_label;

	void Start () 
	{
		initializeDailyLoginBonus();
		IsBonusReady ();
	}

	void initializeDailyLoginBonus()
	{
		DateTime currentTime = DateTime.Now;
		DateTime lastOpened = Convert.ToDateTime(PlayerPrefs.GetString (GAME_LAST_OPENED, currentTime.ToString ()));

		if (lastOpened.ToString().CompareTo(currentTime.ToString()) == 0) 
		{
			saveCurrentTime ();
		} 
	}
	private void saveCurrentTime()
	{
		PlayerPrefs.SetString (GAME_LAST_OPENED, DateTime.Now.ToString ());
		PlayerPrefs.SetInt (Daily_Bonus_Index, 0);	
		PlayerPrefs.Save ();
	}

	public void IsBonusReady()
	{
		DateTime currentTime = DateTime.Now;
		
		DateTime lastOpened = Convert.ToDateTime(PlayerPrefs.GetString (GAME_LAST_OPENED,currentTime.ToString ()));
		
		TimeSpan timeDiff = currentTime.Subtract(lastOpened);
		print (timeDiff.Days);

		if(timeDiff.Days >= 1)
		{
			PlayerPrefs.SetString (GAME_LAST_OPENED,currentTime.ToString ());
			int index = PlayerPrefs.GetInt (Daily_Bonus_Index);
			index++;

			if( index <= unlockedArrows.Length)
			{
				//UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_DAILY_BONUS, Constants.GA_ACTION_TYPE_DAILY_BONUS_UNLOCKS, "Daily Bonus - Day "+ index +" Coins Rewarded", 1);
				UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_DAILYBONUS, Constants.GA_ACTION_TYPE_DAILYBONUSUNLOCKED, "Daily Bonus - Day "+ index +" Coins Rewarded", 1);

				congratulation_label.text = "You have won " + daily_coins[index-1] + " coins";
				congratulation_popUp.SetActive(true);
				congratulation_popUp.transform.parent.GetComponent<MainMenu>().congratulationsPlaceHolder.PlayForward();
				PlayerPrefs.SetInt (Daily_Bonus_Index,index);
//				UpdatePlayerScore(GameManager.Instance.player.GetPlayerScore(),index);
			}
		}

	}
	void UpdatePlayerScore(int score,int index)
	{
		score += daily_coins [index - 1];
//		GameManager.Instance.player.SetPlayerScore(score);
	}

	public void DailyBonusBtnCallBack()
	{

		int index = PlayerPrefs.GetInt(Daily_Bonus_Index);
		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Daily Bonus Button Clicked "+index);

		print (index);
		for( int i = 0 ; i < index ; i++ ) 
		{
			unlockedArrows[i].SetActive(true);
		}

	}
	public void CongratulationOkBtnCallBack()
	{
		congratulation_popUp.GetComponent<TweenPosition> ().PlayReverse ();
	}

	public void CrossBtnCallBack()
	{
		UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON, "Daily Bonus Cross Button Clicked ");


	}

	public void OKBtnCallBack()
	{

	}
}
