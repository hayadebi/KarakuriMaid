using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koumori : MonoBehaviour
{
    public objAngle oa;
    public bool bossmove = false;
    public ColEvent shotCol;
    enemyS objE;
    GameObject p;
    Rigidbody rb;
    public AudioClip inputse;
    public AudioClip shotse;
    bool attacktrg = false;
    bool stoptrg = false;
    public GameObject impactobj;
    // Start is called before the first frame update
    void Start()
    {
        objE = this.GetComponent<enemyS>();
        p = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
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
                    if (!objE.stoptrg)
                    {
                        if (bossmove == false && GManager.instance.bossbattletrg == 0)
                        {
                            Run();
                        }
                        else if (bossmove == true)
                        {
                            Run();
                        }
                    }
                }
            }
            else if (GManager.instance.over == true || GManager.instance.walktrg == false)
            {
                if (stoptrg == false)
                {
                    stoptrg = true;
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }

    void Run()
    {
        if (shotCol.ColTrigger == true && attacktrg == false)
        {
            attacktrg = true;
            //攻撃
            objE.Eanim.SetInteger("Anumber", 1);
            objE.audioS.PlayOneShot(inputse);
            GManager.instance.setrg = 36;
            Invoke("Shot", 1f);
        }
    }
    void Shot()
    {
        if (p != null)
        {
            oa.enabled = false;
            var rotation = Quaternion.LookRotation(p.transform.position - this.transform.position);
            this.transform.rotation = rotation;
            objE.audioS.PlayOneShot(shotse);
            var newppos = p.transform.position;
            newppos.y += 0.3f;
            Vector3 vec = newppos - this.transform.position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= objE.Estatus.speed;
            rb.velocity = vec;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Untagged" || col.tag == "ground" || col.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            Instantiate(impactobj, this.transform.position, impactobj.transform.rotation);
            objE.inputkilltrg = true;
        }
    }
}
