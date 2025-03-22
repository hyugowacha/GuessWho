using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    private PhotonView photonView;

    private void Start()
    {
        distance = 4f;

        height = 3f;

        rotationSpeed = 3f;

        currentRotationX = 0f;

        currentRotationY = 0f;

        ///

        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine) // **본인이 조종하는 캐릭터일 때만 실행**
        {
            cameraPos = Camera.main.transform; // **메인 카메라 찾기**

            targetToFollow = this.transform; // **자신의 캐릭터를 따라가도록 설정**
        }
        else
        {
            // **다른 플레이어의 RotateView는 카메라를 설정하지 않음**
            this.enabled = false;
        }
    }

    void LateUpdate()
    {
        if (!photonView.IsMine) return; // **자신이 조종하는 캐릭터만 카메라 업데이트**

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
