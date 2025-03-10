using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TestingNPC : NPC
{

    //상태 패턴
    [SerializeField] GameObject tempDestination;
    [SerializeField] NavMeshAgent selfAgent;
    INPCState nowState;
    NavMeshSurface gamefield;

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

    public void ChangeState(INPCState changeState)
    {
        nowState = changeState;
        nowState.SetState();
    }

    //public void SetNPCState()
    //{
    //
    //}
}
