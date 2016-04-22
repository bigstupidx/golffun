using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TheGameController : MonoBehaviour {

    public static TheGameController Instance;

	public int selectedPowerUpID=0;

    public State gameState;
	public UILabel textoControl;

    public Animator character;

    public GameObject ball;

    public BallFollower follower;

    public bool teeOff = true;

    public float pathEndThreshold = 0.1f;
    private bool hasPath = false;

    private Vector3 initialPos;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    private Vector3 cameraInitialPos;
    private Quaternion cameraInitialRotation;
    
	private const string GAME_LAST_OPENED = "VAOpened";				//for video ad 
	private const string Video_Bonus_Index = "VAIndex";

    public Camera camera;

    public GameObject go1;
    public GameObject go2;
    public GameObject go3;
    public GameObject go4;

    public Transform characterHand;
    public Transform meshBat;

    public GameObject[] targets;
    public GameObject target;

	public GameObject dummyCharacter;
    public ScreenFader screen;

	public bool isUFOEnabled;



    void Awake()
    {


        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

	void Start () {


//		HUD hud = GameManager.Instance.hud;
//		hud.switchTexture ();

		startForPlayer (TheGameManager.Instance.isPlayer1Turn);

		Invoke ("updateMultiplayerTurnTexture", 1.0f);
	}

	public void updateMultiplayerTurnTexture(){
		GameObject.Find ("HUD(Clone)").GetComponent<HUD> ().switchTexture ();

	}
    
	public void startForPlayer (bool isPlayer1){

		cameraInitialPos = camera.transform.localPosition;
		cameraInitialRotation = camera.transform.localRotation;
		IsBonusReady();
		
		initialPos = dummyCharacter.transform.localPosition;
		initialRotation = dummyCharacter.transform.localRotation;
		initialScale = dummyCharacter.transform.localScale;
		dummyCharacter.SetActive (false);
		
		GameObject playerObject = null;
		if (TheGameManager.Instance!=null)
		{
			if (TheGameManager.Instance.currentActivePlayer == null)
			{
				TheGameManager.Instance.currentActivePlayer = new Player(TheGameManager.Instance.AvatarName);
			}
		}
		if (go1.name == TheGameManager.Instance.currentActivePlayer.AvatarName)
		{
			playerObject  = Instantiate(go1) as GameObject;
		}
		else if (go2.name == TheGameManager.Instance.currentActivePlayer.AvatarName)
		{
			playerObject = Instantiate(go2) as GameObject;
		}
		else if (go3.name == TheGameManager.Instance.currentActivePlayer.AvatarName)
		{
			playerObject = Instantiate(go3) as GameObject;
		}
		else if (go4.name == TheGameManager.Instance.currentActivePlayer.AvatarName)
		{
			playerObject = Instantiate(go4) as GameObject;
		}
		else
		{
			playerObject = Instantiate(go1) as GameObject;
		}
		
		character = playerObject.GetComponent<Animator>();
		
		foreach(Transform child in character.gameObject.GetComponentsInChildren<Transform>()){
			//  if(child.gameObject.tag == "Mesh_Bat"){
			if(child.gameObject.tag == "CharacterBat"){	
				
				GameObject obj;
				if(selectedPowerUpID==0)
					obj= Instantiate(TheGameManager.Instance.normalBat) as GameObject;
				else 
					obj= Instantiate(TheGameManager.Instance.itemPowerUpObject) as GameObject;
				
				obj.transform.parent = child.transform.parent;
				obj.transform.localPosition = child.transform.localPosition;
				obj.transform.localRotation = child.transform.localRotation;
				obj.transform.localScale = child.transform.localScale;
				meshBat = child;
				child.GetComponent<MeshRenderer>().enabled = false;
				child.gameObject.SetActive(false);
				obj.layer = LayerMask.NameToLayer("Score");
			}
		}
		switchFranela(character.transform.GetChild(0));
		switchPantalon(character.transform.GetChild(0));
		switchZapato(character.transform.GetChild(0));
		switchHats (character.transform.GetChild(0));
		switchGlasses (character.transform.GetChild(0));
		switchNecklaces (character.transform.GetChild(0));

		character.transform.parent = camera.transform;
		character.transform.localPosition = initialPos;
		character.transform.localRotation = initialRotation;
		character.transform.localScale = initialScale;
		character.transform.parent = null;
		character.gameObject.layer = LayerMask.NameToLayer("Score");
		foreach (Transform child in character.transform)
		{
			child.gameObject.layer = LayerMask.NameToLayer("Score");
		}
		
		follower = camera.transform.parent.GetComponent<BallFollower>();
		ball = follower.ball.gameObject;
		
		characterHand = meshBat.transform.parent.parent.parent.parent.parent.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0);
		ball.transform.position = characterHand.position;
		ball.GetComponent<Rigidbody>().useGravity = false;
		ball.transform.parent = characterHand;
		ActivateAimingPhase(false);
		
		
		Destroy(camera.transform.GetChild(0).gameObject);
		IComparer targetComparer = new ArraySorter();
		Array.Sort(targets, targetComparer);

		foreach (GameObject obj in targets) {
			if (obj.GetComponent<BallCollision>() != null)
			{
				obj.SetActive(false);
			}
			else
			{
				obj.transform.parent.gameObject.SetActive(false);
			}
			
		}
		
		//camera.transform.parent.GetComponent<BallFollower>().LookAtTarget = ;
		
	//	target = targets[GameManager.Instance.gameStats.Round];

		Debug.Log ("player round " +TheGameManager.Instance.currentActivePlayer.playerRound);
		target = targets[TheGameManager.Instance.currentActivePlayer.playerRound];

		if (target.GetComponent<BallCollision>() != null)
		{
			target.SetActive(true);
		}
		else
		{
			target.transform.parent.gameObject.SetActive(true);
		}
		//target.SetActive(true);
		
//		if (GameManager.Instance.gameStats.Round > 0)
		if (TheGameManager.Instance.currentActivePlayer.playerRound > 0)
		{
			
			Debug.Log("Character Loaded");

//			SetupDefault(targets[GameManager.Instance.gameStats.Round - 1]);
			SetupDefault(targets[TheGameManager.Instance.currentActivePlayer.playerRound - 1]);
		}
		else {
			SetupDefault(null);
		} 

	}



    public void switchFranela(Transform trans)
    {
        trans.GetComponent<SwitchFranela>().ChangeTex(TheGameManager.Instance.currentActivePlayer, true, true);
    }

    public void switchPantalon(Transform trans)
    {
        trans.GetComponent<SwitchPantalon>().ChangeTex(TheGameManager.Instance.currentActivePlayer, true, true);
    }

    public void switchZapato(Transform trans)
    {
        trans.GetComponent<SwitchZapatos>().ChangeTex(TheGameManager.Instance.currentActivePlayer, true, true);
    }

	public void switchNecklaces(Transform trans)
	{
		trans.GetComponent<SwitchNecklaces>().ChangeTex(TheGameManager.Instance.currentActivePlayer, true, true);
	}
	public void switchGlasses(Transform trans)
	{
		trans.GetComponent<SwitchGlasses>().ChangeTex(TheGameManager.Instance.currentActivePlayer, true, true);
	}
	public void switchHats(Transform trans)
	{
		trans.GetComponent<SwitchHat>().ChangeTex(TheGameManager.Instance.currentActivePlayer, true, true);
	}

    public void IdleCharacter() {

            character.SetBool("Idle", true);
            character.SetBool("Flick", false);
            character.SetBool("Walk", false);
            character.SetBool("Throw", false);
            character.SetBool("Putt", false);
          
    }

    public void SetupDefault(GameObject obj) {
        Transform followIt = this.ball.transform;
        if(obj != null){
            followIt = obj.transform;
        }
		Debug.Log ("initial position x= "+initialPos.x +"y="+ initialPos.y + "z=" + initialPos.z);
        if (target.GetComponent<BallCollision>() != null)
        {
            camera.transform.parent.GetComponent<BallFollower>().ResetConfigurations(camera.gameObject, cameraInitialPos, cameraInitialRotation, target, character.gameObject, initialPos, initialRotation, followIt);
        }
        else
        {
            camera.transform.parent.GetComponent<BallFollower>().ResetConfigurations(camera.gameObject, cameraInitialPos, cameraInitialRotation, target.transform.parent.gameObject, character.gameObject, initialPos, initialRotation, followIt);
        }
    }

    public void SwingCharacter()
    {
//		GameManager.Instance.gameStats.Shots++;
		SoundManager.Instance.Play_teeShotApplause();
        character.SetBool("Idle", false);
        character.SetBool("Flick", true);
        character.SetBool("Walk", false);
        character.SetBool("Throw", false);
        character.SetBool("Putt", false);
    }

    public void TossBall()
    {
        ball.transform.position = characterHand.position;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.transform.parent = characterHand;

        character.SetBool("Idle", false);
        character.SetBool("Flick", false);
        character.SetBool("Walk", false);
        character.SetBool("Throw", true);
        character.SetBool("Putt", false);
        Invoke("ThrowBall", 0.5f);
    }

    public void PuttBall()
    {
//		GameManager.Instance.gameStats.Shots++;
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.transform.parent = null;
		SoundManager.Instance.Play_PuttShotApplause();

        character.SetBool("Idle", false);
        character.SetBool("Flick", false);
        character.SetBool("Walk", false);
        character.SetBool("Throw", false);
        character.SetBool("Putt", true);
    }

    public void ThrowBall() {
        ball.transform.parent = null;
        if (TheGameController.Instance.teeOff)
        {
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.GetComponent<Rigidbody>().AddForce(Vector3.up * 700);
        }
    }

    public void WalkCharacter()
    {
        camera.GetComponent<TouchControls>().isEnabled = false;
        screen.EndScene();
        Invoke("ReachedAtNewTarget", GameObject.FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<ScreenFader>().fadeSpeed);
    }


    public void Flash()
    {
        screen.EndScene();
        Invoke("ResetFlash", GameObject.FindObjectOfType<Canvas>().transform.GetChild(0).GetComponent<ScreenFader>().fadeSpeed);
    }
    void ResetFlash()
    {
        screen.StartScene();
    }

    void ReachedAtNewTarget()
    {
		selectedPowerUpID = 0;
		changeBat ();
        IdleCharacter();
        SetupDefault(null);
        screen.StartScene();
        camera.GetComponent<TouchControls>().isEnabled = true;

    }
     bool AtEndOfPath()
    {
        hasPath |=  character.gameObject.GetComponent<NavMeshAgent>().hasPath;
        if (hasPath && character.gameObject.GetComponent<NavMeshAgent>().remainingDistance <= character.gameObject.GetComponent<NavMeshAgent>().stoppingDistance + pathEndThreshold)
        {
            // Arrived
            hasPath = false;
            return true;
        }
 
        return false;
    }

    public void WalkTowardBall() {
        character.gameObject.GetComponent<NavMeshAgent>().destination = (ball.transform.position);
        WalkCharacter();
        follower.ball = character.transform;
    }

   

    private Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        /// create random angle between 0 to 360 degrees 
        float startAngle = angle;
        float endAngle = angle;
        int ang = UnityEngine.Random.Range((int)startAngle, (int)endAngle + 1);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang);
        pos.z = center.z + radius * Mathf.Cos(-ang);
        pos.y = center.y;
        return pos;
    }

    public void ActivateAimingPhase(bool setplayerPosition)
    {
        if (setplayerPosition)
        {
            WalkCharacter();
        }
        else {
            Flash();
        }
        if (textoControl == null)
        {
            if (GameManager.Instance.hud() != null)
            {
                foreach (UILabel lbl in GameManager.Instance.hud().transform.GetComponentsInChildren<UILabel>())
                {
                    if (lbl.name.Contains("Instructions"))
                    {
                        textoControl = lbl;
                    }
                }
            }
        }
        else
        {
            textoControl.text = "Instructions: Swipe left or right to Aim";
        }
        //phaseRollScript.enabled = false;
       //phaseAimScript.enabled = true;
        gameState = State.Aiming;
    }


    public void ActivateTossPhase()
    {
        // camera.transform.GetChild(0).gameObject.SetActive(true);
        if (textoControl == null)
        {
            if (GameManager.Instance.hud() != null)
            {
                foreach (UILabel lbl in GameManager.Instance.hud().transform.GetComponentsInChildren<UILabel>())
                {
                    if (lbl.name.Contains("Instructions"))
                    {
                        textoControl = lbl;
                    }
                }
            }
        }
        else
        {
            textoControl.text = "Instructions: Tap to Toss!!!";
        }
    }


    public void ActivateFlickingPhase()
    {
       // camera.transform.GetChild(0).gameObject.SetActive(true);
        if (textoControl == null)
        {
            if (GameManager.Instance.hud() != null)
            {
                foreach (UILabel lbl in GameManager.Instance.hud().transform.GetComponentsInChildren<UILabel>())
                {
                    if (lbl.name.Contains("Instructions"))
                    {
                        textoControl = lbl;
                    }
                }
            }
        }
        else
        {
            textoControl.text = "Instructions: Flick!!!";
        }

        //phaseAimScript.enabled = false;
        //phaseFlickScript.enabled = true;
        gameState = State.Shooting;
    }

    public void ActivateRollingPhase()
    {
       // camera.transform.GetChild(0).gameObject.SetActive(false);
        if (textoControl == null)
        {
            if (GameManager.Instance.hud() != null)
            {
                foreach (UILabel lbl in GameManager.Instance.hud().transform.GetComponentsInChildren<UILabel>())
                {
                  
                    if (lbl.name.Contains("Instructions"))
                    {
                        textoControl = lbl;
                    }
                }
            }
        }
        else
        {
            textoControl.text = "Instructions: Roll!!!";
        }
        teeOff = false;
        //phaseFlickScript.enabled = false;
        //phaseRollScript.enabled = true; 
        gameState = State.Rolling;
    }

    
	private void saveCurrentTime()
	{
		PlayerPrefs.SetString (GAME_LAST_OPENED, DateTime.Now.ToString ());
		PlayerPrefs.SetInt (Video_Bonus_Index, 2);	
		PlayerPrefs.Save ();
	}

    public void IsBonusReady()
    {
        DateTime currentTime = DateTime.Now;

        DateTime lastOpened = Convert.ToDateTime(PlayerPrefs.GetString(GAME_LAST_OPENED, currentTime.ToString()));

        TimeSpan timeDiff = currentTime.Subtract(lastOpened);
        //print (timeDiff.Days);

        if (timeDiff.Days >= 1)
        {
            PlayerPrefs.SetString(GAME_LAST_OPENED, currentTime.ToString());
            PlayerPrefs.SetInt(Video_Bonus_Index, 2);
        }
    }
