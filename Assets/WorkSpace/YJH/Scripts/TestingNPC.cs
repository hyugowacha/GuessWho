using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestinNPC : MonoBehaviour
{


    [SerializeField] GameObject tempDestination;
    [SerializeField] NavMeshAgent selfAgent;

    // Start is called before the first frame update
    void Start()
    {
        selfAgent.SetDestination(tempDestination.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
