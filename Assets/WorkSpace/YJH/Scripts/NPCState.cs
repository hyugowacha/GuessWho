using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon;
using Photon.Pun;
using Unity.VisualScripting;


public enum NPCStateName
{
    Hit,Idle,Walk,None
}
public interface INPCState
{
    
    public void PlayAnimation();
    public void StopAnimation();
    public void EnterState(NPC npc);

    public void StateAction();
    public bool CheckStateEnd();
    public bool ForceStateEnd();
}

public class NPCMove : INPCState
{
    //private List<Vector3> destinations;//navmesh 안쓰면 
    //private bool isMoveEnd;
    private Vector3 destination;
    private NPC nowNPC;
    private NavMeshAgent selfAgent;
    //float temp = 0;
    private Animator npcAnimator;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashMove = Animator.StringToHash("isMove");
    readonly int hashIdle = Animator.StringToHash("isIdle");

    //GameObject forTest;
    [PunRPC]
    public void PlayAnimation()
    {
        //animator관련
        
        npcAnimator.SetBool(hashIdle, false);
        npcAnimator.SetBool(hashMove, true);
        
    }
    [PunRPC]
    public void StopAnimation()
    {
        //animator관련
        npcAnimator.SetBool(hashIdle, true);
        npcAnimator.SetBool(hashMove, false);
        selfAgent.isStopped = true;

    }
    [PunRPC]
    public void EnterState(NPC npc)
    {
        nowNPC = npc;
        selfAgent = npc.gameObject.GetComponent<NavMeshAgent>();
        selfAgent.isStopped = false;
        npcAnimator = (npc as TestingNPC).animator;
        destination = new Vector3();
        //destination.position = NPCManager.ReturnRandomDestination();//npc 매니저에 존재하는 랜덤 좌표 설정 함수를 사용, 현재 예외처리 안되어 있음
        //selfAgent.SetDestination(destination.position);//목적지 설정 
        //forTest= npc.GetComponent<TestingNPC>().forTest;

        //temp+= Time.deltaTime;//작동함! 개이득!

        destination = NPCManager.ReturnRandomDestination();//npc 매니저에 존재하는 랜덤 좌표 설정 함수를 사용, 현재 예외처리 안되어 있음-> transform 변수 선언하면 굉장히 귀찮음 벡터3 해서 맞추는게 더 쉬움
        //이거 navmesh위에 존재하게 해야 할듯 어케 하징 
        //Debug.Log(destination);   
        
        //isMoveEnd = false;
       
    }
    public Vector3 ReturnDestination()
    {
        return destination;
    }
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
    //public void SetState(Transform destination)
    //{
    //    destinations = new List<Vector3>();
    //    destination.position=destinations[0];
    //    StateAction();
    //    this.destination = destination;
    //}
    [PunRPC]
    public void StateAction()
    {
        PlayAnimation();
        selfAgent.SetDestination(destination);
        nowNPC.transform.LookAt(destination);



    }
    //public bool CheckStateEnd(Transform npcTransform)
    //{
    //    if ((destination.position - npcTransform.position).magnitude < 0.2f)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //
    //}

    public bool ForceStateEnd()
    {
        return true;
    }
    public bool CheckStateEnd()// 이쪽을 좀 더 신경써야 할듯 건물 안에 생성된 목적지가 있으면 자꾸 고장남
    {
        if ((destination - nowNPC.transform.position).magnitude < 5.0f)
        {
            //Debug.Log((destination - nowNPC.transform.position).magnitude);
            StopAnimation();
            return true;
        }
        else
        {
            return false;
        }
    }

}

public class NPCHit : INPCState
{
    private bool isHit;
    private NPC nowNPC;
    private Animator npcAnimator;
    private NavMeshAgent npcAgent;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashMove = Animator.StringToHash("isMove");
    readonly int hashIdle = Animator.StringToHash("isIdle");

    //private bool isEnd;
    [PunRPC]
    public void PlayAnimation()
    {
        //animator관련
        npcAnimator.SetBool(hashHit,true);
        npcAnimator.SetBool(hashIdle,true);
        npcAnimator.SetBool("isAnger", true);
    }
    [PunRPC]
    public void StopAnimation()
    {
        //animator관련
        npcAnimator.SetBool("isAnger", false);
        npcAnimator.SetBool(hashHit, false);

    }

    [PunRPC]
    public void EnterState(NPC npc)
    {
        isHit = false;
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent= (npc as TestingNPC).SelfAgent;
        npcAgent.isStopped = true;
    }
    public void NPCGetHit()
    {
        //게임 매니저나 여타 피격시 시행될 기능
        isHit=true;
    }
    [PunRPC]
    public void StateAction()
    {
        PlayAnimation();
        
    }
    public bool CheckStateEnd()
    {
        if (npcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
        if (isHit == true)
        {
            StopAnimation();
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
public class NPCIdle : INPCState
{
    private float idleTime;// 대기해야할 시간
    private float delaiedime;//대기한 시간
    private bool isEnd;
    private NPC nowNPC;
    private Animator npcAnimator;
    private NavMeshAgent npcAgent;
    readonly int hashHit = Animator.StringToHash("isHit");
    readonly int hashMove = Animator.StringToHash("isMove");
    readonly int hashIdle = Animator.StringToHash("isIdle");
    [PunRPC]
    public void PlayAnimation()
    {
        npcAnimator.SetBool(hashIdle, true);
        npcAnimator.SetBool(hashMove, false);
        npcAnimator.SetBool("isAnger", false);
        npcAnimator.SetBool(hashHit, false);
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
    public void EnterState(NPC npc)
    {
        isEnd = false;
        idleTime = UnityEngine.Random.Range(0.5f,2f);//대기 시간 설정
        //Debug.Log(idleTime);
        nowNPC = npc;
        npcAnimator = (npc as TestingNPC).animator;
        npcAgent = (npc as TestingNPC).SelfAgent;
        npcAgent.isStopped = true;
    }
    [PunRPC]
    public void StateAction()
    {
        //StartCoroutine(NPCIdleAnimationPlay());
        PlayAnimation();
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
            //Debug.Log("endidle");
            StopAnimation();
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



