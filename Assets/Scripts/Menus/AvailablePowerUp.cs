using UnityEngine;
using System.Collections;

public class AvailablePowerUp : MonoBehaviour {
	public int myID;
	// Use this for initialization


	void OnClick()
	{
        Time.timeScale = 1;
		Time.timeScale = 1;
		TheGameController.Instance.selectedPowerUpID = myID;
		foreach (PowerUp pow in transform.parent.parent.parent.parent.parent.gameObject.GetComponent<PowerupsPanel>().powerUps) {
			if(myID == pow.id)
			{
				pow.quantity--;
				transform.GetChild(1).GetComponent<UILabel>().text = "x" + pow.quantity;
				if(pow.quantity < 1)
				{
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON,"Power up recieved ID =" + myID);

					transform.parent.parent.parent.parent.parent.gameObject.SetActive (false);
					transform.parent.GetComponent<UIGrid>().repositionNow = true;
					transform.parent.GetComponent<UIGrid>().Reposition();
					Destroy(gameObject);
				}

			}
		}

		transform.parent.parent.parent.parent.parent.gameObject.SetActive (false);
	}
}
