using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera Cam;
    private float xrotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float MouseX = input.x;
        float MouseY = input.y;

        xrotation -= (MouseY * Time.deltaTime) * ySensitivity;
        xrotation = Mathf.Clamp(xrotation, -80f, 80f);

        Cam.transform.localRotation = Quaternion.Euler(xrotation, 0, 0);

        transform.Rotate(Vector3.up * (MouseX * Time.deltaTime) * xSensitivity);

    }

}
