using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sito : MonoBehaviour
{
    public AddEffect[] adefenable;
    public GameObject[] ateffectenable;
    public int maxrandom = 4;
    public int minrandom = 1;
    bool resettrg = false;
    [Header ("1はスタートタイム、4はイベントリセット")]public float[] time;
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
    Vector3 oldpos;
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
                objE.Estatus.defence += 4;
                GManager.instance.setrg = 18;
                Instantiate(Bullet[2], Bpos[2].position, Bpos[2].rotation, Bpos[2]);
                adefenable[0].enabled = true;
                adefenable[1].enabled = true;
                ateffectenable[0].SetActive(true);
                ateffectenable[1].SetActive(true);
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
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            Invoke("Ev1_1", 0.3f);
        }
    }
    void Ev1_1()
    {
        if(ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[0]);
            GameObject dsobj = Instantiate(Bullet[0], Bpos[0].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            dsobj = Instantiate(Bullet[0], Bpos[1].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            oldpos = p.transform.position;
            Invoke("Ev1_2", 1f);
        }
    }
    void Ev1_2()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;
            Instantiate(Bullet[1], oldpos, Bullet[1].transform.rotation);
            time[4] = 1.3f;
        }
    }
    void Event2()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev2_1", 0.45f);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[1]);
            Invoke("Ev2_2", 1f);
        }
    }
    void Ev2_2()
    {
        if (ontrg == 2)
        {
            ontrg = 3;
            objE.Eanim.SetInteger("Anumber", 0);
            time[4] = 1f;
        }
    }
    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            Invoke("Ev3_1", 0.4f);
        }
    }
    void Ev3_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[2]);
            Invoke("Ev3_2", 1f);
        }
    }
    void Ev3_2()
    {
        if (ontrg == 2)
        {
            ontrg = 3;
            objE.Eanim.SetInteger("Anumber", 0);
            time[4] = 1f;
        }
    }

    void Event4()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 4);
            Invoke("Ev4_1", 1f);
        }
    }
    void Ev4_1()
    {
        if (ontrg == 1 && p != null)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[0]);
            GameObject dsobj = Instantiate(Bullet[0], Bpos[0].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            dsobj = Instantiate(Bullet[0], Bpos[1].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            oldpos = p.transform.position;
            Invoke("Ev4_2", 1f);
        }
    }
    void Ev4_2()
    {
        if (ontrg == 2 && p != null)
        {
            ontrg = 3;
            Instantiate(Bullet[1], oldpos, Bullet[1].transform.rotation);
            objE.audioS.PlayOneShot(Ase[0]);
            GameObject dsobj = Instantiate(Bullet[0], Bpos[0].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            dsobj = Instantiate(Bullet[0], Bpos[1].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            oldpos = p.transform.position;
            Invoke("Ev4_3", 1f);
        }
    }
    void Ev4_3()
    {
        if (ontrg == 3 && p != null)
        {
            ontrg = 4;
            Instantiate(Bullet[1], oldpos, Bullet[1].transform.rotation);
            objE.audioS.PlayOneShot(Ase[0]);
            GameObject dsobj = Instantiate(Bullet[0], Bpos[0].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            dsobj = Instantiate(Bullet[0], Bpos[1].position, Bullet[0].transform.rotation);
            Destroy(dsobj.gameObject, 1f);
            oldpos = p.transform.position;
            Invoke("Ev4_4", 1f);
        }
    }
    void Ev4_4()
    {
        if (ontrg == 4 && p != null)
        {
            ontrg = 5;
            Instantiate(Bullet[1], oldpos, Bullet[1].transform.rotation);
            time[4] = 1.3f;
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
