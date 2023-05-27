using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[RequireComponent(typeof(Flowchart))]
public class npcsay : MonoBehaviour
{
    public bool audiostop = false;
    public GameObject bossoffobj = null;
    public int eventnumber = 0;
    public int defaulteventadd = 1;
    public int[] addevent;
    public int[] addEnumber;
    public GameObject wall;
    public GameObject bossUI;
    public AudioClip bgm;
    public int GetCoin;
    public int Trigger = -1;
    public int itemID;
    public int inputitemnumber;
    public float returnTime = 3;
    public GameObject[] UI;
    public bool nulleventdestroy = false;
    public int nullversion = 1;//1はe!=i、2はe<i、3はe>i
    public int[] shopID;
    public int[] shopNumber;
    public int[] shopID2;
    public int[] shopNumber2;
    //-----------
    public int EventNumber = -1;
    public string DestroyOBJtext = "";
    public bool nextevent = true;
    public bool sayreturn;
    public int inputeventNumber;
    public int npctype;
    public string PlayerTag = "Player";
    bool saytrg = false;
    public string message = "test";
    bool isTalking = false;
    Flowchart flowChart;
    // Start is called before the first frame update
    void Start()
    {
        if (Trigger != -1)
        {
            if(GManager.instance.Triggers[Trigger] == 1)
            {
                Destroy(gameObject);
            }
        }
        flowChart = this.GetComponent<Flowchart>();
        if (EventNumber == 2)
        {
            for (int i = 0; i < (shopNumber.Length);)
            {
                GManager.instance.shopNumber[i] = PlayerPrefs.GetInt("shopNumber" + i, shopNumber[i]);
                GManager.instance.shopID[i] = PlayerPrefs.GetInt("shopID" + i, 0);
                i += 1;
            }
            for (int i = 0; i < (shopNumber2.Length);)
            {
                GManager.instance.shopNumber2[i] = PlayerPrefs.GetInt("shopNumber2" + i, shopNumber2[i]);
                GManager.instance.shopID2[i] = PlayerPrefs.GetInt("shopID2" + i, 0);
                i += 1;
            }
        }
        if(nulleventdestroy == true && inputeventNumber != GManager.instance.EventNumber[eventnumber] && nullversion == 1)
        {
            Destroy(gameObject);
        }
        else if (nulleventdestroy == true && inputeventNumber > GManager.instance.EventNumber[eventnumber] && nullversion == 2)
        {
            Destroy(gameObject);
        }
        else if (nulleventdestroy == true && inputeventNumber < GManager.instance.EventNumber[eventnumber] && nullversion == 3)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (saytrg == false && npctype == 2 && GManager.instance.walktrg == true)
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
        else if (saytrg == false && npctype == 3 && inputeventNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg == true)
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
        else if (saytrg == false && npctype == 5 && GManager.instance.walktrg == true && GManager.instance.Sgetsay == true)
        {
            GManager.instance.Sgetsay = false;
            message = GManager.instance.skillsay;
            saytrg = true;
            StartCoroutine(Talk());
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag  == PlayerTag && saytrg == false && npctype == 0 && GManager.instance.walktrg == true)
        {  
                saytrg = true;
                StartCoroutine(Talk());
        }
        else if (col.tag == PlayerTag && saytrg == false && npctype == 1 && inputeventNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg == true)
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
        else if(col.tag == PlayerTag && saytrg == false && npctype == 4 && GManager.instance.walktrg == true)
        {
            if(GManager.instance.ItemID[itemID ].itemnumber > (inputitemnumber - 1) )
            {
                sayreturn = false;
                GManager.instance.ItemID[itemID].itemnumber -= inputitemnumber;
                GManager.instance.Triggers[Trigger] = 1;
                message = "itemget";
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if(GManager.instance.ItemID[itemID].itemnumber < inputitemnumber )
            {
                message = "itemnot";
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
    }

    public IEnumerator Talk()
    {
        GManager.instance.walktrg = false;
        GameObject P = GameObject.Find("Player");
        Rigidbody rb = P.GetComponent<Rigidbody>();
        rb.useGravity = true;
        if (EventNumber == 1 && GManager.instance.itemhand != -1)
        {
            SaveDate();
            GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
            GManager.instance.O8 = GManager.instance.maxO8;
        }
        else if(EventNumber == 3)
        {
            GameObject C = GameObject.Find("MainC");
            C.GetComponent<CameraController>().enablecamera = false;
            C.transform.position = P.transform.position + (P.transform.right * 2 + P.transform.up * 4.5f + P.transform.forward * -8);
            wall.SetActive(true);
            GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
        }
        
        if(bgm != null)
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
        }
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        GManager.instance.walktrg = true;
        rb.useGravity = false;
        if (nextevent == true)
        {
            GManager.instance.EventNumber[eventnumber] += defaulteventadd;
            if(addevent.Length != 0)
            {
                for (int i = 0; i < addevent.Length;)
                {
                    if(addEnumber.Length != 0)
                    {
                        GManager.instance.EventNumber[addevent[i]] = addEnumber[i];
                    }
                    else
                    {
                        GManager.instance.EventNumber[addevent[i]] += defaulteventadd;
                    }
                    i++;
                }
            }
        }
        if (shopID.Length != 0 && shopID2.Length != 0)
        {
            for (int i = 0; i < 2;)
            {
                PlayerPrefs.DeleteKey("shopNumber" + i);
                PlayerPrefs.DeleteKey("shopNumber2" + i);
                i += 1;
            }
        }
        if (shopID.Length != 0)
        {
            for (int i = 0; i < (shopID.Length);)
            {
                GManager.instance.shopID[i] = shopID[i];
                PlayerPrefs.SetInt("shopID" + i, GManager.instance.shopID[i]);
                GManager.instance.shopNumber[i] = shopNumber[i];
                PlayerPrefs.SetInt("shopNumber" + i, GManager.instance.shopNumber[i]);
                PlayerPrefs.Save();
                i += 1;
            }
        }
        if (shopID2.Length != 0)
        {
            for (int i = 0; i < (shopID2.Length);)
            {
                GManager.instance.shopID2[i] = shopID2[i];
                PlayerPrefs.SetInt("shopID2" + i, GManager.instance.shopID2[i]);
                GManager.instance.shopNumber2[i] = shopNumber2[i];
                PlayerPrefs.SetInt("shopNumber2" + i, GManager.instance.shopNumber2[i]);
                PlayerPrefs.Save();
                i += 1;
            }
        }
        if (EventNumber == 2)
        {
            for (int i = 0; i < (shopNumber.Length );)
            {
                PlayerPrefs.SetInt("shopNumber" + i, GManager.instance.shopNumber[i]);
                PlayerPrefs.SetInt("shopNumber2" + i, GManager.instance.shopNumber2[i]);
                PlayerPrefs.Save();
                i += 1;
            }
            GManager.instance.walktrg = false;
            Instantiate(UI[0], transform.position, transform.rotation);
        }
        
        if (sayreturn == true)
        {
            Invoke("SayTrg", returnTime);
        }
        if(EventNumber == 6)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Application.OpenURL("https://twitter.com/hayadebi");
            Application.Quit();
        }
        if (GetCoin != 0)
        {
            GManager.instance.setrg = 1;
            GManager.instance.Coin += GetCoin;
        }
        if(EventNumber == 7)
        {
            GManager.instance.ItemID[itemID].itemnumber += 1;
            GManager.instance.ItemID[itemID].gettrg = 1;
        }
        if (bgm != null && bossUI == null && audiostop == false)
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
            bgmA.clip = bgm;
            bgmA.Play();
        }
        if (EventNumber == 3)
        {
            GameObject c = GameObject.Find("MainC");
            bossUI.SetActive(true);
            c.GetComponent<Camera>().enabled = false;
            if (bossoffobj != null)
            {
                bossoffobj.SetActive(false);
            }
            Invoke("Boss", 4f);
        }
        if (DestroyOBJtext != "" && EventNumber != 3)
        {
            GameObject obj = GameObject.Find(DestroyOBJtext);
            Destroy(obj.gameObject);
        }
    }
    void Boss()
    {
        GManager.instance.bossbattletrg = 1;
        
        if (bgm != null && audiostop == false)
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
            bgmA.clip = bgm;
            bgmA.Play();
        }
        Instantiate(UI[0], transform.position, transform.rotation);
        Instantiate(UI[1], transform.position, transform.rotation);
        Invoke("Fade", 3);
    }
    void Fade()
    {
        GameObject cmr = GameObject.Find("CMR");
        Vector3 cp = cmr.transform.localPosition;
        cp.x = 0.04f;
        cp.y = 0.08f;
        cp.z = -0.16f;
        cmr.transform.localPosition = cp;
        Vector3 cr = cmr.transform.localEulerAngles;
        cr.x = 16f;
        cr.y = 0;
        cr.z = 0;
        cmr.transform.localEulerAngles = cr;
        GameObject c = GameObject.Find("MainC");
        c.GetComponent<Camera>().enabled = true;
        c.GetComponent<CameraController>().enablecamera = true;
        bossUI.SetActive(false);
        
        Destroy(gameObject, 0.1f);
    }
    void SayTrg()
    {
        saytrg = false;
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
        GManager.instance.posX = P.transform.position.x;
        PlayerPrefs.SetFloat("posX", GManager.instance.posX);
        GManager.instance.posY = P.transform.position.y;
        PlayerPrefs.SetFloat("posY", GManager.instance.posY);
        GManager.instance.posZ = P.transform.position.z;
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
        for (int i = 0; i < (GManager.instance.ItemID.Length );)
        {
            PlayerPrefs.SetInt("itemnumber" + i, GManager.instance.ItemID[i].itemnumber);
            PlayerPrefs.SetInt("itemget" + i, GManager.instance.ItemID[i].gettrg);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.WeaponID.Length );)
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
        PlayerPrefs.SetInt("Wat" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
        PlayerPrefs.SetFloat("Wbs" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed);
        PlayerPrefs.SetInt("Wbn" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber);
        PlayerPrefs.SetFloat("Wst" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime);
        PlayerPrefs.SetFloat("Wrt" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime);
        for (int i = 0; i < (GManager.instance.SkillID.Length);)
        {
            PlayerPrefs.SetInt("skillget" + i, GManager.instance.SkillID[i].skillget);
            i += 1;
        }
        PlayerPrefs.SetInt("reduction", GManager.instance.reduction);
        PlayerPrefs.Save();
    }
}
