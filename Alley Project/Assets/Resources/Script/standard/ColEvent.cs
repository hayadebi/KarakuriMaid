using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColEvent : MonoBehaviour
{
    public bool ColTrigger = false;
    public bool onAction = true;
    public string tagName = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && onAction == true)
        {
            ColTrigger = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" && onAction == true)
        {
            ColTrigger = false;
        }
    }
}
