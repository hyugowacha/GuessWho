using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateView : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPos;

    [SerializeField]
    private Transform targetToFollow;

    [SerializeField]
    [Range(0f, 20.0f)]
    private float distance;

    [SerializeField]
    [Range(0f, 10.0f)]
    private float height;

    [SerializeField]
    [Range(1f, 5.0f)]
    private float rotationSpeed;

    private float mouseX;

    private float mouseY;

    private float currentRotationX;

    private float currentRotationY;

    private void Start()
    {
        distance = 2.8f;

        height = 3f;

        rotationSpeed = 3f;

        currentRotationX = 0f;

        currentRotationY = 0f;
    }

    void LateUpdate()
    {
        if (targetToFollow != null)
        {
            if (targetToFollow.hasChanged)
            {
                cameraPos.position = targetToFollow.position + (Vector3.up * height) + (Vector3.forward * -distance);
            }

            RotateViewByMouse();
        }
    }

    private void RotateViewByMouse()
    {
        mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        currentRotationX += mouseX;

        currentRotationY -= mouseY;

        currentRotationY = Mathf.Clamp(currentRotationY, -40f, 80f);

        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

        cameraPos.position = targetToFollow.position + (Vector3.up * height) + (rotation * Vector3.back * distance);

        cameraPos.LookAt(targetToFollow);
    }
}
