using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball") {
			if (GameManager.Instance.gamestats.Shots > 0) {
				if (GameManager.Instance.isMissionCompleted == false) {

					//GameManager.Instance.gamestats.Shots++;
					GameManager.Instance.isMissionCompleted = true;



					TheGameManager.Instance.currentActivePlayer.playerRound ++;
					Debug.Log ("-------------------------------------------------------------------");
					GameManager.Instance.ConfigureLevelForState (GameManager.GameState.Scores);
					AdsManager.ShowGameStateAd (eGameStates.GAMEPLAY);
					//		SoundManager.Instance.Play_applause();
				}
			}
		}

   
            collision.gameObject.SetActive(true);
            Debug.Log(collision.gameObject.name);
		playSound ();
        
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Character"))
        {
            collision.gameObject.SetActive(false);
        }

    }

	public void playSound(){

		string name = this.gameObject.name;

		int nameInt=int.Parse (name);
		switch(nameInt){
		case 0:  
			break;
		case 1:
			SoundManager.Instance.Play_hole_made_1();
			break;
		case 2:  
			SoundManager.Instance.Play_hole_made_2_attagirl();
			break;
		case 3:
			SoundManager.Instance.Play_hole_made_3();
			break;
		case 4:  
			SoundManager.Instance.Play_hole_made_4();
			break;
		case 5:
			SoundManager.Instance.Play_hole_made_5();
			break;
		case 6:  
			SoundManager.Instance.Play_hole_made_6();
			break;
	

		}

	}
}
