using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public float rotSpeed = 10f;
    
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotSpeed * Time.deltaTime);
    }
}
