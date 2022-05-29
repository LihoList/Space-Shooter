
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject spawnPosition;

    private void Start()
    {
        InvokeRepeating("FireRocket", 1f, 1f);
    }

    void FireRocket()
    {
        (Instantiate(rocket, spawnPosition.transform.position, spawnPosition.transform.rotation) as GameObject).transform.parent = spawnPosition.transform;
    }
}
