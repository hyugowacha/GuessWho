using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using ZL.Unity;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button exitButton;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length > 0);

        exitButton.SetActive(false);

        exitButton.onClick.AddListener(ExitThisGame);
    }

    public void OnExitButton()
    {
        exitButton.SetActive(true);
    }

    private void ExitThisGame()
    {
        PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene("Title Scene");
    }
}
