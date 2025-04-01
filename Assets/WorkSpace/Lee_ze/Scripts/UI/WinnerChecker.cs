using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerChecker : MonoBehaviour
{
    [SerializeField]
    private InGamePlayerList inGamePlayerList;

    [SerializeField]
    private ExitGame exitButton;

    [SerializeField]
    private GameObject winnerIs;

    [SerializeField]
    private GameObject winnerName;

    private GameObject tempWinnerIs;

    private GameObject tempWinnerName;

    private void Start()
    {
        tempWinnerIs = Instantiate(winnerIs, this.transform);

        tempWinnerName = Instantiate(winnerName, this.transform);

        tempWinnerIs.SetActive(false);

        tempWinnerName.SetActive(false);
    }

    public void CheckWinner()
    {
        if (inGamePlayerList.aliveCount == 1)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                bool isHit = (bool)player.CustomProperties["isHit"];

                if (isHit == false)
                {
                    tempWinnerIs.SetActive(true);

                    tempWinnerName.SetActive(true);

                    exitButton.OnExitButton();

                    TextMeshProUGUI tmpText = tempWinnerName.GetComponent<TextMeshProUGUI>();

                    tmpText.text = player.NickName;
                }
            }
        }
    }
}
