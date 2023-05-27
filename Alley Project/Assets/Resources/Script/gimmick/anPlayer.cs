using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anPlayer : MonoBehaviour
{
    public objAngle oa;
    public bool saytimestop = true;
    public ColEvent stopCol;
    public GameObject boss = null;
    GameObject P = null;
    Vector3 target;
    Rigidbody rb;
    public AudioClip shotse;
    public AudioSource audioS;
    public Animator Eanim;
    bool attacktrg = false;
    float looptime = 1f;
    public GameObject[] Bullet;
    public GameObject[] Bpos;
    bool stoptrg = false;
    int ontrg = 0;
    // Start is called before the first frame update
    void Start()
    {
       
        P = GameObject.Find("Player");
        if (oa.targetobj[0] != boss)
        {
            oa.targetobj[0] = boss;
        }
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (saytimestop == false)
        {
            if (GManager.instance.over == false && GManager.instance.walktrg == true && GManager.instance.bossbattletrg == 1)
            {
                Run();
            }
            else if (GManager.instance.over == true || GManager.instance.walktrg == false)
            {
                if (stoptrg == false)
                {
                    stoptrg = true;
                    rb.velocity = Vector3.zero;
                    if (Eanim.GetInteger("Anumber") == 1 || Eanim.GetInteger("Anumber") == 2 || Eanim.GetInteger("Anumber") == 4)
                    {
                        Eanim.SetInteger("Anumber", 0);
                    }
                }
            }
        }
    }

    void Run()
    {
        target = this.transform.forward * 24;
        if (stopCol.ColTrigger == false && attacktrg == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = target;
                if(oa.targetobj[0] != P)
                {
                    oa.targetobj[0] = P;
                }
                if (Eanim.GetInteger("Anumber") == 0)
                {
                    Eanim.SetInteger("Anumber", 1);
                }
                if (stoptrg != false)
                {
                    stoptrg = false;
                }
            }
        }
        //else if (stopCol.ColTrigger == true || attacktrg == true)
        //{
        //    if (objE.Eanim.GetInteger("Anumber") == 1)
        //    {
        //        objE.Eanim.SetInteger("Anumber", 0);
        //    }
        //    if (stoptrg == false)
        //    {
        //        stoptrg = true;
        //        rb.velocity = Vector3.zero;
        //    }
        //}
        if (stopCol.ColTrigger == true && attacktrg == false)
        {
            looptime += Time.deltaTime;
            if (looptime > 1f)
            {
                looptime = 0;
                attacktrg = true;
                if (oa.targetobj[0] != boss)
                {
                    oa.targetobj[0] = boss;
                }
                //攻撃
                rb.velocity = Vector3.zero;
                int randomnumber = Random.Range(1, 5);
                if(randomnumber < 4)
                {
                    Shot();
                }
                else if (randomnumber == 4)
                {
                    Wave();
                }
            }
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            if (attacktrg == false)
            {
                looptime += Time.deltaTime;
                if (looptime > 2f)
                {
                    looptime = 0;
                    attacktrg = true;
                    if (oa.targetobj[0] != boss)
                    {
                        oa.targetobj[0] = boss;
                    }
                    //攻撃
                    rb.velocity = Vector3.zero;
                    int randomnumber = Random.Range(1, 5);
                    if (randomnumber < 4)
                    {
                        Shot();
                    }
                    else if (randomnumber == 4)
                    {
                        Wave();
                    }
                }
            }
        }
    }
    void attackEnd()
    {
        if (attacktrg == true)
        {
            attacktrg = false;
        }
        ontrg = 0;
        Eanim.SetInteger("Anumber", 0);
    }

    void Shot()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            Eanim.SetInteger("Anumber", 4);
            Invoke("Ev3_1", 1.3f);
        }
    }
    void Ev3_1()
    {
        if (ontrg == 1 && P != null && boss != null)
        {
            audioS.PlayOneShot(shotse);
            Instantiate(GManager.instance.shoteffect, Bpos[0].transform.position, GManager.instance.shoteffect.transform.rotation);

            for (int i = 0; i < 5;)
            {
                var newbosspos = boss.transform.position;
                newbosspos.y += 0.3f;
                Vector3 vec = newbosspos - Bpos[0].transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -60 + ((30 * 1) * i), 0) * vec;
                vec *= 40;
                var t = Instantiate(Bullet[0], Bpos[0].transform.position, Bpos[0].transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev3_2", 2f);
        }
    }
    void Ev3_2()
    {
        Eanim.SetInteger("Anumber", 0);
        Invoke("attackEnd", 1);
    }
    void Wave()
    {
        if (ontrg == 0 && P != null && boss != null)
        {
            ontrg = 1;
            Eanim.SetInteger("Anumber", 2);
            Invoke("Ev4_1", 0.2f);
        }
    }
    void Ev4_1()
    {
        if (ontrg == 1 && P != null)
        {
            ontrg = 2;
            audioS.PlayOneShot(shotse);
                var pp = boss.transform.position;
                pp.y = Bpos[1].transform.position.y + 0.2f;
                Vector3 vec = pp - Bpos[1].transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= 40;
                var t = Instantiate(Bullet[1], Bpos[1].transform.position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            Invoke("Ev4_2", 0.3f);
        }
    }
    void Ev4_2()
    {
        if (ontrg == 2 && P != null)
        {
            ontrg = 3;
            for (int i = 0; i < 3;)
            {
                var pp = boss.transform.position;
                pp.y = Bpos[1].transform.position.y + 0.2f;
                Vector3 vec = pp - Bpos[1].transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -30 + ((30 * 1) * i), 0) * vec;
                vec *= 30;
                var t = Instantiate(Bullet[2], Bpos[1].transform.position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev4_3", 0.3f);
        }
    }

    void Ev4_3()
    {
        Eanim.SetInteger("Anumber", 0);
        Invoke("attackEnd", 1);
    }
}
