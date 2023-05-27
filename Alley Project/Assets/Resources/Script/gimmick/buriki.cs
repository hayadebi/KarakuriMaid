using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buriki : MonoBehaviour
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
    float looptime = 2.4f;
    public GameObject shotpos;
    public GameObject Bullet;
    bool stoptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        objE = this.GetComponent<enemyS>();
        p = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
        objE.audioS.volume = 0.08f;
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
            looptime += Time.deltaTime;
            if (looptime > 1f)
            {
                looptime = 0;
                attacktrg = true;
                //攻撃
                objE.Eanim.SetInteger("Anumber", 2);
                Invoke("Shot", 0.24f);
            }
        }
    }
    void attackEnd()
    {
        if (attacktrg == true)
        {
            attacktrg = false;
        }
        objE.Eanim.SetInteger("Anumber", 0);
    }

    void Shot()
    {
            objE.audioS.PlayOneShot(shotse);
            if (p != null)
            {
                Instantiate(GManager.instance.shoteffect, shotpos.transform.position, shotpos.transform.rotation);
                Vector3 vec = p.transform.position - shotpos.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= 16;
                GameObject t = Instantiate(Bullet, shotpos.transform.position, Bullet.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            }
        Invoke("attackEnd", 1);
    }
}
