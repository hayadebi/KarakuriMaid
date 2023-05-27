using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillSelect : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip selectse;
    public AudioClip notse;
    public RenderTexture nullimage;
    public Text skillname;
    public Text bossname;
    public Text skillscript;
    public Text bossscript;
    public RawImage bossimage;
    public Text sousa;
    int selectnumber = 0;
    public int[] onSkill;
    int boxnumber = 0;
    int inputnumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; GManager.instance.SkillID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.SkillID[i].skillget == 1;)
            {
                boxnumber += 1;
                l++;
            }
            i += 1;
        }
        onSkill = new int[boxnumber];
        for (int i = 0; GManager.instance.SkillID.Length > i;)
        {
            for (int l = 0; l == 0 && GManager.instance.SkillID[i].skillget == 1;)
            {
                onSkill[inputnumber] = i;
                inputnumber += 1;
                l++;
            }
            i += 1;
        }
        SkillSet();
        
    }
    void SkillSet()
    {
        if (onSkill.Length == 0)
        {
            bossimage.texture = nullimage;
            skillname.text = "????";
            bossname.text = "????";
            skillscript.text = "????????";
            bossscript.text = "????????";
            sousa.text = "?";
        }
        else if (GManager.instance.SkillID[onSkill[selectnumber]].skillget == 0)
        {
            bossimage.texture = nullimage;
            skillname.text = "????";
            bossname.text = "????";
            skillscript.text = "????????";
            bossscript.text = "????????";
            sousa.text = "?";
        }
        else if (GManager.instance.SkillID[onSkill[selectnumber]].skillget == 1)
        {
            if (GManager.instance.isEnglish == 0)
            {
                bossimage.texture = GManager.instance.SkillID[onSkill[selectnumber]].bossimage;
                skillname.text = GManager.instance.SkillID[onSkill[selectnumber]].skillname;
                bossname.text = GManager.instance.SkillID[onSkill[selectnumber]].bossname;
                skillscript.text = GManager.instance.SkillID[onSkill[selectnumber]].skillscript;
                bossscript.text = GManager.instance.SkillID[onSkill[selectnumber]].bossscript;
                if (GManager.instance.SkillID[onSkill[selectnumber]].notrg == false)
                {
                    sousa.text = GManager.instance.SkillID[onSkill[selectnumber]].buttonNumber.ToString();
                }
                else if (GManager.instance.SkillID[onSkill[selectnumber]].notrg == true)
                {
                    sousa.text = "?";
                }
            }
            else if (GManager.instance.isEnglish == 1)
            {
                bossimage.texture = GManager.instance.SkillID[onSkill[selectnumber]].bossimage;
                skillname.text = GManager.instance.SkillID[onSkill[selectnumber]].skillname2;
                bossname.text = GManager.instance.SkillID[onSkill[selectnumber]].bossname2;
                skillscript.text = GManager.instance.SkillID[onSkill[selectnumber]].skillscript2;
                bossscript.text = GManager.instance.SkillID[onSkill[selectnumber]].bossscript2;
                if (GManager.instance.SkillID[onSkill[selectnumber]].notrg == false)
                {
                    sousa.text = GManager.instance.SkillID[onSkill[selectnumber]].buttonNumber.ToString();
                }
                else if (GManager.instance.SkillID[onSkill[selectnumber]].notrg == true)
                {
                    sousa.text = "?";
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SelectMinus()
    {
        if (onSkill == null)
        {
            audioS.PlayOneShot(notse);
        }
        else if (selectnumber > 0)
        {
            audioS.PlayOneShot(selectse);
            selectnumber -= 1;
            //----
            SkillSet();
            //----
        }
        else
        {
            audioS.PlayOneShot(notse);
        }
    }
    public void SelectPlus()
    {
        if (onSkill.Length == 0)
        {
            audioS.PlayOneShot(notse);
        }
        else if (selectnumber < (onSkill.Length - 1))
        {
            audioS.PlayOneShot(selectse);
            selectnumber += 1;
            //----
            SkillSet();
            //----
        }
        else
        {
            audioS.PlayOneShot(notse);
        }
    }
}