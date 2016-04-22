using UnityEngine;
using System.Collections;

public class PhaseRoll : MonoBehaviour {

	// Use this for initialization
    public GameObject ball;
    public float minSpeed;

    public float lateralForce;

    private Touch touch;

    private float firstX;
    private float secondX;

    private bool ready = false;
    private Rigidbody rgbdyBall;

    public float maxRollTime = 2;
   

    float timeHeldDown = 0;
    float startTime = 0;

    public float TimeHeldDown
    {
        get { return timeHeldDown; }
        internal set { timeHeldDown = value; }
    }
    public float StarTime
    {
        get { return startTime; }
    }
	void Start () {
        rgbdyBall = ball.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        if (TheGameController.Instance.gameState != State.Rolling)
        {
            this.enabled = false;
        }

        if ((rgbdyBall.velocity.magnitude < minSpeed)&&(ready))
        {
            rgbdyBall.velocity = Vector3.zero;
            rgbdyBall.angularVelocity = Vector3.zero;
            TheGameController.Instance.ActivateAimingPhase(true);
        }

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstX = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(0))
            {
                TimeHeldDown = Mathf.Max(0, Time.time - StarTime);
                if (TimeHeldDown >= maxRollTime)
                {
                    rgbdyBall.velocity = Vector3.zero;
                    rgbdyBall.angularVelocity = Vector3.zero;
                    TheGameController.Instance.ActivateAimingPhase(true);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                secondX = Input.mousePosition.x; ;
                if ((rgbdyBall.velocity.magnitude> minSpeed) && (ready))
                {
                    if (firstX > secondX)
                    {
                        rgbdyBall.AddForce(-transform.right * lateralForce);
                    }
                    else if (secondX > firstX)
                    {
                        rgbdyBall.AddForce(transform.right * lateralForce);
                    }
                }
            }
        }

        if (Input.touches.Length != 1) return;
        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            firstX = touch.position.x;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            TimeHeldDown = Mathf.Max(0, Time.time - StarTime);
            if (TimeHeldDown >= maxRollTime)
            {
                rgbdyBall.velocity = Vector3.zero;
                rgbdyBall.angularVelocity = Vector3.zero;
                TheGameController.Instance.ActivateAimingPhase(true);
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            secondX = touch.position.x;

            if (firstX > secondX)
            {
                rgbdyBall.AddForce(-transform.right*lateralForce);
            }
            else if (secondX > firstX)
            {
                rgbdyBall.AddForce(transform.right*lateralForce);
            }
        }
	}

    void OnEnable()
    {
        StartCoroutine(wait1S());
        startTime = Time.time;
    }

    void OnDisable()
    {
        ready = false;
    }

    private IEnumerator wait1S()
    {
        yield return new WaitForSeconds(1f);
        ready = true;
    }
    
}
