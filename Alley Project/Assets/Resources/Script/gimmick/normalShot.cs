using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalShot : MonoBehaviour
{
    public bool bosstrg = false;
    float looptime;
    public float maxlooptime = 2;
    private  AudioSource audioS;
    public AudioClip shotse;
    public GameObject Bullet;
    public string targetTag = "enemy";
    private GameObject target = null;
    float inputremove = 0;
    float oldremove = 999;
    public float bulletspeed = 16;
    public GameObject shotpos;
    // Start is called before the first frame update
    void Start()
    {
        audioS = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bosstrg == false)
        {
            Shot();
        }
        else if (bosstrg == true && GManager.instance.bossbattletrg == 1)
        {
            Shot();
        }
    }

    void Shot()
    {
        looptime += Time.deltaTime;
        if (looptime > maxlooptime)
        {
            looptime = 0;
            audioS.PlayOneShot(shotse);
            GameObject[] searchobj = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject tobj in searchobj)
            {
                inputremove = (tobj.transform.position.x - shotpos.transform.position.x) + (tobj.transform.position.y - shotpos.transform.position.y) + (tobj.transform.position.z - shotpos.transform.position.z);
                if(inputremove < oldremove )
                {
                    oldremove = inputremove;
                    target = tobj;
                }
            }
            if (target != null)
            {
                Instantiate(GManager.instance.shoteffect, shotpos.transform.position,shotpos.transform.rotation,shotpos.transform);
                Vector3 vec = target.transform.position - shotpos.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(-3.2f, 0, 0) * vec;
                vec *= bulletspeed;
                GameObject t = Instantiate(Bullet, shotpos.transform.position, Bullet.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
            }
            else
            {
                oldremove = 999;
            }
        }
    }
}
