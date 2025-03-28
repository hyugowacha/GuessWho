using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AlivePlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject alivePlayerListPrefab;

    [SerializeField]
    private GameObject alivePlayerID;

    private GameObject list;

    private Dictionary<int, GameObject> playerEntries = new Dictionary<int, GameObject>();

    private void Start()
    {
        list = Instantiate(alivePlayerListPrefab, this.transform);

        list.SetActive(false);

        UpdatePlayerList();
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
            GameObject playerEntry = Instantiate(alivePlayerID, list.transform);

            playerEntry.GetComponent<Text>().text = $"{player.ActorNumber}";

            playerEntries[player.ActorNumber] = playerEntry;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.ActorNumber);

        GameObject playerEntry = Instantiate(alivePlayerID, list.transform);

        playerEntry.GetComponent<Text>().text = $"{newPlayer.ActorNumber}";

        playerEntries[newPlayer.ActorNumber] = playerEntry;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (playerEntries.ContainsKey(otherPlayer.ActorNumber))
        {
            Destroy(playerEntries[otherPlayer.ActorNumber]);

            playerEntries.Remove(otherPlayer.ActorNumber);
        }
    }
}
