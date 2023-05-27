using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRain : MonoBehaviour
{
    public bool playertrg = false;
    public Transform maxP;
    public Transform minP;
    public float addhight;
    public GameObject Knife;
    public float startTime;
    public float summonTime;
    private float time;
    private bool starttrg = false;
    private GameObject P;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        Invoke("OnStart", startTime);
    }
    void OnStart()
    {
        starttrg = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(starttrg)
        {
            time += Time.deltaTime;
            if(time > summonTime)
            {
                time = 0;
                float x = Random.Range(minP.position.x, maxP.position.x);
                float z = Random.Range(minP.position.z, maxP.position.z);
                float y = this.transform.position.y + addhight;
                var sP = this.transform.position;
                sP.x = x;
                sP.y = y;
                sP.z = z;
                Instantiate(GManager.instance.shoteffect, sP, GManager.instance.shoteffect.transform.rotation);
                    var t = Instantiate(Knife, sP, Knife.transform.rotation);
                Vector3 force = new Vector3(0, -1, 0);
                t.GetComponent<Rigidbody>().velocity = force * 13;
                if(P && playertrg == false)
                {
                    if(P.transform.position.x > minP.position.x && P.transform.position.x < maxP.position.x && P.transform.position.z > minP.position.z && P.transform.position.z < maxP.position.z)
                    {
                        sP.x = P.transform.position.x;
                        sP.y = y;
                        sP.z = P.transform.position.z;
                        Instantiate(GManager.instance.shoteffect, sP, GManager.instance.shoteffect.transform.rotation);
                        t = Instantiate(Knife, sP, Knife.transform.rotation);
                        force = new Vector3(0, -1, 0);
                        t.GetComponent<Rigidbody>().velocity = force * 13;
                    }
                }
            }
        }
    }
}
