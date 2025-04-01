using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnger : MonoBehaviour
{
    
    [SerializeField] AudioSource angerSoundSource;
    public void AngerSoundPlay()
    {
        angerSoundSource.Play();
    }
}
