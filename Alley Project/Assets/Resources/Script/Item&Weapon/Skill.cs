using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject skillUI;
    GameObject ui;
    GameObject effect = null;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.skillselect != -1)
        {
            if (GManager.instance.skillselect == 10)
            {
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = 0;
                GManager.instance.skillselect = -1;
                GManager.instance.Pstatus.speed = 15;
            }
            else if (GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar != 0)
            {
                ui = Instantiate(skillUI, transform.position, transform.rotation);
            }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GManager.instance.skillselect == -1 && GManager.instance.walktrg == true && GManager.instance.over == false)
        {
            if (GManager.instance.SkillID[0].skillget == 1 && Input.GetKeyDown(KeyCode.Alpha1))
            {
                //スキル1
                GManager.instance.skillselect = 0;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                GameObject ef = Instantiate(GManager.instance.skillobj[0], this.transform.position, this.transform.rotation, this.transform);
                Destroy(ef.gameObject, 8f);
            }
            else if (GManager.instance.SkillID[4].skillget == 1 && Input.GetKeyDown(KeyCode.Alpha2))
            {
                //スキル2
                GManager.instance.skillselect = 4;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                Vector3 pos = this.transform.position + this.transform.right * 2f;
                pos.y += 2;
                Instantiate(GManager.instance.skillobj[3], pos, this.transform.rotation, this.transform);
            }
            else if (GManager.instance.SkillID[6].skillget == 1 && Input.GetKeyDown(KeyCode.Alpha3))
            {
                //スキル3
                GManager.instance.skillselect = 6;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                GManager.instance.setrg = 24;
                Instantiate(GManager.instance.skillobj[6], this.transform.position, GManager.instance.skillobj[6].transform.rotation, this.transform);
            }
            else if (GManager.instance.SkillID[8].skillget == 1 && Input.GetKeyDown(KeyCode.Alpha4))
            {
                //スキル4
                GManager.instance.skillselect = 8;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                Vector3 pos = this.transform.position + this.transform.up * 3.2f + this.transform.forward * -1.6f;
                GameObject bodyobj = GameObject.Find("ボディ");
                Instantiate(GManager.instance.skillobj[9], pos, this.transform.rotation, this.transform);
            }
            else if (GManager.instance.SkillID[10].skillget == 1 && Input.GetKeyDown(KeyCode.Alpha5) && GManager.instance.stageNumber > 7)
            {
                //スキル5希望
                GManager.instance.skillselect = 10;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                Vector3 setpos = this.transform.position + transform.up * -0.5f + transform.forward * -0.7f;
                effect = Instantiate(GManager.instance.skillobj[12], setpos, this.transform.rotation, this.transform);
                GManager.instance.Pstatus.health = GManager.instance.Pstatus.maxHP;
                GManager.instance.O8 = GManager.instance.maxO8;
                GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber = GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber;
                GManager.instance.Pstatus.speed = 30;

            }
            else if (GManager.instance.skillnumber == 1)
            {
                GManager.instance.skillselect = GManager.instance.skillnumber;
                GManager.instance.skillnumber = -1;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                Vector3 skillpos = this.transform.position + this.transform.right * 2;
                GameObject ef = Instantiate(GManager.instance.skillobj[1], skillpos, this.transform.rotation);
                GManager.instance.setrg = 17;
                Instantiate(GManager.instance.lvupeffect, ef.transform.position, ef.transform.rotation, ef.transform);
            }
            else if (GManager.instance.skillnumber == 2)
            {
                GManager.instance.skillselect = GManager.instance.skillnumber;
                GManager.instance.skillnumber = -1;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                Vector3 skillpos = this.transform.position + -this.transform.right * 1.3f;
                GameObject ef = Instantiate(GManager.instance.skillobj[2], skillpos, this.transform.rotation, this.transform);
                skillpos = this.transform.position + this.transform.right * 1.3f;
                GameObject ef2 = Instantiate(GManager.instance.skillobj[2], skillpos, this.transform.rotation, this.transform);
                GManager.instance.setrg = 17;
                Instantiate(GManager.instance.lvupeffect, ef.transform.position, ef.transform.rotation, ef.transform);
                Instantiate(GManager.instance.lvupeffect, ef2.transform.position, ef2.transform.rotation, ef2.transform);
            }
            else if (GManager.instance.skillnumber == 3)
            {
                GManager.instance.skillselect = GManager.instance.skillnumber;
                GManager.instance.skillnumber = -1;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
            }
            else if (GManager.instance.skillnumber == 5)//コインボーナス
            {
                GManager.instance.skillselect = GManager.instance.skillnumber;
                GManager.instance.skillnumber = -1;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
                GManager.instance.setrg = 22;
            }
            else if (GManager.instance.skillnumber == 7)//時限爆弾
            {
                GManager.instance.skillselect = GManager.instance.skillnumber;
                GManager.instance.skillnumber = -1;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
            }
            else if (GManager.instance.skillnumber == 11)//致命傷
            {
                GManager.instance.skillselect = GManager.instance.skillnumber;
                GManager.instance.skillnumber = -1;
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                ui = Instantiate(skillUI, transform.position, transform.rotation);
            }
        }
        else if (GManager.instance.skillnumber == 11 && GManager.instance.walktrg == true && GManager.instance.over == false && GManager.instance.skillselect != -1)
        {
            //致命傷を強制発動
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = 0;
                if (GManager.instance.skillselect == 7)//爆発
                {
                    Instantiate(GManager.instance.skillobj[8], this.transform.position, this.transform.rotation);
                }
                else if (GManager.instance.skillselect == 10 && effect != null)//希望
                {
                    GManager.instance.Pstatus.speed = 15;
                    Destroy(effect.gameObject);
                }
                GManager.instance.skillselect = -1;
            if (ui != null)
            {
                Destroy(ui.gameObject);
            }
            //----------------
            GManager.instance.skillselect = GManager.instance.skillnumber;
            GManager.instance.skillnumber = -1;
            GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
            ui = Instantiate(skillUI, transform.position, transform.rotation);
        }
        else if (GManager.instance.skillselect != -1 && GManager.instance.walktrg == true && GManager.instance.over == false)
        {
            if (GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar == 0 || GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar < 0)
            {
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar = 0;
                if (GManager.instance.skillselect == 7)//爆発
                {
                    Instantiate(GManager.instance.skillobj[8], this.transform.position, this.transform.rotation);
                }
                else if (GManager.instance.skillselect == 10 && effect != null)//希望
                {
                    GManager.instance.Pstatus.speed = 15;
                    Destroy(effect.gameObject);
                }
                GManager.instance.skillselect = -1;

                if (ui != null)
                {
                    foreach (Transform child in ui.transform)
                    {
                        if (child.GetComponent<Animator>())
                        {
                            child.GetComponent<Animator>().Play("end");
                        }
                    }
                }
                Destroy(ui.gameObject, 0.3f);
            }
        }
    }

}
