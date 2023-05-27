using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startweapon : MonoBehaviour
{
    GameObject gunpos;
    // Start is called before the first frame update
    void Start()
    {
        gunpos = GameObject.Find("gunpos");
        if (GManager.instance.itemhand != -1)
        {
            GameObject P = GameObject.Find("全体");
            Animator anim = P.GetComponent<Animator>();
            anim.Play("handG");
            Invoke("StartWeapon",0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartWeapon()
    {
        
        Instantiate(GManager.instance.WeaponID[GManager.instance.itemhand].shotobj, gunpos.transform.position, gunpos.transform.rotation, gunpos.transform);
        GManager.instance.WeaponID[GManager.instance.itemhand].gunmode = "Shoot!";
    }
}
