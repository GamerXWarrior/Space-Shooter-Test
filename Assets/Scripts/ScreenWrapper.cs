using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    float leftConstrain = Screen.width;
    float rightConstrain = Screen.width;
    float bottomConstrain = Screen.height;
    float topConstrain = Screen.height;
    float buffer = 0f;
    Camera cam;
    float distanceZ;

    private void Start()
    {
        EventService.Instance.CameraSet += SetConstrainsValues;
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstrain = cam.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        rightConstrain = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        bottomConstrain = cam.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;
        topConstrain = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
    }

    private void SetConstrainsValues()
    {
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstrain = cam.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        rightConstrain = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x;
        bottomConstrain = cam.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).y;
        topConstrain = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, distanceZ)).y;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < leftConstrain - buffer)
        {
            transform.position = new Vector3(rightConstrain - 0.10f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightConstrain)
        {
            transform.position = new Vector3(leftConstrain, transform.position.y, transform.position.z);
        }
        if (transform.position.y < bottomConstrain - buffer)
        {
            transform.position = new Vector3(transform.position.x, topConstrain + buffer, transform.position.z);
        }
        if (transform.position.y > topConstrain + buffer)
        {
            transform.position = new Vector3(transform.position.x, bottomConstrain - buffer, transform.position.z);
        }
    }
}
