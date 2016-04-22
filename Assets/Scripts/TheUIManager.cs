using UnityEngine;
using System.Collections;

public class TheUIManager : MonoBehaviour {
	

    public void StartGame()
    {
        //GameManager.Instance.ConfigureLevelForState(GameManager.GameState.Inventory);

		if (GameManager.Instance.isGame1Player == true) {
			//	TheGameManager.Instance.StartGameMode(TheGameManager.GameMode.Solo);

			TheGameManager.Instance.StartGameMode (TheGameManager.GameMode.Fungo);
			TheGameManager.Instance.isGameTurnByTurn = false;
		} else {
			TheGameManager.Instance.StartGameMode (TheGameManager.GameMode.Fungo);
			TheGameManager.Instance.isGameTurnByTurn = true;
		}

        GameManager.Instance.gameStats.TotalGames++;
        GameManager.Instance.ConfigureLevelForState(GameManager.GameState.HUD);
        GameManager.Instance.gameStats.ResetGame();
    }

   


}
