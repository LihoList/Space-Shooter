
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public int score;
	Text scoreText;

	void Start () 
	{
		score = 0;

		scoreText = GetComponent<Text>();
		scoreText.text = "Score - " + score.ToString();
	}

	public void ScoreHit(int scorePerHit)
	{
		score = score + scorePerHit;
		scoreText.text = "Score - " + score.ToString();
	}
}


