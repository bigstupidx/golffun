using UnityEngine;
using System.Collections;

public class PhaseFlick : MonoBehaviour {

    public Touch touch;
    public GameObject ball;
    public GameObject bat;
    private bool IsFlickEnabled = false;

    private float resultX;
    private float resultY;
    private float duration;
    private float force;
    private float startTime;
	private float endTime;
    private float iPositionX;
    private float iPositionY;
    private float fPositionX;
    private float fPositionY;
    private Vector3 cam_forward;
    private Vector3 cam_left;
    private Vector3 cam_right;
    private Vector3 forwardX;
    private bool firstSwing = true;
    private Transform target;
    private bool isTossed = false;
    void OnEnable()
    {
        this.cam_forward = new Vector3(Camera.main.transform.TransformDirection(Vector3.forward).x, (float)0, Camera.main.transform.TransformDirection(Vector3.forward).z);
        this.cam_left = Camera.main.transform.TransformDirection(Vector3.left);
        this.cam_right = Camera.main.transform.TransformDirection(Vector3.right);
        this.forwardX = Camera.main.transform.TransformDirection(Vector3.forward);
        target = GameObject.FindObjectOfType<BallCollision>().transform;

        //IsFlickEnabled = true;
        //TheGameController.Instance.ThrowBallCharacter();
    }

	// Update is called once per frame
	void Update () {

        if (TheGameController.Instance.gameState != State.Shooting)
        {
            this.enabled = false;
        }

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0)) {
                this.iPositionX = Input.mousePosition.x;
                this.iPositionY = Input.mousePosition.y;
                this.startTime = Time.time;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                
            }

            if (Input.GetMouseButton(0))
            {

            }

            if (Input.GetMouseButtonUp(0))
            {
                this.endTime = Time.time;
                this.fPositionX = Input.mousePosition.x;
                this.fPositionY = Input.mousePosition.y;
                this.resultX = this.fPositionX - this.iPositionX;
                this.resultY = this.fPositionY - this.iPositionY;
                this.CalculateSwipeSpeed();
                if (this.force < 0f)
                {
                    return;
                }
                if (isTossed == false)
                {
                    isTossed = true;
                    TheGameController.Instance.TossBall();
                    Invoke("CheckIfTossed", 1.8f);
                }
                else if (IsFlickEnabled == false)
                {
                    if (this.force > 1f)
                    {
                        IsFlickEnabled = true;
                        ball.GetComponent<Rigidbody>().isKinematic = false;
                        CancelInvoke("CheckIfTossed");
                        CancelInvoke("NotTossed");
                        CancelInvoke("SetPosition");
                        CancelInvoke("HitTheBall");
                        Invoke("HitTheBall", 0.8f);
                        if (Vector3.Distance(target.position, ball.transform.position) > 15f)
                        {
                            TheGameController.Instance.SwingCharacter();
                        }
                        else
                        {
                            TheGameController.Instance.PuttBall();
                        }
                    }
                }
            }
        }

        if (Input.touches.Length != 1) return;

        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            this.iPositionX = Input.mousePosition.x;
            this.iPositionY = Input.mousePosition.y;
            this.startTime = Time.time;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
       
        if (touch.phase == TouchPhase.Moved)
        {

        }

        if (touch.phase == TouchPhase.Ended)
        {
            this.endTime = Time.time;
            this.fPositionX = Input.mousePosition.x;
            this.fPositionY = Input.mousePosition.y;
            this.resultX = this.fPositionX - this.iPositionX;
            this.resultY = this.fPositionY - this.iPositionY;
            this.CalculateSwipeSpeed();
            if (this.force < 0f)
            {
                return;
            }
            if (isTossed == false)
            {
                isTossed = true;
                TheGameController.Instance.TossBall();
                Invoke("CheckIfTossed", 1.8f);
            }
            else if (IsFlickEnabled == false)
            {
                if (this.force > 1f)
                {
                    IsFlickEnabled = true;
                    ball.GetComponent<Rigidbody>().isKinematic = false;
                    CancelInvoke("CheckIfTossed");
                    CancelInvoke("NotTossed");
                    CancelInvoke("SetPosition");
                    CancelInvoke("HitTheBall");
                    Invoke("HitTheBall", 0.5f);
                    if (Vector3.Distance(target.position, ball.transform.position) > 15f)
                    {
                        TheGameController.Instance.SwingCharacter();
                    }
                    else {
                        TheGameController.Instance.PuttBall();
                    }
                }
            }
        }
	}

    void CheckIfTossed()
    {
      if (!IsFlickEnabled)
      {
          SetBallPosition(GameObject.FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<ScreenFader>().fadeSpeed);
          TheGameController.Instance.IdleCharacter();
          Invoke("NotTossed", 0.5f); 
      }
    }
    void SetBallPosition(float delay)
    {
        Invoke("SetPosition", delay); 
        TheGameController.Instance.ActivateAimingPhase(false);
    }

    void SetPosition() {

        ball.transform.position = TheGameController.Instance.characterHand.position;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.parent = TheGameController.Instance.characterHand;
    }
    void NotTossed() {
        
        isTossed = false;
        IsFlickEnabled = false;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        //TheGameController.Instance.Flash();
    }

    void HitTheBall() {

        if (IsFlickEnabled)
        {
            if (this.force > 0f)
            {
                ball.GetComponent<Rigidbody>().useGravity = true;
                if (firstSwing)
                {
                    firstSwing = false;
                    ball.GetComponent<Rigidbody>().AddForce(this.force * this.cam_forward.x * (float)100, this.force * (float)50, this.force * this.cam_forward.z * (float)100);
                    Invoke("ActivateRollingPhase", 1f);
                }
                else
                {
                    ball.GetComponent<Rigidbody>().AddForce(this.force * this.cam_forward.x * (float)100, this.force * (float)10, this.force * this.cam_forward.z * (float)100);
                    Invoke("ActivateRollingPhase", 1f);
                }
                IsFlickEnabled = false;
            }
            else
            {
                TheGameController.Instance.ActivateAimingPhase(false);
            }
        }

    }

    void ActivateRollingPhase()
    {
       
            TheGameController.Instance.ActivateRollingPhase();
           // GameManager.Instance.gameStats.Shots++;
        
    }

    public  void CalculateSwipeSpeed()
    {
        this.duration = this.endTime - this.startTime;
        this.force = this.resultY / (this.duration * (float)70);
        if (this.force > 30f)
            this.force = 30f;
        Debug.Log("duration : " + this.duration);
        Debug.Log("force : " + this.force);
    }

}
