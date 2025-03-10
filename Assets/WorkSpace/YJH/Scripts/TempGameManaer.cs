using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameManaer : MonoBehaviour
{

    private int numberOfNPC;
    private List<Transform> destinationOfNPCs;





    //임시 -> 싱글톤 사용은 나중에

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SetNpcDestination()
    {
        for(int i = 0; i < numberOfNPC; i++)
        {
            //Transform destination=new
            //destination.position= new Vector3 (Random.Range(1,100),0,Random.Range(1,100));


            //destinationOfNPCs.Add()
        }
    }
}
