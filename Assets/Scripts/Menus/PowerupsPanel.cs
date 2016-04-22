using UnityEngine;
using System.Collections;

public class PowerupsPanel : MonoBehaviour {
	public PowerUp[] powerUps;
	public GameObject availablePowerUpsPanel;
	// Use this for initialization

	public void PowerUpCallBack(PowerUp sender)
	{

//		StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
		if (sender.quantity <= 0) {
			//Intsantiate
			GameObject availablePowUpClone = Instantiate(Resources.Load("AvailablePowerup")) as GameObject;
			availablePowUpClone.transform.SetParent(availablePowerUpsPanel.transform);
			availablePowUpClone.transform.localPosition = new Vector3(((availablePowerUpsPanel.transform.childCount - 1) * 100), 0, 0);
			availablePowUpClone.transform.localScale = Vector3.one;
			availablePowUpClone.transform.GetChild(0).GetComponent<UISprite>().spriteName = sender.transform.GetChild(0).GetComponent<UISprite>().spriteName;
			availablePowUpClone.GetComponent<AvailablePowerUp>().myID = sender.id;
			sender.relatedAvailablGO = availablePowUpClone;
			sender.quantity++;

		} else {
			sender.quantity++;
			sender.relatedAvailablGO.transform.GetChild(1).GetComponent<UILabel>().text = "x" + sender.quantity;

		}

		if (sender.id == 1)
			Invoke ("changeBatAfterInerval", 0.25f);
		else if (sender.id == 4)
//			Instantiate(Resources.Load("PowerUps/SunPowerUp"), new Vector3(target.transform.position.x,target.transform.position.y + 20f,target.transform.position.z), Quaternion.identity);
//			TheGameController.Instance.meshBat.root.localRotation=;
			Instantiate(Resources.Load("PowerUps/SunPowerUp"));
//		else if (sender.id == 5)
//			Instantiate(Resources.Load("PowerUps/LawnMowerPowerUp"));
	}

	public void changeBatAfterInerval (){

		TheGameController.Instance.changeBat();

	}


}
