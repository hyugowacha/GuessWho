using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource angerSoundSource;
    public void AngerSoundPlay()
    {
        angerSoundSource.Play();
    }
}
