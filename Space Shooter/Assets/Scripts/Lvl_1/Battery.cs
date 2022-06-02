
using UnityEngine;

public class Battery : MonoBehaviour
{
	public int batteryHp = 8;
	[SerializeField] ParticleSystem explosionFX;
	[SerializeField] ParticleSystem smokeFX;
	[SerializeField] GameObject innerCylinder;
	[SerializeField] GameObject Cylinder;

	PlayerControl playerControlScript;

	bool alive;

	


    private void Start()
    {
		alive = true;
		playerControlScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

		innerCylinder.GetComponent<Renderer>().material.color = Color.green;
	}

    private void FixedUpdate()
    {
		if(alive)
        {
			Cylinder.transform.Rotate(0, 0, 0.1f);
		}
	}

    void OnParticleCollision(GameObject other)
	{
		batteryHp--;
		if(batteryHp <= 0)
        {
			BatteryDestroyed();
		}
	}

	void BatteryDestroyed()
    {
		alive = false;
		playerControlScript.batteriesDestroyed++;
		Debug.Log(playerControlScript.batteriesDestroyed);

		Destroy(gameObject.GetComponent<BoxCollider>());

		explosionFX.Play();
		smokeFX.Play();
		innerCylinder.GetComponent<Renderer>().material.color = Color.red;
	}


}
