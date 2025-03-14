using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour, IGetable
{
    [SerializeField]
    private GameObject[] items;

    private GameObject tempItem;

    [SerializeField]
    private Transform itemHolder;

    public bool isItemEmpty;

    public ItemList state;

    void Start()
    {
        isItemEmpty = true;

        GetItem(ItemList.empty);
    }

    public void GetItem(ItemList item)
    {
        tempItem = Instantiate(items[(int)item], itemHolder.position, itemHolder.rotation);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            
        }
    }

    void AttackByItem()
    {

    }
}
