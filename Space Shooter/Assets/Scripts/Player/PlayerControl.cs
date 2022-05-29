using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
	//other scripts references
	Score scoreScript;
	MoneyAndScore moneyAndScoreScript;

	int scoreToConvert;


	//collision handler
	float LoadLevelDelay = 1f;
	[SerializeField] GameObject explosionFX; // explosive particles
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

	private void Start()
    {
		batteriesDestroyed = 0;

		darkScreenAnimator = GameObject.Find("Canvas for dark screen").GetComponent<Animator>();
		darkScreenAnimator.Play("HideDark");

		currentScene = SceneManager.GetActiveScene().buildIndex;
	}

    void Update () 
	{
		if(isControlEnabled)
		{
			MoveShip();
			RotateShip();
			FireGuns();
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground": // when player hits a ground

                Debug.Log("Ground");

                OnPlayerDeath();
                explosionFX.SetActive(true);
                Invoke("RestartLevel", LoadLevelDelay);

                break;

			case "Obstacle": // when player hits an obstacle

				Debug.Log("Obstacle");

				OnPlayerDeath();
				explosionFX.SetActive(true);
				Invoke("RestartLevel", LoadLevelDelay);

				break;

			case "Tree": // when player hits a tree

                Debug.Log("Tree");

                OnPlayerDeath();
                explosionFX.SetActive(true);
                Invoke("RestartLevel", LoadLevelDelay);

                break;

			case "Rocket":

				Debug.Log("Rocket");

				OnPlayerDeath();
				explosionFX.SetActive(true);
				Invoke("RestartLevel", LoadLevelDelay);

				break;
		}
    }





    private void OnTriggerEnter(Collider other)
	{
		switch (other.gameObject.tag)
		{
			case "Battery 1 Trigger":
				if (batteriesDestroyed != 1)
				{
					DefeatOnTrigger();
				}
				break;

			case "Battery 2 Trigger":
				if (batteriesDestroyed != 2)
				{
					DefeatOnTrigger();
				}
				break;

			case "Battery 3 Trigger":
				if (batteriesDestroyed != 3)
				{
					DefeatOnTrigger();
				}
				break;

			





			case "FinishTrigger": // when player collides with finish trigger

				darkScreenAnimator.Play("ShowDark");
				Invoke("LoadMenu", LoadLevelDelay);


				scoreScript = FindObjectOfType<Score>().GetComponent<Score>();
				scoreToConvert = scoreScript.score;

				Debug.Log("score converted localy - " + scoreScript.score);

				moneyAndScoreScript = FindObjectOfType<MoneyAndScore>().GetComponent<MoneyAndScore>();
				moneyAndScoreScript.AddScoreToMoney(scoreToConvert);

				break;
		}
	}

	void DefeatOnTrigger()
    {
		Debug.Log("DEFEAT");
		OnPlayerDeath();

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

	void OnPlayerDeath()
	{
		isControlEnabled = false;
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
