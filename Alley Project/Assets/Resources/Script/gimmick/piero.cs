using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piero : MonoBehaviour
{
    public int maxrandom = 4;
    public int minrandom = 1;
    bool resettrg = false;
    [Header("1はスタートタイム、4はイベントリセット")]public float[] time;
    public int ontrg = 0;
    [Header("0はボスの位置")]public Transform[] Bpos;
    public int eventnumber = 0;
    public AudioClip[] Ase;
    public Vector3[] target;
    public Renderer ren;
    enemyS objE;
    [Header ("2は覚醒時のエフェクト")]public GameObject[] Bullet;
    [Header("2までデフォルトで使ってる")]public bool[] trg;
    int oldevent = 0;
    Rigidbody rb;
    GameObject p;
    [Header("触らなくていい")]public int wariaihp = 360;
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
            wariaihp = objE.Estatus.health / 3 * 2/2;
        }
        else if (GManager.instance.mode == 1)
        {
            wariaihp = objE.Estatus.health / 3 * 3 / 2;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = objE.Estatus.health / 3 * 4/2;
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
                //後でSE変更
                GManager.instance.setrg = 15;
                Instantiate(Bullet[2], Bpos[0].transform.position, Bpos[0].transform.rotation, Bpos[0].transform);
                maxrandom = 4;
                minrandom = 1;
                resettrg = true;
            }
            if (resettrg == true)
            {
                resettrg = false;
                ontrg = 0;
                trg[1] = false;
                objE.audioS.Stop();
                if(eventnumber == 2 && wariaihp > objE.Estatus.health)
                {
                    eventnumber = 4;
                }
                else if (eventnumber != 2 && wariaihp > objE.Estatus.health)
                {
                    eventnumber = Random.Range(minrandom, maxrandom);
                    //同じ技を発動しないように
                    for (int i = oldevent; i == eventnumber;)
                    {
                        eventnumber = Random.Range(minrandom, maxrandom);
                    }
                }
                else
                {
                    eventnumber = Random.Range(minrandom, maxrandom);
                    //同じ技を発動しないように
                    for (int i = oldevent; i == eventnumber;)
                    {
                        eventnumber = Random.Range(minrandom, maxrandom);
                    }
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
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            Invoke("Ev1_1", 0.15f);
        }
    }
    void Ev1_1()
    {
        objE.audioS.PlayOneShot(Ase[2]);
        Instantiate(Bullet[3], Bpos[3].position, Bullet[3].transform.rotation);
        Instantiate(Bullet[3], Bpos[4].position, Bullet[3].transform.rotation);
        Invoke("Ev1_2", 2f);
    }
    void Ev1_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 2f;
    }

    void Event2()
    {
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            newpos.y = this.transform.position.y;
            time[6] = Vector3.Distance(this.transform.position, newpos);
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev2_1", 0.45f);
        }
        else if (ontrg == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 75);
        }
    }
    void Ev2_1()
    {
        ontrg = 2;
        Invoke("Ev2_2", 1.15f);
    }
    void Ev2_2()
    {
        if (ontrg == 2)
        {
            ontrg = 3;
            objE.audioS.PlayOneShot(Ase[0]);
            Instantiate(Bullet[1], Bpos[1].transform.position, Bullet[1].transform.rotation);
            Invoke("Ev2_3", 2f);
        }
    }
    void Ev2_3()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1f;
    }
    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            Invoke("Ev3_1", 0.3f);
        }
    }
    void Ev3_1()
    {
        if (p != null)
        {
            objE.audioS.PlayOneShot(Ase[1]);
            Instantiate(GManager.instance.shoteffect, Bpos[2].position, GManager.instance.shoteffect.transform.rotation);

            for (int i = 0; i < 3;)
            {
                Vector3 vec = p.transform.position - Bpos[2].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -30 + ((30 * 1) * i), 0) * vec;
                vec *= 24;
                var t = Instantiate(Bullet[0], Bpos[2].position, Bullet[0].transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
        }
        Invoke("Ev3_2", 1f);
    }
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1f;
    }

    void Event4()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 4);
            Invoke("Ev4_1", 0.3f);
        }
    }
    void Ev4_1()
    {
        objE.audioS.PlayOneShot(Ase[3]);
        Instantiate(Bullet[4], Bpos[0].position, Bullet[4].transform.rotation);
        Invoke("Ev4_2", 1.15f);
    }
    void Ev4_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1f;
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
            GameObject[] bl = GameObject.FindGameObjectsWithTag("bullet");
            if (bl.Length != 0)
            {
                foreach (GameObject at in bl)
                {
                    if (at.GetComponent <enemyTrigger>())
                    {
                        Destroy(at.gameObject);
                    }
                }
            }
            objE.Eanim.SetInteger("Anumber", 0);
            resettrg = true;
        }
    }
}
