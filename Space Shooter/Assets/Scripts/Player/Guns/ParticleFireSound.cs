
using UnityEngine;

public class ParticleFireSound : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
        }

    }

}
