using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        //if (col.tag != "Player" && col.gameObject.layer != 4 && col.gameObject.layer != 14 && col.gameObject.layer != 15 && col.GetComponent<MeshRenderer>() && !col.GetComponent <enemyTrigger>())
        //{
        //    col.gameObject.layer = 13;
        //}
    }
    private void OnTriggerStay(Collider col)
    {
        //if (col.tag != "Player" && col.gameObject.layer != 4 && col.gameObject.layer != 13 && col.gameObject.layer != 14 && col.gameObject.layer != 15 && col.GetComponent<MeshRenderer>() && !col.GetComponent<enemyTrigger>())
        //{
        //   col.gameObject.layer = 13;
        //}
    }
    private void OnTriggerExit(Collider col)
    {
        //if (col.tag != "Player" && col.gameObject.layer != 4 && col.gameObject.layer != 14 && col.gameObject.layer != 15 && col.GetComponent<MeshRenderer>() && !col.GetComponent<enemyTrigger>())
        //{
        //    col.gameObject.layer = 9;
        //}
    }
}