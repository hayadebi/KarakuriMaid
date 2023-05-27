using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minipumpkin : MonoBehaviour
{
    public ColEvent colE;
    public Transform minpos;
    public Transform maxpos;
    bool resettrg = false;
    public int ontrg = 0;
    public Renderer ren;
    enemyS objE;
    public int trg = 0;
    GameObject p;
    public GameObject Bullet;
    public GameObject tpeffect;
    public GameObject shotpos;
    float time_reset = 0;
    float time_tpoutput;
    public float time_tpinput;
    public AudioClip tpse;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (objE.absoluteStop == false)
        {
            if (!objE.stoptrg)
            {
                if (resettrg == true)
                {
                    resettrg = false;
                    ontrg = 0;
                    objE.audioS.Stop();
                }
                if (GManager.instance.over == false && GManager.instance.walktrg == true && objE.deathtrg == false && GManager.instance.bossbattletrg == 0)
                {
                    if (time_reset > 0)
                    {
                        time_reset -= Time.deltaTime;
                        if (time_reset < 0 || time_reset == 0)
                        {
                            time_reset = 0;
                            Eventreset();
                        }
                    }
                    if (p != null && colE.ColTrigger == true)
                    {
                        Event1();
                    }
                }
            }
        }
    }

    void Event1()
    {
        if (ontrg == 0)
        {
            time_tpoutput += Time.deltaTime;
            if (time_tpoutput > time_tpinput)
            {
                time_tpoutput = 0;
                ontrg = 1;
            }
        }
        else if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(tpse);
            Instantiate(tpeffect, this.transform.position, this.transform.rotation);
            float randomX;
            float randomZ;
            randomX = Random.Range(minpos.position.x, maxpos.position.x);
            randomZ = Random.Range(minpos.position.z, maxpos.position.z);
            Vector3 rpos = this.transform.position;
            rpos.x = randomX;
            rpos.z = randomZ;
            this.transform.position = rpos;
            Instantiate(tpeffect, this.transform.position, this.transform.rotation);
        }
        else if (ontrg == 2)
        {
            ontrg = 3;
            Instantiate(Bullet, shotpos.transform.position, shotpos.transform.rotation, shotpos.transform);
            time_reset = 4;
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
