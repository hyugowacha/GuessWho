using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    private Vector3 direction;

    private float rotateSpeed;

    private void Awake()
    {
        direction = Vector3.forward;

        rotateSpeed = 10f;
    }

    private void Update()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    public void SetTargetDirection(Vector3 tempDirection)
    {
        if (tempDirection != Vector3.zero)
        {
            direction = tempDirection;
        }
    }
}
