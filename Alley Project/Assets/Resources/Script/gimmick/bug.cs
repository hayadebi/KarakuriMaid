using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug : MonoBehaviour
{
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent shotCol;
    public ColEvent stopCol;
    enemyS objE;
    GameObject p;
    Vector3 target;
    Rigidbody rb;
    public GameObject Bullet;
    float looptime;
    public AudioClip shotse;
    public GameObject shotpos;
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
                }
            }
        }
    }

    void Run()
    {
        target = this.transform.forward * objE.Estatus.speed;
        if(stopCol.ColTrigger == false)
        {
            rb.velocity = target;
            if(stoptrg != false)
            {
                stoptrg = false;
            }
        }
        else if (stopCol.ColTrigger == true)
        {
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
        if (shotCol.ColTrigger == true)
        {
            Shot();
        }
    }

    void Shot()
    {
        looptime += Time.deltaTime;
        if (looptime > 2.3f)
        {
            looptime = 0;
            objE.audioS.PlayOneShot(shotse);
            if (p != null)
            {
                Instantiate(GManager.instance.shoteffect, shotpos.transform.position, shotpos.transform.rotation);
                Vector3 vec = p.transform.position - shotpos.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= 8;
                GameObject t = Instantiate(Bullet, shotpos.transform.position, Bullet.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            }
        }
    }

}
