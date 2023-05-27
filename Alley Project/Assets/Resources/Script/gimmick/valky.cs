using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class valky : MonoBehaviour
{
    public objAngle ang;
    public int maxrandom = 5;
    public int minrandom = 1;
    bool resettrg = false;
    [Header("1はスタートタイム、4はイベントリセット")] public float[] time;
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
                objE.Eanim.SetInteger("Anumber", 0);
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
                if(maxrandom > 6 && eventnumber == 4)
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
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.audioS.PlayOneShot(Ase[0]);
            newpos.y += 0.16f;
            time[6] = Vector3.Distance(this.transform.position, newpos);
            objE.Eanim.SetInteger("Anumber", 3);
            Invoke("Ev1_1", 1f);
        }
        else if (ontrg == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 55f);
        }
    }
    void Ev1_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            
            objE.audioS.PlayOneShot(Ase[3]);
            GManager.instance.setrg = 32;
            Instantiate(Bullet[1], Bpos[1].transform.position, Bullet[1].transform.rotation);
            Invoke("Ev1_2", 1f);
        }
    }
    void Ev1_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }

    void Event2()
    {
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.audioS.PlayOneShot(Ase[2]);
            newpos.y += 0.16f;
            time[6] = Vector3.Distance(this.transform.position, newpos);
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev2_1", 0.5f);
        }
        else if (ontrg == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 40f);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[1]);
            Invoke("Ev2_2", 0.25f);
        }
    }
    void Ev2_2()
    {
        
        GManager.instance.setrg = 32;
        Instantiate(Bullet[3], Bpos[2].transform.position, Bpos[2].transform.rotation);
        Invoke("Ev2_3", 1f);
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
            objE.Eanim.SetInteger("Anumber", 4);
            Invoke("Ev3_1", 0.35f);
        }
    }
    void Ev3_1()
    {
        objE.audioS.PlayOneShot(Ase[4]);
        Instantiate(Bullet[2], Bpos[2].position, Bullet[2].transform.rotation);
            var pp = p.transform.position;
            pp.y += 0.128f;
            Vector3 vec = pp - Bpos[2].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 32;
            var t = Instantiate(Bullet[4], Bpos[2].position, Bpos[2].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
        Invoke("Ev3_2", 1f);
    }
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.45f;
    }

    void Event4()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            Invoke("Ev4_1", 0.35f);
        }
    }
    void Ev4_1()
    {
        ontrg = 2;
        Vector3 upP = p.transform.position;
        upP.y = Bpos[3].position.y;
        Instantiate(Bullet[5], upP, Bullet[5].transform.rotation);
        Invoke("Ev4_2", 3f);
    }
    void Ev4_2()
    {
        ontrg = 3;
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }
    void Event6()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            Invoke("Ev6_1", 0.25f);
        }
    }
    void Ev6_1()
    {
        objE.audioS.PlayOneShot(Ase[6]);
        Instantiate(Bullet[3], Bpos[2].position, Bullet[3].transform.rotation);

        for (int i = 0; i < 10;)
        {
            var pp = p.transform.position;
            pp.y += 0.16f;
            Vector3 vec = pp - Bpos[2].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0 + ((36 * 1) * i), 0) * vec;
            vec *= 36;
            var t = Instantiate(Bullet[9], Bpos[2].position, Bullet[9].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            i++;
        }
        Invoke("Ev6_2", 1f);
    }
    void Ev6_2()
    {
        ontrg = 3;
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.45f;
    }
    void Event7()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 7);
            Invoke("Ev7_1", 0.3f);
        }
    }
    void Ev7_1()
    {
        objE.audioS.PlayOneShot(Ase[7]);
        var rotation = Quaternion.LookRotation(p.transform.position - Bpos[5].transform.position);
            rotation.z = 0;
        Bpos[5].transform.rotation = rotation;
        for (int i = 1; i < 5;)
        {
            Instantiate(Bullet[10], Bpos[4+i].position, Bpos[5].transform.rotation);
            i++;
        }
        Invoke("Ev7_2", 2f);
    }
    void Ev7_2()
    {
        ontrg = 3;
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }
    void Event5()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            ang.enabled = false;
            objE.Eanim.SetInteger("Anumber", 8);
            Invoke("Ev5_1", 0.3f);
        }
    }
    void Ev5_1()
    {
        objE.audioS.PlayOneShot(Ase[8]);
        Instantiate(Bullet[11], Bpos[2].position, Bpos[2].rotation, Bpos[2].transform);
        Invoke("Ev5_2", 2.3f);
    }
    void Ev5_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        ang.enabled = true;
        time[4] = 1.4f;
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
        objE.Eanim.SetInteger("Anumber", 5);
        Instantiate(Bullet[6], Bpos[0].position, Bullet[6].transform.rotation);
        Invoke("second1", 0.3f);
    }
    void second1()
    {
        GManager.instance.expTargetTrg = true;
        GameObject[] yariobjs = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject yariobj in yariobjs)
        {
            if(yariobj.GetComponent<enemyTrigger>() && yariobj.GetComponent<enemyTrigger>().trgname == "yari")
            {
                yariobj.GetComponent<enemyS>().inputkilltrg = true;
            }
        }
        Invoke("second2", 0.3f);
    }
    void second2()
    {
        objE.audioS.PlayOneShot(Ase[5]);
        Invoke("secondEnd", 3f);
    }
    void secondEnd()
    {
        GManager.instance.expTargetTrg = false;
        objE.Eanim.SetInteger("Anumber", 0);
        objE.damageOn = true;
        //-------------------
        ontrg = 0;
        trg[1] = false;
        objE.audioS.Stop();
        eventnumber = 3;
        oldevent = eventnumber;
    }
}
