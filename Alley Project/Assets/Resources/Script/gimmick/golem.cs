using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem : MonoBehaviour
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
    int wariaihp = 90;
    bool stoptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
        eventnumber = Random.Range(minrandom, maxrandom);
        oldevent = eventnumber;
        if (GManager.instance.mode == 0)
        {
            wariaihp = 60;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = 120;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (trg[0] == false && GManager.instance.bossbattletrg == 1)
        {
            time[0] += Time.deltaTime;
            if (time[0] > time[1])
            {
                trg[0] = true;
                time[0] = 0;
                objE.Estatus.speed = GManager.instance.Pstatus.speed + 1f;
            }
        }
        else if (trg[0] == true)
        {
            if (wariaihp > objE.Estatus.health && trg[1] == false && trg[2] == false)
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
                GManager.instance.setrg = 21;
                Instantiate(Bullet[2], Bullet[3].transform.position, Bullet[3].transform.rotation, Bullet[3].transform);
                maxrandom = 4;
                minrandom = 1;
                resettrg = true;
            }
            if (resettrg == true)
            {
                resettrg = false;
                ontrg = 0;
                Bullet[0].SetActive(false);
                trg[1] = false;
                objE.audioS.Stop();
                eventnumber = Random.Range(minrandom, maxrandom);
                //for (int i = oldevent; i == eventnumber;)
                //{
                // eventnumber = Random.Range(minrandom, maxrandom);
                //  }
                //oldevent = eventnumber;
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
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            objE.audioS.PlayOneShot(Ase[1]);
            time[5] = 0;
        }
        if (ontrg == 1)
        {
            if (stopCol.ColTrigger == false)
            {
                if (stoptrg != false)
                {
                    stoptrg = false;
                }
                time[5] += Time.deltaTime;
                if(time[5] > 0.45f)
                {
                    time[5] = 0;
                    objE.audioS.PlayOneShot(Ase[1]);
                }
                target[0] = this.transform.forward * objE.Estatus.speed;
                rb.velocity = target[0];
            }
            else if (stopCol.ColTrigger == true )
            {
                ontrg = 2;
                Bullet[0].SetActive(true);
                Vector3 no;
                no.x = 0;
                no.y = 0;
                no.z = 0;
                rb.velocity = no;
                objE.Eanim.SetInteger("Anumber", 3);
                time[4] = 2f;
                Invoke("Punch", 0.3f);
            }
        }
    }

    void Punch()
    {
        objE.audioS.PlayOneShot(Ase[0]);
    }
    void Event2()
    {
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            newpos.y = 60.13f;
            time[6] = Vector3.Distance(this.transform.position, newpos);
            objE.Eanim.SetInteger("Anumber", 2);
            Invoke("Ev2_1", 1.3f);
        }
        else if (ontrg == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 90);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[2]);
            Instantiate(Bullet[1], Bpos[0].transform.position, Bullet[1].transform.rotation);
            ontrg = 3;
            Invoke("Ev2_2", 1f);
        }
    }
    void Ev2_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 0.3f;
    }
    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev3_1", 2f);
        }
    }
    void Ev3_1()
    {
        objE.audioS.PlayOneShot(Ase[3]);
        Instantiate(GManager.instance.shoteffect, Bpos[1].position, GManager.instance.shoteffect.transform.rotation);

        for (int i = 0; i < 5;)
        {
            Vector3 vec = (p.transform.position - Bpos[1].position).normalized;
            vec.y += 0.016f;
            vec = Quaternion.Euler(0, -30 + ((15 * 1) * i), 0) * vec;
            vec *= 24;
            var t = Instantiate(Bullet[4], Bpos[1].position, Bpos[1].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            i++;
        }
        Invoke("Ev3_2", 1.3f);
    }
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 0.3f;
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
