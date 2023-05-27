using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pumpkin : MonoBehaviour
{
    public Transform minpos;
    public Transform maxpos;
    bool resettrg = false;
    public float[] time;
    public int ontrg = 0;
    public Transform[] Bpos;
    public int eventnumber = 0;
    public AudioClip[] Ase;
    public Renderer ren;
    enemyS objE;
    public GameObject[] obj;
    public int trg = 0;
    GameObject p;
    int wariaihp2 = 66;
    int wariaihp3 = 33;
    GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
        if (GManager.instance.mode == 0)
        {
            wariaihp2 = 44;
            wariaihp3 = 22;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp2 = 88;
            wariaihp3 = 44;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (trg == 0 && GManager.instance.bossbattletrg == 1)
        {
            time[0] += Time.deltaTime;
            if (time[0] > time[1])
            {
                time[0] = 0;
                trg += 1;
            }
        }
        else if (trg > 0 && !objE.stoptrg)
        {
            if (wariaihp2 > objE.Estatus.health && trg == 1)
            {
                eventnumber = -1;
                trg += 1;
                GManager.instance.setrg = 18;
                Instantiate(obj[1], Bpos[0].transform.position, Bpos[0].transform.rotation);
                Instantiate(obj[1], Bpos[1].transform.position, Bpos[1].transform.rotation);
                resettrg = true;
            }
            else if (wariaihp3 > objE.Estatus.health && trg == 2)
            {
                eventnumber = -1;
                trg += 1;
                GManager.instance.setrg = 18;
                Instantiate(obj[2], Bpos[0].transform.position, Bpos[0].transform.rotation);
                Instantiate(obj[2], Bpos[1].transform.position, Bpos[1].transform.rotation);
                resettrg = true;
            }
            if (resettrg == true)
            {
                resettrg = false;
                eventnumber = 0;
                ontrg = 0;
                objE.audioS.Stop();

            }
            if (GManager.instance.over == false && GManager.instance.walktrg == true && objE.deathtrg == false)
            {
                if (time[3] > 0)
                {
                    time[3] -= Time.deltaTime;
                    if (time[3] < 0 || time[3] == 0)
                    {
                        time[3] = 0;
                        Eventreset();
                    }
                }
                if (GManager.instance.bossbattletrg == 1)
                {
                    if (p != null && eventnumber != -1)
                    {
                        Event1();
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
        if(ontrg == 0)
        {
            time[4] += Time.deltaTime;
            if(time[4] > time[5])
            {
                time[4] = 0;
                ontrg = 1;
            }
        }
        else if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[0]);
            Instantiate(obj[3],this.transform.position, this.transform.rotation);
            float randomX;
            float randomZ;
            randomX = Random.Range(minpos.position.x, maxpos.position.x);
            randomZ = Random.Range(minpos.position.z, maxpos.position.z);
            Vector3 rpos = this.transform.position;
            rpos.x = randomX;
            rpos.z = randomZ;
            this.transform.position = rpos;
            Instantiate(obj[3], this.transform.position, this.transform.rotation);
        }
        else if(ontrg == 2)
        {
            ontrg = 3;
            Instantiate(obj[4], Bpos[2].transform.position, Bpos[2].transform.rotation,Bpos[2].transform);
            time[3] = 4;
        }
    }
    void Eventreset()
    {
            GameObject[] ats = GameObject.FindGameObjectsWithTag("bossA");
            if (ats.Length != 0)
            {
                foreach (GameObject at in ats)
                {
                    Destroy(at.gameObject);
                }
            }
            resettrg = true;
    }
}
