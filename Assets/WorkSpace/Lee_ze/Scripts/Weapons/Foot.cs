using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public event Action<bool> WhatIsIt;

    private bool IsNPC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHittable>()?.GetHit();

            IsNPC = false;

            WhatIsIt?.Invoke(IsNPC);
        }

        if (other.CompareTag("NPC"))
        {
            other.GetComponent<IHittable>()?.GetHit();

            IsNPC = true;

            WhatIsIt?.Invoke(IsNPC);
        }
    }
}
