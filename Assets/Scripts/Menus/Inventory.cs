using UnityEngine;
using System.Collections;

public class Inventory : BaseMenu
{
    public UIButton buttonOk;
	// Use this for initialization
	void Start () {
        buttonOk.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void StartGame()
    {
        // GameManager.Instance.ConfigureLevelForState(GameManager.GameState.Inventory);
        TheGameManager.Instance.StartGameMode(TheGameManager.GameMode.Solo);
        GameManager.Instance.gameStats.TotalGames++;
        GameManager.Instance.ConfigureLevelForState(GameManager.GameState.HUD);
        GameManager.Instance.gameStats.ResetGame();
    }
}
