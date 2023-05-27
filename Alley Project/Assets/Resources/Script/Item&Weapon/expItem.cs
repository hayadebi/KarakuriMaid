using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expItem : MonoBehaviour
{
    //GManager/holeexp
    public int getexp;
    bool gettrg = false;
    public bool hole = false;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.mode == 2)
        {
            getexp += 1;
        }
        Invoke("HoleS", 1f);

    }
    void HoleS()
    {
        hole = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -16 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
        else if (GManager.instance.over == false && hole == true)
        {
            if (GManager.instance.expTargetTrg == false)
            {
                GameObject P = GameObject.Find("Player");
                if (P != null)
                {
                    Vector3 pos = P.transform.position - this.transform.position;
                    Vector3 old = pos;
                    old.y = pos.y + 2;
                    this.GetComponent<Rigidbody>().velocity = pos * 11;
                }
            }
            else if (GManager.instance.expTargetTrg == true)
            {
                GameObject E = GameObject.Find("exppos");
                if (E != null)
                {
                    Vector3 pos = E.transform.position - this.transform.position;
                    Vector3 old = pos;
                    old.y = pos.y + 2;
                    this.GetComponent<Rigidbody>().velocity = pos * 7;
                }
            }
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && gettrg == false && GManager.instance.expTargetTrg == false)
        {
            gettrg = true;
            GManager.instance.setrg = 14;
            GManager.instance.Pstatus.inputExp += getexp;
            Destroy(gameObject,0.1f);
        }
        else if (col.tag == "boss" && gettrg == false && col.GetComponent<enemyS>() && GManager.instance.expTargetTrg == true)
        {
            gettrg = true;
            GManager.instance.setrg = 11;
            col.GetComponent<enemyS>().Estatus.health += 10;
            Destroy(gameObject, 0.1f);
        }
    }
}
