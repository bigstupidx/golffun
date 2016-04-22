using UnityEngine;
using System.Collections;

public class BallFollower : MonoBehaviour {

    public Transform ball;
    public float followSpeed;
    public float rotationDamping = 0.6f;
    public bool isBallInFocus = true;

    public bool gameStarted = true;

	// Use this for initialization
	void Start () {
        gameStarted = true;
		//transform.localPosition = new Vector3 (transform.position.x, transform.position.y, 0);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // Calculate the current rotation angles
        if (ball != null)
        {

			if(TheGameController.Instance.isUFOEnabled==true)
				transform.position = Vector3.Lerp(transform.position,new Vector3(ball.position.x,ball.position.y,ball.position.z+20.0f), Time.deltaTime * followSpeed);


            transform.position = Vector3.Lerp(transform.position, ball.position, Time.deltaTime * followSpeed);
            if (Vector3.Distance(transform.position, ball.position) <= 0.01f)
            {
                isBallInFocus = true;
            }
            else
            {
                isBallInFocus = false;
            }
        }
	}
    
    public void ResetConfigurations(GameObject camera, Vector3 cameraInitialPos, Quaternion cameraInitialRotation, GameObject target, GameObject character, Vector3 initialPos, Quaternion initialRotation, Transform _follower)
    {

        
        character.transform.parent = camera.transform;

        character.transform.localPosition = initialPos;
        character.transform.localRotation = initialRotation;
        camera.transform.localPosition = cameraInitialPos;
        camera.transform.localRotation = cameraInitialRotation;
        transform.position = _follower.position;

        faceAwayFromObj(transform.gameObject, target);
        transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);
        character.transform.parent = null;
        
        //ball = _follower;
    }

    public void faceAwayFromObj(GameObject actor, GameObject obj)
    {
        Quaternion origRot = actor.transform.rotation;
        actor.transform.LookAt(obj.transform);
        float rotDest = actor.transform.rotation.eulerAngles.y - 180;
        actor.transform.eulerAngles = new Vector3(0, rotDest, 0);
    }
}
