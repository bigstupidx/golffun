using UnityEngine;
using System.Collections;

public class MainMenuQuitPanel : BaseMenu {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ContinueBtnCallBack()
	{
		MenuManager.Instance.PopMenu();
	}

	public void QuitBtnCallBack()
	{
		Application.Quit ();
	}
}
