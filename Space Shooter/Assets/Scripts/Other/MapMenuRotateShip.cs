
using UnityEngine;

public class MapMenuRotateShip : MonoBehaviour
{
    bool canFreeRotate;
    float rotaionSpeed = 5f;



    private void Start()
    {
        canFreeRotate = true;
    }

    private void FixedUpdate()
    {
        if(canFreeRotate)
        {
            transform.Rotate(0, 1.0f, 0, Space.World);
        } 
    }

    void OnMouseDrag()
    {
        canFreeRotate = false;

        float X_axisRotation = Input.GetAxis("Mouse X") * rotaionSpeed;
        transform.Rotate(Vector3.down, X_axisRotation, Space.World);
        
    }



    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(30, 157, -2.5f);
        canFreeRotate = true;
    }
}
