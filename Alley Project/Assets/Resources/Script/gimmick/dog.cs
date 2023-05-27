using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : MonoBehaviour
{
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent shotCol;
    enemyS objE;
    GameObject p;
    Vector3 target;
    Rigidbody rb;
    public AudioClip shotse;
    bool attacktrg = false;
    bool stoptrg = false;
    int ontrg = 0;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        objE = this.GetComponent<enemyS>();
        p = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objE.absoluteStop == false)
        {
            if (GManager.instance.over == false && GManager.instance.walktrg == true)
            {
                if (ontrg == 1)
                {
                    var newpos = p.transform.position;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time / 120);
                }
                if (objE.damagetrg == false && objE.deathtrg == false)
                {
                    if (moveCol.ColTrigger == true && !objE.stoptrg)
                    {
                        if (bossmove == false && GManager.instance.bossbattletrg == 0)
                        {
                            Run();
                        }
                        else if (bossmove == true)
                        {
                            Run();
                        }
                    }
                    else if (moveCol.ColTrigger == false)
                    {

                    }
                }
            }
            else if (GManager.instance.over == true || GManager.instance.walktrg == false)
            {
                if (stoptrg == false)
                {
                    stoptrg = true;
                    rb.velocity = Vector3.zero;
                    if (objE.Eanim.GetInteger("Anumber") == 1)
                    {
                        objE.Eanim.SetInteger("Anumber", 0);
                    }
                }
            }
        }
    }

    void Run()
    {
        target = this.transform.forward * objE.Estatus.speed;
        if (shotCol.ColTrigger == false && attacktrg == false)
        {
            rb.velocity = target;
            if (objE.Eanim.GetInteger("Anumber") != 1)
            {
                objE.Eanim.SetInteger("Anumber", 1);
            }
            if (stoptrg != false)
            {
                stoptrg = false;
            }
        }
        else if (shotCol.ColTrigger == true && attacktrg == false)
        {
            attacktrg = true;
            if (objE.Eanim.GetInteger("Anumber") != 2)
            {
                objE.Eanim.SetInteger("Anumber", 2);
            }
            if (stoptrg == false)
            {
                stoptrg = true;
                rb.velocity = Vector3.zero;
            }
            objE.audioS.PlayOneShot(shotse);
            Event2();
        }
           
    }

    void Event2()
    {
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            time = Vector3.Distance(this.transform.position, newpos);
            Invoke("Ev2_1", 2f);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            ontrg = 3;
            Invoke("Ev2_2", 1f);
        }
    }
    void Ev2_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        Invoke("attackEnd",0.3f);
    }
    void attackEnd()
    {
        attacktrg = false;
        ontrg = 0;
        moveCol.ColTrigger = true;
    }
}
