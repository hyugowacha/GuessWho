using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using ZL.Unity;

public class TestingNPC : MonoBehaviourPunCallbacks,IHittable,IPunObservable
{

    
    
    [SerializeField] NavMeshAgent selfAgent;
    [SerializeField] Collider selfCollider;
    [SerializeField] AudioSource hitSoundSource;
    
    private INPCState nowState;
    
    public Animator animator;
   
    private Vector3 npcDestination=new Vector3();
    readonly int hashselfVel = Animator.StringToHash("selfVel");
    public NavMeshAgent SelfAgent
    {
        get { return selfAgent; }
        set { selfAgent = value; }
    }

    public INPCState NowState
    {
        get { return nowState; }
        set { nowState = value; }
    }
    public Collider SelfCollider
    {
        get { return selfCollider; }
    }

    

    public override void OnEnable()
    {
        base.OnEnable();
        
    }
    public void SetNPCTransform(Vector3 transformTo)
    {
        transform.position=transformTo;
    }


    public void InitialSet()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            StartCoroutine(CheckState());
            return;
            
        }
        else
        {
            
            StartCoroutine (NPCAnimationControl());
            if (PhotonNetwork.IsMasterClient == true)
            {
                if (Random.Range(0f, 1f) < 0.5f)//일부는 바로 이동 일부는 대기 
                {
                    photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Idle, UnityEngine.Random.Range(0.2f, 1.0f));
                    
                }
                else
                {
                    photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Walk, 0.0f);
                    
                }
                StartCoroutine(CheckState());
            }
            

        }
    }


    
    #region 상태변화 관련 코드
    
    [PunRPC]
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
            case NPCStateName.Dead:
                nowState = new NPCDead();
                nowState.EnterState(this);
                nowState.StateAction();
                break;
            default:
                break;
        }


        
    }
    
    [PunRPC]
    public void ChangeState(NPCStateName stateName, float time)//RPC용
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
                nowState.EnterState(this,time);
                nowState.StateAction();
                break;
            case NPCStateName.Walk:
                nowState = new NPCMove();
                (nowState as NPCMove).EnterState(this, ISingleton<NPCManager>.Instance.RandomDestination(this));//목적지 지정 및 상태 진입
                nowState.StateAction();
                npcDestination = (nowState as NPCMove).ReturnDestination();//목적지 저장
                break;
            case NPCStateName.Dead:
                nowState = new NPCDead();
                nowState.EnterState(this,0);
                nowState.StateAction(); 
                break;
            default:
                break;
        }
        PhotonNetwork.SendAllOutgoingCommands();
    }
    public void AfterHit()
    {
        if (nowState is NPCHit)
        {
            (nowState as NPCHit).StopAnimation();
        }
        else
        {
            return;
        }
        photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Idle, UnityEngine.Random.Range(0.2f, 1.0f));
    }


    IEnumerator NPCAnimationControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            animator.SetFloat(hashselfVel, selfAgent.velocity.magnitude);
        }
    }

    


    IEnumerator CheckState()//master client만 실행하고 있는 상태의 변화 필요 여부를 감지하는 코루틴
    {
        while (true)
        {            
            yield return new WaitForSeconds(0.1f);
            if (PhotonNetwork.IsMasterClient == false)//혹시 모를 예외처리로 마스터 클라이언트가 아니면 작동 안함
            {

            }
            else if (nowState != null)//마스터 클라이언트이면서 nowstate가 정상적으로 할당되어 있다면
            {
                if(nowState is NPCHit)//NPC가 피격 판정이 아니면 
                {
                    
                }else if (nowState.CheckStateEnd() == true)//상태가 종료되면 
                {
                    
                    if(nowState is NPCIdle)//대기였으면 이동으로
                    {
                        photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Walk, 0f);
                    }
                    else if(nowState is NPCMove)//이동이었으면
                    {
                        if (Random.Range(0, 100) < 70)//70퍼확률로
                        {
                            photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Idle,0.3f);//0.3초 대기 또는
                        }
                        else
                        {
                            photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Idle, UnityEngine.Random.Range(0.8f, 1.5f));//대기상태 진입
                        }
                    }
                }
            }
        }
    }

    #endregion
    
    

    #region 피격 관련 코드
    [PunRPC]
    public void GetHit()//플레이어에게 맞거나 돌에 맞으면 작동하는 함수
    {
        hitSoundSource.Play();
        


        if (PhotonNetwork.IsConnected == false)
        {
            ChangeState(NPCStateName.Hit);//포톤 안쓸 때 사용하는 것
            
        }
        else
        {
            photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Hit);//포톤일 때 콜백으로 마스터 클라이언트에 전달함
        }

    }
    public void GetDie()//총에 맞아 죽을 때 호출되는 함수
    {
        selfCollider.enabled = false;
        hitSoundSource.Play();
        photonView.RPC("ChangeState", Photon.Pun.RpcTarget.All, NPCStateName.Dead);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            selfCollider.enabled = false;
            
            var player = other.transform.root.transform;//플레이어관련 변경 일어날 시에는 코딩 새로 해야 함
            
            transform.LookAt(player);
            


        }
        else if(other.transform.root.tag == "Stone")
        {
            var stone = other.transform.root.transform;
            transform.LookAt(stone);
            GetHit();

        }else if(other.transform.root.tag == "Bullet")
        {
            var stone = other.transform.root.transform;
            transform.LookAt(stone);
            GetDie();
        }
    }

    #endregion
    
    public override void OnMasterClientSwitched(Player newMasterClient)//마스터 클라이언트가 변경되면
    {
        
        if(PhotonNetwork.IsMasterClient)//새로운 마스터 클라이언트는
        {
            SelfAgent.enabled = true;
            StartCoroutine(CheckState());//상태 변화 코루틴을 시작하고
            if(nowState is NPCMove)//이동 상태였을 경우
            {
                (nowState as NPCMove).EnterState(this, npcDestination);//저장된 목적지로 다시 이동상태 진입
                nowState.StateAction();
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}
