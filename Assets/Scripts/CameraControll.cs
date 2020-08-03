using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform target;
    public Transform rotTarget;
    Vector3 lastPos;

    float sensitivity = 0.25f;
    void Awake()
    {
        rotTarget = transform.parent;
        target = rotTarget.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }

        Orbit();
    }

    void Orbit()
    {
        if (Input.GetMouseButton(0))
        {
            // point between mousPos and lastPos
            Vector3 delta = Input.mousePosition - lastPos;
            float angleY = -delta.y * sensitivity;
            float angleX = delta.x * sensitivity;

            // x
            Vector3 angles = rotTarget.transform.eulerAngles;
            angles.x += angleY;
            angles.x = ClampAngle(angles.x, -85f, 85f);

            rotTarget.transform.eulerAngles = angles;

            // y

            target.RotateAround(target.position, Vector3.up, angleX);
            lastPos = Input.mousePosition;

        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        
        if (angle < 0) angle = 360 + angle;
        if (angle > 180) return Mathf.Max(angle, 360 + from);
        
        return Mathf.Min(angle, to);

    }
}

