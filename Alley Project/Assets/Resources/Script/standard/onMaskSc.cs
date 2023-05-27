using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMaskSc : MonoBehaviour
{
	private MeshRenderer mr = null;
    // Start is called before the first frame update
    void Start()
    {
		mr = this.GetComponent<MeshRenderer>();
		
		mr.enabled = false;
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "area" && mr != null)
		{
			// 表示されている場合の処理
			mr.enabled = true;
		}
	}
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "area" && mr != null)
		{
			// 表示されている場合の処理
			mr.enabled = true;
		}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "area" && mr != null )
		{
			// 表示されている場合の処理
			mr.enabled = false;
		}
	}
}
