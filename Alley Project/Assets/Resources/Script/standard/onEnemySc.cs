using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onEnemySc : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		GameObject[] stopobjs = GameObject.FindGameObjectsWithTag("enemy");
		foreach (GameObject sto in stopobjs )
		{
			if(sto.GetComponent <enemyS >() && sto.GetComponent <Collider >())
			{
				sto.GetComponent<enemyS>().absoluteStop = true;
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "enemy" && col.GetComponent<enemyS>() && col.GetComponent<Collider>() && col.GetComponent<enemyS>().absoluteStop == true)
		{
			col.GetComponent<enemyS>().absoluteStop = false;
		}
	}
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "enemy" && col.GetComponent<enemyS>() && col.GetComponent<Collider>() && col.GetComponent<enemyS>().absoluteStop == true)
		{
			col.GetComponent<enemyS>().absoluteStop = false;
		}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "enemy" && col.GetComponent<enemyS>() && col.GetComponent<Collider>() && col.GetComponent<enemyS>().absoluteStop == false)
		{
			col.GetComponent<enemyS>().absoluteStop = true;
		}
	}
}
