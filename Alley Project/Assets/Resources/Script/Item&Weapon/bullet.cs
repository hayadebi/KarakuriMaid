using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int senumber = -1;
    public int destroyEvent = -1;
    public float destroyTime = 1.6f;
    public bool boundtrg = false;
    public bool pbullet = false;
    public GameObject bossboom = null;
    public string returnObj = "";
    public float returntime = 1f;
    public float returnspeed = 24;
    private bool returntrg = false;
    [Header("イベントに使うオブジェクト")] public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Gdestroy", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -5 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
        else if (returnObj != "" && returntrg == false)
        {
            returntime -= Time.deltaTime;
            if(returntime < 0)
            {
                returntrg = true;
                GameObject obj = GameObject.Find(returnObj);
                if(obj != null)
                {
                    Vector3 vec = obj.transform.position - this.transform.position;
                    vec.Normalize();
                    vec = Quaternion.Euler(0, 0, 0) * vec;
                    vec *= returnspeed;
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    this.GetComponent<Rigidbody>().velocity = vec;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (bossboom == null)
        {
            if (col.tag == "rabbit")
            {
                GManager.instance.setrg = 9;
                Instantiate(GManager.instance.damageeffect, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
            }
            else if (col.tag == "ground" || col.tag == "Untagged")
            {
                if (boundtrg == false)
                {
                    GManager.instance.setrg = 9;
                    Instantiate(GManager.instance.damageeffect, this.transform.position, this.transform.rotation);
                    if (destroyEvent != -1)
                    {
                        dsEvent();
                    }
                    Destroy(gameObject);
                }
                else if (boundtrg == true)
                {
                    Instantiate(GManager.instance.shoteffect, this.transform.position, this.transform.rotation);
                }
            }
            else if (col.tag == "circle" && pbullet == false)
            {
                if (boundtrg == false)
                {
                    GManager.instance.setrg = 9;
                    Instantiate(GManager.instance.damageeffect, this.transform.position, this.transform.rotation);
                    Destroy(gameObject);
                }
                else if (boundtrg == true)
                {
                    Instantiate(GManager.instance.shoteffect, this.transform.position, this.transform.rotation);
                }
            }
        }
        else if(bossboom != null)
        {
            if (col.tag == "ground" || col.tag == "Untagged")
            {
                if (senumber != -1)
                {
                    GManager.instance.setrg = senumber;
                }
                Instantiate(bossboom, this.transform.position, bossboom.transform.rotation);
                Destroy(gameObject,0.1f);
            }
        }
    }
    void Gdestroy()
    {

        Instantiate(GManager.instance.killeffect, this.transform.position, this.transform.rotation);
        Destroy(gameObject, 0.1f);
    }
    void dsEvent()
    {
        if(destroyEvent == 1)
        {
            GManager.instance.setrg = 30;
            for (int i = 0; i < 8;)
            {
                GameObject p = GameObject.Find("Player");
                var pp = p.transform.position;
                pp.y += 0.3f;
                Vector3 vec = pp - this.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0 + ((45 * 1) * i), 0) * vec;
                vec *= 20;
                var t = Instantiate(obj, this.transform.position, obj.transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
        }
    }
}
