using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System;

public class InGamePlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject inGamePlayerListPanel;

    [SerializeField]
    private GameObject inGamePlayerID;

    [SerializeField]
    private GameObject inGamePlayerCount;

    private GameObject list;

    private GameObject playerCount;

    public int playerNum;

    private Dictionary<int, GameObject> playerEntries = new Dictionary<int, GameObject>();

    IEnumerator Start()
    {
        yield return new WaitUntil(() => (PhotonNetwork.PlayerList).Length > 0);

        playerNum = PhotonNetwork.PlayerList.Length;

        playerCount = Instantiate(inGamePlayerCount, this.transform);

        list = Instantiate(inGamePlayerListPanel, this.transform);

        list.SetActive(false);

        UpdatePlayerList();

        SetPlayerCount();
    }

    public void OnCheckAlivePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            list.SetActive(true);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            list.SetActive(false);
        }
    }

    private void UpdatePlayerList()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerEntry = Instantiate(inGamePlayerID, list.transform);

            playerEntry.GetComponent<TMP_Text>().text = $"{player.ActorNumber}";

            playerEntries[player.ActorNumber] = playerEntry;
        }
    }

    public void SetPlayerCount()
    {
        int aliveCount = 0;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("isHit"))
            {
                bool isHit = (bool)player.CustomProperties["isHit"];
                if (!isHit) // 맞지 않은 플레이어만 카운트
                {
                    aliveCount++;
                }
            }
            else
            {
                aliveCount++; // isHit 값이 없으면 기본적으로 살아있는 상태
            }
        }

        playerCount.GetComponent<TMP_Text>().text = $"{aliveCount}";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject playerEntry = Instantiate(inGamePlayerID, list.transform);

        playerEntry.GetComponent<TMP_Text>().text = $"{newPlayer.ActorNumber}";

        playerEntries[newPlayer.ActorNumber] = playerEntry;

        // 방 생성되면 추가적으로 들어올 수 없기 때문에 나중에 지워야 함.
        playerNum = PhotonNetwork.PlayerList.Length;

        SetPlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (playerEntries.ContainsKey(otherPlayer.ActorNumber))
        {
            Destroy(playerEntries[otherPlayer.ActorNumber]);

            playerEntries.Remove(otherPlayer.ActorNumber);
        }

        playerNum = PhotonNetwork.PlayerList.Length;

        SetPlayerCount();
    }
}
