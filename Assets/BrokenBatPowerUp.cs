using UnityEngine;
using System.Collections;

public class BrokenBatPowerUp : MonoBehaviour {

	GameObject ball;
	public float forcefactor;
	void Start(){

		ball = GameObject.Find("Ball");
//		transform.LookAt (ball.transform);
		transform.position = ball.transform.position;
//		Destroy(gameObject, 4);
		this.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f,0.5f,-1f) *forcefactor , ForceMode.VelocityChange);
	
	}

}


//public GameObject featherParticles;
//bool isHit;
//// Use this for initialization
//void Start()
//{
//	ball = GameObject.Find("Ball");
//
//
//}
//
//// Update is called once per frame
//void Update() {
//	transform.LookAt (ball.transform);
//	if(!isHit)
//		transform.Translate (Vector3.forward * 50  *Time.deltaTime);
//}
//
//void OnCollisionEnter(Collision other)
//{
//	if (other.gameObject.name == "Ball")
//	{
//		isHit = true;
//		//      Instantiate(featherParticles, transform.position, Quaternion.identity);
//		//       Destroy(featherParticles, 1f);
//		ball.GetComponent<Rigidbody>().isKinematic = true;
//		ball.GetComponent<Rigidbody>().isKinematic = false;
//		Destroy(gameObject, 1);
//	}
//}
//}