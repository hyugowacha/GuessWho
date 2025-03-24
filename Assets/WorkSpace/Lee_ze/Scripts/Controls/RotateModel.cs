using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateModel : MonoBehaviourPun
{
    private Vector3 direction;

    private float rotateSpeed; 
    
    private PhotonTransformView photonTransformView;


    private void Awake()
    {
        direction = Vector3.forward;

        rotateSpeed = 10f;

        photonTransformView = GetComponentInParent<PhotonTransformView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

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
