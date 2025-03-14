using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TestingNPC : NPC
{

    //상태 패턴
    [SerializeField] GameObject tempDestination;// 랜덤 목적지 지정 시스템 만들기 전에 사용하는 임시 변수
    [SerializeField] NavMeshAgent selfAgent;
    private INPCState nowState;
    private NavMeshSurface gamefield;
    private bool haveToChangeState;//현재로서는 필요 없으나 게임매니저에 사용할 수도 있을거 같기에 선언해 놓음

    // Start is called before the first frame update
    void Start()
    {
        selfAgent.SetDestination(tempDestination.transform.position);//랜덤 목적지 지정 시스템 만들기 전에 사용하는 임시 코드 


        haveToChangeState = false;
        if (Random.Range(0,1) < 0.5f)//일부는 바로 이동 일부는 대기 
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
        nowState.EnterState(this);
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
                    if(nowState is NPCIdle)
                    {
                        ChangeState(new NPCMove());
                        haveToChangeState = false;
                    }else if(nowState is NPCMove)
                    {
                        ChangeState(new NPCIdle());
                        haveToChangeState = false;
                    }
                }
                else
                {
                    haveToChangeState=false;
                }

            }



        }
    }

    //IEnumerator CheckNPCPlacedRight()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //
    //
    //
    //    }
    //}

    public bool CheckNPCPlacedRight()
    {
        if(selfAgent.isOnNavMesh)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void HitByPlayer()
    {
        ChangeState(new NPCHit());
        haveToChangeState = false;
    }
    //public void SetNPCState()
    //{
    //
    //}
}
