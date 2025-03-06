using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private Rigidbody rb;

    private Vector3 moveDir;

    private void FixedUpdate()
    {
        if (moveDir != Vector3.zero)
        {
            Vector3 move = moveSpeed * Time.fixedDeltaTime * new Vector3(moveDir.x, 0, moveDir.z);

            rb.MovePosition(transform.position + move);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();

            moveDir = new Vector3(direction.x, 0, direction.y);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            moveDir = Vector3.zero;

            rb.velocity = Vector3.zero;
        }
    }
}
