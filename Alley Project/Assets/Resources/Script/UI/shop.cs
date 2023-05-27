using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shop : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip getse;
    public AudioClip notse;
    public int inputshopID;
    public RenderTexture nullsprite;
    public Text itemname;
    public RawImage itemsprite;
    public Text itemscript;
    public Text itemprice;
    public Text getnumber;
    bool pushtrg = false;
    float pushtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.shopID[inputshopID] != -1)
        {
            if (GManager.instance.shopNumber[inputshopID] < 1)
            {
                GManager.instance.shopID[inputshopID] = -1;
            }
        }
        if (GManager.instance.shopID[inputshopID] == -1)
        {
            itemname.text = "？？？？？？";
            itemsprite.texture = nullsprite;
            itemscript.text = "？？？？？？？？？？";
            itemprice.text = "？×";
            getnumber.text = "？×";
        }
        else if (GManager.instance.shopID[inputshopID] != -1)
        {
            itemsprite.texture = GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemimage;
            if (GManager.instance.isEnglish == 0)
            {
                itemscript.text = GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemscript;
                itemname.text = GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemname;

            }
            else if (GManager.instance.isEnglish == 1)
            {
                itemscript.text = GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemscript2;
                itemname.text = GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemname2;

            }
            itemprice.text = GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemprice + "×";
            getnumber.text = GManager.instance.shopNumber[inputshopID] + "×";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pushtrg == true)
        {
            pushtime += Time.deltaTime;
            if (pushtime > 1f)
            {
                pushtime = 0;
                pushtrg = false;
            }
        }
    }

    public void ShopClick()
    {
        if (pushtrg == false)
        {
            pushtrg = true;
            if (GManager.instance.shopID[inputshopID] == -1)
            {
                audioS.PlayOneShot(notse);
            }
            else if (GManager.instance.shopID[inputshopID] != -1)
            {
                if (GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemprice > GManager.instance.Coin)
                {
                    audioS.PlayOneShot(notse);
                }
                else if (GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemprice < (GManager.instance.Coin + 1))
                {
                    audioS.PlayOneShot(getse);
                    GManager.instance.Coin -= GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemprice;
                    GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].itemnumber += 1;
                    GManager.instance.ItemID[GManager.instance.shopID[inputshopID]].gettrg = 1;

                    GManager.instance.shopNumber[inputshopID] -= 1;
                    PlayerPrefs.SetInt("shopNumber" + inputshopID, GManager.instance.shopNumber[inputshopID]);
                    PlayerPrefs.Save();
                    getnumber.text = GManager.instance.shopNumber[inputshopID] + "×";
                    if (GManager.instance.shopNumber[inputshopID] < 1)
                    {
                        GManager.instance.shopID[inputshopID] = -1;
                        itemname.text = "？？？？？？";
                        itemsprite.texture = nullsprite;
                        itemscript.text = "？？？？？？？？？？";
                        itemprice.text = "？×";
                        getnumber.text = "？×";
                    }
                }
            }
        }
    }
}
