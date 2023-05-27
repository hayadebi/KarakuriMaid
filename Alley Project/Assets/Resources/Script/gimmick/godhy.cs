using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class godhy : MonoBehaviour
{
    public objAngle ang;
    public int maxrandom = 5;
    public int minrandom = 1;
    bool resettrg = false;
    [Header("1はスタートタイム、4はイベントリセット")] public float[] time;
    public int returnNumber = 0;
    public int ontrg = 0;
    [Header("0はボスの位置")] public Transform[] Bpos;
    public int eventnumber = 0;
    public AudioClip[] Ase;
    public Vector3[] target;
    public Renderer ren;
    enemyS objE;
    public GameObject[] Bullet;
    [Header("2までデフォルトで使ってる")] public bool[] trg;
    int oldevent = 0;
    Rigidbody rb;
    GameObject p;
    [Header("触らなくていい")] public int wariaihp = 420;
    bool stoptrg = false;
    //Fungus
    private bool isTalking = false;
    private Flowchart flowChart;
    public string message = "second";
    // Start is called before the first frame update
    void Start()
    {
        flowChart = this.GetComponent<Flowchart>();
        rb = this.GetComponent<Rigidbody>();
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
        eventnumber = Random.Range(minrandom, maxrandom);
        oldevent = eventnumber;
        if (GManager.instance.mode == 0)
        {
            wariaihp = objE.Estatus.health / 3 * 2 / 2;
        }
        else if (GManager.instance.mode == 1)
        {
            wariaihp = objE.Estatus.health / 3 * 3 / 2;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = objE.Estatus.health / 3 * 4 / 2;
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
                ontrg = 999;
                trg[1] = true;
                trg[2] = true;
                objE.Eanim.SetInteger("Anumber", 1);
                if (stoptrg == false)
                {
                    rb.velocity = Vector3.zero;
                }
                objE.Estatus.defence += 3;
                minrandom = 1;
                maxrandom = 8;
                //会話イベントからの第二形態
                StartCoroutine(Talk());
            }
            if (resettrg == true && minrandom != -1)
            {
                resettrg = false;
                ontrg = 0;
                trg[1] = false;
                objE.audioS.Stop();
                eventnumber = Random.Range(minrandom, maxrandom);
                //同じ技を発動しないように
                for (int i = oldevent; i == eventnumber;)
                {
                    eventnumber = Random.Range(minrandom, maxrandom);
                }
                if (maxrandom > 7 && eventnumber == 7)
                {
                    eventnumber = 3;
                }
                oldevent = eventnumber;
            }
            if (GManager.instance.over == false && GManager.instance.walktrg == true && objE.deathtrg == false && trg[1] == false && minrandom != -1)
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
                if (GManager.instance.bossbattletrg == 1 && isTalking == false)
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
        if (p != null)
        {
            var newpos = p.transform.position;
            if (ontrg == 0)
            {
                ontrg = 1;

                newpos.y += 0.16f;
                time[6] = Vector3.Distance(this.transform.position, newpos);
                objE.Eanim.SetInteger("Anumber", 3);
                Invoke("Ev1_1", 1f);
            }
            else if (ontrg == 1)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 50f);
            }
        }
    }
    void Ev1_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;

            objE.audioS.PlayOneShot(Ase[1]);
            GManager.instance.setrg = 30;
            Instantiate(Bullet[1], Bpos[0].transform.position, Bullet[1].transform.rotation);
            for (int i = 0; i < 12;)
            {
                var pp = p.transform.position;
                pp.y += 0.3f;
                Vector3 vec = pp - Bpos[1].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0 + ((30 * 1) * i), 0) * vec;
                vec *= 30;
                var t = Instantiate(Bullet[2], Bpos[1].position, Bullet[2].transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev1_2", 1f);
        }
    }
    void Ev1_2()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;

            objE.audioS.PlayOneShot(Ase[2]);
            for (int i = 0; i < 5;)
            {
                var pp = p.transform.position;
                pp.y = Bpos[0].position.y + 0.2f;
                Vector3 vec = pp - Bpos[0].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -60 + ((30 * 1) * i), 0) * vec;
                vec *= 50;
                var t = Instantiate(Bullet[3], Bpos[0].position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev1_3", 0.45f);
        }
    }
    void Ev1_3()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        time[4] = 1.15f;
    }

    void Event2()
    {
        if (p != null)
        {
            var newpos = p.transform.position;
            if (ontrg == 0)
            {
                ontrg = 1;
                objE.audioS.PlayOneShot(Ase[0]);
                newpos.y += 0.16f;
                time[6] = Vector3.Distance(this.transform.position, newpos);
                objE.Eanim.SetInteger("Anumber", 4);
                Invoke("Ev2_1", 0.5f);
            }
            else if (ontrg == 1)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 30f);
            }
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[3]);
            Instantiate(Bullet[4], Bpos[2].transform.position, this.transform.rotation);
            Invoke("Ev2_2", 0.4f);
        }
    }
    void Ev2_2()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;
            objE.audioS.PlayOneShot(Ase[2]);
            for (int i = 0; i < 3;)
            {
                var pp = p.transform.position;
                pp.y = Bpos[0].position.y + 0.2f;
                Vector3 vec = pp - Bpos[0].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -30 + ((30 * 1) * i), 0) * vec;
                vec *= 50;
                var t = Instantiate(Bullet[3], Bpos[0].position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev2_3", 0.4f);
        }
    }
    void Ev2_3()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        time[4] = 1.15f;
    }
    void Event3()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            objE.audioS.PlayOneShot(Ase[2]);
            Invoke("Ev3_1", 0.3f);
        }
    }
    void Ev3_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            for (int i = 0; i < 5;)
            {
                var pp = p.transform.position;
                pp.y = Bpos[0].position.y + 0.2f;
                Vector3 vec = pp - Bpos[0].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -60 + ((30 * 1) * i), 0) * vec;
                vec *= 45;
                var t = Instantiate(Bullet[5], Bpos[0].position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev3_2", 0.5f);
        }
    }
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        time[4] = 1.3f;
    }

    void Event4()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 7);
            Invoke("Ev4_1", 0.5f);
        }
    }
    void Ev4_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[4]);
            for (int i = 0; i < 6;)
            {
                int textnumber = i + 1;
                GameObject lp = GameObject.Find("lightPos_" + textnumber);
                target[i] = lp.transform.position;
                GameObject dsobj = Instantiate(Bullet[6], target[i], this.transform.rotation);
                Destroy(dsobj.gameObject, 1f);
                i++;
            }
            target[7] = p.transform.position;
            Invoke("Ev4_2", 0.45f);
        }
    }
    void Ev4_2()
    {
        if (ontrg == 2 && p != null)
        {
            Instantiate(Bullet[7], target[7], Bullet[7].transform.rotation);
            returnNumber++;
            if (returnNumber < 8)
            {
                target[7] = p.transform.position;
                Invoke("Ev4_2", 0.4f);
            }
            else
            {
                ontrg = 3;
                returnNumber = 0;
                Invoke("Ev4_3", 0.3f);
            }
        }
    }
    void Ev4_3()
    {
            objE.Eanim.SetInteger("Anumber", 1);
            time[4] = 1f;
    }
    void Event6()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 7);
            objE.audioS.PlayOneShot(Ase[5]);
            var newbosspos = this.transform.position;
            newbosspos.y += 5;
            GameObject dsobj = Instantiate(Bullet[13], newbosspos, this.transform.rotation);
            Destroy(dsobj.gameObject, 3f);
            Invoke("Ev6_1",2f);
        }
    }
    void Ev6_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[4]);
            target[7] = p.transform.position;
            target[7].y = this.transform.position.y;
            Invoke("Ev6_2", 1f);
        }
    }
    void Ev6_2()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;
            GameObject rotobj = Instantiate(Bullet[14], target[7], Bullet[14].transform.rotation);
            var newppos = p.transform.position;
            newppos.y = rotobj.transform.position.y;
            var rotation = Quaternion.LookRotation(newppos - rotobj.transform.position);
            rotobj.transform.rotation = rotation;
            Invoke("Ev6_3", 1f);
        }
    }
    void Ev6_3()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        time[4] = 1f;
    }
    void Event7()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            Invoke("Ev7_1", 0.3f);
        }
    }
    void Ev7_1()
    {
        GManager.instance.setrg = 33;
        objE.audioS.PlayOneShot(Ase[6]);
        Instantiate(Bullet[4], Bpos[2].transform.position, this.transform.rotation);
        Vector3 upP = p.transform.position;
        upP.y = 80;
        Instantiate(Bullet[15], upP, Bullet[15].transform.rotation);
        for (int i = 0; i < 4;)
        {
            Vector3 upR = p.transform.position;
            upR.x = Random.Range(Bpos[3].transform.position.x, Bpos[4].transform.position.x);
            upR.z = Random.Range(Bpos[3].transform.position.z, Bpos[4].transform.position.z);
            upR.y = 80;
            GameObject rock = Instantiate(Bullet[15], upR, Bullet[15].transform.rotation);
            rock.GetComponent<useG>().gravity = Random.Range(16, 32);
            i++;
        }
        Invoke("Ev7_2", 1.3f);
    }
    void Ev7_2()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        time[4] = 2.3f;
    }
    void Event5()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            for (int i = 0; i < 4;)
            {
                if (Bullet[i + 8] != null)
                {
                    GameObject dsobj = Instantiate(Bullet[12], Bullet[i + 8].transform.position, Bullet[i + 8].transform.rotation, Bullet[i + 8].transform);

                    Destroy(dsobj.gameObject, 7.3f);
                }
                i++;
            }
            Invoke("Ev5_1", 5.3f);
        }
    }
    void Ev5_1()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        time[4] = 1.3f;
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
                    if (at.GetComponent<enemyTrigger>())
                    {
                        Destroy(at.gameObject);
                    }
                }
            }
            objE.Eanim.SetInteger("Anumber", 0);
            resettrg = true;
        }
    }

    public IEnumerator Talk()
    {
        objE.damageOn = false;
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        //第二形態になったら切り替えるやつ
        secondStart();
    }
    void secondStart()
    {
        objE.Eanim.SetInteger("Anumber", 1);
        ontrg = 0;
        trg[1] = false;
        objE.damageOn = true;
        objE.audioS.Stop();
        eventnumber = 5;
        oldevent = eventnumber;
    }
    
}
