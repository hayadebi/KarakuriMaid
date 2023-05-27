using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cure : MonoBehaviour
{
    float time;
    bool notrg = false;
    public int curehp;
    bool gettrg = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -5 || this.transform.position.y > 1600)
        {
            Destroy(gameObject);
        }
        else if (notrg == false)
        {
            time += Time.deltaTime;
            if (time > 1.6)
            {
                notrg = true;
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && gettrg == false && notrg == true)
        {
            gettrg = true;
            GManager.instance.setrg = 3;
            GManager.instance.Pstatus.health += curehp;
            if(GManager.instance.Pstatus.maxHP < GManager.instance.Pstatus.health)
            {
                GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
            }
            Destroy(gameObject);
        }
    }
}
