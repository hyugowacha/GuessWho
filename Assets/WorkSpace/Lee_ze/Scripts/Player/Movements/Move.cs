using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private float targetSpeed;

    [SerializeField]
    private Rigidbody rb;

    private Vector2 direction;

    private bool isRunning;

    [SerializeField]
    private RotateModel modelRotator;

    [SerializeField]
    private Attacks attacks;

    private bool isAttacking;

    [SerializeField]
    private Animator playerAnim;

    private void Start()
    {
        isRunning = false;

        moveSpeed = 0f;

        targetSpeed = 0f;
    }

    private void FixedUpdate()
    {
        if (isAttacking == true)  // 공격 중이면 이동 및 회전 로직을 실행하지 않음
        {
            return;
        }

        Vector3 camForward = Camera.main.transform.forward;
        
        camForward.y = 0;

        Vector3 camRight = Camera.main.transform.right;

        camRight.y = 0;

        if (direction != Vector2.zero)
        {
            targetSpeed = isRunning ? 0.12f : 0.06f;

            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, Time.deltaTime * 5f);

            Vector3 moveDir = (camRight * direction.x + camForward * direction.y).normalized;

            Vector3 move = moveSpeed * new Vector3(moveDir.x, 0, moveDir.z);

            rb.MovePosition(transform.position + move);

            modelRotator.SetTargetDirection(move);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, Time.deltaTime * 5f); ;
        }

        playerAnim.SetFloat("Speed", moveSpeed / 0.12f);
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

    private void HandleAttackStateChanged(bool attacking)
    {
        isAttacking = attacking;  // 공격 상태 업데이트
    }

    private void OnEnable()
    {
        // Attacks 스크립트의 이벤트 구독
        attacks.OnAttackStateChanged += HandleAttackStateChanged;
    }

    private void OnDisable()
    {
        // 구독 해제
        attacks.OnAttackStateChanged -= HandleAttackStateChanged;
    }
}
