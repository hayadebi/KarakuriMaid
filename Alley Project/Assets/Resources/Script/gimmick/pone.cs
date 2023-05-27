using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pone : MonoBehaviour
{
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent shotCol;
    public ColEvent stopCol;
    enemyS objE;
    GameObject p;
    Vector3 target;
    Rigidbody rb;
    public AudioClip shotse;
    bool attacktrg = false;
    bool stoptrg = false;
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
                    Vector3 no;
                    no.x = 0;
                    no.y = 0;
                    no.z = 0;
                    rb.velocity = no;
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
            if (stopCol.ColTrigger == false && attacktrg == false)
            {
            rb.velocity = target;
            if (objE.Eanim.GetInteger("Anumber") == 0)
            {
                objE.Eanim.SetInteger("Anumber", 1);
            }
            if (stoptrg != false)
            {
                stoptrg = false;
            }
            }
            else if (stopCol.ColTrigger == true || attacktrg == true)
            {
            if (objE.Eanim.GetInteger("Anumber") == 1)
            {
                objE.Eanim.SetInteger("Anumber", 0);
            }
            if (stoptrg == false)
            {
                stoptrg = true;
                Vector3 no;
                no.x = 0;
                no.y = 0;
                no.z = 0;
                rb.velocity = no;
            }
            }
            if (shotCol.ColTrigger == true && attacktrg == false)
            {
                attacktrg = true;
                objE.audioS.PlayOneShot(shotse);
                //攻撃
                objE.Eanim.SetInteger("Anumber", 2);
                Invoke("attackEnd", 1.3f);
            }
    }
    void attackEnd()
    {
        if(attacktrg == true)
        {
            attacktrg = false;
        }
        objE.Eanim.SetInteger("Anumber", 0);
    }
}
