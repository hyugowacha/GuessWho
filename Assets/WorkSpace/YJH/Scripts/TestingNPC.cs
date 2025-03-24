using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TestingNPC : NPC,IHittable
{

    //상태 패턴
    //[SerializeField] GameObject tempDestination;// 랜덤 목적지 지정 시스템 만들기 전에 사용하는 임시 변수
    [SerializeField] NavMeshAgent selfAgent;
    [SerializeField] Collider selfCollider;
    private INPCState nowState;
    //private NavMeshSurface gamefield;
    private bool haveToChangeState;//현재로서는 필요 없으나 게임매니저에 사용할 수도 있을거 같기에 선언해 놓음
    private bool hasHit = false;
    private float hitPenaltyTime=0.15f;
    [SerializeField] float hitTime = 0;
    //public GameObject forTest;//목적지 디버그용 완제품엔 필요 없음
    public Animator animator;
    
    public NavMeshAgent SelfAgent { get { return selfAgent; } set { selfAgent = value; } }



    void Start()
    {
        //selfAgent.SetDestination(tempDestination.transform.position);//랜덤 목적지 지정 시스템 만들기 전에 사용하는 임시 코드 

       // Debug.Log("npcStart");
        haveToChangeState = false;
        #region 디버그용 코드
        //nowState = new NPCMove();
        //(nowState as NPCMove).SetDestination(NPCManager.ReturnRandomDestination());
        //nowState.EnterState(this);
        //nowState.StateAction();
        //Instantiate(forTest, (nowState as NPCMove).ReturnDestination(),Quaternion.identity);
        //StartCoroutine(CheckState());
        #endregion
        #region 실제 사용 코드 
        //호스트인지 감지해서 호스트일 경우에만 작동
        if (PhotonNetwork.IsConnected == false)
        {
            if (Random.Range(0f, 1f) < 0.5f)//일부는 바로 이동 일부는 대기 
            {
                ChangeState(NPCStateName.Idle);
                //nowState=;
                //nowState.EnterState(this);
                //Debug.Log("setidle");
            }
            else
            {
                ChangeState(NPCStateName.Walk);
                //nowState = new NPCMove();
                //nowState.EnterState(this);
                // Debug.Log("setmove");
            }
            StartCoroutine(CheckState());
        }
        else
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                if (Random.Range(0f, 1f) < 0.5f)//일부는 바로 이동 일부는 대기 
                {
                    ChangeState(NPCStateName.Idle);//
                    //nowState=;
                    //nowState.EnterState(this);
                    //Debug.Log("setidle");
                }
                else
                {
                    ChangeState(NPCStateName.Walk);
                    //nowState = new NPCMove();
                    //nowState.EnterState(this);
                    // Debug.Log("setmove");
                }
                StartCoroutine(CheckState());
            }

        }
        
        #endregion
        //   selfAgent.
    }
    public override void OnEnable()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            return;
            //if (Random.Range(0f, 1f) < 0.5f)//일부는 바로 이동 일부는 대기 
            //{
            //    ChangeState(new NPCIdle());
            //    //nowState=;
            //    //nowState.EnterState(this);
            //    //Debug.Log("setidle");
            //}
            //else
            //{
            //    ChangeState(new NPCMove());
            //    //nowState = new NPCMove();
            //    //nowState.EnterState(this);
            //    // Debug.Log("setmove");
            //}
            //StartCoroutine(CheckState());
        }
        else
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                if (Random.Range(0f, 1f) < 0.5f)//일부는 바로 이동 일부는 대기 
                {
                    ChangeState(NPCStateName.Idle);//
                    //nowState=;
                    //nowState.EnterState(this);
                    //Debug.Log("setidle");
                }
                else
                {
                    ChangeState(NPCStateName.Walk);
                    //nowState = new NPCMove();
                    //nowState.EnterState(this);
                    // Debug.Log("setmove");
                }
                StartCoroutine(CheckState());
            }

        }
    }
    private void OnDisable()
    {
        

    }
    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
    //private void FixedUpdate()
    //{
    //    //if(nowState != null)
    //    //{
    //    //    nowState.StateAction();
    //    //}
    //}
    #region 상태변화 관련 코드
    [PunRPC]
    //public void ChangeState(INPCState changeState)
    //{
    //    
    //    nowState = changeState;
    //    nowState.EnterState(this);
    //    nowState.StateAction();
    //   
    //}

    public void ChangeState(NPCStateName stateName)
    {
        switch (stateName)
        {
            case NPCStateName.None:
                break;
            case NPCStateName.Hit:
                nowState = new NPCHit();
                nowState.EnterState(this);
                nowState.StateAction();
                break;
            case NPCStateName.Idle:
                nowState = new NPCIdle();
                nowState.EnterState(this);
                nowState.StateAction();
                break;
            case NPCStateName.Walk:
                nowState = new NPCMove();
                nowState.EnterState(this);
                nowState.StateAction();
                break;
            default:
                break;
        }


        
    }


    IEnumerator CheckState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (nowState != null)
            {
                if(nowState is NPCHit)
                {
                    hitTime += Time.deltaTime;
                    if (hitPenaltyTime <= hitTime)
                    {
                        selfCollider.enabled = true;
                        hitTime = 0;
                        (nowState as NPCHit).StopAnimation();
                        ChangeState(NPCStateName.Idle);//new NPCIdle());
                    }
                    
                }


                if (nowState.CheckStateEnd() == true)
                {
                    haveToChangeState=true;
                    if(nowState is NPCIdle)
                    {
                        ChangeState(NPCStateName.Walk);//new NPCMove());
                        //if (Random.Range(0, 100) < 70)
                        //{
                        //    //Debug.Log("changetomove");
                        //    ChangeState(new NPCMove());
                        //    
                        //}
                        //else
                        //{
                        //    //Debug.Log("stayidle");
                        //    ChangeState(new NPCIdle());
                        //}
                        haveToChangeState = false;

                    }
                    else if(nowState is NPCMove)
                    {
                        ChangeState(NPCStateName.Idle);
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

    #endregion
    public bool CheckNPCPlacedRight()//구현해놨지만 사용하지는 않는중 
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



    #region 피격 관련 코드
    [PunRPC]
    public void GetHit()//puncallback해야 함-> 애니메이션 상 로테이션을 변경해서 플레이어쪽을 보고 화내야 함 
    {
        selfCollider.enabled = false;


        if (PhotonNetwork.IsConnected == false)
        {
            ChangeState(NPCStateName.Hit);//포톤 안쓸 때 사용하는 것
            //Debug.Log("hitmethod");
        }
        else
        {
            photonView.RPC("ChangeState", Photon.Pun.RpcTarget.MasterClient, NPCStateName.Hit);//포톤일 때 콜백으로 마스터 클라이언트에 전달함
        }


        //Debug.Log("gethit");
        haveToChangeState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            var player = other.transform.root.transform;//플레이어관련 변경 일어날 시에는 코딩 새로 해야 함
            //Debug.Log("detectplayer");
            transform.LookAt(player);


        }
    }

    #endregion
}
