using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NPCPool : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPool<GameObject> npcs;
    public ObjectPool<GameObject> NPCS { get { return npcs; } set { npcs = value; } }
        

    public GameObject npcPrefab;
    public NPCPool()
    {
        npcs = new ObjectPool<GameObject>(CreateNPC,NPCActivate,NPCDisable,NPCDestroy,true ,50,100);
    }
    public GameObject CreateNPC()
    {
        return Instantiate(npcPrefab);
    }
    public void NPCActivate(GameObject npc)
    {
        npc.SetActive(true);
    }

    public void NPCDisable(GameObject npc)
    {
        npc.SetActive(false);
    }

    public void NPCDestroy(GameObject npc)
    {
        Destroy(npc);
    }
    public GameObject GetNPC()
    {
        GameObject npc = npcs.Get();
        return npc;
    }
    public void ReturnNPC(GameObject npc)
    {
        npcs.Release(npc);
    }

    public void DestroyPool()
    {
        npcs.Clear();
    }

    void Start()
    {
        //npcs = new ObjectPool<NPC>();//오브젝트 생성함수, 오브젝트 풀에서 가져오는 함수, 오브젝트를 비활성화 하면 호출되는 함수, 오브젝트 파괴 함수,중복 반환 체크 함수(bool), 최초 생성시 오브젝트 개수, 최대 개수
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
