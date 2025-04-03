using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject exitButton;

    private GameObject tempButton;

    private Button bnt;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length > 0);

        tempButton = Instantiate(exitButton, this.transform);

        tempButton.SetActive(false);

        bnt = tempButton.GetComponent<Button>();

        bnt.onClick.AddListener(ExitThisGame);
    }

    public IEnumerator OnExitButton()
    {
        yield return new WaitForSeconds(1.5f);

        tempButton.SetActive(true);
    }

    private void ExitThisGame()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();

        PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene("Title Scene");

        bnt.onClick.RemoveListener(ExitThisGame);
    }
}
