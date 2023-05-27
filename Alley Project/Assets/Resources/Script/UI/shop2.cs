using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shop2 : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip getse;
    public AudioClip notse;
    public int inputshopID2;
    public RenderTexture nullsprite;
    public Text gunname;
    public RawImage gunsprite;
    public Text gunscript;
    public Text gunprice;
    public Text getnumber;
    bool pushtrg = false;
    float pushtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.shopID2[inputshopID2] != -1)
        {
            if (GManager.instance.shopNumber2[inputshopID2] < 1)
            {
                GManager.instance.shopID2[inputshopID2] = -1;
            }
        }
        if (GManager.instance.shopID2[inputshopID2] == -1)
        {
            gunname.text = "？？？？？？";
            gunsprite.texture = nullsprite;
            gunscript.text = "？？？？？？？？？？";
            gunprice.text = "？×";
            getnumber.text = "？×";
        }
        else if (GManager.instance.shopID2[inputshopID2] != -1)
        {
            gunsprite.texture = GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemimage;
            if (GManager.instance.isEnglish == 0)
            {
                gunscript.text = GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemscript;
                gunname.text = GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemname;
                
            }
            else if (GManager.instance.isEnglish == 1)
            {
                gunscript.text = GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemscript2;
                gunname.text = GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemname2;
                
            }
            gunprice.text = GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemprice + "×";
            getnumber.text = GManager.instance.shopNumber2[inputshopID2] +"×";
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
            if (GManager.instance.shopID2[inputshopID2] == -1)
            {
                audioS.PlayOneShot(notse);
            }
            else if (GManager.instance.shopID2[inputshopID2] != -1)
            {
                if (GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemprice > GManager.instance.Coin)
                {
                    audioS.PlayOneShot(notse);
                }
                else if (GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemprice < (GManager.instance.Coin + 1))
                {
                    audioS.PlayOneShot(getse);
                    GManager.instance.Coin -= GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].itemprice;
                    GManager.instance.WeaponID[GManager.instance.shopID2[inputshopID2]].getTrigger = 1;
                    GManager.instance.shopNumber2[inputshopID2] -= 1;
                    PlayerPrefs.SetInt("shopNumber2" + inputshopID2, GManager.instance.shopNumber2[inputshopID2]);
                    PlayerPrefs.Save();
                    getnumber.text = GManager.instance.shopNumber2[inputshopID2] + "×";
                    if (GManager.instance.shopNumber2[inputshopID2] < 1)
                    {
                        GManager.instance.shopID2[inputshopID2] = -1;
                        gunname.text = "？？？？？？";
                        gunsprite.texture = nullsprite;
                        gunscript.text = "？？？？？？？？？？";
                        gunprice.text = "？×";
                        getnumber.text = "？×";
                    }
                    GameObject set = GameObject.Find("weaponSet");
                    changeWeapon changew = set.GetComponent<changeWeapon>();
                    changew.setWeapon();
                }
            }
        }
    }
}
