
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.1f;

    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed, rotationSpeed * Time.deltaTime);
    }
}
