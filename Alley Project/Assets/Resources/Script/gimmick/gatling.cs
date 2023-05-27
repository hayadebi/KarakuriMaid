using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatling : MonoBehaviour
{
    public bool bossmove = false;
    public ColEvent shotCol;
    enemyS objE;
    GameObject p;
    Rigidbody rb;
    public AudioClip shotse;
    bool attacktrg = false;
    public GameObject shotpos;
    public GameObject shotpos2;
    public GameObject Bullet;
    private float hittime = 0;
    // Start is called before the first frame update
    void Start()
    {
        objE = this.GetComponent<enemyS>();
        p = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
        objE.audioS.volume = 0.08f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objE.absoluteStop == false)
        {
            if (GManager.instance.over == false && GManager.instance.walktrg == true)
            {
                if (objE.damagetrg == false && objE.deathtrg == false)
                {
                    if (shotCol.ColTrigger == true && !objE.stoptrg)
                    {
                        if (objE.audioS.clip == null)
                        {
                            objE.audioS.clip = shotse;
                            objE.audioS.loop = true;
                            objE.audioS.Play();
                        }
                        if (bossmove == false && GManager.instance.bossbattletrg == 0)
                        {
                            Run();
                        }
                        else if (bossmove == true)
                        {
                            Run();
                        }
                    }
                    else if (shotCol.ColTrigger == false && objE.hitdamagetrg == true && !objE.stoptrg)
                    {
                        if (objE.audioS.clip == null)
                        {
                            objE.audioS.clip = shotse;
                            objE.audioS.loop = true;
                            objE.audioS.Play();
                        }
                        if (bossmove == false && GManager.instance.bossbattletrg == 0)
                        {
                            Run();
                        }
                        else if (bossmove == true)
                        {
                            Run();
                        }
                    }
                    else
                    {
                        if (objE.audioS.clip != null)
                        {
                            objE.audioS.clip = null;
                            objE.audioS.loop = false;
                            objE.audioS.Stop();
                        }
                    }
                }
                if (objE.hitdamagetrg == true)
                {
                    hittime += Time.deltaTime;
                    if (hittime > 1f)
                    {
                        hittime = 0;
                        objE.hitdamagetrg = false;
                    }
                }
            }
        }
    }

    void Run()
    {
        if (attacktrg == false)
        {
                attacktrg = true;
                //攻撃
                Shot();
        }
    }
    void attackEnd()
    {
        if (attacktrg == true)
        {
            attacktrg = false;
        }
    }

    void Shot()
    {
        if (p != null)
        {
            int randomnumber = Random.Range(1, 3);
            if (randomnumber == 1)
            {
                Instantiate(GManager.instance.shoteffect, shotpos.transform.position, shotpos.transform.rotation);
                var ppos = p.transform.position;
                ppos.y += 0.16f;
                Vector3 vec = ppos - shotpos.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= 24;
                GameObject t = Instantiate(Bullet, shotpos.transform.position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            }
            else if (randomnumber == 2)
            {
                Instantiate(GManager.instance.shoteffect, shotpos2.transform.position, shotpos2.transform.rotation);
                var ppos = p.transform.position;
                ppos.y += 0.16f;
                Vector3 vec = ppos - shotpos2.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= 24;
                GameObject t = Instantiate(Bullet, shotpos2.transform.position, this.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            }
        }
        Invoke("attackEnd", 0.15f);
    }
}
