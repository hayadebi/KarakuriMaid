using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeWeapon : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip selectse;
    public AudioClip notse;
    int selectnumber = 0;
    public int[] onWeapon;
    int boxnumber = 0;
    int inputnumber = 0;
    GameObject gunpos;
    // Start is called before the first frame update
    void Start()
    {
        gunpos = GameObject.Find("gunpos");
        setWeapon();
    }
    public void setWeapon()
    {
        for (int i = 0; GManager.instance.WeaponID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.WeaponID[i].getTrigger == 1;)
            {
                boxnumber += 1;
                l++;
            }
            i += 1;
        }
        onWeapon = new int[boxnumber];
        for (int i = 0; GManager.instance.WeaponID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.WeaponID[i].getTrigger == 1;)
            {
                onWeapon[inputnumber] = i;
                inputnumber += 1;
                l++;
            }
            i += 1;
        }
        if (GManager.instance.itemhand != -1)
        {
            for (int i = 0; i < onWeapon.Length;)
            {
                if (onWeapon[i] == GManager.instance.itemhand)
                {
                    selectnumber = i;
                }
                i++;
            }
        }
        if (onWeapon.Length != 0 && GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger != 0)
        {
            GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack = PlayerPrefs.GetInt("Wat" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack);
            GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed = PlayerPrefs.GetFloat("Wbs" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed);
            GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber);
            GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber = PlayerPrefs.GetInt("Wbn2" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber);
            GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
            GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
        if (GManager.instance.itemhand != -1 && GManager.instance.Triggers[23] != 1 && GManager.instance.over == false )
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll > 0)
                {
                    scroll = 0;
                    SelectPlus();
                }
                else if (scroll < 0)
                {
                    scroll = 0;
                    SelectMinus();
                }
        }
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
                GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber = PlayerPrefs.GetInt("Wbn2" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber);
                GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
                GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
            }
            //----
            EquipmentSet();
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
        else if (selectnumber < (onWeapon.Length - 1))
        {
            audioS.PlayOneShot(selectse);
            selectnumber += 1;
            //----
            if (onWeapon.Length != 0 && GManager.instance.WeaponID[onWeapon[selectnumber]].getTrigger != 0)
            {
                GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack = PlayerPrefs.GetInt("Wat" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].upAttack);
                GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed = PlayerPrefs.GetFloat("Wbs" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletspeed);
                GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber = PlayerPrefs.GetInt("Wbn" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].maxbulletnumber);
                GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber = PlayerPrefs.GetInt("Wbn2" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber);
                GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
                GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
            }
            //----
            EquipmentSet();
        }
        else
        {
            audioS.PlayOneShot(notse);
        }
    }
    public void EquipmentSet()
    {
        
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
            GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber = PlayerPrefs.GetInt("Wbn2" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].bulletnumber);
            GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime = PlayerPrefs.GetFloat("Wst" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].shotmaxtime);
            GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime = PlayerPrefs.GetFloat("Wrt" + onWeapon[selectnumber], GManager.instance.WeaponID[onWeapon[selectnumber]].roadmaxtime);
        }
    }
}