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
    //public GameObject test;

    
    private NPCPool pool;//NPC 풀
    [SerializeField] GameObject npc;// NPC 프리팹
    [SerializeField] GameObject npcGroup;//생성된 npc들을 가지고 있을 부모 오브젝트
    [SerializeField] GameObject spawnGroup;// 스폰포인트를 가지고 있을 부모 오브젝트
    [SerializeField] List<GameObject> npcSpawnList;// 부모 오브젝트로부터 스폰포인트를 저장할 변수

    //[SerializeField] float mapSizeZ1;//z축 최소값
    //[SerializeField] float mapSizeZ2;//z축 최대값
    //[SerializeField] float mapSizeX1;//x축 최소값
    //[SerializeField] float mapSizeX2;//x축 최대값

    //private List<GameObject> npcList;//생성된 NPC들 보유하는 리스트 둘중 택1?
    private List<TestingNPC> npcScriptList;//생성된 NPC들 보유하는 리스트 둘중 택1?
                                    



    //public GameObject temp;

    //public Transform[] forTestSpawnPoint;
    private void Awake()
    {
        ISingleton<NPCManager>.TrySetInstance(this);

        pool = new NPCPool();//풀 생성
        pool.SetPrefab(npc);
    }

    void Start()
    {
        //Debug.Log(temp.size.x+","+ temp.size.y+","+ temp.size.z);

        
        npcScriptList = new List<TestingNPC>();
        npcSpawnList = new List<GameObject>();
        //npcDestinations = new List<Vector3>();
        SetSpawnPoint();//초기 스폰 메커니즘 -> 나중에 완성도를 끌어올릴때 다른 로직을 사용해 보자 -> 단점으로는 밀도가 높아져 빈 공간이 생길 수 밖에 없음
        
        
        
        //InitialSetBySpawnPoint();// 스폰포인트용 초기 세팅
        
        //InitialForDebug();// 디버그용 하나 생성
        
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
    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
    
    public void InitialSetBySpawnPoint()//스폰포인트를 바탕으로 npc 배치 
    {
        //PhotonNetwork.InstantiateRoomObject(test.name, Vector3.zero, Quaternion.identity, 0);
        CreateAllNPC();
        //Debug.Log(npcScriptList.Count);
        if (PhotonNetwork.IsConnected == false)//포톤에 연결되어 있지 않다면
        {
            foreach (TestingNPC npc in npcScriptList)//npc들을
            {

                
                int spawnIndex = Random.Range(0, npcSpawnList.Count);

                //npc.transform.position = npcSpawnList[spawnIndex].transform.position*2;
                (npc as TestingNPC).SelfAgent.enabled = false;
                SetNPCTransform(npc.gameObject, npcSpawnList[spawnIndex].transform.position + new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)));//랜덤하게 위치 설정
                npc.gameObject.transform.Rotate(0, Random.Range(0f, 360f), 0);
                (npc as TestingNPC).SelfAgent.enabled = true;
                (npc as TestingNPC).InitialSet();
                //Quaternion ransRoation= Quaternion
                //npc 랜덤 배치시 로테이션도 회전       
                

                //npc.transform.Translate(new Vector3(npcSpawnList[spawnIndex].transform.position.x, 3.0f, npcSpawnList[spawnIndex].transform.position.z));


            }
        }
        else//포톤에 연결되어 있으면
        {
            if (PhotonNetwork.IsMasterClient != true)
            {
                foreach (TestingNPC npc in npcScriptList)
                {
                    (npc as TestingNPC).SelfAgent.enabled = false;
                }
                    //Debug.Log("notmaster");
                    //CreateAllNPC();
                    //return;
            }
            else
            {
                
                Debug.Log(npcScriptList.Count);
                foreach (TestingNPC npc in npcScriptList)//npc들을
                {
                    //Debug.Log("1");
                    //CreateAllNPC();
                    int spawnIndex = Random.Range(0, npcSpawnList.Count);

                    //npc.transform.position = npcSpawnList[spawnIndex].transform.position*2;
                    (npc as TestingNPC).SelfAgent.enabled = false;
                    int tempID = npc.photonView.ViewID;
                    Vector3 temp = new Vector3();
                    temp = npcSpawnList[spawnIndex].transform.position +new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
                    photonView.RPC("SetNPCTransformByID", Photon.Pun.RpcTarget.All, tempID, temp);
                    //photonView.RPC("AddNPCListByPhotonID", Photon.Pun.RpcTarget.All, tempID);
                    //SetNPCTransform(npc.gameObject, npcSpawnList[spawnIndex].transform.position + new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)));//랜덤하게 위치 설정
                    npc.gameObject.transform.Rotate(0, Random.Range(0f, 360f), 0);                                                                                            
                    (npc as TestingNPC).SelfAgent.enabled = true;
                    (npc as TestingNPC).InitialSet();
                    //npc.transform.Translate(new Vector3(npcSpawnList[spawnIndex].transform.position.x, 3.0f, npcSpawnList[spawnIndex].transform.position.z));


                }
            }
        }

        PhotonNetwork.SendAllOutgoingCommands();
    }

    //public void InitialForDebug()
    //{
    //    SpawNPC();
    //    SetNPCTransform(temp, new Vector3(0, 1.5f, 0));
    //    //SetNPCTransform(npc.gameObject, npcSpawnList[Random.Range(0, npcSpawnList.Count)].transform.position);
    //    //SetNPCTransform(npc.gameObject, new Vector3(0, 1.5f, 0));
    //    
    //}


    [PunRPC]// RPC로 NPC를 못 쏴줘서 못하고 바꿨었음...
    public void SetNPCTransform(GameObject npc, Vector3 position)//y 좌표 1.09// 작동은 하는데 원하는 위치까지 이동하지 않고 중간에 멈추는 현상 발생 설마 하는데 Agent 때문인가? 
    {
        //Vector3 temp = new Vector3(position.x, 1.5f, position.z);
        npc.transform.position= new Vector3(position.x, 3.0f, position.z);
    }
    [PunRPC]
    public void SetNPCTransformByID(int viewID,Vector3 position)
    {
        PhotonView.Find(viewID).transform.position = position;
    }
    [PunRPC]
    public void AddNPCListByPhotonID(int viewID)
    {
        var tempNPC=PhotonView.Find(viewID);
        Debug.Log(npcGroup.transform);
        tempNPC.gameObject.transform.SetParent(npcGroup.transform);
        npcScriptList.Add(tempNPC.GetComponent<TestingNPC>());

    }
    //public void SpawNPC()
    //{
    //   temp= pool.NPCS.Get();
    //}
    public static Vector3 ReturnRandomDestination()
    {
        Vector3 destination;
        destination = new Vector3(Random.Range(-74,72),1.5f, Random.Range(-78, 74));//현재 맵의 크기를 측정해서 대입시킴 static 함수로 선언했기 때문에 이렇게 대입함 필요하다면 npcmanager를 static class로 선언하던가 싱글톤 패턴으로 구현하면 될듯
        //Debug.Log(mapSi);
        return destination;
    }
    
    public void CreateAllNPC()//npc를 초기 숫자만큼 생성
    {
        for(int i=0; i<pool.InitialNPCNum; i++)
        {
            //npcList.Add(pool.NPCS.Get());
            if (PhotonNetwork.IsConnected)
            {
                



                if (PhotonNetwork.IsMasterClient == true)
                {
                    
                    
                    var tempNPC = PhotonNetwork.InstantiateRoomObject(npc.name, Vector3.zero, Quaternion.identity, 0);
                    tempNPC.transform.SetParent(npcGroup.transform);
                    int tempID=tempNPC.GetComponent<PhotonView>().ViewID;
                    //Debug.Log(tempID);
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
        if(PhotonNetwork.IsConnected)
        {
            if(stream.IsWriting)
            {

            }
            else
            {

            }
        }
    }

    

}
