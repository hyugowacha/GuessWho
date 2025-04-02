using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    float coolTime = 1.0f;
    float elapsedTime = 0.0f;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource.Play();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime; 
        if(elapsedTime > coolTime)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
