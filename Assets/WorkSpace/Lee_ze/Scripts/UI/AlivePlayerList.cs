using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlivePlayerList : MonoBehaviour
{
    [SerializeField]
    private GameObject alivePlayerList;

    private GameObject list;

    private void Start()
    {
        list = Instantiate(alivePlayerList, this.transform);

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
}
