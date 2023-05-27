using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunUI : MonoBehaviour
{
    public RenderTexture nullimage;
    public Text bulletText;
    public Text modeText;
    public RawImage gunimage;

    private int oldInt = 0;
    private string oldString = "";
    private RenderTexture  oldSprite = null;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.itemhand == -1)
        {
            gunimage.texture = nullimage;
            modeText.text = "??????";
            bulletText.text = "??";
        }
        else if (GManager.instance.itemhand != -1)
        {
            gunimage.texture = GManager.instance.WeaponID[GManager.instance.itemhand].itemimage;
            modeText.text = GManager.instance.WeaponID[GManager.instance.itemhand].gunmode;
            bulletText.text = GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber.ToString();
            oldInt = GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber;
            oldString = GManager.instance.WeaponID[GManager.instance.itemhand].gunmode;
            oldSprite = GManager.instance.WeaponID[GManager.instance.itemhand].itemimage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.itemhand != -1)
        {
            if (oldString != GManager.instance.WeaponID[GManager.instance.itemhand].gunmode)
            {
                oldString = GManager.instance.WeaponID[GManager.instance.itemhand].gunmode;
                modeText.text = GManager.instance.WeaponID[GManager.instance.itemhand].gunmode;
            }
            if (oldInt != GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber)
            {
                oldInt = GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber;
                bulletText.text = GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber.ToString();
            }
            if (oldSprite != GManager.instance.WeaponID[GManager.instance.itemhand].itemimage)
            {
                oldSprite = GManager.instance.WeaponID[GManager.instance.itemhand].itemimage;
                gunimage.texture = GManager.instance.WeaponID[GManager.instance.itemhand].itemimage;
            }
        }
    }
}