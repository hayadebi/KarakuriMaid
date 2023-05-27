using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickbutton : MonoBehaviour
{
    public string settingtype = "";
    public float addsettingfloat;
    public int addsettingint;
    public bool menutrg = false;
    public bool addstage = false;
    public bool stagetrg = false;
    public bool resettrg = false;
    public float maxUI = 0;
    public string nextscene;
    public GameObject settingUI;
    public GameObject fadeinUI;
    public AudioClip clickse;
    public AudioClip[] se;
    public bool[] trigger;
    public float[] number;
    AudioSource audioSource;
    GameObject r = null;
    Animator ar = null;
    // Start is called before the first frame update
    void Start()
    {
        r = GameObject.Find("Player");
        ar = r.GetComponent<Animator>();
        ar.SetInteger("Anumber", 0);
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.Length != 0 && trigger[0] == true && r != null)
        {
            r.transform.Translate(Vector3.forward * Time.deltaTime * number[0]);
        }
    }

    public void settingClick()
    {
        if (GManager.instance.setmenu < maxUI && GManager.instance.walktrg == true)
        {
            GManager.instance.setmenu += 1;
            GManager.instance.walktrg = false;
            audioSource.PlayOneShot(clickse);
            if (menutrg == false)
            {
                Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (menutrg == true)
            {
                Instantiate(GManager.instance.spawnUI, transform.position, transform.rotation);
            }
        }
        else if (GManager.instance.setmenu < maxUI && GManager.instance.setmenu > 0)
        {
            GManager.instance.setmenu += 1;
            GManager.instance.walktrg = false;
            audioSource.PlayOneShot(clickse);
            if (menutrg == false)
            {
                Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (menutrg == true)
            {
                Instantiate(GManager.instance.spawnUI, transform.position, transform.rotation);
            }
        }
        else if(GManager.instance.setmenu < maxUI && GManager.instance.walktrg == false)
        {
            GManager.instance.setmenu += 1;
            GManager.instance.walktrg = false;
            audioSource.PlayOneShot(clickse);
            if (menutrg == false)
            {
                Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (menutrg == true)
            {
                Instantiate(GManager.instance.spawnUI, transform.position, transform.rotation);
            }
        }
    }

    public void startClick()
    {
        if (GManager.instance.bossbattletrg == 0 && SceneManager.GetActiveScene().name != "stage1")
        {
            audioSource.PlayOneShot(clickse);
            Instantiate(fadeinUI, transform.position, transform.rotation);
            Resources.UnloadUnusedAssets();
            Invoke("GameStart", 3.0f);
        }
        else if (GManager.instance.over == true)
        {
            audioSource.PlayOneShot(clickse);
            Instantiate(fadeinUI, transform.position, transform.rotation);
            Resources.UnloadUnusedAssets();
            Invoke("GameStart", 3.0f);
        }
    }

    public void quitClick()
    {
        Application.Quit();
    }
    public void StoryClick()
    {
        if (resettrg == true)
        {
            resetN();
        }
        GameObject u1 = GameObject.Find("titleback");
        GameObject u2 = GameObject.Find("buttons");
        Animator a1 = u1.GetComponent<Animator>();
        Animator a2 = u2.GetComponent<Animator>();
        a1.Play("titleR");
        a2.Play("buttonR");
        if (r != null)
        {
            ar = r.GetComponent<Animator>();
            ar.SetInteger("Anumber", 1);
            AudioSource sr = r.GetComponent<AudioSource>();
            sr.PlayOneShot(clickse);
        }
        Invoke("storyStart", 1);
    }
    void storyStart()
    {
        if (r != null)
        {
            Vector3 vec = r.transform.forward * 12;
            vec.y += 12;
            r.GetComponent<Rigidbody>().velocity = vec;
            ar = r.GetComponent<Animator>();
            ar.SetInteger("Anumber", 3);
        }
        Invoke("Sevent1", 0.59f);
    }
    void Sevent1()
    {
        if (r != null)
        {
            AudioSource sr = r.GetComponent<AudioSource>();
            sr.PlayOneShot(se[0]);
        }
        Invoke("Sevent2", 0.3f);
    }
    void Sevent2()
    {
        if (r != null)
        {
            ar = r.GetComponent<Animator>();
            ar.SetInteger("Anumber", 2);
            AudioSource sr = r.GetComponent<AudioSource>();
            sr.PlayOneShot(se[1]);
        }
        trigger[0] = true;
        Invoke("Sevent3", 2f);
    }

    void Sevent3()
    {
        if (r != null)
        {
            GameObject b = GameObject.Find("BGM");
            AudioSource sb = b.GetComponent<AudioSource>();
            AudioSource sr = r.GetComponent<AudioSource>();
            sr.Stop();
            sb.Stop();
        }
        Instantiate(fadeinUI, transform.position, transform.rotation);
        Invoke("Sevent4", 2.3f);
    }
    void Sevent4()
    {
            SceneManager.LoadScene(nextscene);
    }
    public void DestroyClick()
    {
        GManager.instance.walktrg = true;
        Destroy(gameObject);
    }
    
    void GameStart()
    {
        if (menutrg == false && GManager.instance.bossbattletrg == 0)
        {
            if (resettrg == true)
            {
                resetN();
            }
            else if (resettrg == false)
            {
                if(GManager.instance.over == true)
                {
                    if(GManager.instance.skillselect != -1)
                    {
                        GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = 0;
                        GManager.instance.skillselect = -1;
                    }
                }
                GManager.instance.walktrg = true;
                GManager.instance.password = "";
                GManager.instance.over = false;
                GManager.instance.setmenu = 0;
                GManager.instance.itemhand = PlayerPrefs.GetInt("itemhand" , -1);
                if (GManager.instance.itemhand != -1)
                {
                    GManager.instance.WeaponID[GManager.instance.itemhand].upAttack = PlayerPrefs.GetInt("Wat" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
                    GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed = PlayerPrefs.GetFloat("Wbs" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed);
                    GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber);
                    GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber = PlayerPrefs.GetInt("Wbn2" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber);
                    GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime = PlayerPrefs.GetFloat("Wst" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime);
                    GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime);
                }
                GManager.instance.txtget = "";
                for (int i = 0; i < (GManager.instance.SkillID.Length);)
                {
                    GManager.instance.SkillID[i].skillget = PlayerPrefs.GetInt("skillget" + i, 0);
                    i += 1;
                }
                for (int i = 0; i < (GManager.instance.EventNumber.Length);)
                {
                    GManager.instance.EventNumber[i] = PlayerPrefs.GetInt("EventNumber" + i, 0);
                    i += 1;
                }
                GManager.instance.pushtrg = false;
                GManager.instance.audioMax = PlayerPrefs.GetFloat("audioMax",GManager.instance.audioMax);
                GManager.instance.kando = PlayerPrefs.GetFloat("kando", GManager.instance.kando);
                GManager.instance.siya = PlayerPrefs.GetFloat("siya", GManager.instance.siya);
                GManager.instance.mode = PlayerPrefs.GetInt("mode", GManager.instance.mode);
                GManager.instance.clearTime = PlayerPrefs.GetFloat("clearTime",0);
                GManager.instance.clearTime2 = PlayerPrefs.GetFloat("clearTime2",0);
                GManager.instance.posX = PlayerPrefs.GetFloat("posX",0);
                GManager.instance.posY = PlayerPrefs.GetFloat("posY",0);
                GManager.instance.posZ = PlayerPrefs.GetFloat("posZ",0);
                GManager.instance.setrg = -1;
                GManager.instance.stageNumber = PlayerPrefs.GetInt("stageNumber",1);
                GManager.instance.itemselect = PlayerPrefs.GetInt("itemselect",GManager.instance.itemselect);
                GManager.instance.Pstatus.maxHP = PlayerPrefs.GetInt("maxHP",8);
                GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
                GManager.instance.Pstatus.attack = PlayerPrefs.GetInt("attack", 0);
                GManager.instance.Pstatus.defense = PlayerPrefs.GetInt("defense", 0);
                GManager.instance.Pstatus.speed = PlayerPrefs.GetFloat("speed", 13f);
                GManager.instance.Coin = PlayerPrefs.GetInt("Coin", 0);
                GManager.instance.maxO8 = PlayerPrefs.GetFloat("maxO8", 100);
                GManager.instance.Pstatus.Lv = PlayerPrefs.GetInt("Lv", 1);
                GManager.instance.Pstatus.maxExp = PlayerPrefs.GetInt("maxExp", 20);
                GManager.instance.Pstatus.inputExp = PlayerPrefs.GetInt("inputExp", 0);
                GManager.instance.O8 = GManager.instance.maxO8;

                for (int i = 0; i < (GManager.instance.ItemID.Length);)
                {
                    GManager.instance.ItemID[i].itemnumber = PlayerPrefs.GetInt("itemnumber" + i, 0);
                    GManager.instance.ItemID[i].gettrg = PlayerPrefs.GetInt("itemget" + i, 0);
                    i += 1;
                }
                for (int i = 0; i < (GManager.instance.WeaponID.Length);)
                {
                    GManager.instance.WeaponID[i].getTrigger = PlayerPrefs.GetInt("weapongettrg" + i, 0);
                    GManager.instance.WeaponID[i].strengthen = PlayerPrefs.GetInt("weaponstreng" + i, 0);
                    i += 1;
                }
                for (int i = 0; i < (GManager.instance.Triggers.Length);)
                {
                    GManager.instance.Triggers[i] = PlayerPrefs.GetInt("Triggers" + i, 0);
                    i += 1;
                }
                GManager.instance.reduction = PlayerPrefs.GetInt("reduction", 0);
                GManager.instance.bossbattletrg = 0;
            }
        }
        if(menutrg == true)
        {
            SceneManager.LoadScene(GManager.instance.SceneText);
        }
        else if (stagetrg == false)
        {
            SceneManager.LoadScene(nextscene);
        }
        else if (stagetrg == true)
        {
            SceneManager.LoadScene(nextscene + GManager.instance.stageNumber);
        }
    }
    public void Slider()
    {
        if(settingtype == "audio")
        {
            GManager.instance.audioMax += addsettingfloat;
            if(GManager.instance.audioMax > 1)
            {
                GManager.instance.audioMax = 1;
            }
            else if (GManager.instance.audioMax < 0)
            {
                GManager.instance.audioMax = 0;
            }
        }
        else if (settingtype == "mode")
        {
            GManager.instance.mode += addsettingint;
            if (GManager.instance.mode> 2)
            {
                GManager.instance.mode = 2;
            }
            else if (GManager.instance.mode < 0)
            {
                GManager.instance.mode = 0;
            }
        }
        else if (settingtype == "siya")
        {
            GManager.instance.siya += addsettingint;
            if (GManager.instance.siya > 90)
            {
                GManager.instance.siya = 90;
            }
            else if (GManager.instance.siya < 15)
            {
                GManager.instance.siya = 15;
            }
        }
        else if (settingtype == "kando")
        {
            GManager.instance.kando += addsettingint;
            if (GManager.instance.kando > 5)
            {
                GManager.instance.kando = 5;
            }
            else if (GManager.instance.kando < 1)
            {
                GManager.instance.kando = 1;
            }
        }
        else if(settingtype == "reduction")
        {
            if(GManager.instance.reduction == 0)
            {
                GManager.instance.reduction = 1;
            }
            else if(GManager.instance.reduction == 1)
            {
                GManager.instance.reduction = 0;
            }
        }
    }

    public void JapaneseL()
    {
        GManager.instance.isEnglish = 0;
    }
    public void EnglishL()
    {
        GManager.instance.isEnglish = 1;
    }
    public void resetN()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < (GManager.instance.SkillID.Length);)
        {
            GManager.instance.SkillID[i].skillget = 0;
            i += 1;
        }
        GManager.instance.walktrg = true;
        GManager.instance.password = "";
        GManager.instance.over = false;
        GManager.instance.setmenu = 0;
        GManager.instance.itemhand = -1;
        GManager.instance.txtget = "";
        for (int i = 0; i < (GManager.instance.EventNumber.Length);)
        {
            GManager.instance.EventNumber[i] = 0;
            i += 1;
        }
        GManager.instance.pushtrg = false;
        GManager.instance.audioMax = 0.16f;
        GManager.instance.kando = 2;
        GManager.instance.siya = 60;
        GManager.instance.mode = 1;
        GManager.instance.clearTime = 0f;
        GManager.instance.clearTime2 = 0f;
        GManager.instance.posX = 0f;
        GManager.instance.posY = 0f;
        GManager.instance.posZ = 0;
        GManager.instance.setrg = -1;
        GManager.instance.stageNumber = 1;
        GManager.instance.itemselect = -1;
        GManager.instance.Pstatus.maxHP = 8;
        GManager.instance.Pstatus.health = 8;
        GManager.instance.Pstatus.attack = 0;
        GManager.instance.Pstatus.defense = 0;
        GManager.instance.Pstatus.speed = 13f;
        GManager.instance.Coin = 0;
        GManager.instance.maxO8 = 100;
        GManager.instance.Pstatus.Lv = 1;
        GManager.instance.Pstatus.maxExp = 20;
        GManager.instance.Pstatus.inputExp = 0;
        GManager.instance.O8 = GManager.instance.maxO8;

        for (int i = 0; i < (GManager.instance.ItemID.Length);)
        {
            GManager.instance.ItemID[i].itemnumber = 0;
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.WeaponID.Length);)
        {
            GManager.instance.WeaponID[i].getTrigger = 0;
            GManager.instance.WeaponID[i].strengthen = 0;

            i += 1;
        }
        for (int i = 0; i < (GManager.instance.Triggers.Length);)
        {
            GManager.instance.Triggers[i] = 0;
            i += 1;
        }
        GManager.instance.bossbattletrg = 0;
    }
}
