using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class NPCManager : MonoBehaviour
{
    // Start is called before the first frame update
    //필드에 존재하는 엔피시들 
    //public GameObject npcPrefab;
    private NPCPool pool;//NPC 풀
    [SerializeField] GameObject npc;// NPC 프리팹
    [SerializeField] GameObject npcGroup;//생성된 npc들을 가지고 있을 부모 오브젝트
    [SerializeField] GameObject spawnGroup;// 스폰포인트를 가지고 있을 부모 오브젝트
    [SerializeField] List<GameObject> npcSpawnList;// 부모 오브젝트로부터 스폰포인트를 저장할 변수

    [SerializeField] float mapSizeZ1;//z축 최소값
    [SerializeField] float mapSizeZ2;//z축 최대값
    [SerializeField] float mapSizeX1;//x축 최소값
    [SerializeField] float mapSizeX2;//x축 최대값

    private List<GameObject> npcList;//생성된 NPC들 보유하는 리스트 둘중 택1?
    private List<NPC> npcScriptList;//생성된 NPC들 보유하는 리스트 둘중 택1?
    private List<Vector3> npcDestinations;//npc가 목적지로 정할 위치를 저장할 리스트
    public NavMeshModifierVolume temp;
    void Start()
    {
        Debug.Log(temp.size.x+","+ temp.size.y+","+ temp.size.z);


        pool = new NPCPool();//풀 생성
        npcList = new List<GameObject>();
        npcScriptList = new List<NPC>();
        npcSpawnList = new List<GameObject>();
        npcDestinations = new List<Vector3>();
        SetSpawnPoint();//초기 스폰 메커니즘 -> 나중에 완성도를 끌어올릴때 다른 로직을 사용해 보자 -> 단점으로는 밀도가 높아져 빈 공간이 생길 수 밖에 없음
        pool.SetPrefab(npc);
        //InitialSet();
        //InitialSetBySpawnPoint();
        InitialForDebug();
        //pool.NPCS.Get();//NPC 생성

    }
    public void SetSpawnPoint()//일단 스폰 포인트를 바탕으로 랜덤 배치할 예정이기 때문에 사용 X 혹시 모르니 남겨놓기 나중에 삭제
    {
        for (int i = 0; i < spawnGroup.transform.childCount; i++)//스폰 포인트 
        {
            npcSpawnList.Add(spawnGroup.transform.GetChild(i).gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InitialSetBySpawnPoint()//스폰포인트를 바탕으로 npc 배치 
    {
        CreateAllNPC();
        foreach (NPC npc in npcScriptList)//npc들을
        {
            //Debug.Log(npcSpawnList.Count);
            SetNPCTransform(npc.gameObject, npcSpawnList[Random.Range(0, npcSpawnList.Count)].transform.position);//랜덤하게 위치 설정

        }
    }

    public void InitialForDebug()
    {
        SpawNPC();
        SetNPCTransform(npc.gameObject, npcSpawnList[Random.Range(0, npcSpawnList.Count)].transform.position);
    }




    public void InitialSet()//맵 리셋시 NPC 뿌리기 
    {
        CreateAllNPC();//최초 숫자인 50개만큼 NPC 호출
        foreach(NPC npc in npcScriptList)//npc들을
        {
            SetNPCTransform(npc.gameObject, ReturnRandomDestination());//랜덤하게 위치 설정
        }


    }

    



    
    public void SetNPCTransform(GameObject npc, Vector3 position)//y 좌표 1.09
    {
        npc.transform.position = position;
    }
    public void SpawNPC()
    {
        pool.NPCS.Get();
    }
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
            npcScriptList.Add(pool.GetNPC(npcGroup).GetComponent<NPC>());
        }
    }
    




}
