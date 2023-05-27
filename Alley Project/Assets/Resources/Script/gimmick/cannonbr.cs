using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonbr : MonoBehaviour
{
    public objAngle oa;
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent shotCol;
    public ColEvent stopCol;
    public ColEvent groundCol;
    enemyS objE;
    GameObject p;
    Rigidbody rb;
    Vector3 target;
    bool attacktrg = false;
    float looptime = 4f;
    public GameObject shotpos;
    public GameObject Bullet;
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
                    if (objE.Eanim.GetInteger("Anumber") == 1 || objE.Eanim.GetInteger("Anumber") == 2)
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
        if (stopCol.ColTrigger == false && attacktrg == false && groundCol == true)
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
                rb.velocity = Vector3.zero;
            }
        }
        if (shotCol.ColTrigger == true && attacktrg == false)
        {
            looptime += Time.deltaTime;
            if (looptime > 4f)
            {
                looptime = 0;
                attacktrg = true;
                //攻撃
                objE.Eanim.SetInteger("Anumber", 2);
                oa.enabled = false;
                Invoke("Shot", 0.3f);
            }
        }
    }
    void attackEnd()
    {
        if (attacktrg == true)
        {
            attacktrg = false;
        }
        oa.enabled = true;
        objE.Eanim.SetInteger("Anumber", 0);
    }

    void Shot()
    {
        if (p != null)
        {
           GameObject dsobj = Instantiate(Bullet, shotpos.transform.position, shotpos.transform.rotation);
            Destroy(dsobj.gameObject, 6);
        }
        Invoke("attackEnd", 6.3f);
    }
}
