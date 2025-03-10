using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NPCManager : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPool<NPC> npcs;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public static Vector3 ReturnRandomDestination()
    {
        Vector3 destination;
        destination = new Vector3(Random.Range(1,100),0, Random.Range(1, 100));
        return destination;
    }
}
