using Photon.Pun;
using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNetwork : MonoBehaviour
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        if (PhotonNetwork.IsConnected) //만약 포톤과 연결이 되어있다면
        {
            PhotonNetwork.JoinRandomRoom(); //랜덤한 방에 입장 지시
            Debug.Log("연결 완");
        }
    }

    public void OnConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
