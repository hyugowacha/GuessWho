using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon;
using Photon.Pun;
using Unity.VisualScripting;



public enum NPCStateName
{
    Hit,Idle,Walk,Dead ,None

}
public interface INPCState
{
    
    public void PlayAnimation();
    public void StopAnimation();
    public void EnterState(TestingNPC npc);
    public void EnterState(TestingNPC npc,float time);

    public void StateAction();
    public bool CheckStateEnd();
    public bool ForceStateEnd();
}

public class NPCMove : INPCState
{
    //private List<Vector3> destinations;//navmesh 안쓰면 
    //private bool isMoveEnd;
    private Vector3 destination;
    private TestingNPC nowNPC;
    private NavMeshAgent selfAgent;
    private Rigidbody npcRb;
    //float temp = 0;
    private Animator npcAnimator;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashMove = Animator.StringToHash("isMove");
    readonly int hashIdle = Animator.StringToHash("isIdle");
    readonly int hashselfVel = Animator.StringToHash("selfVel");


    private float maxDelayTime = 1000f;
    private float delayTime = 0;
    //GameObject forTest;
    [PunRPC]
    public void PlayAnimation()
    {
        //animator관련
        
        npcAnimator.SetBool(hashIdle, false);
        npcAnimator.SetBool(hashMove, true);
        npcAnimator.SetFloat(hashselfVel, 1f);
        
    }
    [PunRPC]
    public void StopAnimation()
    {
        //animator관련
        npcAnimator.SetBool(hashIdle, true);
        npcAnimator.SetBool(hashMove, false);
        npcAnimator.SetFloat(hashselfVel, 0f);
        
            selfAgent.isStopped = true;
        

    }
    [PunRPC]
    public void EnterState(TestingNPC npc)
    {

        nowNPC = npc;
        selfAgent = npc.gameObject.GetComponent<NavMeshAgent>();
        selfAgent.isStopped = false;
        npcAnimator = (npc as TestingNPC).animator;
        destination = new Vector3();
        delayTime = 0;
        RandomDestination();
        //selfAgent.SetDestination(destination.position);//목적지 설정 


        //temp+= Time.deltaTime;//작동함! 개이득!

        //destination = NPCManager.ReturnRandomDestination(nowNPC);//npc 매니저에 존재하는 랜덤 좌표 설정 함수를 사용, 현재 예외처리 안되어 있음-> transform 변수 선언하면 굉장히 귀찮음 벡터3 해서 맞추는게 더 쉬움


    }
    public void EnterState(TestingNPC npc, float time)
    {
        
            
        
        selfAgent.enabled = true;
        nowNPC = npc;
        selfAgent = npc.gameObject.GetComponent<NavMeshAgent>();
        selfAgent.isStopped = false;
        nowNPC.SelfCollider.enabled = true;
        npcAnimator = (npc as TestingNPC).animator;
        destination = new Vector3();
        delayTime = 0;
        RandomDestination();
        
        
    }
    public void EnterState(TestingNPC npc, Vector3 randomDestination)//RPC용으로 오버로딩
    {
        
            nowNPC = npc;
            selfAgent = npc.gameObject.GetComponent<NavMeshAgent>();
        
        selfAgent.isStopped = false;
        npcAnimator = (npc as TestingNPC).animator;
            destination = new Vector3();
            destination = randomDestination;
            delayTime = 0;
       
    }
    private void RandomDestination()
    {
        
        var init = Random.insideUnitCircle;
        destination = nowNPC.transform.position + new Vector3(init.x * 20, 0, init.y * 20);
        while (IsDestinationOutOfRange() == true)
        {
           
            var temp = Random.insideUnitCircle;
            destination = nowNPC.transform.position + new Vector3(temp.x * 20, 0, temp.y * 20);
        }
        

    }
   
