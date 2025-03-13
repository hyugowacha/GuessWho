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
    void Start()
    {
        pool = new NPCPool();//풀 생성
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InitialSet()
    {
        
    }
    

    public static Vector3 ReturnRandomDestination()
    {
        Vector3 destination;
        destination = new Vector3(Random.Range(1,100),0, Random.Range(1, 100));
        return destination;
    }

    




}
