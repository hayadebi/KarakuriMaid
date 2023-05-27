using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class skillGet : MonoBehaviour
{
    public GameObject ui = null;
    public int getskill;
    bool gettrg = false;
    public bool hole = false;
    GameObject P = null;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.y < -5 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
        else if (GManager.instance.over == false && hole == true)
            {
                if (P != null)
                {
                    Vector3 pos = P.transform.position - this.transform.position;
                pos = Quaternion.Euler(0, 0, 0) * pos;
                pos *= 8f;
                    this.GetComponent<Rigidbody>().velocity = pos;
                }
            }

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && gettrg == false && hole == true)
        {
            gettrg = true;
            if(ui != null)
            {
                Instantiate(ui,transform.position, transform.rotation);
            }
            Instantiate(GManager.instance.lvupeffect, P.transform.position, GManager.instance.lvupeffect.transform.rotation, P.transform);
            GManager.instance.setrg = 12;
            GManager.instance.SkillID[getskill].skillget = 1;
            GManager.instance.skillsay = "skill" + getskill;
            GManager.instance.Sgetsay = true;
            Destroy(gameObject);
        }
    }
}
