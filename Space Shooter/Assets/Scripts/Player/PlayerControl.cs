using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	//godmode
	public bool godMode = false;
	GameObject godModePanel;
	[SerializeField] GameObject godModeShield;
	BoxCollider playerCollider;

	//victory screen
	GameObject vitoryPanel;
	[SerializeField] GameObject victorySoundPrefab;


	[Header("Check Progress")]
	int check1 = 46;
	int check2 = 61;
	int check3 = 76;
	int finishCheck = 79;

	//other scripts references
	Score scoreScript;
	MoneyAndScore moneyAndScoreScript;

	int scoreToConvert;


	//collision handler
	float LoadLevelDelay = 1f;
	[SerializeField] GameObject deathFX; // explosive particles
	int currentScene;


	//player control variables
	[Header("General")]
	[Tooltip("м/с")][SerializeField] float Speed = 4f;
	[SerializeField] float XClamp = 4.5f;
	[SerializeField] float YClamp = 4.5f;
	[SerializeField] GameObject[] guns;

	[Header("Rotation")]
	[SerializeField] float xRotFactor = -5f;
	[SerializeField] float yRotFactor = 5f;
	[SerializeField] float zRotFactor = 4f;

	[Header("RotationOnMove")]
	[SerializeField] float xMoveRot = -10f;
	[SerializeField] float yMoveRot = 10f;
	[SerializeField] float zMoveRot = 10f;

	bool isControlEnabled = true;
	float xMove,yMove;

	//dark screen animator controller
	Animator darkScreenAnimator;

	//batteries
	public int batteriesDestroyed;

	//Level Enter sound
	AudioSource levelStartAudioSource;

	private void Start()
    {

		//references
		levelStartAudioSource = GetComponent<AudioSource>();
		darkScreenAnimator = GameObject.Find("Canvas for dark screen").GetComponent<Animator>();
		currentScene = SceneManager.GetActiveScene().buildIndex;
        godModePanel = GameObject.Find("GodModePanel");
		playerCollider = GetComponent<BoxCollider>();
		vitoryPanel = GameObject.Find("Victory Panel"); vitoryPanel.gameObject.SetActive(false);



		darkScreenAnimator.Play("HideDark");
		batteriesDestroyed = 0;

		godModePanel.gameObject.SetActive(false); //turning ui godmode panel
		godModeShield.gameObject.SetActive(false); //turning godmode shield

		CheckController();
	}

    void Update () 
	{
		if(isControlEnabled)
		{
			MoveShip();
			RotateShip();
			FireGuns();

			if(Input.GetKeyDown(KeyCode.C))
            {
				GodModeOn();
			}
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground": // when player hits a ground

				PlayerDeath("Ground");
				break;

			case "Tree": // when player hits a tree

				PlayerDeath("Tree");
				break;

			case "Rocket": // when player hits a rocket

				PlayerDeath("Rocket");
				break;

			case "Enemy": // when player hits an enemy

				PlayerDeath("Enemy");
				break;

		}
    }

	void CheckController()
    {
		Invoke("Check1", check1);
		Invoke("Check2", check2);
		Invoke("Check3", check3);
		Invoke("ShowVictoryScreen", finishCheck - 1);
		Invoke("FinishCheck", finishCheck);
    }

	//---------------------------------------------
	void Check1()
    {
		if (godMode)
			return;

		if (batteriesDestroyed < 1)
		{
			DefeatOnTrigger();
		}
	}
	void Check2()
	{
		if (godMode)
			return;

		if (batteriesDestroyed < 2)
		{
			DefeatOnTrigger();
		}
	}
	void Check3()
	{
		if (godMode)
			return;

		if (batteriesDestroyed < 3)
		{
			DefeatOnTrigger();
		}
	}

	void ShowVictoryScreen()
	{
		Time.timeScale = 0;
		vitoryPanel.gameObject.SetActive(true);

		Instantiate(victorySoundPrefab, gameObject.transform.position, Quaternion.identity);
	}
	void FinishCheck()
	{
		Finish();
	}

	

	//---------------------------------------------

	//private void OnTriggerEnter(Collider other)
	//{
	//	switch (other.gameObject.tag)
	//	{
	//		case "Battery 1 Trigger":
	//			if (batteriesDestroyed < 1)
	//			{
	//				DefeatOnTrigger();	
	//			}
	//			break;

	//		case "Battery 2 Trigger":
	//			if (batteriesDestroyed < 2)
	//			{
	//				DefeatOnTrigger();
	//			}
	//			break;

	//		case "Battery 3 Trigger":
	//			if (batteriesDestroyed < 3)
	//			{
	//				DefeatOnTrigger();
	//			}
	//			break;

	//		case "FinishTrigger": // when player collides with finish trigger
	//			Finish();
	//			break;
	//	}
	//}

	void Finish()
    {
		darkScreenAnimator.Play("ShowDark");
		Invoke("LoadMenu", 2);
		levelStartAudioSource.Play();

		moneyAndScoreScript = FindObjectOfType<MoneyAndScore>().GetComponent<MoneyAndScore>(); // reference to money script
		scoreScript = FindObjectOfType<Score>().GetComponent<Score>(); // reference to score script

		scoreToConvert = scoreScript.score; //getting score earned during level

		moneyAndScoreScript.AddScoreToMoney(scoreToConvert); //adding local score to global money


	}

	void PlayerDeath(string tag)
	{
		if (isControlEnabled)
		{
			Debug.Log(tag); //print what killed the player

			GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); //spawn object with sound and particles
			Destroy(fx, 1); //destroy it
			Invoke("RestartLevel", LoadLevelDelay); //restart level

			isControlEnabled = false; //turn off player controll
			darkScreenAnimator.Play("ShowDark"); //show dark screen

		}
	}


	void DefeatOnTrigger()
    {
		Debug.Log("DEFEAT");
		isControlEnabled = false;

		darkScreenAnimator.Play("ShowDark");
		Invoke("RestartLevel", LoadLevelDelay);
	}

	void LoadMenu()
    {
		SceneManager.LoadScene(1);
	}

	void RestartLevel()
	{
		SceneManager.LoadScene(currentScene);
	}


	void GodModeOn()
    {
		godMode = !godMode;
		Debug.Log("годмод - " + godMode);

		godModePanel.gameObject.SetActive(godMode); //turning ui godmode panel
		godModeShield.gameObject.SetActive(godMode); //turning godmode shield

		playerCollider.enabled = !playerCollider.enabled; //turning player collider
	}




























	void MoveShip()
	{
		xMove = Input.GetAxis("Horizontal");
		yMove = Input.GetAxis("Vertical");

		float xOffset = xMove * Speed * Time.deltaTime;
		float yOffset = yMove * Speed * Time.deltaTime;
		
		float newXPos = transform.localPosition.x + xOffset;
		float clampXPos = Mathf.Clamp(newXPos,-XClamp,XClamp);

		float newYPos = transform.localPosition.y + yOffset;
		float clampYPos = Mathf.Clamp(newYPos,-YClamp,YClamp);

		transform.localPosition = new Vector3(clampXPos,clampYPos,transform.localPosition.z);
	}

	void RotateShip()
	{
		float xRot = transform.localPosition.y * xRotFactor + yMove * xMoveRot;
		float yRot = transform.localPosition.x * yRotFactor + xMove * yMoveRot;
		float zRot = xMove * zMoveRot;

		transform.localRotation = Quaternion.Euler(xRot,yRot,zRot);
	}

	void FireGuns()
	{
		if(Input.GetButton("Fire"))
		{
			ActiveGuns();
		}
		else
		{
			DeactiveGuns();
		}
	}

	void ActiveGuns()
	{
		foreach(GameObject gun in guns)
		{
			gun.GetComponent<ParticleSystem>().enableEmission = true;
		}
	}
	void DeactiveGuns()
	{
		foreach(GameObject gun in guns)
		{
			gun.GetComponent<ParticleSystem>().enableEmission = false;
		}
	}
}
