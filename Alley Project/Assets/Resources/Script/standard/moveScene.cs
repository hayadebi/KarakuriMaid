using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveScene : MonoBehaviour
{
    public bool entertrg = false;
    public SphereCollider areaspcol = null;
    public float colmax = 128;
    public AudioSource bgmas = null;
    public AudioClip setbgm;
    public AudioClip onese = null;
    public GameObject offobj = null;
    public GameObject onobj = null;
    public bool doortrg = true;
    public int eventnumber;
    public int inputEvent = -1;
    public int number = 1;
    bool changetrg = false;
    public bool addtrg = false;
    public bool removetrg = false;
    public float saveX;
    public float saveY;
    public float saveZ;
    public bool stagetrg = true;
    public string sceneName = "";
    public GameObject fadeinUI;
    private string PlayerTag = "Player";
    GameObject P = null;
    bool rottrg = false;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        if (offobj != null && GManager.instance.houseTrg == true)
        {
            offobj.SetActive(false);
            P.GetComponent<player>().o8removetrg = false;
        }

        if (areaspcol != null && GManager.instance.houseTrg == true)
        {
            areaspcol.radius = colmax;
        }
        if (bgmas != null && GManager.instance.houseTrg == true)
        {
            bgmas.Stop();
            bgmas.clip = null;
            bgmas.clip = setbgm;
            bgmas.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider col)
    {
        if(col.tag == PlayerTag && changetrg == false && Input.GetKeyDown(KeyCode.E) && entertrg == false)
        {
            if (inputEvent == -1)
            {
                changetrg = true;
                GManager.instance.walktrg = false;
                if (doortrg == true)
                {
                    if (rottrg == false)
                    {
                        rottrg = true;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", -90, "time", 1));
                    }
                    else if (rottrg == true)
                    {
                        rottrg = false;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", 90, "time", 1));
                    }
                }
                Instantiate(fadeinUI, transform.position, transform.rotation);
                Resources.UnloadUnusedAssets();
                Invoke("SceneChange", 2);
            }
            else if ((inputEvent - 1) < GManager.instance.EventNumber[eventnumber])
            {
                changetrg = true;
                GManager.instance.walktrg = false;
                if (doortrg == true)
                {
                    if (rottrg == false)
                    {
                        rottrg = true;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", -90, "time", 1));
                    }
                    else if (rottrg == true)
                    {
                        rottrg = false;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", 90, "time", 1));
                    }
                }
                Instantiate(fadeinUI, transform.position, transform.rotation);
                Resources.UnloadUnusedAssets();
                Invoke("SceneChange", 2);
            }
        }
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == PlayerTag && changetrg == false && entertrg == true)
        {
            if (inputEvent == -1)
            {
                changetrg = true;
                if (doortrg == true)
                {
                    if (rottrg == false)
                    {
                        rottrg = true;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", -90, "time", 1));
                    }
                    else if (rottrg == true)
                    {
                        rottrg = false;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", 90, "time", 1));
                    }
                }
                Instantiate(fadeinUI, transform.position, transform.rotation);
                SceneChange();
            }
            else if ((inputEvent - 1) < GManager.instance.EventNumber[eventnumber])
            {
                changetrg = true;
                if (doortrg == true)
                {
                    if (rottrg == false)
                    {
                        rottrg = true;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", -90, "time", 1));
                    }
                    else if (rottrg == true)
                    {
                        rottrg = false;
                        iTween.RotateAdd(gameObject, iTween.Hash("y", 90, "time", 1));
                    }
                }
                Instantiate(fadeinUI, transform.position, transform.rotation);
                SceneChange();
            }
        }
    }
    void SaveDate()
    {
        PlayerPrefs.SetFloat("siya", GManager.instance.siya);
        PlayerPrefs.SetInt("mode", GManager.instance.mode);
        PlayerPrefs.SetInt("itemhand", GManager.instance.itemhand);
        for (int i = 0; i < (GManager.instance.EventNumber.Length);)
        {
            PlayerPrefs.SetInt("EventNumber" + i, GManager.instance.EventNumber[i]);
            i += 1;
        }
        PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
        PlayerPrefs.SetInt("mode", GManager.instance.mode);
        PlayerPrefs.SetFloat("clearTime", GManager.instance.clearTime);
        PlayerPrefs.SetFloat("clearTime2", GManager.instance.clearTime2);
        GameObject P = GameObject.Find("Player");
        PlayerPrefs.SetFloat("posX", GManager.instance.posX);
        PlayerPrefs.SetFloat("posY", GManager.instance.posY);
        PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
        PlayerPrefs.SetInt("stageNumber", GManager.instance.stageNumber);
        PlayerPrefs.SetInt("itemselect", GManager.instance.itemselect);
        PlayerPrefs.SetInt("maxHP", GManager.instance.Pstatus.maxHP);
        PlayerPrefs.SetInt("health", GManager.instance.Pstatus.health);
        PlayerPrefs.SetInt("attack", GManager.instance.Pstatus.attack);
        PlayerPrefs.SetInt("defense", GManager.instance.Pstatus.defense);
        PlayerPrefs.SetFloat("speed", GManager.instance.Pstatus.speed);
        PlayerPrefs.SetInt("Coin", GManager.instance.Coin);
        PlayerPrefs.SetFloat("maxO8", GManager.instance.maxO8);
        PlayerPrefs.SetFloat("O8", GManager.instance.O8);
        PlayerPrefs.SetInt("Lv", GManager.instance.Pstatus.Lv);
        PlayerPrefs.SetInt("maxExp", GManager.instance.Pstatus.maxExp);
        PlayerPrefs.SetInt("inputExp", GManager.instance.Pstatus.inputExp);
        for (int i = 0; i < (GManager.instance.ItemID.Length);)
        {
            PlayerPrefs.SetInt("itemnumber" + i, GManager.instance.ItemID[i].itemnumber);
            PlayerPrefs.SetInt("itemget" + i, GManager.instance.ItemID[i].gettrg);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.WeaponID.Length);)
        {
            PlayerPrefs.SetInt("weapongettrg" + i, GManager.instance.WeaponID[i].getTrigger);
            PlayerPrefs.SetInt("Wbn2" + i, GManager.instance.WeaponID[i].bulletnumber);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.Triggers.Length);)
        {
            PlayerPrefs.SetInt("Triggers" + i, GManager.instance.Triggers[i]);
            i += 1;
        }
        if (GManager.instance.itemhand != -1)
        {
            PlayerPrefs.SetInt("Wat" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
            PlayerPrefs.SetFloat("Wbs" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed);
            PlayerPrefs.SetInt("Wbn" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber);
            PlayerPrefs.SetFloat("Wst" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime);
            PlayerPrefs.SetFloat("Wrt" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime);
        }
        for (int i = 0; i < (GManager.instance.SkillID.Length);)
        {
            PlayerPrefs.SetInt("skillget" + i, GManager.instance.SkillID[i].skillget);
            i += 1;
        }
        PlayerPrefs.SetInt("reduction", GManager.instance.reduction);
        PlayerPrefs.Save();
    }
    void SceneChange()
    {
        if (P != null)
        {
            SaveDate();
            GManager.instance.posX = saveX;
            PlayerPrefs.SetFloat("posX", GManager.instance.posX);
            GManager.instance.posY = saveY;
            PlayerPrefs.SetFloat("posY", GManager.instance.posY);
            GManager.instance.posZ = saveZ;
            PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
            PlayerPrefs.Save();
            GManager.instance.walktrg = true;
            if (offobj != null)
            {
                GManager.instance.houseTrg = true;
                offobj.SetActive(false);
                P.GetComponent<player>().o8removetrg = false;
            }
            else if (offobj == null)
            {
                GManager.instance.houseTrg = false;
                P.GetComponent<player>().o8removetrg = true;
            }
            if (onobj != null)
            {
                onobj.SetActive(true);
            }
            if (areaspcol != null)
            {
                if (GManager.instance.houseTrg == false)
                {
                    GManager.instance.houseTrg = true;
                }
                areaspcol.radius = colmax;
            }
            else if (areaspcol == null)
            {
                if (GManager.instance.houseTrg == true)
                {
                    GManager.instance.houseTrg = false;
                }
            }

            if (bgmas != null)
            {
                if (GManager.instance.houseTrg == false)
                {
                    GManager.instance.houseTrg = true;
                }
                bgmas.Stop();
                bgmas.clip = null;
                bgmas.clip = setbgm;
                bgmas.Play();

            }
            else if (bgmas == null)
            {
                if (GManager.instance.houseTrg == true)
                {
                    GManager.instance.houseTrg = false;
                }
            }
            if (onese != null && this.GetComponent<AudioSource>())
            {
                this.GetComponent<AudioSource>().PlayOneShot(onese);
            }
            if (sceneName == "")
            {
                if (addtrg == true)
                {
                    addtrg = false;
                    GManager.instance.stageNumber += number;
                }
                if (removetrg == true)
                {
                    removetrg = false;
                    GManager.instance.stageNumber -= number;
                }
                PlayerPrefs.SetInt("stageNumber", GManager.instance.stageNumber);
                PlayerPrefs.Save();
                changetrg = false;
                var tppos = P.transform.position;
                tppos.x = saveX;
                tppos.y = saveY;
                tppos.z = saveZ;
                P.transform.position = tppos;
                P.GetComponent<player>().jumpmode = 0;
                P.GetComponent<player>().jumptrg = false;
                P.GetComponent<player>().ySpeed = 0;
                P.GetComponent<player>().spacetime = 0;
                P.GetComponent<player>().nogroundtime = 0;
            }
            else if (stagetrg == false)
            {
                if (addtrg == true)
                {
                    addtrg = false;
                    GManager.instance.stageNumber += number;
                }
                if (removetrg == true)
                {
                    removetrg = false;
                    GManager.instance.stageNumber -= number;
                }
                PlayerPrefs.SetInt("stageNumber", GManager.instance.stageNumber);
                PlayerPrefs.Save();
                P.GetComponent<player>().jumpmode = 0;
                P.GetComponent<player>().jumptrg = false;
                P.GetComponent<player>().ySpeed = 0;
                P.GetComponent<player>().spacetime = 0;
                P.GetComponent<player>().nogroundtime = 0;
                SceneManager.LoadScene(sceneName);
            }
            else if (stagetrg == true)
            {
                if (addtrg == true)
                {
                    addtrg = false;
                    GManager.instance.stageNumber += number;
                }
                if (removetrg == true)
                {
                    removetrg = false;
                    GManager.instance.stageNumber -= number;
                }
                P.GetComponent<player>().jumpmode = 0;
                P.GetComponent<player>().jumptrg = false;
                P.GetComponent<player>().ySpeed = 0;
                P.GetComponent<player>().spacetime = 0;
                P.GetComponent<player>().nogroundtime = 0;
                PlayerPrefs.SetInt("stageNumber", GManager.instance.stageNumber);
                PlayerPrefs.Save();
                SceneManager.LoadScene(sceneName + GManager.instance.stageNumber);
            }
        }
    }
}
