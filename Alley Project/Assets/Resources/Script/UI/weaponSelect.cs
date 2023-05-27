using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponSelect : MonoBehaviour
{
    public GameObject selectmenuUI;
    public AudioSource audioS;
    public AudioClip selectse;
    public AudioClip onse;
    public AudioClip notse;
    public RenderTexture nullimage;
    public Text nameText;
    public Text attackText;
    public Text scriptText;
    public RawImage gunimage;
    public Text Equipment;
    int selectnumber = 0;
   public int[] onWeapon;
    int boxnumber = 0;
    int inputnumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; GManager.instance.WeaponID.Length > i;)
        {
            for(int l = 0;l == 0 && GManager.instance.WeaponID[i].getTrigger == 1;)
            {
                boxnumber += 1;
                l ++;
            }
            i+=1;
        }
        onWeapon = new int[boxnumber];
        for (int i = 0; GManager.instance.WeaponID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.WeaponID[i].getTrigger == 1;)
            {
                onWeapon[inputnumber] = i;
                inputnumber+=1;
                l++;
            }
            i+=1;
        }
        if (onWeapon.Length != 0 && GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger != 0)
        {
            GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack = PlayerPrefs.GetInt("Wat" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack);
            GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed = PlayerPrefs.GetFloat("Wbs" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed);
            GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber);
            GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
            GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
        }
        if (onWeapon.Length == 0 )
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            attackText.text = "AT/??";
            scriptText.text = "????????";
            Equipment.text = "";
        }
        else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 0)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            attackText.text = "AT/??";
            scriptText.text = "????????";
            Equipment.text = "";
        }
        else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 1)
        {
            gunimage.texture = GManager.instance.WeaponID[onWeapon[selectnumber]].itemimage;
           
            attackText.text = "AT/" + GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack;
            if (GManager.instance.isEnglish == 0)
            {
                nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname;
                scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname2;
                scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript2;
            }
            if (GManager.instance.itemhand == onWeapon[selectnumber])
            {
                Equipment.text = "E";
            }
            else
            {
                Equipment.text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    public void SelectMinus()
    {
        if (onWeapon == null)
        {
            audioS.PlayOneShot(notse);
        }
        else if (selectnumber > 0)
        {
            audioS.PlayOneShot(selectse);
            selectnumber -= 1;
            //----
            if (onWeapon.Length != 0 && GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger != 0)
            {
                GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack = PlayerPrefs.GetInt("Wat" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack);
                GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed = PlayerPrefs.GetFloat("Wbs" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed);
                GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber);
                GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
                GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
            }
            if (onWeapon.Length == 0)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                attackText.text = "AT/??";
                scriptText.text = "????????";
                Equipment.text = "";
            }
            else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 0)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                attackText.text = "AT/??";
                scriptText.text = "????????";
                Equipment.text = "";
            }
            else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 1)
            {
                gunimage.texture = GManager.instance.WeaponID[onWeapon[selectnumber]].itemimage;
                
                attackText.text = "AT/" + GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack;
                if (GManager.instance.isEnglish == 0)
                {
                    nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname;
                    scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript;
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname2;
                    scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript2;
                }
                if (GManager.instance.itemhand == onWeapon[selectnumber])
                {
                    Equipment.text = "E";
                }
                else
                {
                    Equipment.text = "";
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
        if (onWeapon.Length == 0)
        {
            audioS.PlayOneShot(notse);
        }
        else if (selectnumber < (onWeapon.Length -1 ))
        {
            audioS.PlayOneShot(selectse);
            selectnumber += 1;
            //----
            if (onWeapon.Length != 0 && GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger != 0)
            {
                GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack = PlayerPrefs.GetInt("Wat" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack);
                GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed = PlayerPrefs.GetFloat("Wbs" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed);
                GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber);
                GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
                GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
            }
            if (onWeapon == null)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                attackText.text = "AT/??";
                scriptText.text = "????????";
                Equipment.text = "";
            }
            else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 0)
            {
                gunimage.texture = nullimage;
                nameText.text = "????";
                attackText.text = "AT/??";
                scriptText.text = "????????";
                Equipment.text = "";
            }
            else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 1)
            {
                gunimage.texture = GManager.instance.WeaponID[onWeapon[selectnumber]].itemimage;
                
                attackText.text = "AT/" + GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack;
                if (GManager.instance.isEnglish == 0)
                {
                    nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname;
                    scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript;
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname2;
                    scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript2;
                }
                if (GManager.instance.itemhand == onWeapon[selectnumber])
                {
                    Equipment.text = "E";
                }
                else
                {
                    Equipment.text = "";
                }
            }
            //----
        }
        else
        {
            audioS.PlayOneShot(notse);
        }
    }
    public void MenuUI()
    {
        if (onWeapon != null && onWeapon.Length != 0)
        {
            audioS.PlayOneShot(onse);
            selectmenuUI.SetActive(true);
        }
    }

    public void EquipmentSet()
    {
        GameObject gunpos = GameObject.Find("gunpos");
        GameObject P = GameObject.Find("全体");
        Animator anim = P.GetComponent<Animator>();
        anim.Play("handG");
        if (GManager.instance.itemhand != -1)
        {
            //GManager.instance.Pstatus.attack -= GManager.instance.WeaponID[GManager.instance.itemhand].upAttack;
            foreach (Transform n in gunpos.transform)
            {
                GameObject.Destroy(n.gameObject);
            }
            PlayerPrefs.SetInt("Wbn2" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber);
            PlayerPrefs.Save();
            GManager.instance.itemhand = -1;
        }
        if (GManager.instance.itemhand == -1)
        {
            GManager.instance.setrg = 5;
            //GManager.instance.Pstatus.attack += GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack;
            Instantiate(GManager.instance.WeaponID[onWeapon[selectnumber]].shotobj, gunpos.transform.position, gunpos.transform.rotation, gunpos.transform);
            GManager.instance.itemhand = onWeapon[selectnumber];
        }
        if (onWeapon.Length != 0 && GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger != 0)
        {
            GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack = PlayerPrefs.GetInt("Wat" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack);
            GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed = PlayerPrefs.GetFloat("Wbs" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed);
            GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber);
            GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber = PlayerPrefs.GetInt("Wbn2" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber);
            GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
            GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
        }
        if (onWeapon == null)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            attackText.text = "AT/??";
            scriptText.text = "????????";
            Equipment.text = "";
        }
        else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 0)
        {
            gunimage.texture = nullimage;
            nameText.text = "????";
            attackText.text = "AT/??";
            scriptText.text = "????????";
            Equipment.text = "";
        }
        else if (GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger == 1)
        {
            gunimage.texture = GManager.instance.WeaponID[onWeapon[selectnumber]].itemimage;
            
            attackText.text = "AT/" + GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack;
            if (GManager.instance.isEnglish == 0)
            {
                nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname;
                scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                nameText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemname2;
                scriptText.text = GManager.instance.WeaponID[onWeapon[selectnumber]].itemscript2;
            }
            if (GManager.instance.itemhand == onWeapon[selectnumber])
            {
                Equipment.text = "E";
            }
            else
            {
                Equipment.text = "";
            }
        }
        selectmenuUI.SetActive(false);
    }
    public void NotSet()
    {
        audioS.PlayOneShot(notse);
        selectmenuUI.SetActive(false);
    }
}