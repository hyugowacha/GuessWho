using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using WebSocketSharp;

public class AlivePlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject alivePlayerListPrefab;

    [SerializeField]
    private GameObject alivePlayerID;

    private GameObject list;

    private void Start()
    {
        list = Instantiate(alivePlayerListPrefab, this.transform);

        list.SetActive(false);
    }

    public void OnCheckAlivePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            UpdatePlayerList();

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
            GameObject playerEntry = Instantiate(alivePlayerID, alivePlayerListPrefab.transform);

            Text textComponent = playerEntry.GetComponent<Text>();

            textComponent.text = $"(ID: {player.ActorNumber})";

            Debug.Log(player.ActorNumber);
        }
    }
}
