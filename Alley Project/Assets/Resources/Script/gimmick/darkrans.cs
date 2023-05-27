using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkrans : MonoBehaviour
{
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent shotCol;
    enemyS objE;
    GameObject p;
    public GameObject shotobj;
    public GameObject effectobj;
    public GameObject shotpos;
    Vector3 target;
    Rigidbody rb;
    public AudioClip shotse;
    public AudioClip shotse2;
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
                if (ontrg == 2)
                {
                    var newpos = p.transform.position;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time / 70);
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
        else if (objE.hitdamagetrg == true && attacktrg == false)
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

    }
    void Event2()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            Invoke("Ev2_0", 0.2f);
        }
        
    }
    void Ev2_0()
    {
        var newpos = p.transform.position;
        if (ontrg == 1)
        {
            ontrg = 2;
            time = Vector3.Distance(this.transform.position, newpos);
            Invoke("Ev2_1", 1f);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 2)
        {
            ontrg = 3;
            Instantiate(effectobj, this.transform.position, effectobj.transform.rotation);
            Invoke("Ev2_2", 1f);
        }
    }
    void Ev2_2()
    {
        objE.Eanim.SetInteger("Anumber", 3);
        Invoke("Ev2_3", 0.4f);
    }
    void Ev2_3()
    {
        objE.audioS.PlayOneShot(shotse2);
            Instantiate(GManager.instance.shoteffect, shotpos.transform.position, shotpos.transform.rotation);
        if (p != null)
        {
            var ppos = p.transform.position;
            ppos.y += 0.16f;
            Vector3 vec = ppos - shotpos.transform.position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 30;
            GameObject t = Instantiate(shotobj, shotpos.transform.position, this.transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
        }
        Invoke("attackEnd", 1.15f);
    }
    void attackEnd()
    {
        attacktrg = false;
        ontrg = 0;
        moveCol.ColTrigger = true;
    }
}
