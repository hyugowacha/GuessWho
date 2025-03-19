using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private IPlayerStates currentState;

    [Header("Move"), Space(10)]

    public float moveSpeed;

    public float targetSpeed;

    public Rigidbody rb;

    public Vector2 direction;

    public bool isRunning;

    private bool isAttacking;

    public RotateModel modelRotator;

    public Animator moveAnimation;


    [Space(20), Header("Attack"), Space(10)]

    public ItemType holdingWeapon;

    public GameObject[] weapons;

    public Animator kickAnimation;

    private void OnEnable()
    {
        isRunning = false;

        holdingWeapon = ItemType.None;

        foreach (var weapon in weapons)
        {
            weapon.SetActive(false); // 모든 무기 비활성화
        }
    }

    private void Start()
    {
        ChangeStateTo(new IdleState());
    }

    private void Update()
    {
        currentState?.UpdatePerState();
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
        if (ctx.phase == InputActionPhase.Started && isAttacking == false)
        {
            ChangeStateTo(new AttackState());
        }
    }

    public void OnKickEnable() // 애니메이션 특정 위치에 이벤트 바인딩했음.
    {
        weapons[0].SetActive(true);
    }

    public void OnKickDisable() // 애니메이션 특정 위치에 이벤트 바인딩했음.
    {
        weapons[0].SetActive(false);
    }

}
