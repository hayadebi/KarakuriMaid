using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class crane : MonoBehaviour
{
    public GameObject Fadein;
    public GameObject ParentPos;
    public bool bossmove = false;
    public ColEvent moveCol;
    public ColEvent stopCol;
    enemyS objE;
    GameObject P;
    Vector3 target;
    Rigidbody rb;
    public AudioClip shotse;
    bool stoptrg = false;
    int typetrg = 0;
    float changetime = 0;
    // Start is called before the first frame update
    void Start()
    {
        objE = this.GetComponent<enemyS>();
        P = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //時間
        if (objE.absoluteStop == false)
        {
            if (typetrg != 0 && P.GetComponent<player>().stoptrg == true)
            {
                changetime -= Time.deltaTime;
                if (changetime < 0)
                {
                    switch (typetrg)
                    {
                        case 1:
                            typetrg = 0;
                            changetime = 0;
                            parentP();
                            break;
                        case 2:
                            typetrg = 0;
                            changetime = 0;
                            LoadAttack();
                            break;
                        case 3:
                            typetrg = 0;
                            changetime = 0;
                            SceneChange();
                            break;
                    }
                }
            }
            //阻止
            if (P != null)
            {
                if (P.GetComponent<player>().stoptrg == true && Input.GetKeyDown(KeyCode.Alpha1) && GManager.instance.skillselect == -1)
                {
                    P.GetComponent<player>().stoptrg = false;
                    stopCol.ColTrigger = false;
                    typetrg = 0;
                    changetime = 0;
                    GManager.instance.walktrg = true;
                    GManager.instance.Triggers[24] = 0;
                    P.transform.parent = null;
                    P.GetComponent<Rigidbody>().useGravity = true;
                    rb.velocity = Vector3.zero;
                    objE.neweffect = 1;

                }
            }
            //------------------------
            if (GManager.instance.over == false && GManager.instance.walktrg == true && GManager.instance.Triggers[24] == 0)
            {
                if (objE.damagetrg == false && objE.deathtrg == false)
                {
                    if (moveCol.ColTrigger == true && !objE.stoptrg)
                    {
                        if (bossmove == false && GManager.instance.bossbattletrg == 0)
                        {
                            Run();
                        }
                        else if (bossmove == true)
                        {
                            Run();
                        }
                    }
                    else if (moveCol.ColTrigger == false)
                    {

                    }
                }
            }
            else if (GManager.instance.over == true || GManager.instance.walktrg == false || GManager.instance.Triggers[24] == 1)
            {
                if (stoptrg == false)
                {
                    stoptrg = true;
                    rb.velocity = Vector3.zero;
                    if (objE.Eanim.enabled == true)
                    {
                        objE.Eanim.enabled = false;
                    }
                }
            }
        }
    }

    void Run()
    {
            target = (P.transform.position - this.transform.position).normalized;
            if (stopCol.ColTrigger == false)
            {
                rb.velocity = (target * objE.Estatus.speed);
                if (stoptrg != false)
                {
                    stoptrg = false;
                }
            }
            else if (stopCol.ColTrigger == true && P.GetComponent<player>().stoptrg == false && GManager.instance.skillselect != 0)
            {
            objE.audioS.PlayOneShot(shotse);
                if (objE.Eanim.enabled == false)
                {
                objE.Eanim.enabled = true;
                }
                if (stoptrg == false)
                {
                    stoptrg = true;
                rb.velocity = Vector3.zero;
                }
            P.GetComponent<player>().stoptrg = true;
            GManager.instance.Triggers[24] = 1;
            changetime = 1;
            typetrg = 1;
            }
    }
    void parentP()
    {
        if (P.GetComponent<player>().stoptrg == true)
        {
            P.transform.parent = ParentPos.transform;
            P.GetComponent<Rigidbody>().useGravity = false;
            changetime = 2;
            typetrg = 2;
        }
    }
    void LoadAttack()
    {
        if (P.GetComponent<player>().stoptrg == true)
        {
            Instantiate(Fadein, transform.position, transform.rotation);
            GManager.instance.setmenu = 0;
            GManager.instance.posX = 256;
            GManager.instance.posY = 0.5f;
            GManager.instance.posZ = 0;
            GManager.instance.stageNumber = 5;
            PlayerPrefs.SetFloat("posX", GManager.instance.posX);
            PlayerPrefs.SetFloat("posY", GManager.instance.posY);
            PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
            PlayerPrefs.SetInt("stageNumber", GManager.instance.stageNumber);
            changetime = 2;
            typetrg = 3;
        }
    }
    void SceneChange()
    {
        if (P.GetComponent<player>().stoptrg == true)
        {
            GManager.instance.walktrg = true;
            GManager.instance.Triggers[24] = 0;
            SceneManager.LoadScene("stage" + GManager.instance.stageNumber);
        }
    }
}
