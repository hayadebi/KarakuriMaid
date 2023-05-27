using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShot : MonoBehaviour
{
    [Header("ステータスだよ")]
    public float shotspeed = 32;
    public float starttime = 0.3f;
    public GameObject shotP;
    public GameObject bullet;
    public AudioSource audioS;
    public AudioClip se;
    GameObject P;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        Invoke("Shot", starttime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shot()
    {
        audioS.PlayOneShot(se);
        Instantiate(GManager.instance.shoteffect, this.transform.position, this.transform.rotation);
        Vector3 vec = P.transform.position - shotP.transform.position;
        vec.Normalize();
        vec = Quaternion.Euler(0, 0, 0) * vec;
        vec *= shotspeed;
        GameObject t = Instantiate(bullet, shotP.transform.position, shotP.transform.rotation);
        t.GetComponent<Rigidbody>().velocity = vec;
    }
}
