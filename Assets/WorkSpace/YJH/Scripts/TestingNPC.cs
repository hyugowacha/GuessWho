using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestinNPC : MonoBehaviour
{

    //상태 패턴
    [SerializeField] GameObject tempDestination;
    [SerializeField] NavMeshAgent selfAgent;

    // Start is called before the first frame update
    void Start()
    {
        selfAgent.SetDestination(tempDestination.transform.position);
     //   selfAgent.
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SetNPCState()
    {

    }
}
