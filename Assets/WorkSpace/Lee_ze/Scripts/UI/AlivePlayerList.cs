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
            list.SetActive(true);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            list.SetActive(false);
        }
    }

    private void UpdatePlayerList()
    {

    }
}
