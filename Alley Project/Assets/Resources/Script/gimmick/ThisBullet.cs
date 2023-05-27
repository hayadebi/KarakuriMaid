using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisBullet : MonoBehaviour
{
    public float shottime = 2;
    public float bulletspeed = 20;
    public int senumber;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Shot", shottime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Shot()
    {
        GManager.instance.setrg = senumber;
        GameObject p = GameObject.Find("Player");
            if (p != null)
            {
                Instantiate(GManager.instance.shoteffect, this.transform.position, this.transform.rotation);
                Vector3 vec = p.transform.position - this.transform.position;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0, 0) * vec;
                vec *= bulletspeed;
                this.GetComponent<Rigidbody>().velocity = vec;
            }
    }
}
