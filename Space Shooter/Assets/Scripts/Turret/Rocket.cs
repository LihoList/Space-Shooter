
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
