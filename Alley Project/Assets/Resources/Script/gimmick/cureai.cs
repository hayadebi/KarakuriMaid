using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cureai : MonoBehaviour
{
    public ColEvent cureCol;
    public ColEvent stopCol;
    public float speed = 7;
    GameObject p;
    Vector3 target;
    Rigidbody rb;
    float looptime = 2.1f;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GManager.instance.over == false && GManager.instance.walktrg == true)
        {
                    Run();
        }
        else if (GManager.instance.over == true || GManager.instance.walktrg == false)
        {
            Vector3 no;
            no.x = 0;
            no.y = 0;
            no.z = 0;
            rb.velocity = no;
        }
    }

    void Run()
    {
        target = this.transform.forward * speed;
        if (stopCol.ColTrigger == false)
        {
            rb.velocity = target;
        }
        else if (stopCol.ColTrigger == true)
        {
            Vector3 no;
            no.x = 0;
            no.y = 0;
            no.z = 0;
            rb.velocity = no;
        }
        if (cureCol.ColTrigger == true && GManager.instance.Pstatus.health < GManager.instance.Pstatus.maxHP)
        {
            looptime += Time.deltaTime;
            if (looptime > 2f)
            {
                looptime = 0;
                GManager.instance.Pstatus.health += 1;
            }
        }
    }
}
