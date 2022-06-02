
using UnityEngine;
using UnityEngine.UI;

public class AttitudeDetector : MonoBehaviour
{
    float heightAboveGround = 0.0f;

    Text attitudeText;

    private void Start()
    {
        attitudeText = GameObject.Find("Attitude Text").GetComponent<Text>();
    }

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







        //attitude text
        attitudeText.text = "Attitude - " + heightAboveGround.ToString("F0");
    }
}
