using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGet : MonoBehaviour
{
    bool gettrg = false;
    public int WeaponNumber;
    GameObject gunpos;
    // Start is called before the first frame update
    void Start()
    {
        gunpos = GameObject.Find("gunpos");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -5 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player" && gettrg == false && Input.GetKeyDown(KeyCode.E))
        {
            gettrg = true;
            if (GManager.instance.itemhand != -1 && GManager.instance.WeaponID[WeaponNumber].getTrigger == 0)
            {
                //GManager.instance.Pstatus.attack -= GManager.instance.WeaponID[GManager.instance.itemhand].upAttack;
                foreach (Transform n in gunpos.transform)
                {
                    GameObject.Destroy(n.gameObject);
                }
                GManager.instance.itemhand = -1;
            }
            if (GManager.instance.itemhand == -1)
            {
                GManager.instance.setrg = 8;
                GameObject P = GameObject.Find("全体");
                Animator anim = P.GetComponent<Animator>();
                anim.Play("handG");
                GManager.instance.WeaponID[WeaponNumber].getTrigger = 1;
                    //GManager.instance.Pstatus.attack += GManager.instance.WeaponID[WeaponNumber].upAttack;
                    Instantiate(GManager.instance.WeaponID[WeaponNumber].shotobj, gunpos.transform.position, gunpos.transform.rotation, gunpos.transform);
                    GManager.instance.itemhand = WeaponNumber;
            }

            Destroy(gameObject, 0.04f);
        }
        
    }
}
