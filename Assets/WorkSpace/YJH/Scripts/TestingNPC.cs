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
    private INPCState nowState;
    private NavMeshSurface gamefield;
    private bool haveToChangeState;

    // Start is called before the first frame update
    void Start()
    {
        selfAgent.SetDestination(tempDestination.transform.position);
        haveToChangeState = false;
        if (Random.Range(0,1) < 0.5f)
        {
            nowState=new NPCIdle();
        }
        else
        {
            nowState = new NPCMove();
        }
        StartCoroutine(CheckState());
     //   selfAgent.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(nowState != null)
        {
            nowState.StateAction();
        }
    }
    public void ChangeState(INPCState changeState)
    {
        nowState = changeState;
        nowState.SetState();
        nowState.StateAction();
       
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (nowState != null)
            {
                if (nowState.CheckStateEnd() == true)
                {
                    haveToChangeState=true;
                }
                else
                {
                    haveToChangeState=false;
                }

            }



        }
    }

    //public void SetNPCState()
    //{
    //
    //}
}