//	public void checkPowerUp(){
//
//		if(selectedPowerUpID 
//
//	}
	public void changeBat(){


		if (selectedPowerUpID == 1) {
			foreach (Transform child in character.gameObject.GetComponentsInChildren<Transform>()) {
				//				if(child.gameObject.tag == "Mesh_Bat"){
				if (child.gameObject.tag == "CharacterBat") {	

					GameObject obj = Instantiate (TheGameManager.Instance.itemPowerUpObject) as GameObject;
					//					GameObject obj = Instantiate(TheGameManager.Instance.itemPowerObjectAfterHit) as GameObject;
					obj.transform.parent = child.transform.parent;
					obj.transform.localPosition = child.transform.localPosition;
					obj.transform.localRotation = child.transform.localRotation;
					obj.transform.localScale = child.transform.localScale;
					meshBat = child;
					child.GetComponent<MeshRenderer> ().enabled = false;
					obj.layer = LayerMask.NameToLayer ("Score");
				
				
				}
			}

		}
		else{
		foreach (Transform child in character.gameObject.GetComponentsInChildren<Transform>()) {
			//				if(child.gameObject.tag == "Mesh_Bat"){
			if (child.gameObject.tag == "CharacterBat") {	
				
				GameObject obj = Instantiate (TheGameManager.Instance.normalBat) as GameObject;
				//					GameObject obj = Instantiate(TheGameManager.Instance.itemPowerObjectAfterHit) as GameObject;
				obj.transform.parent = child.transform.parent;
				obj.transform.localPosition = child.transform.localPosition;
				obj.transform.localRotation = child.transform.localRotation;
				obj.transform.localScale = child.transform.localScale;
				meshBat = child;
				child.GetComponent<MeshRenderer> ().enabled = false;
				obj.layer = LayerMask.NameToLayer ("Score");
				
				}			
			}
		}
	}

	public IEnumerator StartPowerUp(int powerUpID, float time)
	{
		yield return new WaitForSeconds (time);
		switch (powerUpID) {
		case 1:
		{

			foreach(Transform child in character.gameObject.GetComponentsInChildren<Transform>()){
				if(child.gameObject.tag == "CharacterBat"){	

					GameObject obj = Instantiate(TheGameManager.Instance.itemPowerUpObject) as GameObject;
					obj.transform.parent = child.transform.parent;
					obj.transform.localPosition = child.transform.localPosition;
					obj.transform.localRotation = child.transform.localRotation;
					obj.transform.localScale = child.transform.localScale;
					meshBat = child;
					child.GetComponent<MeshRenderer>().enabled = false;
					obj.layer = LayerMask.NameToLayer("Score");
				}
			}
			break;
		}
		case 2:
		{
			Instantiate(Resources.Load("PowerUps/BirdPowerUp"), new Vector3(target.transform.position.x,target.transform.position.y + 20f,target.transform.position.z), Quaternion.identity);

			break;
		}
		case 3:
		{
			foreach(Transform child in character.gameObject.GetComponentsInChildren<Transform>()){
				//				if(child.gameObject.tag == "Mesh_Bat"){
				if(child.gameObject.tag == "CharacterBat"){	
					
					GameObject obj = Instantiate(TheGameManager.Instance.brokenBat) as GameObject;
					//					GameObject obj = Instantiate(TheGameManager.Instance.itemPowerObjectAfterHit) as GameObject;
					obj.transform.parent = child.transform.parent;
					obj.transform.localPosition = child.transform.localPosition;
					obj.transform.localRotation = child.transform.localRotation;
					obj.transform.localScale = child.transform.localScale;
					meshBat = child;
					child.GetComponent<MeshRenderer>().enabled = false;
					obj.layer = LayerMask.NameToLayer("Score");
				}
			}
			Instantiate(Resources.Load("PowerUps/BrokenBatPowerUp"), new Vector3(target.transform.position.x,target.transform.position.y + 20f,target.transform.position.z), Quaternion.identity);
			break;
		}
		case 4:
		{
			GameObject sunObj= GameObject.Find("SunPowerUp(Clone)");
			Destroy(sunObj);

			break;
		}
		case 5 :
		{
			Invoke("startLawnMover",1.0f);
			break;
		}
		case 6 :
		{
			Invoke("startGropher",1.0f);
			break;
		}
		case 7 :
		{
			Invoke("startUFO",1.0f);
			break;
		}
		default:
			break;
		}
	}

	public void startUFO (){
		
		Instantiate(Resources.Load("PowerUps/UFOPowerUp"));
	}

	public void startGropher (){
		
	//	Instantiate(Resources.Load("PowerUps/GropherPowerUp"));
		Instantiate(Resources.Load("PowerUps/UFOPowerUp"));

	}

	public void startLawnMover (){
		
	
		Instantiate(Resources.Load("PowerUps/LawnMowerPowerUp"));

		
	}


}



public class ArraySorter : IComparer
{

    // Calls CaseInsensitiveComparer.Compare on the monster name string.
    int IComparer.Compare(System.Object x, System.Object y)
    {
        return ((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
    }

}