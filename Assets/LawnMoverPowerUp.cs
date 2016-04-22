using UnityEngine;
using System.Collections;

public class LawnMoverPowerUp : MonoBehaviour {


	GameObject ball;
	GameObject startingGameObject;
	bool isHit;
	// Use this for initialization
	void Start()
	{
		ball = GameObject.Find("Ball");
		startingGameObject = TheGameController.Instance.target;
		Vector3 endingPosition = new Vector3 (startingGameObject.transform.position.x, startingGameObject.transform.position.y+2.0f, startingGameObject.transform.position.z + 50.0f);

		//   Destroy(gameObject, 5);
		transform.position = endingPosition;

//		transform.LookAt (ball.transform.position);
		transform.localRotation = Quaternion.Euler(new Vector3 (transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 90.0f, transform.localRotation.eulerAngles.z));
//		transform.localRotation.eulerAngles 
	}
	
	// Update is called once per frame
	void Update() {
		float step = 10.0f * Time.deltaTime;
//		transform.LookAt (ball.transform);
//		if(!isHit)
//			transform.Translate (Vector3.forward * 5  *Time.deltaTime);

		if(!isHit)
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.transform.position.x,transform.position.y,ball.transform.position.z), step);
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Ball")
		{
			if(!isHit)
			{	throwballback();
				Destroy(gameObject,2.0f);
			}
		//		Invoke("throwballback",0.25f);

			isHit = true;

//			ball.GetComponent<Rigidbody>().isKinematic = true;
//			ball.GetComponent<Rigidbody>().isKinematic = false;
		//	Destroy(gameObject, 3);
		}
	}

	void throwballback(){
		ball.GetComponent<Rigidbody>().AddForce(TheGameController.Instance.characterHand.position , ForceMode.VelocityChange);
//		ball.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f,0.5f,-1f) *25 , ForceMode.VelocityChange);

	}


}
