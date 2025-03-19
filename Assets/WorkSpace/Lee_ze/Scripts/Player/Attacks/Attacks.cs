using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacks : MonoBehaviour
{
    public event Action<bool> OnAttackStateChanged;

    [SerializeField]
    private GameObject foot;

    [SerializeField]
    private Animator kickAnim;

    private bool isAttacking;

    public bool IsAttacking => isAttacking;

    private void Start()
    {
        foot.SetActive(false);
    }

    public void OnAttack(InputAction.CallbackContext ctx) // 좌클릭 바인딩
    {
        if (ctx.phase == InputActionPhase.Started && isAttacking == false)
        {
            StartCoroutine(KickAttack());
        }
    }

    public void OnKickEnable() // 애니메이션 특정 위치에 이벤트 바인딩했음.
    {
        foot.SetActive(true);
    }

    public void OnKickDisable() // 애니메이션 특정 위치에 이벤트 바인딩했음.
    {
        foot.SetActive(false);
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
