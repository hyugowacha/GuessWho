using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootGizmo : MonoBehaviour
{
    public float radius = 0.2f;

    public Color gizmoColor = Color.green;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Gizmos.DrawSphere(transform.position, radius);
    }
}
