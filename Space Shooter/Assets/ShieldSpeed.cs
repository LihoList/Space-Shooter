
using UnityEngine;

public class ShieldSpeed : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.1f;

    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed * Time.deltaTime);
    }
}
