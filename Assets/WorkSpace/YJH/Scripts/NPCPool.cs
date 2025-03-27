using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Photon.Pun;

public class NPCPool// : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPool<GameObject> npcs;
    public ObjectPool<GameObject> NPCS { get { return npcs; } set { npcs = value; } }
    private int maxNPCNum = 50;
    private int initialNPCNum = 50;
    public int InitialNPCNum
    {
        get { return initialNPCNum; }
        set { initialNPCNum = value; }
    }

    private GameObject npcPrefab;
    public NPCPool()
    {
        npcs = new ObjectPool<GameObject>(CreateNPC,NPCActivate,NPCDisable,NPCDestroy,true , initialNPCNum, maxNPCNum);
    }
    public void SetPrefab(GameObject prefab)
    {
        npcPrefab= prefab;
    }
    public GameObject CreateNPC()
    {
        if (npcs == null)
        {
            Debug.Log("no prefab");
            return null;
        }
        if (PhotonNetwork.IsConnected)
        {
            return PhotonNetwork.InstantiateRoomObject(npcPrefab.name, Vector3.zero, Quaternion.identity,0);

            //return PhotonNetwork.InstantiateSceneObject(npcPrefab.name, Vector3.zero, Quaternion.identity,0);
        }
        else
        {
            return MonoBehaviour.Instantiate(npcPrefab);
        }
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
       MonoBehaviour.Destroy(npc);
    }
    public GameObject GetNPC(GameObject group)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            GameObject npc = npcs.Get();
            npc.transform.parent = group.transform;
            return npc;
        }
        else
        {
            
            GameObject npc = npcs.Get();
            npc.transform.parent = group.transform;
            return npc;
        }
        //return null;
    }
    public void ReturnNPC(GameObject npc)
    {
        npcs.Release(npc);
    }

    public void DestroyPool()
    {
        npcs.Clear();
    }
    //public void ActivateAllNPCs()
    //{
    //    for (int i = 0; i < InitialNPCNum; i++)
    //    {
    //        
    //    }
    //}

    
   
}
