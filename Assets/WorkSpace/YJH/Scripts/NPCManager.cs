using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using ZL.Unity;

public class NPCManager : MonoBehaviourPunCallbacks, IPunObservable, ISingleton<NPCManager>
{
    
    private NPCPool pool;//NPC 풀
    [SerializeField] GameObject npc;// NPC 프리팹
    [SerializeField] GameObject npcGroup;//생성된 npc들을 가지고 있을 부모 오브젝트
    [SerializeField] GameObject spawnGroup;// 스폰포인트를 가지고 있을 부모 오브젝트
    [SerializeField] List<GameObject> npcSpawnList;// 부모 오브젝트로부터 스폰포인트를 저장할 변수
    private float poolNumber = 50;
 
    private List<TestingNPC> npcScriptList;//생성된 NPC들 보유하는 리스트 둘중 택1?
                                    

    private void Awake()
    {
        //ISingleton<NPCManager>.TrySetInstance(this);

        pool = new NPCPool();//풀 생성
        pool.SetPrefab(npc);
    }

    void Start()
    {
        
       //npcScriptList = new List<TestingNPC>();
       //npcSpawnList = new List<GameObject>();
       //
       //SetSpawnPoint();//초기 스폰 메커니즘 -> 나중에 완성도를 끌어올릴때 다른 로직을 사용해 보자 -> 단점으로는 밀도가 높아져 빈 공간이 생길 수 밖에 없음
        
          
    }

    private void OnDestroy()
    {
        ISingleton<NPCManager>.Release(this);
    }

    public void SetSpawnPoint()
    {
        for (int i = 0; i < spawnGroup.transform.childCount; i++)//스폰 포인트 
        {
            npcSpawnList.Add(spawnGroup.transform.GetChild(i).gameObject);
        }
    }
    
    
    public void InitialSetBySpawnPoint()//스폰포인트를 바탕으로 npc 배치 
    {
        ISingleton<NPCManager>.TrySetInstance(this);
        
        npcScriptList = new List<TestingNPC>();
        npcSpawnList = new List<GameObject>();
        SetSpawnPoint();
        CreateAllNPC();
        

        if (PhotonNetwork.IsConnected == false)//포톤에 연결되어 있지 않다면
        {
            foreach (TestingNPC npc in npcScriptList)//npc들을
            {

                
                int spawnIndex = Random.Range(0, npcSpawnList.Count);

               
                (npc as TestingNPC).SelfAgent.enabled = false;
                SetNPCTransform(npc.gameObject, npcSpawnList[spawnIndex].transform.position + new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)));//랜덤하게 위치 설정
                npc.gameObject.transform.Rotate(0, Random.Range(0f, 360f), 0);
                (npc as TestingNPC).SelfAgent.enabled = true;
                (npc as TestingNPC).InitialSet();
                

            }
        }
        else//포톤에 연결되어 있으면
        {
            if (PhotonNetwork.IsMasterClient != true)
            {
                
            }
            else
            {
                
                
                foreach (TestingNPC npc in npcScriptList)//npc들을
                {
                    
                    int spawnIndex = Random.Range(0, npcSpawnList.Count);

                   
                    (npc as TestingNPC).SelfAgent.enabled = false;
                    int tempID = npc.photonView.ViewID;
                    Vector3 temp = new Vector3();
                    temp = npcSpawnList[spawnIndex].transform.position +new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
                    photonView.RPC("SetNPCTransformByID", Photon.Pun.RpcTarget.AllBuffered, tempID, temp);
                    
                    npc.gameObject.transform.Rotate(0, Random.Range(0f, 360f), 0);                                                                                            
                    (npc as TestingNPC).SelfAgent.enabled = true;
                    (npc as TestingNPC).InitialSet();
                    


                }
            }
        }

        PhotonNetwork.SendAllOutgoingCommands();
    }

    


    [PunRPC]// 
    public void SetNPCTransform(GameObject npc, Vector3 position)//y 좌표 1.09// 작동은 하는데 원하는 위치까지 이동하지 않고 중간에 멈추는 현상 발생 설마 하는데 Agent 때문인가? 
    {
        
        npc.transform.position= new Vector3(position.x, 3.0f, position.z);
    }
    [PunRPC]
    public void SetNPCTransformByID(int viewID,Vector3 position)
    {
        var npc = PhotonView.Find(viewID);
        
        var npcagent = npc.GetComponent<NavMeshAgent>();
       
        npcagent.Warp(position);
    }
    [PunRPC]
    public void AddNPCListByPhotonID(int viewID)
    {
        var tempNPC=PhotonView.Find(viewID);
        //Debug.Log(npcGroup.transform);
        tempNPC.gameObject.transform.SetParent(npcGroup.transform);
        npcScriptList.Add(tempNPC.GetComponent<TestingNPC>());

    }
   
    public static Vector3 ReturnRandomDestination()
    {
        Vector3 destination;
        destination = new Vector3(Random.Range(-74,72),1.5f, Random.Range(-78, 74));
        
        return destination;
    }
    
    public void CreateAllNPC()//npc를 초기 숫자만큼 생성
    {
        for(int i=0; i<poolNumber; i++)
        {
            
            if (PhotonNetwork.IsConnected)
            {

                if (PhotonNetwork.IsMasterClient == true)
                {
                    
                    
                    var tempNPC = PhotonNetwork.InstantiateRoomObject(npc.name, Vector3.zero, Quaternion.identity, 0);
                    tempNPC.transform.SetParent(npcGroup.transform);
                    int tempID=tempNPC.GetComponent<PhotonView>().ViewID;
                   
                    photonView.RPC("AddNPCListByPhotonID", Photon.Pun.RpcTarget.AllBuffered, tempID);
                    
                }
                else
                {
                    return;
                }
            }
            else
            {
                var tempNPC = pool.GetNPC(npcGroup);
                npcScriptList.Add(tempNPC.GetComponent<TestingNPC>());
            }
            
        }
    }

    public Vector3 RandomDestination(TestingNPC nowNPC)
    {
        
        Vector3 destination = new Vector3();
        var init = Random.insideUnitCircle;
        destination = nowNPC.transform.position + new Vector3(init.x * 20, 0, init.y * 20);
        while (IsDestinationOutOfRange(destination,nowNPC) == true)
        {
            
            var temp = Random.insideUnitCircle;
            destination = nowNPC.transform.position + new Vector3(temp.x * 20, 0, temp.y * 20);
        }
        
        return destination;
    }
    



    public bool IsDestinationOutOfRange(Vector3 destination,TestingNPC nowNPC)
    {
        if ((destination - nowNPC.transform.position).magnitude < 5f)
        {
            return true;
        }
        if (destination.x < -74 || destination.x > 72)
        {
            return true;
        }
        else if (destination.z > 74 || destination.z < -78)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }

    

}
