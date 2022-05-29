
using UnityEngine;

public class Rocket : MonoBehaviour
{
    float speed = 15f;
    public GameObject[] fires;
    public GameObject fireRotarionExample;
    public ParticleSystem explosionFX;

    private void Start()
    {
        int currentFire = Random.Range(0, fires.Length);
        Instantiate(fires[currentFire], fireRotarionExample.transform.position, fireRotarionExample.transform.rotation).transform.SetParent(gameObject.transform);

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            explosionFX.Play();
            Destroy(gameObject);
        }
    }
}
