using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItext : MonoBehaviour
{
    public string bossname;
    public string InputText = "";
    private Text scoreText = null;

    private int oldInt = 0;
    private float oldFloat = 0;
    private string oldString = "";
    private int oldEnglish = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (InputText == "textget")
        {
            scoreText.text = GManager.instance.txtget;
            oldString = GManager.instance.txtget;
        }
        else if (InputText == "stage")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "ステージ" + GManager.instance.stageNumber;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Stage" + GManager.instance.stageNumber;
            }
            oldInt = GManager.instance.stageNumber;
        }
        else if (InputText == "time")
        {
            scoreText.text = "Clear time／" + GManager.instance.clearTime2 + "：" + GManager.instance.clearTime;
            oldFloat = GManager.instance.clearTime2;
        }
        else if (InputText == "coin")
        {
            scoreText.text = GManager.instance.Coin + "×";
            oldInt = GManager.instance.Coin;
        }
        else if (InputText == "hp")
        {
            scoreText.text =GManager.instance.Pstatus.health.ToString();
            oldInt = GManager.instance.Pstatus.health;
        }
        else if (InputText == "o8")
        {
            scoreText.text = GManager.instance.O8.ToString ();
            oldFloat = GManager.instance.O8;
        }
        else if (InputText == "lv")
        {
            scoreText.text = GManager.instance.Pstatus.Lv.ToString();
            oldFloat = GManager.instance.Pstatus.Lv;
        }
        else if (InputText == "hpS")
        {
            scoreText.text = "MaxHP/"+ GManager.instance.Pstatus.maxHP;
        }
        else if (InputText == "lvS")
        {
            scoreText.text = "Lv/" + GManager.instance.Pstatus.Lv;
        }
        else if (InputText == "expS")
        {
            scoreText.text = "Next Lv/" + (GManager.instance.Pstatus.maxExp - GManager.instance.Pstatus.inputExp);
        }
        else if (InputText == "atS")
        {
            if (GManager.instance.itemhand != -1)
            {
                scoreText.text = "AT/" + (GManager.instance.Pstatus.attack + GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
            }
            else if (GManager.instance.itemhand == -1)
            {
                scoreText.text = "AT/" + GManager.instance.Pstatus.attack;
            }
        }
        else if (InputText == "dfS")
        {
            scoreText.text = "DF/" + GManager.instance.Pstatus.defense;
        }
        else if (InputText == "spS")
        {
            scoreText.text = "SP/" + GManager.instance.Pstatus.speed;
        }
        if(InputText == "itemcl")
        {
            //for (int i = 0; i < (GManager.instance.ItemID.Length);)
            //{
            //    GManager.instance.ItemID[i].itemnumber = PlayerPrefs.GetInt("itemnumber" + i, 0);
            //    GManager.instance.ItemID[i].gettrg = PlayerPrefs.GetInt("itemget" + i, 0);
            //    i += 1;
            //}
            int allitem = GManager.instance.ItemID.Length;
            int getitem = 0;
            for(int i = 0; i < GManager.instance.ItemID.Length;)
            {
                if(GManager.instance.ItemID[i].gettrg == 1)
                {
                    getitem += 1;
                }
                i++;
            }
            int percent = (100 / allitem) * getitem;
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "アイテム収集率:" + percent + "%";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Item collection:" + percent + "%";
            }
        }
        else if (InputText == "weaponcl")
        {
            //for (int i = 0; i < (GManager.instance.WeaponID.Length);)
            //{
            //    GManager.instance.WeaponID[i].getTrigger = PlayerPrefs.GetInt("weapongettrg" + i, 0);
            //    i += 1;
            //}
            int allitem = GManager.instance.WeaponID.Length;
            int getitem = 0;
            for (int i = 0; i < GManager.instance.WeaponID.Length;)
            {
                if (GManager.instance.WeaponID[i].getTrigger == 1)
                {
                    getitem += 1;
                }
                i++;
            }
            int percent = (100 / allitem) * getitem;
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "武器収集率:" + percent + "%";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Gun collection:" + percent + "%";
            }
        }
        else if(InputText == "reduction")
        {
            if(GManager.instance.reduction == 0)
            {
                if(GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:OFF";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: OFF";
                }
                oldInt = 0;
            }
            else if(GManager.instance.reduction == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:ON";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: ON";
                }
                oldInt = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(oldEnglish != GManager.instance.isEnglish )
        {
            oldEnglish = GManager.instance.isEnglish;
            if (InputText == "itemcl")
            {
                int allitem = GManager.instance.ItemID.Length;
                int getitem = 0;
                for (int i = 0; i < GManager.instance.ItemID.Length;)
                {
                    if (GManager.instance.ItemID[i].gettrg == 1)
                    {
                        getitem += 1;
                    }
                    i++;
                }
                int percent = (100 / allitem) * getitem;
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "アイテム収集率:" + percent + "%";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Item collection:" + percent + "%";
                }
            }
            else if (InputText == "weaponcl")
            {
                int allitem = GManager.instance.WeaponID.Length;
                int getitem = 0;
                for (int i = 0; i < GManager.instance.WeaponID.Length;)
                {
                    if (GManager.instance.WeaponID[i].getTrigger == 1)
                    {
                        getitem += 1;
                    }
                    i++;
                }
                int percent = (100 / allitem) * getitem;
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "武器収集率:" + percent + "%";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Gun collection:" + percent + "%";
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape ))
        {
            if (InputText == "textget")
            {
                scoreText.text = "";
                GManager.instance.txtget = "";
                oldString = "";
            }
        }
        if (InputText == "textget" && oldString != GManager.instance.txtget)
        {
            scoreText.text = GManager.instance.txtget;
            oldString = GManager.instance.txtget;
        }
        else if (InputText == "coin" && oldInt != GManager.instance.Coin)
        {
            if(oldInt < GManager.instance.Coin)
            {
                GameObject coin = GameObject.Find("coinIcon");
                iTween.ScaleAdd(coin.gameObject, iTween.Hash("x", 1.6f, "y", 1.6f, "time", 0.15f, "delay", 0f));
                iTween.ScaleAdd(coin.gameObject, iTween.Hash("x", -1.6f, "y", -1.6f, "time", 0.3f, "delay", 0.15f));
            }
            scoreText.text = GManager.instance.Coin + " ×";
            oldInt = GManager.instance.Coin;
        }
        else if (InputText == "hp" && oldInt != GManager.instance.Pstatus.health)
        {
            scoreText.text = GManager.instance.Pstatus.health.ToString();
            oldInt = GManager.instance.Pstatus.health;
        }
        else if (InputText == "o8" && oldFloat != GManager.instance.O8)
        {
            scoreText.text = GManager.instance.O8.ToString();
            oldFloat = GManager.instance.O8;
        }
        else if (InputText == "lv" && oldFloat != GManager.instance.Pstatus.Lv)
        {
            scoreText.text = GManager.instance.Pstatus.Lv.ToString();
            oldFloat = GManager.instance.Pstatus.Lv;
        }
        else if (InputText == "reduction" && oldInt != GManager.instance.reduction)
        {
            if (GManager.instance.reduction == 0)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:OFF";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: OFF";
                }
                oldInt = 0;
            }
            else if (GManager.instance.reduction == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:ON";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: ON";
                }
                oldInt = 1;
            }
        }
    }
}