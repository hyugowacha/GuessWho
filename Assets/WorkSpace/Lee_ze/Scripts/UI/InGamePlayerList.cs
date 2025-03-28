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
    private PlayerControl player;

    [SerializeField]
    private GameObject inGamePlayerListPanel;

    [SerializeField]
    private GameObject inGamePlayerID;

    [SerializeField]
    private GameObject inGamePlayerCount;

    private GameObject list;

    private GameObject playerCount;

    private int deathCount = 0;

    private Dictionary<int, GameObject> playerEntries = new Dictionary<int, GameObject>();

    IEnumerator Start()
    {
        yield return new WaitUntil(() => (PhotonNetwork.PlayerList).Length > 0);

        player.OnCheckIsDead += UpdateDeathCount;

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

    private void SetPlayerCount()
    {
        playerCount = Instantiate(inGamePlayerCount, this.transform);

        playerCount.GetComponent<TMP_Text>().text = $"{PhotonNetwork.PlayerList.Length}";
    }

    private void UpdatePlayerCount()
    {
        playerCount.GetComponent<TMP_Text>().text = $"{PhotonNetwork.PlayerList.Length - deathCount}";
    }

    private void UpdateDeathCount()
    {
        Debug.Log(deathCount);

        deathCount++;

        UpdatePlayerCount();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject playerEntry = Instantiate(inGamePlayerID, list.transform);

        playerEntry.GetComponent<TMP_Text>().text = $"{newPlayer.ActorNumber}";

        playerEntries[newPlayer.ActorNumber] = playerEntry;

        // 방 생성되면 추가적으로 들어올 수 없기 때문에 나중에 지워야 함.
        playerCount.GetComponent<TMP_Text>().text = $"{PhotonNetwork.PlayerList.Length}"; // 들어올 때 Count 업데이트
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (playerEntries.ContainsKey(otherPlayer.ActorNumber))
        {
            Destroy(playerEntries[otherPlayer.ActorNumber]);

            playerEntries.Remove(otherPlayer.ActorNumber);
        }

        UpdatePlayerCount(); // 나갈 때 Count 업데이트
    }
}
