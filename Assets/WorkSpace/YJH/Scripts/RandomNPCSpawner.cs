using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomNPCSpawner : MonoBehaviour
{
    [SerializeField] float spawnRadius;
    [SerializeField] Vector3 spawnCenter;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnCenter, spawnRadius);
    }
}
