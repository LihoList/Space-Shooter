
using UnityEngine;

public class AttitudeDetector : MonoBehaviour
{
    float heightAboveGround = 0.0f;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if(hit.collider.tag == "Ground")
            {
                heightAboveGround = hit.distance;
            } 
        }

        Debug.Log("attitude" + heightAboveGround);
    }
}
