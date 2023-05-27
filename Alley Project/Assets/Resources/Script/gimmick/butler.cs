using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class butler : MonoBehaviour
{
    public GameObject minpos;
    public GameObject maxpos;
    public int maxrandom = 4;
    public int minrandom = 1;
    bool resettrg = false;
    [Header("1はボスの動き開始/3はTPの感覚")]public float[] time;
    [Header("1はコピーかどうか、0はボス")] public int[] number;
    public int ontrg = 0;
    [Header("0は弾の発射位置")] public Transform[] Bpos;
    public int eventnumber = 0;
    [Header("0はTPの音/1は銃の音")] public AudioClip[] Ase;
    public Vector3[] target;
    public Renderer ren;
    enemyS objE;
    [Header("0は拳銃の弾")] public GameObject[] Bullet;
    public bool[] trg;
    int oldevent = 0;
    Rigidbody rb;
    GameObject p;
    GameObject mainC;
    int wariaihp = 125;
    bool stoptrg = false;
    public GameObject tpeffect;
    private bool isTalking;
    private Flowchart flowChart;
    public string message = "second";
    // Start is called before the first frame update
    void Start()
    {
        flowChart = this.GetComponent<Flowchart>();
        rb = this.GetComponent<Rigidbody>();
        p = GameObject.Find("Player");
        mainC = GameObject.Find("MainC");
        objE = this.GetComponent<enemyS>();
        eventnumber = Random.Range(minrandom, maxrandom);
        oldevent = eventnumber;
        if (GManager.instance.mode == 0)
        {
            wariaihp = 83;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = 166;
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (trg[0] == false && GManager.instance.bossbattletrg == 1)
        {
            if(number[1] == 5)
            {
                trg[0] = true;
            }
            if (number[1] != 5)
            {
                time[0] += Time.deltaTime;
                if (time[0] > time[1])
                {
                    trg[0] = true;
                    time[0] = 0;
                }
            }
        }
        else if (trg[0] == true)
        {
            if (wariaihp > objE.Estatus.health && trg[1] == false && trg[2] == false && number[1] == 0)//第二形態
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
                objE.Estatus.defence += 1;
                maxrandom = 5;
                minrandom = 1;
                resettrg = true;
                objE.damageOn = false;
                //途中会話
                StartCoroutine(Talk());
            }
            if (resettrg == true && !isTalking)
            {
                resettrg = false;
                ontrg = 0;
                trg[1] = false;
                objE.audioS.Stop();
                eventnumber = Random.Range(minrandom, maxrandom);
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
                if (GManager.instance.bossbattletrg == 1 && !isTalking)
                {
                    if (p != null && eventnumber != -1)
                    {
                        if (number[1] == 0)
                        {
                            if (number[0] > -1 && number[0] < 2)
                            {
                                if(objE.damageOn != false)
                                {
                                    objE.damageOn = false;
                                }
                                Invoke("Copy3", 0f);
                            }
                            else if (number[0] == 2 || number[0] == 14)
                            {
                                if (objE.damageOn != true)
                                {
                                    objE.damageOn = true;
                                }
                                Invoke("Event" + eventnumber, 0f);
                            }
                            else if (number[0] > 2 && number[0] <14)
                            {
                                if (objE.damageOn != false)
                                {
                                    objE.damageOn = false;
                                }
                                Invoke("Copy5", 0f);
                            }
                        }
                        else if (number[1] == 3)
                        {
                                Invoke("Copy3", 0f);
                        }
                        else if (number[1] == 5)
                        {
                            Invoke("Copy5", 0f);
                        }
                    }
                }
            }
            else if (GManager.instance.over == true || GManager.instance.walktrg == false)
            {

            }
        }
    }
    //技
    void Copy3()
    {
        if (ontrg == 0)
        {
            time[2] += Time.deltaTime;
            if (time[2] > time[3])
            {
                time[2] = 0;
                ontrg = 1;
            }
        }
        else if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[0]);
            Instantiate(tpeffect, this.transform.position, this.transform.rotation);
            float randomX;
            float randomZ;
            if(number[1] == 3)
            {
                minpos = GameObject.Find("minpos");
                maxpos = GameObject.Find("maxpos");
            }
            randomX = Random.Range(minpos.transform.position.x, maxpos.transform.position.x);
            randomZ = Random.Range(minpos.transform.position.z, maxpos.transform.position.z);
            Vector3 rpos = this.transform.position;
            rpos.x = randomX;
            rpos.z = randomZ;
            this.transform.position = rpos;
            Instantiate(tpeffect, this.transform.position, this.transform.rotation);
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("copyAT", 0.3f);
        }
    }
    void Copy5()
    {
        if (ontrg == 0)
        {
                ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("copyAT", 0.3f);
        }
    }
    void copyAT()
    {
        objE.audioS.PlayOneShot(Ase[1]); if (p != null)
        {
            Instantiate(GManager.instance.shoteffect, Bpos[0].transform.position, Bpos[0].transform.rotation);
            Vector3 vec = p.transform.position - Bpos[0].transform.position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 40;
            GameObject t = Instantiate(Bullet[0], Bpos[0].transform.position, Bullet[0].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
        }
        time[4] = 1.3f;
    }

    void Event1()
    {
        if(ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[2]);
            mainC.GetComponent<Pscript>().enabled = true;
            objE.damageOn = false;
            p.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            p.GetComponent<Animator>().speed = 0;
            p.GetComponent<player>().stoptrg = true;
            for (int i = 0; i < 12;)
            {
                Vector3 vec = p.transform.position - Bpos[1].transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, -90 + (15 * i), 0) * vec;
                vec *= 36;
                GameObject t = Instantiate(Bullet[1], Bpos[1].transform.position, Bpos[1].transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("bulletStop", 0.15f);
        }
    }
    void bulletStop()
    {
        GameObject[] blts = GameObject.FindGameObjectsWithTag("bullet");
        foreach (GameObject blt in blts)
        {
            if (blt.GetComponent<Rigidbody>())
            {
                blt.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        Invoke("timeStart", 1.45f);
    }

    void timeStart()
    {
        objE.audioS.PlayOneShot(Ase[3]);
        mainC.GetComponent<Pscript>().enabled = false;
        objE.damageOn = true;
        p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        p.GetComponent<Animator>().speed = 1;
        p.GetComponent<player>().stoptrg = false;
        p.GetComponent<player>().startjump = true;
        GameObject[] blts = GameObject.FindGameObjectsWithTag("bullet");
        foreach (GameObject blt in blts)
        {
            Vector3 vec = p.transform.position - blt.transform.position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 36;
            if (blt.GetComponent<Rigidbody>())
            {
                blt.GetComponent<Rigidbody>().velocity = vec;
            }
        }
        time[4] = 3;
        Invoke("animreset", 1.2f);
    }

    void Event2()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.audioS.PlayOneShot(Ase[4]);
            objE.Eanim.SetInteger("Anumber", 1);
            Instantiate(Bullet[2], Bpos[2].transform.position, Bpos[2].transform.rotation, Bpos[2].transform);
            Instantiate(Bullet[2], Bpos[3].transform.position, Bpos[3].transform.rotation, Bpos[3].transform);
            time[4] = 5;
            Invoke("animreset", 2f);
        }
    }

    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[5]);
            var cicrcleP = p.transform.position;
            cicrcleP.y = this.transform.position.y;
            Instantiate(Bullet[3], cicrcleP, Bullet[3].transform.rotation);
            time[4] = 7;
            Invoke("animreset", 6f);
        }
    }
    void Event4()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[2]);
            mainC.GetComponent<Pscript>().enabled = true;
            objE.damageOn = false;
            p.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            p.GetComponent<Animator>().speed = 0;
            p.GetComponent<player>().stoptrg = true;
            for (int i = 0; i < 8;)
            {
                float randomX = Random.Range(minpos.transform.position.x, maxpos.transform.position.x);
                float randomZ = Random.Range(minpos.transform.position.z, maxpos.transform.position.z);
                Vector3 rpos = this.transform.position;
                rpos.x = randomX;
                rpos.z = randomZ;
                Instantiate(Bullet[5], rpos, Bullet[5].transform.rotation);
                i++;
            }
            var exppos = p.transform.position;
            exppos.y = this.transform.position.y;
            Instantiate(Bullet[5], exppos , Bullet[5].transform.rotation);
            Invoke("LastStart", 2f);
        }
    }
    void LastStart()
    {
        objE.audioS.PlayOneShot(Ase[6]);
        mainC.GetComponent<Pscript>().enabled = false;
        objE.damageOn = true;
        p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        p.GetComponent<Animator>().speed = 1;
        p.GetComponent<player>().stoptrg = false;
        p.GetComponent<player>().startjump = true;
        GameObject[] effects = GameObject.FindGameObjectsWithTag("noeffect");
        foreach (GameObject effect in effects)
        {
            effect.SetActive(false);
        }
        time[4] = 5;
        Invoke("animreset", 4f);
    }

    void animreset()
    {
        objE.Eanim.SetInteger("Anumber", 0);
    }

    void SecondFrom()
    {
        GManager.instance.setrg = 23;
        mainC.GetComponent<Pscript>().enabled = true;
        objE.damageOn = false;
        p.GetComponent<Rigidbody>().velocity = Vector3.zero;
        p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        p.GetComponent<Animator>().speed = 0;
        Invoke("SecondStart", 1f);
    }
    void SecondStart()
    {
        objE.audioS.PlayOneShot(Ase[6]);
        mainC.GetComponent<Pscript>().enabled = false;
        objE.damageOn = true;
        p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        p.GetComponent<Animator>().speed = 1;
        p.GetComponent<player>().stoptrg = false;
        p.GetComponent<player>().startjump = true;
        var newpos = p.transform.position;
        newpos.x = 0;
        newpos.y = 119.5f;
        newpos.z = 36;
        p.transform.position = newpos;
        GameObject original = GameObject.FindGameObjectWithTag("original");
        Instantiate (Bullet[4],original.transform.position ,Bullet[4].transform.rotation );
        GameObject[] clones = GameObject.FindGameObjectsWithTag("clone");
        foreach(GameObject clone in clones)
        {
            Instantiate(Bullet[4], clone.transform.position, Bullet[4].transform.rotation);
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
        GManager.instance.walktrg = true;
        p.GetComponent<player>().stoptrg = true;

        SecondFrom();
    }
}
