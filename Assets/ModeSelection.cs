using UnityEngine;
using System.Collections;

public class ModeSelection : BaseMenu {

	public void StartSinglePlayerGame()
	{
		GameManager.Instance.isGame1Player = true;
		GameManager.Instance.ConfigureLevelForState(GameManager.GameState.CharacterSelection);

		
	}
	public void StartMultiplayerPlayerGame()
	{
		GameManager.Instance.isGame1Player = false;
		GameManager.Instance.ConfigureLevelForState(GameManager.GameState.CharacterSelection);

	}
}

