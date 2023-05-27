using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class an2 : MonoBehaviour
{
    public objAngle oa;
    public int maxrandom = 4;
    public int minrandom = 1;
    bool resettrg = false;
    [Header("1は動き始める時間、4はイベントリセット")]public float[] time;
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
    private bool isTalking;
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
                    rb.velocity = Vector3.zero;
                }
                objE.Estatus.defence += 3;
                maxrandom = 6;
                minrandom = 1;
                //会話イベント
                StartCoroutine(Talk());
                
            }
            if (resettrg == true)
            {
                resettrg = false;
                ontrg = 0;
                trg[1] = false;
                objE.audioS.Stop();
                if(oa.enabled == false)
                {
                    oa.enabled = true;
                }
                eventnumber = Random.Range(minrandom, maxrandom);
                //同じ技防止
                for (int i = oldevent; i == eventnumber;)
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
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.audioS.PlayOneShot(Ase[0]);
            time[6] = Vector3.Distance(this.transform.position, newpos);
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev1_0", 0.3f);
        }
        else if (ontrg == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 70f);
        }
    }
    void Ev1_0()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            Invoke("Ev1_1", 1f);
        }
    }
    void Ev1_1()
    {
        if (ontrg == 2)
        {
            ontrg = 3;
            objE.audioS.PlayOneShot(Ase[1]);
            GManager.instance.setrg = 28;
            Instantiate(Bullet[1], Bpos[1].transform.position, Bullet[1].transform.rotation);
            
            Invoke("Ev1_2", 1f);
        }
    }
    void Ev1_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.45f;
    }
    void Event2()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            Invoke("Ev2_1", 0.4f);
        }
    }
    void Ev2_1()
    {
        objE.audioS.PlayOneShot(Ase[2]);
        Instantiate(GManager.instance.shoteffect, Bpos[2].position, GManager.instance.shoteffect.transform.rotation);
        var newp = p.transform.position;
        newp.y += 0.16f;
        for (int i = 0; i < 9;)
        {
            Vector3 vec = newp - Bpos[2].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, -60 + ((15 * 1) * i), 0) * vec;
            vec *= 21;
            var t = Instantiate(Bullet[0], Bpos[2].position, this.transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            i++;
        }
        Invoke("Ev2_2", 0.3f);
    }
    void Ev2_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }

    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            Instantiate(Bullet[2], Bpos[2].transform.position, Bpos[2].transform.rotation, Bpos[2].transform);
            time[4] = 9f;
            Invoke("Ev3_1", 2.3f);
        }
    }
    void Ev3_1()
    {
        oa.enabled = false;
        var rotation = Quaternion.LookRotation(p.transform.position - Bpos[2].transform.position);
        Bpos[2].transform.rotation = rotation;
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
        for(int i = 0; i < 7;)
        {
            GameObject setobj = Instantiate(Bullet[3], Bpos[3 + i].transform.position, Bpos[3 + i].transform.rotation,Bpos[3+i]);
            var rotation = Quaternion.LookRotation(p.transform.position - setobj.transform.position);
                rotation.y = 0;
                rotation.z = 0;
            setobj.transform.rotation = rotation;
            i++;
        }
        Invoke("Ev4_2", 2.3f);
    }
    void Ev4_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }
    void Event5()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            Invoke("Ev5_1", 0.5f);
        }
    }
    void Ev5_1()
    {
        objE.audioS.PlayOneShot(Ase[4]);
        Instantiate(Bullet[5], Bpos[2].position, GManager.instance.shoteffect.transform.rotation);
        var newp = p.transform.position;
        newp.y += 0.16f;
            Vector3 vec = newp - Bpos[2].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 24;
            var t = Instantiate(Bullet[4], Bpos[2].position, this.transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
        Invoke("Ev5_2", 0.3f);
    }
    void Ev5_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1f;
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
        objE.damageOn = true;
        resettrg = true;
    }
}
