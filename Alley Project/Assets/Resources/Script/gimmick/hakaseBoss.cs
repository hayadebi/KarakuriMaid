using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class hakaseBoss : MonoBehaviour
{
    public BoxCollider boxcol = null;
    public objAngle ang;
    public objAngle ang2;
    public anPlayer anp;
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
    public GameObject secondObj = null;
    [Header("触らなくていい")] public int wariaihp = 420;
    bool stoptrg = false;
    //Fungus
    private bool isTalking;
    private Flowchart flowChart;
    public string message = "second";
    int returnNumber = 0;
    Vector3 attackpos;
    GameObject darkBall = null;
    GameObject rotObj = null;
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
                anp.saytimestop = false;
                objE.Estatus.speed = GManager.instance.Pstatus.speed * 2;
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
                    rb.velocity = Vector3.zero;
                }
                objE.Estatus.defence += 3;
                minrandom = -1;
                maxrandom = 9;
                objE.secondMode = true;
                //会話イベントからの第二形態
                anp.saytimestop = true;
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
        if (p != null)
        {
           
            if (ontrg == 0)
            {
                ontrg = 1;
                ang.enabled = false;
                attackpos = p.transform.position;
                attackpos.y += 0.3f;
                time[6] = Vector3.Distance(this.transform.position, attackpos);
                objE.audioS.PlayOneShot(Ase[0]);
                GameObject dsobj = Instantiate(Bullet[2], Bpos[2].transform.position, this.transform.rotation, Bpos[2].transform);
                Destroy(dsobj.gameObject, 1);
                objE.Eanim.SetInteger("Anumber", 3);
                objE.damageOn = false;
                Invoke("Ev1_1", 1f);
            }
            else if (ontrg == 1)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, attackpos, time[6] / 50f);
            }
        }
    }
    void Ev1_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            ang.enabled = true;
            objE.damageOn = true;
            GManager.instance.setrg = 37;
            Instantiate(Bullet[0], Bpos[0].transform.position, this.transform.rotation);
            for (int i = 0; i < 9;)
            {
                Vector3 vec =this.transform.position + this.transform.forward * 2;
                vec.Normalize();
                vec = Quaternion.Euler(-20, 0 + ((40 * 1) * i), 0) * vec;
                vec *= 30;
                var t = Instantiate(Bullet[1], Bpos[1].position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
        }
        Invoke("Ev1_2", 1f);
    }
    void Ev1_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }

    void Event2()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 1);
            objE.audioS.PlayOneShot(Ase[1]);
            Invoke("Ev2_1", 0.45f);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[2]);
            var pp = p.transform.position;
            pp.y += 0.5f;
            Vector3 vec = pp - Bpos[3].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 40;
            var t = Instantiate(Bullet[3], Bpos[3].position, this.transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;

            vec = pp - Bpos[4].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 50;
            t = Instantiate(Bullet[3], Bpos[4].position, this.transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            
        }
        Invoke("Ev2_2", 0.4f);
    }
    void Ev2_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }
    void Event3()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[3]);
            darkBall = Instantiate(Bullet[4], Bpos[5].position, this.transform.rotation, Bpos[5]);
            iTween.ScaleTo(darkBall.gameObject, iTween.Hash("x",0.12f,"y",0.12f,"z",0.12f,"time",0.4f));
            Destroy(darkBall.gameObject, 4f);
            Invoke("Ev3_1", 0.5f);
        }
    }
    void Ev3_1()
    {
        if (ontrg == 1 && p != null && darkBall != null)
        {
            ontrg = 2;
            GManager.instance.setrg = 31;
            darkBall.transform.parent = null;
            var pp = p.transform.position;
            pp.y += 1f;
            Vector3 vec = pp - Bpos[5].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 45;
            darkBall.GetComponent<Rigidbody>().velocity = vec;
            
        }
        Invoke("Ev3_2", 1f);
    }
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.4f;
    }

    void Event4()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 4);
            Invoke("Ev4_1", 0.3f);
        }
    }
    void Ev4_1()
    {
        if (ontrg == 1 && p != null)
        {
            objE.audioS.PlayOneShot(Ase[4]);
            for (int i = 0; i < 3;)
            {
                var pp = p.transform.position;
                pp.y += 0.3f;
                Vector3 vec = pp - Bpos[6].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -20 + ((20 * 1) * i), 0) * vec;
                vec *= 45;
                var t = Instantiate(Bullet[5], Bpos[6].position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            returnNumber++;
            if (returnNumber < 5)
            {
                Invoke("Ev4_1", 0.2f);
            }
            else
            {
                ontrg = 2;
                returnNumber = 0;
                Invoke("Ev4_2", 0.3f);
            }
        }
    }
    void Ev4_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1f;
    }

    void Event5()
    {
        if (p != null)
        {

            if (ontrg == 0)
            {
                ontrg = 1;
                attackpos = p.transform.position;
                attackpos.y += 0.3f;
                time[6] = Vector3.Distance(secondObj.transform.position, attackpos);
                objE.Eanim.SetInteger("Anumber", 1);
                Invoke("Ev5_0", 0.3f);
            }
            else if (ontrg == 2)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, attackpos, time[6] / 50f);
            }
            else if(ontrg == 3 && rotObj != null)
            {
                rotObj.transform.Rotate(0f, 360.0f * Time.deltaTime, 0f);
            }
        }
    }
    void Ev5_0()
    {
        if(ontrg == 1 && p!= null)
        {
            objE.audioS.PlayOneShot(Ase[1]);
            ontrg = 2;
        }
        Invoke("Ev5_1", 1f);
    }
    void Ev5_1()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;
            objE.audioS.PlayOneShot(Ase[2]);
            ang.enabled = false;
            Instantiate(Bullet[8], Bpos[8].position, Bullet[8].transform.rotation);
            rotObj = Instantiate(Bullet[7], Bpos[7].transform.position, secondObj.transform.rotation, secondObj.transform);
            
        }
        Invoke("Ev5_2", 1f);
    }
    void Ev5_2()
    {
        ontrg = 4;
        ang.enabled = true;
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }
    void Event6()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[7]);
            Invoke("Ev6_1", 0.35f);
        }
    }
    void Ev6_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[6]);
            for (int i = 0; i < 5;)
            {
                var pp = p.transform.position;
                pp.y = Bpos[9].position.y + 1f;
                Vector3 vec = pp - Bpos[9].position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -50 + ((25 * 1) * i), 0) * vec;
                vec *= 36;
                var t = Instantiate(Bullet[9], Bpos[9].position, secondObj.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            
        }
        Invoke("Ev6_2", 0.5f);
    }
    void Ev6_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }
    void Event7()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            objE.audioS.PlayOneShot(Ase[8]);
            GameObject dsobj = Instantiate(Bullet[10], Bpos[7].position, secondObj.transform.rotation,Bpos[7]);
            Destroy(dsobj.gameObject, 3f);
            Invoke("Ev7_1", 2f);
        }
    }
    void Ev7_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[9]);
            target[7] = p.transform.position;
            target[7].y = this.transform.position.y;
            
        }
        Invoke("Ev7_2", 1f);
    }
    void Ev7_2()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;
            GameObject rotobj = Instantiate(Bullet[11], target[7], Bullet[11].transform.rotation);
            Invoke("Ev7_3", 1f);
        }
        Invoke("Ev7_3", 1f);
    }
    void Ev7_3()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }

    void Event8()
    {
        if (ontrg == 0 && p != null)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 4);
            
            Invoke("Ev8_1", 0.35f);
        }
    }
    void Ev8_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[10]);
            for (int i = 0; i < 3;)
            {
                Vector3 vec = (secondObj.transform.position + secondObj.transform.forward * 2) - secondObj.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(-15, -30 + ((30 * 1) * i), 0) * vec;
                vec *= 40;
                var t = Instantiate(Bullet[12], Bpos[7].position, secondObj.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
        }
        Invoke("Ev8_2", 0.3f);
    }
    void Ev8_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
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
        Instantiate(Bullet[6], p.transform.position, Bullet[6].transform.rotation);
        Invoke("second1", 0.3f);
    }
    void second1()
    {
        secondObj.SetActive(true);
        ang = secondObj.GetComponent<objAngle>();
        objE.Eanim = secondObj.GetComponent<Animator>();
        objE.audioS = secondObj.GetComponent<AudioSource>();
        this.GetComponent<objAngle>().enabled = false;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        boxcol.enabled = false;
        anp.boss = secondObj;
        ang2.targetobj[0] = secondObj;
        rb = secondObj.GetComponent<Rigidbody>();
        this.tag = "Untagged";
        GameObject[] scobjs = GameObject.FindGameObjectsWithTag("second");
        foreach (GameObject scobj in scobjs)
        {
            scobj.SetActive(false);
        }
        secondObj.transform.parent = this.transform;
        secondObj.GetComponent<enemyS>().damageOn = false;
        Invoke("second2", 0.3f);
    }
    void second2()
    {
        objE.Eanim.SetInteger("Anumber", 3);
        objE.audioS.PlayOneShot(Ase[5]);
        Invoke("secondEnd", 2.1f);
    }
    void secondEnd()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        anp.saytimestop = false;
        secondObj.GetComponent<enemyS>().damageOn = true;
        minrandom = 5;
        resettrg = true;
    }
}
