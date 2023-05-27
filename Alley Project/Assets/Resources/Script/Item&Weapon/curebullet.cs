using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curebullet : MonoBehaviour
{
    bool gettrg = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -16 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
        else if (GManager.instance.over == false)
        {
            GameObject P = GameObject.Find("Player");
            var ppos = P.transform.position;
            ppos.y = P.transform.position.y + 0.4f;
            Vector3 pos = ppos - this.transform.position;
            this.GetComponent<Rigidbody>().velocity = pos * 8;
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && gettrg == false)
        {
            gettrg = true;
            GManager.instance.setrg = 3;
            Instantiate(GManager.instance.skillobj[11], col.gameObject.transform.position, GManager.instance.skillobj[11].transform.rotation);
            int curenumber = GManager.instance.WeaponID[13].upAttack / 4;
            GManager.instance.Pstatus.health += curenumber;
            if(GManager.instance.Pstatus.health > GManager.instance.Pstatus.maxHP)
            {
                GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
