using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oracle : MonoBehaviour
{
    public int inputtrgnumber = -1;
    public int maxrandom = 4;
    public int minrandom = 1;
    bool resettrg = false;
    public float[] time;
    public int[] number;
    public int ontrg = 0;
    public Transform[] poss;
    public int eventnumber = 0;
    public AudioClip[] Ase;
    public Vector3[] target;
    enemyS objE;
    public GameObject[] Objs;
    public bool[] trg;
    int oldevent = 0;
    Rigidbody rb;
    GameObject p;
    public int wariaihp = 75;
    bool stoptrg = false;
    int angletrg = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
        eventnumber = Random.Range(minrandom, maxrandom);
        oldevent = 5;
        if (GManager.instance.mode == 0)
        {
            wariaihp = 53;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = 106;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (trg[0] == false && GManager.instance.bossbattletrg == 1 && GManager.instance.Triggers[inputtrgnumber] == 1)
        {
            time[0] += Time.deltaTime;
            if (time[0] > 4f)
            {
                time[0] = 0;
                trg[0] = true;
            }
        }
        else if (trg[0] == true && GManager.instance.Triggers[inputtrgnumber] == 1)
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
                GManager.instance.setrg = 20;
                Instantiate(Objs[0], poss[0].transform.position, poss[0].transform.rotation, poss[0].transform);
                maxrandom = 5;
                minrandom = 2;
                resettrg = true;
            }
            if (resettrg == true)
            {
                resettrg = false;
                objE.Eanim.SetInteger("Anumber", 0);
                ontrg = 0;
                angletrg = 1;
                trg[1] = false;
                objE.audioS.Stop();
                eventnumber = Random.Range(minrandom, maxrandom);
                //実装が終わったらコメントアウトを解除
                for (int i = oldevent; i == eventnumber;)
                {
                    eventnumber = Random.Range(minrandom, maxrandom);
                }
                oldevent = eventnumber;
                //-------------------------------------
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
                        if(angletrg == 1)
                        {
                            this.transform.LookAt(p.transform);
                            Vector3 angle = this.transform.localEulerAngles;
                                angle.z = 0;
                                angle.x = 0;
                            this.transform.localEulerAngles = angle;
                        }
                        else if (angletrg == 2)
                        {
                            this.transform.LookAt(p.transform);
                            Vector3 angle = this.transform.localEulerAngles;
                            this.transform.localEulerAngles = angle;
                        }
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
            objE.Eanim.SetInteger("Anumber", 1);
                Invoke("Shot", 0.3f);
        }
    }
    void Shot()
    {
            objE.audioS.PlayOneShot(Ase[0]);
            if (p != null)
            {
                Instantiate(GManager.instance.shoteffect, poss[1].transform.position, poss[1].transform.rotation);
                Vector3 vec = p.transform.position - poss[1].transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= 20;
                GameObject t = Instantiate(Objs[1], poss[1].transform.position, Objs[1].transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            }
        Invoke("animR", 1.3f);
    }
    void animR()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        Invoke("shotR", 0.2f);
    }
    void shotR()
    {
        number[0] += 1;
        ontrg = 0;
        if (number[0] > 6)
        {
            number[0] = 0;
            ontrg = 2;
            time[4] = 2f;
        }
        
    }
    void Event2()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            Invoke("Laser", 1f);
        }
    }
    void Laser()
    {
        angletrg = 0;
        objE.audioS.PlayOneShot(Ase[1]);
        Instantiate(Objs[2], poss[1].transform.position, poss[1].transform.rotation, poss[1].transform);
        time[4] = 4;
    }
    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            Invoke("Boom", 0.3f);
        }
    }
    void Boom()
    {
        objE.audioS.PlayOneShot(Ase[2]);
        Instantiate(GManager.instance.shoteffect, poss[2].transform.position, poss[2].transform.rotation);
        Vector3 rP = poss[2].transform.position;
        rP.x = p.transform.position.x;
        rP.z = p.transform.position.z;
        Instantiate(Objs[3], rP, Objs[3].transform.rotation);
        for (int i = 0; i < 8;)
        {
            float randomX = Random.Range(poss[2].transform.position.x, poss[3].transform.position.x);
            float randomZ = Random.Range(poss[2].transform.position.z, poss[3].transform.position.z);
            Vector3 randomP = poss[2].transform.position;
            randomP.x = randomX;
            randomP.z = randomZ;
            Instantiate(Objs[3], randomP, Objs[3].transform.rotation);
            i++;
        }
        time[4] = 6;
    }
    void Event4()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            angletrg = 2;
            objE.audioS.PlayOneShot(Ase[3]);
            objE.Eanim.SetInteger("Anumber", 4);
            for (int i = 0; i < 8; i++)
            {
                GameObject god = GameObject.Find("godPos" + i);
                if (god != null)
                {
                    Instantiate(Objs[4], god.transform.position, god.transform.rotation, god.transform);
                }
            }
            Invoke("God", 2.2f);
        }
    }
    void God()
    {
        objE.audioS.PlayOneShot(Ase[4]);
        time[4] = 4.3f;
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
