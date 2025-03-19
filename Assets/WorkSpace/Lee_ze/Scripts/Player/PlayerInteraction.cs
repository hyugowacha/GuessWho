using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IHittable
{
    [SerializeField]
    private Animator knockDown;

    public void GetHit()
    {
        knockDown.SetBool("IsKnockDown", true);

        // GAME OVER 로직 수행
    }
}
