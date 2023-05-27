using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animset : MonoBehaviour
{
    Animator anim;
    public int inputanim;
    public string variname = "Anumber";
    int oldanim = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputanim != oldanim )
        {
            oldanim = inputanim;
            anim.SetInteger(variname, inputanim);
        }
    }
}
