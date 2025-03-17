using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeManager : MonoBehaviour, IHittable
{
    [SerializeField]
    private Animator knockDown;

    public void GetHit()
    {
        knockDown.SetBool("IsKnockDown", true);
    }
}
