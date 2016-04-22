using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour
{
	public bool flag = false;

	public GameObject[] buttons;

	private bool isSoundEnabled = true;


	// Use this for initialization
	void Start ()
	{
		if (AudioListener.volume == 0.0f) {
			buttons [1].GetComponent<UISprite> ().spriteName = "button_sound_off";
		} else {
			buttons [1].GetComponent<UISprite> ().spriteName = "button_sound";

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnMenuButtonPressed ()
	{

		if (flag) {
			foreach (GameObject obj in buttons) {
				obj.GetComponent<TweenPosition> ().PlayReverse ();
				obj.GetComponent<Collider> ().enabled = false;
				flag = false;
				Debug.Log ("true");

			}
		} else {

			foreach (GameObject obj in buttons) {
				obj.GetComponent<TweenPosition> ().PlayForward ();
				obj.GetComponent<Collider> ().enabled = true;
				flag = true;
			}
		}

	}

	public void OnHomeButtonPressed ()
	{
		//MenuManager.Instance.PopMenuToState (GameManager.GameState.MainMenuPanel);
		//SoundManager.Instance.PlayButtonClickSound ();

	}

	public void OnSoundButtonPressed ()
	{
		//SoundManager.Instance.PlayButtonClickSound ();

		if (isSoundEnabled) {
			isSoundEnabled = false;
			buttons [1].GetComponent<UISprite> ().spriteName = "button_sound_off";
			AudioListener.volume = 0.0f;

		} else {
			isSoundEnabled = true;
			buttons [1].GetComponent<UISprite> ().spriteName = "button_sound";
			AudioListener.volume = 1.0f;


		}



	}

	public void OnReplayButtonPressed ()
	{
		//MenuManager.Instance.PopMenuToState (GameManager.GameState.MapPanel);
		//SoundManager.Instance.PlayButtonClickSound ();

	}




}
