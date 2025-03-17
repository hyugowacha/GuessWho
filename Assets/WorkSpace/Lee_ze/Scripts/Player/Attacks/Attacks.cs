using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacks : MonoBehaviour
{
    public event Action<bool> OnAttackStateChanged;

    [SerializeField]
    private Animator kickAnim;

    private bool isAttacking;

    public bool IsAttacking => isAttacking;

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            StartCoroutine(KickAttack());
        }
    }

    IEnumerator KickAttack()
    {
        isAttacking = true;  // 공격 시작

        OnAttackStateChanged?.Invoke(isAttacking);

        kickAnim.SetBool("IsKick", true);

        yield return new WaitForSeconds(1f);

        kickAnim.SetBool("IsKick", false);

        isAttacking = false;

        OnAttackStateChanged?.Invoke(isAttacking);
    }
}
