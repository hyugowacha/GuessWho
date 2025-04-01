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
    private GameObject exitButton;

    private GameObject tempButton;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length > 0);

        tempButton = Instantiate(exitButton, this.transform);

        tempButton.SetActive(false);

        Button bnt = tempButton.GetComponent<Button>();

        bnt.onClick.AddListener(() => ExitThisGame());
    }

    public void OnExitButton()
    {
        tempButton.SetActive(true);
    }

    private void ExitThisGame()
    {
        PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene("Title Scene");
    }
}
