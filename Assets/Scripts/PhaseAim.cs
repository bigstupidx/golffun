using UnityEngine;
using System.Collections;

public class PhaseAim : MonoBehaviour {

    public Transform target;
    public float speedOfRotation;

    private float minSwipeLength = 5;
    private Touch touch;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    
    public bool tapped = false;

    private Vector2 mouseDeltaPos;
    private Vector2 lastPositon;

    private BallFollower ball;

	void Start () {
        ball = transform.parent.GetComponent<BallFollower>();
	}

	// Update is called once per frame
	void Update () {
        if (ball.isBallInFocus) {
            if (TheGameController.Instance.gameState != State.Aiming)
            {
                this.enabled = false;  
            }

            if (Application.isEditor)
            {
                if (Input.GetMouseButton(0))
                {
                    firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    mouseDeltaPos = firstPressPos - lastPositon;
                    lastPositon = firstPressPos;

                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        //Code for action on mouse moving left
                        transform.RotateAround(target.position, Vector3.up, mouseDeltaPos.x * speedOfRotation * Time.deltaTime);
                        TheGameController.Instance.character.transform.RotateAround(target.position, Vector3.up, mouseDeltaPos.x * speedOfRotation * Time.deltaTime);
                    }

                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        //Code for action on mouse moving right
                        transform.RotateAround(target.position, Vector3.up, mouseDeltaPos.x * speedOfRotation * Time.deltaTime);
                        TheGameController.Instance.character.transform.RotateAround(target.position, Vector3.up, mouseDeltaPos.x * speedOfRotation * Time.deltaTime);
                    }

                }

                if (Input.GetMouseButtonUp(0))
                {
                    secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    if (currentSwipe.magnitude < minSwipeLength)
                    {
                        if (!tapped)
                        {
                            TheGameController.Instance.ActivateFlickingPhase();
                            this.enabled = false;
                            tapped = true;
                        }
                    }
                }
            }

            if (Input.touches.Length != 1) return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(touch.position.x, touch.position.y);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                transform.RotateAround(target.position, Vector3.up, touch.deltaPosition.x * speedOfRotation * Time.deltaTime);
                TheGameController.Instance.character.transform.RotateAround(target.position, Vector3.up,  touch.deltaPosition.x * speedOfRotation * Time.deltaTime);
                
                secondPressPos = new Vector2(touch.position.x, touch.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                if (currentSwipe.magnitude < minSwipeLength)
                {

                }


            }

            if (touch.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(touch.position.x, touch.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    if (!tapped)
                    {
                        TheGameController.Instance.ActivateFlickingPhase();
                        tapped = true;
                    }
                }
            }
        }
	}

    void OnEnable()
    {
        tapped = false;
        TheGameController.Instance.ActivateAimingPhase(false);

    }

}
