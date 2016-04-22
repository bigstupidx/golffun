using UnityEngine;
using System.Collections;

public class UFOPowerUp : MonoBehaviour {

		GameObject ball;
		bool isHit;
		bool hasColloidedPlayer;
		bool hasUFOCaught;
		bool shouldRunUpdate;
		public GameObject ufoBallKeepPosition;
		bool hassoundplayed = false;
	void Start()
		{
			hasUFOCaught = false;
			ball = GameObject.Find("Ball");
			Vector3 startingPosition = TheGameController.Instance.target.transform.position;
			transform.position = new Vector3 (startingPosition.x,transform.position.y, startingPosition.z);
			hasColloidedPlayer = false;
			shouldRunUpdate = true;
		}
		
		void Update() {

		if (shouldRunUpdate == false)
			return;

		float step = 10.0f * Time.deltaTime;

		if (hasColloidedPlayer == false) {

			if (Mathf.Abs (transform.position.z - TheGameController.Instance.meshBat.position.z) < .05f) 
				hasColloidedPlayer = true;
			
			if (hasColloidedPlayer == false)
			if (!isHit && hasUFOCaught == false)
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (ball.transform.position.x, transform.position.y, ball.transform.position.z), step);
			else if (isHit == true && hasUFOCaught == false) {


				if (Vector3.Distance (ball.transform.position, transform.position) > 0.2f) {
					ball.transform.position = Vector3.MoveTowards (ball.transform.position, transform.position, step * 2);
				} else {
					Debug.Log ("less than 0.2");
					ball.GetComponent<Rigidbody> ().useGravity = false;
					hasUFOCaught = true;
					TheGameController.Instance.isUFOEnabled=true;

					if(hassoundplayed==false){
						SoundManager.Instance.Play_UFO();
						hassoundplayed=true;
					}

				}
			} else if (isHit == true && hasUFOCaught == true) {
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (TheGameController.Instance.meshBat.position.x, transform.position.y, TheGameController.Instance.meshBat.position.z), step * 0.5f);
				ball.transform.position = Vector3.MoveTowards (ball.transform.position, ufoBallKeepPosition.transform.position, step);

			}
		} else if (hasColloidedPlayer == true){

				ball.GetComponent<Rigidbody> ().useGravity = true;
				TheGameController.Instance.isUFOEnabled=false;
				Destroy(gameObject,2.0f);
				shouldRunUpdate=false;

				}

		}
		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.name == "Ball")
			{
				if(!isHit)
				{	
				isHit = true;

				}
			}
//		else if (other.gameObject.tag == "Score")
//			hasColloidedPlayer = true;
		}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Ball") {
			if (!isHit)
				isHit = true;


		} else if (other.gameObject.tag == "Score")
			hasColloidedPlayer = true;
	}
}
