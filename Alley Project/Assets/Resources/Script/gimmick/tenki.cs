using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenki : MonoBehaviour
{
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent shotCol;
    public ColEvent stopCol;
    enemyS objE;
    GameObject p;
    Vector3 target;
    Rigidbody rb;
    bool attacktrg = false;
    float looptime = 2f;
    public AudioClip effectse;
    public AudioClip shotse;
    public GameObject effectobj;
    public GameObject attackobj;
    public Vector3 oldtarget;
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
                    rb.velocity = Vector3.zero;
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
            if (stoptrg != false)
            {
                stoptrg = false;
            }
        }
        else if (stopCol.ColTrigger == true || attacktrg == true)
        {
            if (stoptrg == false)
            {
                stoptrg = true;
                rb.velocity = Vector3.zero;
            }
        }
        if (shotCol.ColTrigger == true && attacktrg == false)
        {
            looptime += Time.deltaTime;
            if (looptime > 2f)
            {
                looptime = 0;
                attacktrg = true;
                //攻撃
                Shot();
            }
        }
    }
    void attackEnd()
    {
        if (attacktrg == true)
        {
            attacktrg = false;
        }
    }

    void Shot()
    {
        if (p != null)
        {
            objE.audioS.PlayOneShot(effectse);
            var newbosspos = this.transform.position;
            newbosspos.y += 5;
            GameObject dsobj = Instantiate(effectobj, newbosspos, this.transform.rotation);
            Destroy(dsobj.gameObject, 3f);
            Invoke("Ev6_1", 2f);
        }
    }
    void Ev6_1()
    {
        if ( p != null)
        {
            objE.audioS.PlayOneShot(shotse);
            oldtarget = p.transform.position;
            Invoke("Ev6_2", 0.4f);
        }
    }
    void Ev6_2()
    {
        if ( p != null)
        {
           Instantiate(attackobj, oldtarget, attackobj.transform.rotation);
            Invoke("Ev6_3", 1f);
        }
    }
    void Ev6_3()
    {
        attackEnd();
    }
}
