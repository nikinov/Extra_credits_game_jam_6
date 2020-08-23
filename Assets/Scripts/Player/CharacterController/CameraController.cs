using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensivity;
    [SerializeField] private Transform playerBody;

    private const string mouseXAxis = "Mouse X";
    private const string mouseYAxis = "Mouse Y";


    private float xAxisClamp = 0f;

    private void Awake()
    {
        LockCursor();

    }

    private void Update()
    {
        RotateCamera();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RotateCamera()
    {

        float mouseX = Input.GetAxis(mouseXAxis) * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYAxis) * mouseSensivity * Time.deltaTime;

        //Why do we need it ??
        //Because normal humans can't spin(up and down) their heads on 360 degrees

        xAxisClamp += mouseY;
        if (xAxisClamp > 90f)
        {
            xAxisClamp = 90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(270f);

        }
        else if (xAxisClamp < -90f)
        {
            xAxisClamp = -90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(90f);
        }


        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }


    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
