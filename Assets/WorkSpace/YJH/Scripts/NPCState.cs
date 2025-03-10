using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface INPCState
{
    public void PlayAnimation();
    public void StopAnimation();
    public void SetState();

}

public class NPCMove : INPCState
{
    List<Vector3> destinations;//navmesh 안쓰면 


    public void PlayAnimation()
    {
        //animator관련
        
    }

    public void StopAnimation()
    {
        //animator관련

    }
    public void SetState()
    {
        destinations = new List<Vector3> ();
        destinations.Add (NPCManager.ReturnRandomDestination ());
    }
    



}
public class NPCRun : INPCState
{
    public void PlayAnimation()
    {
        //animator관련

    }

    public void StopAnimation()
    {
        //animator관련

    }

    public void SetState()
    {

    }

    


}
public class NPCHit : INPCState
{
    public void PlayAnimation()
    {
        //animator관련

    }

    public void StopAnimation()
    {
        //animator관련

    }


    public void SetState()
    {

    }
    public void NPCGetHit()
    {
        //게임 매니저나 여타 피격시 시행될 기능
    }

}
public class NPCIdle : INPCState
{
    private float idleTime;


    public void PlayAnimation()
    {
        //animator관련

    }

    public void StopAnimation()
    {
        //animator관련

    }

    public void SetState()
    {
        idleTime = UnityEngine.Random.Range(0.5f,2f);//대기 시간 설정
    }



}



