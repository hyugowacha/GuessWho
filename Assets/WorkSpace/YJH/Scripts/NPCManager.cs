using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NPCManager : MonoBehaviour
{
    // Start is called before the first frame update
    //필드에 존재하는 엔피시들 
    //public GameObject npcPrefab;
    private NPCPool pool;//NPC 풀
    [SerializeField] GameObject npc;// NPC 프리팹
    [SerializeField] GameObject npcGroup;
    [SerializeField] GameObject spawnGroup;
    [SerializeField] List<GameObject> npcSpawnList;
    private List<GameObject> npcList;//생성된 NPC들 보유하는 리스트
    private List<NPC> npcScriptList;
    
    void Start()
    {
        pool = new NPCPool();//풀 생성
        npcList = new List<GameObject>();
        npcScriptList = new List<NPC>();
        npcSpawnList = new List<GameObject>();
        SetSpawnPoint();//초기 스폰 메커니즘 -> 나중에 완성도를 끌어올릴때 다른 로직을 사용해 보자 -> 단점으로는 밀도가 높아져 빈 공간이 생길 수 밖에 없음
        pool.SetPrefab(npc);
        //InitialSet();
        InitialSetBySpawnPoint();
        
        //pool.NPCS.Get();//NPC 생성

    }
    public void SetSpawnPoint()
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
    
    public void InitialSetBySpawnPoint()
    {
        CreateAllNPC();
        foreach (NPC npc in npcScriptList)//npc들을
        {
            SetNPCTransform(npc.gameObject, npcSpawnList[Random.Range(0, npcSpawnList.Count)].transform.position);//랜덤하게 위치 설정

        }
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
        destination = new Vector3(Random.Range(1,100),1.5f, Random.Range(1, 100));
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
