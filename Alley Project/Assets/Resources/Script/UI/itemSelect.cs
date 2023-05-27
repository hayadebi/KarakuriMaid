using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class itemSelect : MonoBehaviour
{
    
    public GameObject Fadein;
    public GameObject selectmenuUI;
    //public GameObject selectmenuUI;
    public AudioSource audioS;
    public AudioClip selectse;
    public AudioClip onse;
    public AudioClip notse;
    public RenderTexture nullimage;
    public Text numberText;
    public Text nameText;
    public Text scriptText;
    public RawImage gunimage;
    int selectnumber = 0;
    public int[] onItem;
    int boxnumber = 0;
    int inputnumber = 0;
    bool usetrg = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; GManager.instance.ItemID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.ItemID[i].itemnumber > 0;)
            {
                boxnumber += 1;
                l++;
            }
            i += 1;
        }
        onItem = new int[boxnumber];
        for (int i = 0; GManager.instance.ItemID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.ItemID[i].itemnumber > 0;)
            {
                onItem[inputnumber] = i;
                inputnumber += 1;
                l++;
            }
            i += 1;
        }
        if (onItem.Length == 0)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            if (GManager.instance.isEnglish == 0)
            {
                numberText.text = "??個所持";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                numberText.text = "Got ??";
            }
            scriptText.text = "????????";
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber < 1)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            if (GManager.instance.isEnglish == 0)
            {
                numberText.text = "??個所持";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                numberText.text = "Got ??";
            }
            scriptText.text = "????????";
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber > 0)
        {
            gunimage.texture = GManager.instance.ItemID[onItem[selectnumber]].itemimage;
            
            if (GManager.instance.isEnglish == 0)
            {
                numberText.text = GManager.instance.ItemID[onItem[selectnumber]].itemnumber + "個所持";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                numberText.text = "Got " + GManager.instance.ItemID[onItem[selectnumber]].itemnumber;
            }
            if (GManager.instance.isEnglish == 0)
            {
                nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname;
                scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname2;
                scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectMinus()
    {
        if (onItem.Length  == 0)
        {
            audioS.PlayOneShot(notse);
        }
        else if (selectnumber > 0)
        {
            audioS.PlayOneShot(selectse);
            selectnumber -= 1;
            //----
            if (onItem.Length == 0)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                if (GManager.instance.isEnglish == 0)
                {
                    numberText.text = "??個所持";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    numberText.text = "Got ??";
                }
                scriptText.text = "????????";
            }
            else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber < 1)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                if (GManager.instance.isEnglish == 0)
                {
                    numberText.text = "??個所持";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    numberText.text = "Got ??";
                }
                scriptText.text = "????????";
            }
            else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber > 0)
            {
                gunimage.texture = GManager.instance.ItemID[onItem[selectnumber]].itemimage;
                
                if (GManager.instance.isEnglish == 0)
                {
                    numberText.text = GManager.instance.ItemID[onItem[selectnumber]].itemnumber + "個所持";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    numberText.text = "Got " + GManager.instance.ItemID[onItem[selectnumber]].itemnumber;
                }
                if (GManager.instance.isEnglish == 0)
                {
                    nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname;
                    scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript;
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname2;
                    scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript2;
                }
            }
            //----
        }
        else
        {
            audioS.PlayOneShot(notse);
        }
    }
    public void SelectPlus()
    {
        if (onItem.Length == 0)
        {
            audioS.PlayOneShot(notse);
        }
        else if (selectnumber < (onItem.Length - 1))
        {
            audioS.PlayOneShot(selectse);
            selectnumber += 1;
            //----
            if (onItem.Length == 0)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                if (GManager.instance.isEnglish == 0)
                {
                    numberText.text = "??個所持";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    numberText.text = "Got ??";
                }
                scriptText.text = "????????";
            }
            else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber < 1)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                if (GManager.instance.isEnglish == 0)
                {
                    numberText.text = "??個所持";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    numberText.text = "Got ??";
                }
                scriptText.text = "????????";
            }
            else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber > 0)
            {
                gunimage.texture = GManager.instance.ItemID[onItem[selectnumber]].itemimage;
                
                if (GManager.instance.isEnglish == 0)
                {
                    numberText.text = GManager.instance.ItemID[onItem[selectnumber]].itemnumber + "個所持";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    numberText.text = "Got " + GManager.instance.ItemID[onItem[selectnumber]].itemnumber;
                }
                if (GManager.instance.isEnglish == 0)
                {
                    nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname;
                    scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript;
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname2;
                    scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript2;
                }
            }
            //----
        }
        else
        {
            audioS.PlayOneShot(notse);
        }
    }
    //----
    public void MenuUI()
    {
        if (onItem != null && onItem.Length != 0)
        {
            if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber != 0 && GManager.instance.ItemID[onItem[selectnumber]].eventnumber != -1)
            {
                audioS.PlayOneShot(onse);
                usetrg = false;
                selectmenuUI.SetActive(true);
            }
            else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 0 || GManager.instance.ItemID[onItem[selectnumber]].eventnumber == -1)
            {
                audioS.PlayOneShot(notse);
            }
        }
    }
    void SceneChange()
    {
        GManager.instance.walktrg = true;
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("stage2");
    }
    void SceneChange2()
    {
        GManager.instance.walktrg = true;
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("stage8");
    }
    public void ItemSet()
    {
        
        if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 1)//HP回復
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.setrg = 11;
            GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 2)//O8回復
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.setrg = 11;
            GManager.instance.O8 = GManager.instance.maxO8;
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 3 && GManager.instance.bossbattletrg == 0)//帰還1
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.houseTrg = false;
            Instantiate(Fadein, transform.position, transform.rotation);
            GameObject P = GameObject.Find("Player");
            GManager.instance.setmenu = 0;
            GManager.instance.posX = 0;
            PlayerPrefs.SetFloat("posX", GManager.instance.posX);
            GManager.instance.posY = 0;
            PlayerPrefs.SetFloat("posY", GManager.instance.posY);
            GManager.instance.posZ = 0;
            PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
            GManager.instance.stageNumber = 2;
            PlayerPrefs.SetInt("stageNumber", 2);
            PlayerPrefs.Save();
            
            Invoke("SceneChange", 3);
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 4 && GManager.instance.bossbattletrg == 0 && SceneManager.GetActiveScene().name == "stage"+GManager.instance.stageNumber && GManager.instance.itemhand != -1)//セーブアイテム
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            SaveDate();
            GManager.instance.setrg = 12;
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 5 && GManager.instance.WeaponID[GManager.instance.itemhand].strengthen < 5 && GManager.instance.bossbattletrg == 0)//武器強化
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.WeaponID[GManager.instance.itemhand].upAttack += 1;
            GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed += 0.1f;
            GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber += 1;
            GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime -= 0.05f;
            if (GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime < 0.1f)
            {
                GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime = 0.1f;
            }
            GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime -= 0.1f;
            if (GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime < 0.3f)
            {
                GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime = 0.3f;
            }
            GManager.instance.WeaponID[GManager.instance.itemhand].strengthen += 1;
            PlayerPrefs.SetInt("weaponstreng" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].strengthen);
            PlayerPrefs.SetInt("Wat" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
            PlayerPrefs.SetFloat("Wbs" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed);
            PlayerPrefs.SetInt("Wbn" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber);
            PlayerPrefs.SetInt("Wbn2" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber);
            PlayerPrefs.SetFloat("Wst" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime);
            PlayerPrefs.SetFloat("Wrt" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime);
            PlayerPrefs.Save();
            GManager.instance.setrg = 13;
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 6 && GManager.instance.bossbattletrg == 0)//帰還2
        {
            usetrg = true;
            Instantiate(Fadein, transform.position, transform.rotation);
            GameObject P = GameObject.Find("Player");
            GManager.instance.setmenu = 0;
            GManager.instance.posX = 0;
            PlayerPrefs.SetFloat("posX", GManager.instance.posX);
            GManager.instance.posY = 0;
            PlayerPrefs.SetFloat("posY", GManager.instance.posY);
            GManager.instance.stageNumber = 8;
            PlayerPrefs.SetInt("stageNumber", 8);
            PlayerPrefs.Save();
            Invoke("SceneChange2", 3);
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 7 && GManager.instance.skillselect == -1 )//自動回復
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.skillnumber = 1;
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 8 && GManager.instance.skillselect == -1)//自動攻撃
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.skillnumber = 2;
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 9 && GManager.instance.skillselect == -1)//コインボーナス
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.skillnumber = 5;
        }
        if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 10)//HP回復
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.setrg = 26;
            GManager.instance.Pstatus.health += (GManager.instance.Pstatus.maxHP / 2);
            if (GManager.instance.Pstatus.maxHP < GManager.instance.Pstatus.health)
            {
                GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
            }
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 11)//O8回復
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.setrg = 25;
            GManager.instance.O8 += (GManager.instance.maxO8 / 2);
            if (GManager.instance.maxO8 < GManager.instance.O8)
            {
                GManager.instance.O8 = GManager.instance.maxO8;
            }
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].eventnumber == 12)//ユニムーシュ
        {
            usetrg = true;
            GManager.instance.ItemID[onItem[selectnumber]].itemnumber -= 1;
            GManager.instance.Pstatus.inputExp = GManager.instance.Pstatus.maxExp;
            GManager.instance.setrg = 26;
        }
        else if (usetrg == false)
        {
            GManager.instance.setrg = 34;
        }
        //----------------------------------------------
        if (onItem.Length == 0)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            if (GManager.instance.isEnglish == 0)
            {
                numberText.text = "??個所持";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                numberText.text = "Got ??";
            }
            scriptText.text = "????????";
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber < 1)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            if (GManager.instance.isEnglish == 0)
            {
                numberText.text = "??個所持";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                numberText.text = "Got ??";
            }
            scriptText.text = "????????";
        }
        else if (GManager.instance.ItemID[onItem[selectnumber]].itemnumber > 0)
        {
            gunimage.texture = GManager.instance.ItemID[onItem[selectnumber]].itemimage;
            
            if (GManager.instance.isEnglish == 0)
            {
                numberText.text = GManager.instance.ItemID[onItem[selectnumber]].itemnumber + "個所持";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                numberText.text = "Got " + GManager.instance.ItemID[onItem[selectnumber]].itemnumber;
            }
            if (GManager.instance.isEnglish == 0)
            {
                nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname;
                scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                nameText.text = GManager.instance.ItemID[onItem[selectnumber]].itemname2;
                scriptText.text = GManager.instance.ItemID[onItem[selectnumber]].itemscript2;
            }
        }
        selectmenuUI.SetActive(false);
    }
    
    public void NotSet()
    {
        audioS.PlayOneShot(notse);
        selectmenuUI.SetActive(false);
    }

    void SaveDate()
    {
        for (int i = 0; i < (GManager.instance.SkillID.Length);)
        {
            PlayerPrefs.SetInt("skillget" + i, GManager.instance.SkillID[i].skillget);
            i += 1;
        }
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
        for (int i = 0; i < (GManager.instance.ItemID.Length);)
        {
            PlayerPrefs.SetInt("itemnumber" + i, GManager.instance.ItemID[i].itemnumber);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.WeaponID.Length);)
        {
            PlayerPrefs.SetInt("weapongettrg" + i, GManager.instance.WeaponID[i].getTrigger);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.Triggers.Length);)
        {
            PlayerPrefs.SetInt("Triggers" + i, GManager.instance.Triggers[i]);
            i += 1;
        }

        PlayerPrefs.Save();
    }
}