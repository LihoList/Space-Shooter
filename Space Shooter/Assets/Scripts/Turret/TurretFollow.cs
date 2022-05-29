using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollow : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        transform.LookAt(target.transform);
    }
}