    public bool IsDestinationOutOfRange()
    {
        if ((destination-nowNPC.transform.position).magnitude<5f)
        {
            return true;
        }
        if (destination.x < -74 || destination.x > 72)
        {
            return true;
        }else if (destination.z>74||destination.z<-78) {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public Vector3 ReturnDestination()
    {
        return destination;
    }
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
    
    [PunRPC]
    public void StateAction()
    {
        selfAgent.SetDestination(destination);
        nowNPC.transform.LookAt(destination);//이거 커브 돌때는 어케 되는거지?
        

    }
  

    public bool ForceStateEnd()
    {
        return true;
    }
    public bool CheckStateEnd()
    {
        delayTime += Time.deltaTime;
        if ((destination - nowNPC.transform.position).magnitude < 5.0f)
        {
            
            return true;
        }
        else
        {
            if (delayTime >= maxDelayTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    
}

public class NPCHit : INPCState
{
    //private bool isHit;
    private TestingNPC nowNPC;
    private Animator npcAnimator;
    private NavMeshAgent npcAgent;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashMove = Animator.StringToHash("isMove");
    readonly int hashIdle = Animator.StringToHash("isIdle");
    readonly int hashselfVel = Animator.StringToHash("selfVel");

    //private bool isEnd;
    [PunRPC]
    public void PlayAnimation()
    {
        
    }
    [PunRPC]
    public void StopAnimation()
    {
        //animator관련
        npcAnimator.SetBool("isAnger", false);
        npcAnimator.SetBool(hashHit, false);
        npcAnimator.SetFloat(hashselfVel, 0f);
    }

    [PunRPC]
    public void EnterState(TestingNPC npc)
    {
        //isHit = false;
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent= (npc as TestingNPC).SelfAgent;
        npcAgent.isStopped = true;
        npcAnimator.SetBool(hashHit, true);
        

    }
    public void NPCGetHit()
    {
        
    }
    [PunRPC]
    public void StateAction()
    {
        
        
    }
    public bool CheckStateEnd()
    {
       
        return false;
        
    }
    public bool ForceStateEnd()
    {
        return true;
    }

    public void EnterState(TestingNPC npc, float time)
    {
        
    }
}
public class NPCIdle : INPCState
{
    private float idleTime;// 대기해야할 시간
    private float delaiedime;//대기한 시간
    private bool isEnd;
    private TestingNPC nowNPC;
    private Animator npcAnimator;
    private NavMeshAgent npcAgent;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashMove = Animator.StringToHash("isMove");
    readonly int hashIdle = Animator.StringToHash("isIdle");
    readonly int hashselfVel = Animator.StringToHash("selfVel");
    [PunRPC]
    public void PlayAnimation()
    {
        
        npcAnimator.SetBool("isAnger", false);
        npcAnimator.SetBool(hashHit, false);
        npcAnimator.SetFloat(hashselfVel, 0f);
        //animator관련

    }
    [PunRPC]
    public void StopAnimation()
    {
        npcAnimator.SetBool(hashIdle, false);
        npcAnimator.SetBool(hashMove, true);
        //animator관련

    }
    [PunRPC]
    public void EnterState(TestingNPC npc)
    {
        isEnd = false;
        idleTime = UnityEngine.Random.Range(0.2f,1.0f);//대기 시간 설정
        //Debug.Log(idleTime);
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent = (npc as TestingNPC).SelfAgent;
        npcAgent.isStopped = true;
        npcAnimator.SetBool("isAnger", false);
        npcAnimator.SetBool(hashHit, false);
        
        npcAnimator.SetFloat(hashselfVel, 0f);
        //npcAnimator.SetBool("isAnger", false);
        //npcAnimator.SetBool(hashHit, false);
    }
    public void EnterState(TestingNPC npc, float time)
    {

        isEnd = false;
        idleTime = time;
        //Debug.Log(idleTime);
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent = (npc as TestingNPC).SelfAgent;
        nowNPC.SelfCollider.enabled = true;
        npcAgent.isStopped = true;
        npcAnimator.SetBool("isAnger", false);
        npcAnimator.SetBool(hashHit, false);
        
        npcAnimator.SetFloat(hashselfVel, 0f);
    }
    [PunRPC]
    public void StateAction()
    {
        //StartCoroutine(NPCIdleAnimationPlay());
        //PlayAnimation();
    }

    public void SetDelayTime(float time)
    {
        idleTime=time;
    }
    public bool CheckStateEnd()
    {
        delaiedime += Time.deltaTime;
        if(delaiedime>idleTime)
        {
            isEnd = true;
        }
        else
        {
            isEnd=false;
        }
        if (isEnd == true)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ForceStateEnd()
    {
        return true;
    }

    
}
public class NPCDead : INPCState
{

    private TestingNPC nowNPC;
    private Animator npcAnimator;
    private NavMeshAgent npcAgent;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashDead = Animator.StringToHash("isDead");
    readonly int hashselfVel = Animator.StringToHash("selfVel");
    public bool CheckStateEnd()
    {
        return false;
    }

    public void EnterState(TestingNPC npc)
    {
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent = (npc as TestingNPC).SelfAgent;
        npcAgent.isStopped = true;
    }

    public void EnterState(TestingNPC npc, float time)
    {
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent = (npc as TestingNPC).SelfAgent;
        npcAgent.isStopped = true;
    }

    public bool ForceStateEnd()
    {
        return true;
    }

    public void PlayAnimation()
    {
        npcAnimator.SetBool(hashDead, true);
    }

    public void StateAction()
    {
        PlayAnimation();
    }

    public void StopAnimation()
    {
        return;
    }
}











//public class NPCRun : INPCState// 일단 걷기만 구현 달리기는 나중에
//{
//    public void PlayAnimation()
//    {
//        //animator관련
//
//    }
//
//    public void StopAnimation()
//    {
//        //animator관련
//
//    }
//
//    public void SetState()
//    {
//
//    }
//
//    public void StateAction()
//    {
//
//    }
//
//
//}



