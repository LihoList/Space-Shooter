
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    AudioSource audioSource_FireSound;

    public int activationDelay = 1;

    private void Start()
    {
        audioSource_FireSound = GetComponent<AudioSource>();

        InvokeRepeating("SpawnRocket", activationDelay, 0.8f);
        Destroy(gameObject, activationDelay + 6);
    }

    void SpawnRocket()
    {
        Instantiate(rocket, gameObject.transform.position, gameObject.transform.rotation);
        audioSource_FireSound.Play();

        Debug.Log("rocket fired");

    }

}
