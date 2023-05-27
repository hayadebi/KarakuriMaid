using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shop3 : MonoBehaviour
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
    int[] slimeshopID;
    int boxnumber = 0;
    int addnumber = 0;
    int gunNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; GManager.instance.WeaponID.Length > i;)
        {
            if(GManager.instance.WeaponID[i].getTrigger < 1 && GManager.instance.WeaponID[i].inputeventnumber < GManager.instance.EventNumber[1])
            {
                boxnumber += 1;
            }
            i += 1;
        }
        slimeshopID = new int[boxnumber];
        if (boxnumber != 0)
        {
            for (int i = 0; GManager.instance.WeaponID.Length > i;)
            {
                if (GManager.instance.WeaponID[i].getTrigger < 1 && GManager.instance.WeaponID[i].inputeventnumber < GManager.instance.EventNumber[1])
                {
                    if (addnumber < 3)
                    {
                        slimeshopID[addnumber] = i;
                        addnumber += 1;
                    }
                }
                i += 1;
            }
        }
        if (slimeshopID.Length > inputshopID2)
        {
            //-----------------------------
            if (slimeshopID[inputshopID2] == -1)
            {
                gunname.text = "？？？？？？";
                gunsprite.texture = nullsprite;
                gunscript.text = "？？？？？？？？？？";
                gunprice.text = "？×";
                getnumber.text = "？×";
            }
            else if (slimeshopID[inputshopID2] != -1)
            {
                gunNumber = 1;
                gunsprite.texture = GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemimage;
                if (GManager.instance.isEnglish == 0)
                {
                    gunscript.text = GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemscript;
                    gunname.text = GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemname;

                }
                else if (GManager.instance.isEnglish == 1)
                {
                    gunscript.text = GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemscript2;
                    gunname.text = GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemname2;

                }
                gunprice.text = GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemprice + "×";
                getnumber.text = gunNumber + "×";
            }
        }
        else
        {
            gunname.text = "？？？？？？";
            gunsprite.texture = nullsprite;
            gunscript.text = "？？？？？？？？？？";
            gunprice.text = "？×";
            getnumber.text = "？×";
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
        if (pushtrg == false && slimeshopID.Length > inputshopID2)
        {
            pushtrg = true;
            if (slimeshopID[inputshopID2] == -1)
            {
                audioS.PlayOneShot(notse);
            }
            else if (slimeshopID[inputshopID2] != -1)
            {
                if (GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemprice > GManager.instance.Coin)
                {
                    audioS.PlayOneShot(notse);
                }
                else if (GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemprice < (GManager.instance.Coin + 1))
                {
                    audioS.PlayOneShot(getse);
                    GManager.instance.Coin -= GManager.instance.WeaponID[slimeshopID[inputshopID2]].itemprice;
                    GManager.instance.WeaponID[slimeshopID[inputshopID2]].getTrigger = 1;
                    gunNumber -= 1;
                    getnumber.text = gunNumber + "×";
                    if (gunNumber < 1)
                    {
                        slimeshopID[inputshopID2] = -1;
                        gunname.text = "？？？？？？";
                        gunsprite.texture = nullsprite;
                        gunscript.text = "？？？？？？？？？？";
                        gunprice.text = "？×";
                        getnumber.text = "？×";
                    }
                }
            }
        }
    }
}
