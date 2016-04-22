using UnityEngine;
using System.Collections;

public class QuitPopup : BaseMenu {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartBtnCallBack()
	{
		MenuManager.Instance.PopMenu ();
		GameManager.Instance.ConfigureLevelForState (GameManager.GameState.MainMenu);
	}

	public void ContinueBtnCallBack()
	{
		MenuManager.Instance.PopMenu ();
	}

	public void QuitBtnCallBack()
	{
		MenuManager.Instance.PopMenu ();
		//MenuManager.Instance.PopMenu ();
		if (MenuManager.Instance.NavigationStackPeek () == GameManager.GameState.CharacterSelection) {
			GameManager.Instance.ConfigureLevelForState (GameManager.GameState.MainMenu);
		}
		else if (MenuManager.Instance.NavigationStackPeek () == GameManager.GameState.Inventory) {
			GameManager.Instance.ConfigureLevelForState (GameManager.GameState.CharacterSelection);
		}
		else if (MenuManager.Instance.NavigationStackPeek () == GameManager.GameState.HUD) {
			MenuManager.Instance.PopMenu ();
			GameManager.Instance.ConfigureLevelForState (GameManager.GameState.CharacterSelection);
		}

	}
}
