using UnityEngine;
using System.Collections;


using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

public class CameraRotation : MonoBehaviour {


	bool mAuthOnStart = true;
	bool mInMatch = false;
	// Use this for initialization
	void Start () {

//		
//		PlayGamesClientConfiguration config = 
//			new PlayGamesClientConfiguration.Builder()
//				.WithInvitationDelegate(OnGotInvitation)
//				.WithMatchDelegate(OnGotMatch)
//				.Build();
//		
//		PlayGamesPlatform.InitializeInstance(config);
//		// recommended for debugging:
//		PlayGamesPlatform.DebugLogEnabled = true;
//		// Activate the Google Play Games platform
//		PlayGamesPlatform.Activate();
//	//	 authenticate user:
//				Social.localUser.Authenticate((bool success) => {
//					// handle success or failure
//				});

	}
	
	// Update is called once per frame
	void Update () {
        //fftransform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, new Vector3 (37.8f, 24.5f, -44.4f), Time.deltaTime);
	}

    public void SwitchScene()
    {
        Application.LoadLevel(1);
    }


	protected void OnGotInvitation(Invitation invitation, bool shouldAutoAccept) {
		if (invitation.InvitationType != Invitation.InvType.TurnBased) {
			// wrong type of invitation!
			return;
		}
		mInMatch = true;
//		gameObject.GetComponent<MainMenuGui>().HandleInvitation(invitation, shouldAutoAccept);
	}
	
	protected void OnGotMatch(TurnBasedMatch match, bool shouldAutoLaunch) {
		mInMatch = true;
//		gameObject.GetComponent<MainMenuGui>().HandleMatchTurn(match, shouldAutoLaunch);
	}

}
