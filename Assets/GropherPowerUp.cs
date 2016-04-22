using UnityEngine;
using System.Collections;

public class GropherPowerUp : MonoBehaviour {
	

	GameObject ball;
	bool isHit;
	Vector3 destination;
	bool hasReachedDestination;
	bool isComingOutOfGround;
	bool isEmerging;
	public Collider boxColloider;

	void Start()
	{
		hasReachedDestination = false;
		ball = GameObject.Find ("Ball");
		destination = new Vector3 (-30.0f, 0.0f, 0.15f);
		hasReachedDestination = false;
		isComingOutOfGround = false;
		isEmerging = true;
		gameObject.transform.localPosition = new Vector3 (0.0f, -1.0f, 0.0f);
		Invoke("endEmerging",0.90f);
		boxColloider.enabled =false;
	}
	
	void Update() {
	
		float step = 20.0f * Time.deltaTime;
//
		if (isEmerging == true) {

			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y+0.1f, transform.position.z), step);

			return;
		}
		if (!isHit)
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (ball.transform.position.x, transform.position.y, ball.transform.position.z), step);
		else if (isHit && hasReachedDestination==false) {

			ball.SetActive(false);
			Invoke("throwballback",1.0f);
			hasReachedDestination = true;

		}else if(isComingOutOfGround==true)
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y+0.02f, transform.position.z), step);
			Invoke ("stopGropher", 1.0f);

	}
	
	void OnCollisionEnter(Collision other)
	{				if (other.gameObject.name == "Ball")
				{
					if(!isHit)
					{	
						isHit = true;
					}
		
				}
	}

	public void endEmerging(){
		isEmerging = false;
		boxColloider.enabled =true;

	}

	void throwballback(){

		Invoke("showball",0.5f);
		transform.position = destination;
		ball.transform.position=transform.position;
		isComingOutOfGround=true;


		
	}
	void  stopGropher(){

		isComingOutOfGround = false;
		Destroy(gameObject,2.0f);

	}
	void showball(){

		ball.SetActive(true);
		ball.transform.position=transform.position;
	}
	
	
}

