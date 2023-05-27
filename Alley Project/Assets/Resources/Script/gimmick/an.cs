using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class an : MonoBehaviour
{
    public ColEvent stopCol;
    public int maxrandom = 4;
    public int minrandom = 1;
    bool resettrg = false;
    public float[] time;
    public int ontrg = 0;
    public Transform[] Bpos;
    public int eventnumber = 0;
    public AudioClip[] Ase;
    public Vector3[] target;
    public Renderer ren;
    enemyS objE;
    public GameObject[] Bullet;
    public bool[] trg;
    int oldevent = 0;
    Rigidbody rb;
    GameObject p;
    int wariaihp = 40;
    bool stoptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
        eventnumber = Random.Range(minrandom, maxrandom);
        oldevent = eventnumber;
        if(GManager.instance.mode == 0)
        {
            wariaihp = 26;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = 53;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(trg[0] == false && GManager.instance.bossbattletrg == 1)
        {
            time[0] += Time.deltaTime;
            if(time[0] > time[1])
            {
                trg[0] = true;
                time[0] = 0;
            }
        }
        else if (trg[0] == true)
        {
            if(wariaihp > objE.Estatus.health && trg[1] == false && trg[2] == false)
            {
                eventnumber = -1;
                trg[1] = true;
                trg[2] = true;
                objE.Eanim.SetInteger("Anumber", 0);
                if (stoptrg == false)
                {
                    stoptrg = true;
                    Vector3 no;
                    no.x = 0;
                    no.y = 0;
                    no.z = 0;
                    rb.velocity = no;
                }
                objE.Estatus.defence += 1;
                GManager.instance.setrg = 15;
                Instantiate(Bullet[2], Bullet[3].transform.position, Bullet[3].transform.rotation,Bullet[3].transform);
                maxrandom = 5;
                minrandom = 2;
                resettrg = true;
            }
            if (resettrg == true)
            {
                resettrg = false;
                ontrg = 0;
                trg[1] = false;
                objE.audioS.Stop();
                eventnumber = Random.Range(minrandom, maxrandom);
                for (int i = oldevent;i == eventnumber;)
                {
                    eventnumber = Random.Range(minrandom, maxrandom);
                }
                oldevent = eventnumber;

            }
            if (GManager.instance.over == false && GManager.instance.walktrg == true && objE.deathtrg == false && trg[1] == false)
            {
                if (time[4] > 0)
                {
                    time[4] -= Time.deltaTime;
                    if (time[4] < 0 || time[4] == 0)
                    {
                        time[4] = 0;
                        Eventreset();
                    }
                }
                if (GManager.instance.bossbattletrg == 1)
                {
                    if (p != null && eventnumber != -1)
                    {
                        Invoke("Event" + eventnumber, 0f);
                    }
                }
            }
            else if (GManager.instance.over == true || GManager.instance.walktrg == false)
            {
                
            }
        }
    }

    void Event1()
    {
        if (ontrg == 0)
        {
            objE.Eanim.SetInteger("Anumber",1);
            if (stopCol.ColTrigger == false)
            {
                if(stoptrg != false)
                {
                    stoptrg = false;
                }
                target[0] = this.transform.forward * objE.Estatus.speed;
                rb.velocity = target[0];
            }
            else if (stopCol.ColTrigger == true)
            {
                ontrg = 1;
                if (stoptrg == false)
                {
                    stoptrg = true;
                    Vector3 no;
                    no.x = 0;
                    no.y = 0;
                    no.z = 0;
                    rb.velocity = no;
                }
                objE.Eanim.SetInteger("Anumber", 2);
                objE.audioS.PlayOneShot(Ase[0]);
            }
        }
        else if (ontrg == 1)
        {
            time[2] += Time.deltaTime;
            if(time[2] > 1.15f)
            {
                time[2] = 0;
                ontrg = 0;
            }
        }
        time[3] += Time.deltaTime;
        if(time[3] > 13)
        {
            time[3] = 0;
            ontrg = 2;
            objE.Eanim.SetInteger("Anumber", 0);
            time[4] = 3f;
        }
    }
    void Event2()
    {
        if(ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            Invoke("Ev2_1", 0.3f);
        }
        else if(ontrg == 2)
        {
            rb.velocity = target[1] * (objE.Estatus.speed * 2);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[2]);
            target[1] = p.transform.position - this.transform.position;
            target[1].Normalize();
            Invoke("Ev2_2", 1.15f);
        }
    }
    void Ev2_2()
    {
        objE.audioS.PlayOneShot(Ase[3]);
        Instantiate(Bullet[0], Bpos[0].transform.position, Bullet[0].transform.rotation);
        ontrg = 3;
        Invoke("Ev2_3", 0.3f);
    }
    void Ev2_3()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 4f;
    }
    void Event3()
    {
        if(ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 4);
            Invoke("Ev3_1", 1.3f);
        }
    }
    void Ev3_1()
    {
        objE.audioS.PlayOneShot(Ase[4]);
        Instantiate(GManager.instance.shoteffect, Bpos[1].position, GManager.instance.shoteffect.transform.rotation);
        
        for (int i = 0; i < 5;)
        {
            Vector3 vec = p.transform.position - Bpos[1].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, -60 + ((30 * 1) * i), 0) * vec;
            vec *= 12;
            var t = Instantiate(Bullet[1], Bpos[1].position, Bpos[1].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            i++;
        }
        Invoke("Ev3_2", 2f);
    }
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 2.3f;
    }

    void Event4()
    {
        if(ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            Instantiate(Bullet[4], Bpos[1].transform.position, Bpos[1].transform.rotation, Bpos[1].transform);
            time[4] = 15.3f;
        }
    }
    void Eventreset()
    {
        if (trg[1] == false)
        {
            GameObject[] ats = GameObject.FindGameObjectsWithTag("bossA");
            if (ats.Length != 0)
            {
                foreach (GameObject at in ats)
                {
                    Destroy(at.gameObject);
                }
            }
            objE.Eanim.SetInteger("Anumber", 0);
            resettrg = true;
        }
    }
}
