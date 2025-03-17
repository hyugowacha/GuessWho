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

    public GameObject forTest;
    // Start is called before the first frame update
    void Start()
    {
        //selfAgent.SetDestination(tempDestination.transform.position);//랜덤 목적지 지정 시스템 만들기 전에 사용하는 임시 코드 

       // Debug.Log("npcStart");
        haveToChangeState = false;
        #region 디버그용 코드
        nowState = new NPCMove();
        (nowState as NPCMove).SetDestination(NPCManager.ReturnRandomDestination());
        nowState.EnterState(this);
        nowState.StateAction();
        Instantiate(forTest, (nowState as NPCMove).ReturnDestination(),Quaternion.identity);
        StartCoroutine(CheckState());
        #endregion
        #region 실제 사용 코드 
        //if (Random.Range(0f,1f) < 0.5f)//일부는 바로 이동 일부는 대기 
        //{
        //    nowState=new NPCIdle();
        //    nowState.EnterState(this);
        //    Debug.Log("setidle");
        //}
        //else
        //{
        //    nowState = new NPCMove();
        //    nowState.EnterState(this);
        //    Debug.Log("setmove");
        //}
        //StartCoroutine(CheckState());
        #endregion
        //   selfAgent.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //if(nowState != null)
        //{
        //    nowState.StateAction();
        //}
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
                        if (Random.Range(0, 100) < 70)
                        {
                            //Debug.Log("changetomove");
                            ChangeState(new NPCMove());
                            
                        }
                        else
                        {
                            //Debug.Log("stayidle");
                            ChangeState(new NPCIdle());
                        }
                        haveToChangeState = false;

                    }
                    else if(nowState is NPCMove)
                    {
                        ChangeState(new NPCIdle());
                        //Debug.Log("changetoidle");
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
