
using UnityEngine;

public class Enemy : MonoBehaviour {

	int scoreToAdd = 15;
	int hits = 4;

	Score scoreBoard;

	[SerializeField] GameObject deathFX;

	void Start () {
		scoreBoard = FindObjectOfType<Score>();
		AddNonTriggerCollider ();

	}
	
	void AddNonTriggerCollider () {
		Collider boxColider = gameObject.AddComponent<BoxCollider>();
	}


	void OnParticleCollision(GameObject other)
    {
		hits = hits - 1;
		if(hits <= 0)
		{
			Destroy(GetComponent<BoxCollider>());
			Death();
		} 
	}

	void Death()
	{
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); //spawn object with sound and particles
		Destroy(fx, 1); //destroy it

		scoreBoard.ScoreHit(scoreToAdd);

		Destroy(gameObject);
	}
}
