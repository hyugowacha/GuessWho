using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NPCManager : MonoBehaviour
{
    // Start is called before the first frame update
    //필드에 존재하는 엔피시들 
    //public GameObject npcPrefab;
    private NPCPool pool;
    [SerializeField] GameObject npc;
    void Start()
    {
        pool = new NPCPool();//풀 생성
        pool.SetPrefab(npc);
        //pool.NPCS.Get();//NPC 생성

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InitialSet()//맵 리셋시 NPC 뿌리기 
    {
        
    }
    
    public void SetNPCTransform()
    {

    }
    public void SpawNPC()
    {
        pool.NPCS.Get();
    }
    public static Vector3 ReturnRandomDestination()
    {
        Vector3 destination;
        destination = new Vector3(Random.Range(1,100),0, Random.Range(1, 100));
        return destination;
    }

    




}
