using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchControls : MonoBehaviour
{
	
    private float minSwipeLength = 2.50f;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 firstPressPosForAim;
    private Vector2 firstPressPosForPutt;
    
    private Vector2 currentSwipe;
    private Vector2 currentSwipeForAim;
    private Vector2 currentSwipeForPutt;


    private Vector2 secondPressPosForPutt;
    private Vector2 lastPressPositonPutt;

    /** Flick Below **/

    public Transform ball;
    public float speedOfRotation;
    
    public Touch t;
    private float force;

    private float startTime;
    private float endTime;
    private float resultX;
    private float resultY;
    private bool isTossed = false;

    private bool IsFlickEnabled = false;
    private float duration;
    private bool firstSwing = true;
    private Vector3 cam_forward;
    /** Aim Below **/

    private Vector2 mouseDeltaPos;
    private Vector2 lastPressPositon;
    public bool tapped = false;


    /** Roll Below **/

    float timeHeldDown = 0;
    float startTimeRoll = 0;
    private Rigidbody rgbdyBall;
    private bool ready = false;
    public float maxRollTime = 2;
    private float firstX;
    private float secondX;
    public float minSpeed;
    public float lateralForce;
    public Transform target;
    public Camera nguiCam;

    public bool isEnabled = true;

    public float TimeHeldDown
    {
        get { return timeHeldDown; }
        internal set { timeHeldDown = value; }
    }
    public float StarTime
    {
        get { return startTimeRoll; }
    }
	void Start () {
		
        rgbdyBall = ball.GetComponent<Rigidbody>();

        nguiCam = Camera.main.GetComponentInChildren<Camera>();

        

	}

    void Update()
    {
        DetectSwipe();


//		if (TheGameManager.Instance.gameMode == TheGameManager.GameMode.Fungo) 
//			if(TheGameManager.Instance.isPlayer1Turn==true)

    }

    void OnEnable()
    {
        tapped = false;


    }

    void DetectSwipe()
    {
        if (nguiCam != null)
        {
            Ray inputRay;
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("UI");
//            Debug.Log(layerMask);
            // pos is the Vector3 representing the screen position of the input
            if (Input.GetMouseButtonDown(0))
            {
                inputRay = MenuManager.Instance.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(inputRay.origin, inputRay.direction, out hit, Mathf.Infinity, layerMask))
                {

                    Debug.Log("Clicking on UI Element" + hit.collider.name);
                    return;// UI was hit, so don't allow this input to fall through to the gameplay input handler
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                inputRay = MenuManager.Instance.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(inputRay.origin, inputRay.direction, out hit, Mathf.Infinity, layerMask))
                {
					UniversalAnalytics.LogEvent (Constants.GA_CATEGORY_TYPE_GAMEPLAY, Constants.GA_ACTION_TYPE_BUTTON,"Clicked  -" + hit.collider.name);

                    Debug.Log("Clicking on UI Element" + hit.collider.name);
                    return;// UI was hit, so don't allow this input to fall through to the gameplay input handler
                }
            }
           
        }
        if (isEnabled == false) {
            return;
        }
        
		if (TheGameController.Instance.gameState == State.Rolling)
		{
			TimeHeldDown = Mathf.Max(0, Time.time - StarTime);
			if (TimeHeldDown >= maxRollTime)
			{
				rgbdyBall.velocity = Vector3.zero;
				rgbdyBall.angularVelocity = Vector3.zero;
				ActivateAmingPhase(true);
				
			}
		}

        if (Input.GetMouseButton(0))
        {
            if (TheGameController.Instance.gameState == State.Aiming)
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				Debug.Log ("last press position for AIM "+ lastPressPositon.x+ "y"+lastPressPositon.y);

                Aim();
            }
            else if (TheGameController.Instance.gameState == State.Rolling)
            {
                TimeHeldDown = Mathf.Max(0, Time.time - StarTime);
                if (TimeHeldDown >= maxRollTime)
                {
                    rgbdyBall.velocity = Vector3.zero;
                    rgbdyBall.angularVelocity = Vector3.zero;
                    ActivateAmingPhase(true);
                    
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (TheGameController.Instance.gameState == State.Shooting)
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                firstPressPosForPutt = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				Debug.Log ("first Press Pos For Putt  "+ firstPressPosForPutt.x+ "y"+firstPressPosForPutt.y);


                this.startTime = Time.time;
                rgbdyBall.isKinematic = false;
            }
            else if (TheGameController.Instance.gameState == State.Rolling)
            {
                firstX = Input.mousePosition.x;
            }
            else if (TheGameController.Instance.gameState == State.Aiming)
            {
                firstPressPosForAim = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				Debug.Log ("first Press Pos For Aim  "+ firstPressPosForAim.x+ "y"+firstPressPosForAim.y);
                mouseDeltaPos = firstPressPosForAim - lastPressPositon;
                lastPressPositon = firstPressPosForAim;
                this.startTime = Time.time;

				Debug.Log ("last press position " + lastPressPositon.x+ "y"+lastPressPositon.y);
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            if (TheGameController.Instance.gameState == State.Aiming)
            {
                currentSwipeForAim = new Vector3(secondPressPos.x - firstPressPosForAim.x, secondPressPos.y - firstPressPosForAim.y);
                if (currentSwipeForAim.magnitude < minSwipeLength)
                {
                    if (!tapped)
                    {
                        ActivateFlickPhase();
                        tapped = true;
                        if (firstSwing)
                        {
                            TheGameController.Instance.ActivateTossPhase();
                            isTossed = true;
                            TheGameController.Instance.TossBall();
                            Invoke("CheckIfTossed", 1.8f);
                            TheGameController.Instance.ActivateFlickingPhase();
                        }
                        
                        //ShootChecker();
                    }
                    return;
                }
            }
            else if (TheGameController.Instance.gameState == State.Shooting)
            {
                ShootChecker();
            }
            else if (TheGameController.Instance.gameState == State.Rolling){

                secondX = Input.mousePosition.x; ;
                if ((rgbdyBall.velocity.magnitude > minSpeed) && (ready))
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

            //Make sure it was a legit swipe, not a click
            currentSwipe.Normalize();
            currentSwipeForAim.Normalize();
            //Swipeup
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                //dosomething
            }
            //Swipedown
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                //dosomething
            }
            //Swipeleft
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                //dosomething
            }
            //Swiperight
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                //dosomething
            }
        }
        
    }


	public void autoAim(){

			//secondPressPosForPutt = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			secondPressPosForPutt = firstPressPos;
			currentSwipeForPutt = new Vector3(secondPressPos.x - firstPressPosForAim.x, secondPressPos.y - firstPressPosForAim.y);
			
			this.endTime = Time.time;
		//	secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			secondPressPos = firstPressPos;
			this.resultX = this.secondPressPos.x - this.firstPressPosForAim.x;
			this.resultY = this.secondPressPos.y - this.firstPressPosForAim.y;
			//this.CalculateSwipeSpeed();
				this.force = 30f;
			if (Mathf.Abs(currentSwipeForPutt.x) < minSwipeLength )
			{
				if (Mathf.Abs(currentSwipeForPutt.y) >= Mathf.Abs(currentSwipeForPutt.x))
				{
					
						if (this.force < 0f)
						{
							TheGameController.Instance.IdleCharacter();
							return;
						}
						ActivateFlickPhase();
						HitTheBall();
						TheGameController.Instance.PuttBall();
						return;
						
					}
				
				
			}

			
			
		}


    void ShootChecker(){
		//SoundManager.Instance.Play_afterHit ();
		//add swipe positions here
        this.endTime = Time.time;
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        this.resultX = this.secondPressPos.x - this.firstPressPos.x;
        this.resultY = this.secondPressPos.y - this.firstPressPos.y;
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
            TheGameController.Instance.ActivateFlickingPhase();
        }
        else if (IsFlickEnabled == false)
        {
            if (this.force > 1f)
            {
                IsFlickEnabled = true;
                rgbdyBall.isKinematic = false;
                CancelInvoke("CheckIfTossed");
                CancelInvoke("NotTossed");
                CancelInvoke("SetPosition");
                CancelInvoke("HitTheBall");
                Invoke("HitTheBall", 0.8f);
                if (Vector3.Distance(ball.position, target.position) > 15f)
                {

//
//					Debug.Log("this.secondPressPosition.y "+this.secondPressPos.y);
					Debug.Log("meshBat.transform.position.y "+TheGameController.Instance.meshBat.transform.position.y);
//					Debug.Log("TheGameController.Instance.character.transform.position.y "+TheGameController.Instance.character.transform.position.y);
//
					Debug.Log("ball.y "+ball.position.y);

					if((this.firstPressPos.y<this.secondPressPos.y) && (ball.position.y>TheGameController.Instance.meshBat.transform.position.y)){

						TheGameController.Instance.SwingCharacter();
						Debug.Log("Bat ---- Swing");

					}
					else{
						TheGameController.Instance.PuttBall();
						Debug.Log("Bat ---- Putt");

					}
                }
                else
                {
                    TheGameController.Instance.PuttBall();
                }
            }
        }
    }



	public void playAutoShot(){

			if (TheGameController.Instance.gameState == State.Aiming)
			{
				//firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				firstPressPos = new Vector2(550.0f,550.0f);
				Debug.Log ("last press position for AIM "+ lastPressPositon.x+ "y"+lastPressPositon.y);
				
				autoAim();
			}
			else if (TheGameController.Instance.gameState == State.Rolling)
			{
				TimeHeldDown = Mathf.Max(0, Time.time - StarTime);
				if (TimeHeldDown >= maxRollTime)
				{
					rgbdyBall.velocity = Vector3.zero;
					rgbdyBall.angularVelocity = Vector3.zero;
					ActivateAmingPhase(true);
					
				}
			}

			if (TheGameController.Instance.gameState == State.Shooting)
			{ // add swipe positions here 
				firstPressPos=new Vector2(600.0f,500.0f);
				//firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				//firstPressPosForPutt = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				firstPressPosForPutt=firstPressPos;
				Debug.Log ("first Press Pos For Putt  "+ firstPressPosForPutt.x+ "y"+firstPressPosForPutt.y);
				
				
				this.startTime = Time.time;
				rgbdyBall.isKinematic = false;
			}
			else if (TheGameController.Instance.gameState == State.Rolling)
			{
			//	firstX = Input.mousePosition.x;
				firstX=600.0f;
			}
			else if (TheGameController.Instance.gameState == State.Aiming)
			{
				//firstPressPosForAim = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				firstPressPosForAim= new Vector2(600.0f,500.0f);	
				Debug.Log ("first Press Pos For Aim  "+ firstPressPosForAim.x+ "y"+firstPressPosForAim.y);
				mouseDeltaPos = firstPressPosForAim - lastPressPositon;
				lastPressPositon = firstPressPosForAim;
				this.startTime = Time.time;
				
				Debug.Log ("last press position "+ lastPressPositon.x+ "y"+lastPressPositon.y);
			}
			

			//secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			secondPressPos=new Vector2(1000.0f,500.0f);
			currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			if (TheGameController.Instance.gameState == State.Aiming)
			{
				currentSwipeForAim = new Vector3(secondPressPos.x - firstPressPosForAim.x, secondPressPos.y - firstPressPosForAim.y);
				if (currentSwipeForAim.magnitude < minSwipeLength)
				{
					if (!tapped)
					{
						ActivateFlickPhase();
						tapped = true;
						if (firstSwing)
						{
							TheGameController.Instance.ActivateTossPhase();
							isTossed = true;
							TheGameController.Instance.TossBall();
							Invoke("CheckIfTossed", 1.8f);
							TheGameController.Instance.ActivateFlickingPhase();
						}
						
						//ShootChecker();
					}
					return;
				}
			}
			else if (TheGameController.Instance.gameState == State.Shooting)
			{
				autoShootChecker();
			}
			else if (TheGameController.Instance.gameState == State.Rolling){
				
				//secondX = Input.mousePosition.x; ;
				//secondX =new Vector2(700.0f,500.0f);
				secondX=700.0f;
				if ((rgbdyBall.velocity.magnitude > minSpeed) && (ready))
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
			
			//Make sure it was a legit swipe, not a click
			currentSwipe.Normalize();
			currentSwipeForAim.Normalize();
			//Swipeup
			if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				//dosomething
			}
			//Swipedown
			else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				//dosomething
			}
			//Swipeleft
			else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				//dosomething
			}
			//Swiperight
			else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				//dosomething
			}
		
	}

	void autoShootChecker(){
		//SoundManager.Instance.Play_afterHit ();
		//add swipe positions here
		this.endTime = Time.time;
		//secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		secondPressPos = new Vector2(750.0f,500.0f);

		this.resultX = this.secondPressPos.x - this.firstPressPos.x;
		this.resultY = this.secondPressPos.y - this.firstPressPos.y;
		//this.CalculateSwipeSpeed();
		this.force = 30.0f;
		if (this.force < 0f)
		{
			return;
		}
		if (isTossed == false)
		{
			isTossed = true;
			TheGameController.Instance.TossBall();
			Invoke("CheckIfTossed", 1.8f);
			TheGameController.Instance.ActivateFlickingPhase();
		}
		else if (IsFlickEnabled == false)
		{
			if (this.force > 1f)
			{
				IsFlickEnabled = true;
				rgbdyBall.isKinematic = false;
				CancelInvoke("CheckIfTossed");
				CancelInvoke("NotTossed");
				CancelInvoke("SetPosition");
				CancelInvoke("HitTheBall");
				Invoke("HitTheBall", 0.8f);
				if (Vector3.Distance(ball.position, target.position) > 15f)
				{
					
					//
					//					Debug.Log("this.secondPressPosition.y "+this.secondPressPos.y);
					Debug.Log("meshBat.transform.position.y "+TheGameController.Instance.meshBat.transform.position.y);
					//					Debug.Log("TheGameController.Instance.character.transform.position.y "+TheGameController.Instance.character.transform.position.y);
					//
					Debug.Log("ball.y "+ball.position.y);
					
					if((this.firstPressPos.y<this.secondPressPos.y) && (ball.position.y>TheGameController.Instance.meshBat.transform.position.y)){
						
						TheGameController.Instance.SwingCharacter();
						Debug.Log("Bat ---- Swing");
						
					}
					else{
						TheGameController.Instance.PuttBall();
						Debug.Log("Bat ---- Putt");
						
					}
				}
				else
				{
					TheGameController.Instance.PuttBall();
				}
			}
		}
	}

    void Aim() {

        secondPressPosForPutt = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        currentSwipeForPutt = new Vector3(secondPressPos.x - firstPressPosForAim.x, secondPressPos.y - firstPressPosForAim.y);

        this.endTime = Time.time;
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        this.resultX = this.secondPressPos.x - this.firstPressPosForAim.x;
        this.resultY = this.secondPressPos.y - this.firstPressPosForAim.y;
        this.CalculateSwipeSpeed();

        if (Mathf.Abs(currentSwipeForPutt.x) < minSwipeLength )
        {
            if (Mathf.Abs(currentSwipeForPutt.y) > Mathf.Abs(currentSwipeForPutt.x))
            {
                if (firstSwing == false)
                {
                    if (this.force < 0f)
                    {
                        TheGameController.Instance.IdleCharacter();
                        return;
                    }
                    ActivateFlickPhase();
                    HitTheBall();
                    TheGameController.Instance.PuttBall();
                    return;

                }
            }
            else {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    //Code for action on mouse moving left
                    transform.RotateAround(ball.position, Vector3.up, -speedOfRotation * Time.deltaTime);
                    TheGameController.Instance.character.transform.RotateAround(ball.position, Vector3.up, -speedOfRotation * Time.deltaTime);
                }

                if (Input.GetAxis("Mouse X") > 0)
                {
                    //Code for action on mouse moving right
                    transform.RotateAround(ball.position, Vector3.up, speedOfRotation * Time.deltaTime);
                    TheGameController.Instance.character.transform.RotateAround(ball.position, Vector3.up, speedOfRotation * Time.deltaTime);
                }
            }
        }
        else {

            if (Input.GetAxis("Mouse X") < 0)
            {
                //Code for action on mouse moving left
                transform.RotateAround(ball.position, Vector3.up, -speedOfRotation * Time.deltaTime);
                TheGameController.Instance.character.transform.RotateAround(ball.position, Vector3.up, -speedOfRotation * Time.deltaTime);
            }

            if (Input.GetAxis("Mouse X") > 0)
            {
                //Code for action on mouse moving right
                transform.RotateAround(ball.position, Vector3.up, speedOfRotation * Time.deltaTime);
                TheGameController.Instance.character.transform.RotateAround(ball.position, Vector3.up, speedOfRotation * Time.deltaTime);
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
        ActivateAmingPhase(false);
        
        
    }

    void SetPosition()
    {

        ball.transform.position = TheGameController.Instance.characterHand.position;
        rgbdyBall.useGravity = false;
        rgbdyBall.isKinematic = true;
        ball.transform.parent = TheGameController.Instance.characterHand;
    }
    void NotTossed()
    {

        isTossed = false;
        IsFlickEnabled = false;
        rgbdyBall.isKinematic = false;
        //TheGameController.Instance.Flash();
    }

    void HitTheBall()
    {

        if (IsFlickEnabled)
        {
            if (this.force > 0f)
            {
				GameManager.Instance.gameStats.Shots++;
                IsFlickEnabled = false;
                rgbdyBall.useGravity = true;

				InvokeSelectedPowerUp();


                if (firstSwing)
                {
                    firstSwing = false;
  //                  rgbdyBall.AddForce(this.force * this.cam_forward.x * (float)100, this.force * (float)50, this.force * this.cam_forward.z * (float)100);
                    Invoke("ActivateRollingPhase", 1f);



					if((this.firstPressPos.y<this.secondPressPos.y) && (ball.position.y>TheGameController.Instance.meshBat.transform.position.y+0.15)){
						
						//						TheGameController.Instance.SwingCharacter();
						Debug.Log("Bat Hit The Ball ---- Swing");
						rgbdyBall.AddForce(this.force * this.cam_forward.x * (float)70, this.force * (float)50, this.force * this.cam_forward.z * (float)100);
						
						
					}
					else if((this.firstPressPos.y<this.secondPressPos.y) && (ball.position.y>TheGameController.Instance.meshBat.transform.position.y-0.5)){
						if(TheGameController.Instance.selectedPowerUpID==1) this.force=30.0f;

//						TheGameController.Instance.SwingCharacter();
						Debug.Log("Bat Hit The Ball ---- Swing");
						rgbdyBall.AddForce(this.force * this.cam_forward.x * (float)100, this.force * (float)70, this.force * this.cam_forward.z * (float)100);

					}
					else{
//						TheGameController.Instance.PuttBall();
						if(TheGameController.Instance.selectedPowerUpID==1) this.force=50.0f;
						rgbdyBall.AddForce(this.force * this.cam_forward.x * (float)100, this.force * (float)10, this.force * this.cam_forward.z * (float)100);
						Debug.Log("Bat Hit The Ball ---- Putt");
						
					}
                }
                else
                {
                    rgbdyBall.AddForce(this.force * this.cam_forward.x * (float)100, this.force * (float)10, this.force * this.cam_forward.z * (float)100);
                    Invoke("ActivateRollingPhase", 1f);
                }
                isEnabled = false;

            }
            else
            {
                ActivateAmingPhase(false);
            }
        }

    }

    void ActivateFlickPhase() {
        IsFlickEnabled = false;
        TheGameController.Instance.ActivateFlickingPhase();
        this.cam_forward = new Vector3(Camera.main.transform.TransformDirection(Vector3.forward).x, (float)0, Camera.main.transform.TransformDirection(Vector3.forward).z);
        target = GameObject.FindObjectOfType<BallCollision>().transform;
        
    }

    void ActivateRollingPhase()
    {
        ready = true;
        TheGameController.Instance.ActivateRollingPhase();
  //      GameManager.Instance.gameStats.Shots++;
        
		StartCoroutine(wait1S());
        startTimeRoll = Time.time;
        isEnabled = true;
        
    }

    void ActivateAmingPhase(bool resetPlayerPos){

        tapped = false;
        TheGameController.Instance.ActivateAimingPhase(resetPlayerPos);
    }

    public void CalculateSwipeSpeed()
    {
        this.duration = this.endTime - this.startTime;
        this.force = this.resultY / (this.duration * (float)70);
        if (this.force > 30f)
            this.force = 30f;
        Debug.Log("duration : " + this.duration);
        Debug.Log("force : " + this.force);
    }

    private IEnumerator wait1S()
    {
        yield return new WaitForSeconds(1f);
        ready = true;
    }


	void InvokeSelectedPowerUp()
	{
		switch(TheGameController.Instance.selectedPowerUpID)
		{
		case 1:
		{	this.force=35.0f;
			StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
			break;
		}
		case 2:
		{
			StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
			break;
		}
		case 3:
		{
			this.force=5.0f;
			//rgbdyBall.AddForce(this.force * this.cam_forward.x * (float)50, this.force * (float)10, this.force * this.cam_forward.z * (float)100);
			StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
			break;
		}
		case 4:
		{
			StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));

			break;
		}
		case 5:
		{
			StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
			break;
		}
		case 6:
		{
				StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
			break;
		}
		case 7:
		{
			StartCoroutine(TheGameController.Instance.StartPowerUp(TheGameController.Instance.selectedPowerUpID, 0.1f));
			break;
		}
		case 8:
		{
			break;
		}
		case 9:
		{
			break;
		}
		case 10:
		{
			break;
		}
			
		default:
			break;
		}
	}

	public void resetTouchValues(){

		firstPressPos = new Vector2 (0.0f, 0.0f);
		firstPressPosForAim= new Vector2 (0.0f, 0.0f);
		secondPressPos= new Vector2 (0.0f, 0.0f);
		currentSwipe = new Vector2 (0.0f, 0.0f);
		currentSwipeForAim = new Vector2 (0.0f, 0.0f);
	}
}


