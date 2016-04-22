using UnityEngine;
using System.Collections;

public class SunPowerup : MonoBehaviour {

	GameObject startingGameObject;
	bool isHit;
	// Use this for initialization
	void Start()
	{
		startingGameObject = TheGameController.Instance.target;
		Vector3 endingPosition = new Vector3 (startingGameObject.transform.position.x, startingGameObject.transform.position.y+2.0f, startingGameObject.transform.position.z);
		
		//   Destroy(gameObject, 5);
		transform.position = endingPosition;
	}
}
