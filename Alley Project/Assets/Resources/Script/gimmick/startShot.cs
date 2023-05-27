using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startShot : MonoBehaviour
{
    public bool forwardtrg = false;
    public bool randomtrg = false;
    public int senumber = -1;
    public GameObject shotP;
    public float shottime = 2.2f;
    public float bulletspeed = 24;
    // Start is called before the first frame update
    void Start()
    {
        if(randomtrg == true)
        {
            shottime = Random.Range(0.3f, shottime);
        }
        Invoke("Shot", shottime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shot()
    {
        this.transform.parent = null;
        Instantiate(GManager.instance.shoteffect, this.transform.position, this.transform.rotation);
        if (shotP != null)
        {
            if(senumber != -1)
            {
                GManager.instance.setrg = senumber;
            }
            Vector3 vec = shotP.transform.position - this.transform.position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= bulletspeed;
            this.GetComponent<Rigidbody>().velocity = vec;
        }
        else if (forwardtrg == true)
        {
            if (senumber != -1)
            {
                GManager.instance.setrg = senumber;
            }
            Vector3 vec = (this.transform.position + this.transform.forward * 2) - this.transform.position ;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= bulletspeed;
            this.GetComponent<Rigidbody>().velocity = vec;
        }
        else if (shotP == null)
        {
            if (senumber != -1)
            {
                GManager.instance.setrg = senumber;
            }
            GameObject P = GameObject.Find("Player");
            var ppos = P.transform.position;
            ppos.y += 0.16f;
            Vector3 vec = ppos - this.transform.position ;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= bulletspeed;
            this.GetComponent<Rigidbody>().velocity = vec;
        }
    }
}
