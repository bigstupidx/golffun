using UnityEngine;
using System.Collections;

public class CheckGameEndScript : MonoBehaviour {

	int totallevels=3;

	void Start () {
	
		Invoke ("isGameOver", 2.0f);

	}
	

	public void isGameOver(){

		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) {
			if (TheGameManager.Instance.isPlayer1Turn == false) {
				if(TheGameManager.Instance.currentActivePlayer.playerRound>totallevels){}


			}
		}

	}
}
