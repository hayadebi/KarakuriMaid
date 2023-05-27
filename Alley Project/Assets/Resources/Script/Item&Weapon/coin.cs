using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    bool starttrg = false;
    float starttime;
    float time;
    bool notrg = false;
    public int getcoin;
    bool gettrg = false;
    public bool hole = false;
    GameObject P = null;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        if(GManager.instance.mode == 2)
        {
            getcoin += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -16 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
        else if (starttrg == false)
        {
            starttime += Time.deltaTime;
            if(starttime > 2)
            {
                starttime = 0;
                starttrg = true;
            }
        }
        else if (starttrg == true)
        {
            if (hole == false)
            {
                hole = true;
            }
            else if (GManager.instance.over == false && hole == true)
            {
                if (P != null)
                {
                    Vector3 pos = P.transform.position - this.transform.position;
                    pos *= 11f;
                    this.GetComponent<Rigidbody>().velocity = pos;
                }
            }
        }
        if (notrg == false)
        {
            time += Time.deltaTime;
            if(time > 1.6)
            {
                notrg = true;
            }
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && gettrg == false && notrg == true)
        {
            gettrg = true;
            GManager.instance.setrg = 2;
            GManager.instance.Coin += getcoin;
            if(GManager.instance.skillselect == 5)
            {
                GManager.instance.Coin += getcoin;
            }
            Destroy(gameObject);
        }
    }
}
