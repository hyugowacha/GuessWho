using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTest : MonoBehaviour
{
    Collider collider;
    Ray ray;
    RaycastHit hit;
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        ray = new Ray();
        hit = new RaycastHit();

        var temp=Instantiate(test, transform);
        //transform.position = new Vector3(0, 0, 0);
        temp.transform.position = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //if (collider.bounds.Contains(transform.position))
        //{
        //
        //}
        //var temp=Physics.OverlapSphere(transform.position, 0);
        //if(temp.Length>=2)
        //{
        //    Debug.Log("in");
        //}
        //else
        //{
        //    Debug.Log("out");
        //}
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("enter");
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("stay");
    //}
}
