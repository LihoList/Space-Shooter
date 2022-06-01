
using UnityEngine;

public class ParticleFireSoundSimple : MonoBehaviour
{
    AudioSource audioSource;
    ParticleSystem particleSystem;
    int currentNumberOfParticles = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(particleSystem.particleCount > currentNumberOfParticles)
        {
            audioSource.Play();
        }
        currentNumberOfParticles = particleSystem.particleCount;
    }
}
