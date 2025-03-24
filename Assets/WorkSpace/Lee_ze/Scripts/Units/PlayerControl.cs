using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviourPun, IHittable
{
    private IPlayerStates currentState;

    public Animator playerAnim;

    [Header("Move"), Space(10)]

    public float moveSpeed;

    public float targetSpeed;

    public Rigidbody rb;

    public Vector2 direction;

    public bool isRunning = false;

    public RotateModel modelRotator;


    [Space(20), Header("Attack"), Space(10)]

    public ItemType holdingWeapon;

    public bool isAttackTriggered = false;

    public GameObject[] weapons;

    public bool isHit = false;

    public bool isNPC = false; 

    private void OnEnable()
    {
        holdingWeapon = ItemType.None;

        foreach (var weapon in weapons)
        {
            weapon.SetActive(false); // 모든 무기 비활성화
        }
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            ChangeStateTo(new IdleState());

            GetComponent<RotateView>().SetTarget(this.transform);
        }

        if (photonView.IsMine)
        {
            ChangeStateTo(new IdleState());
        }
    }

    private void Update()
    {
        if (photonView.IsMine) // 자신의 캐릭터만 로컬에서 조작 가능
        {
            currentState?.UpdatePerState();
        }
    }

    public void ChangeStateTo(IPlayerStates nextState)
    {
        currentState?.ExitState();

        currentState = nextState;

        currentState.EnterState(this);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            direction = ctx.ReadValue<Vector2>();
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            direction = Vector2.zero;

            rb.velocity = Vector3.zero;
        }
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (ctx.phase == InputActionPhase.Performed)
        {
            isRunning = true;
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            isRunning = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx) // 좌클릭 바인딩
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (ctx.phase == InputActionPhase.Started)
        {
            isAttackTriggered = true;
        }
    }

    public void OnKickEnable() // Kick 애니메이션 특정 위치에 이벤트 바인딩했음.
    {
        weapons[0].SetActive(true);
    }

    public void OnKickDisable() // Kick 애니메이션 특정 위치에 이벤트 바인딩했음.
    {
        weapons[0].SetActive(false);
    }

    public void GetHit()
    {
        photonView.RPC("SyncHitState", RpcTarget.Others, true);
    }

    // V RPC Methods

    [PunRPC]
    private void SyncHitState(bool hit)
    {
        isHit = hit;
    }
}
