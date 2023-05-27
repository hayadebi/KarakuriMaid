using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class slider : MonoBehaviour
{
    public string sliderType = "";
    Image _slider;
    float addnumber = 0;

    public string bossname;
    int oldbosshp;
    GameObject boss;
    enemyS script;
    void Start()
    {
        // スライダーを取得する
        _slider = this.GetComponent<Image>();
        GManager.instance.audioMax = PlayerPrefs.GetFloat("audioMax", GManager.instance.audioMax);
        GManager.instance.mode = PlayerPrefs.GetInt("mode", GManager.instance.mode);
        GManager.instance.siya = PlayerPrefs.GetFloat("siya", GManager.instance.siya);
        GManager.instance.kando = PlayerPrefs.GetFloat("kando", GManager.instance.kando);
        if (sliderType == "audio")
        {
            addnumber = 100 / 1;
            addnumber /= 100;
            addnumber = addnumber * 100;
            addnumber = Mathf.Floor(addnumber) / 100;
            _slider.fillAmount = addnumber * GManager.instance.audioMax;
        }
        else if (sliderType == "boss")
        {
            boss = GameObject.Find(bossname);
            script = boss.GetComponent<enemyS>();
            oldbosshp = script.Estatus.health;
            addnumber = 1000000 / oldbosshp;
            addnumber /= 1000000;
            addnumber = addnumber * 1000000;
            addnumber = Mathf.Floor(addnumber) / 1000000;
            _slider.fillAmount = addnumber * script.Estatus.health;
        }
        else if (sliderType == "mode")
        {
            addnumber = 100 / 2;
            addnumber /= 100;
            addnumber = addnumber * 100;
            addnumber = Mathf.Floor(addnumber) / 100;
            _slider.fillAmount = addnumber * GManager.instance.mode;
        }
        else if (sliderType == "siya")
        {
            addnumber = 10000 / 90;
            addnumber /= 10000;
            addnumber = addnumber * 10000;
            addnumber = Mathf.Floor(addnumber) / 10000;
            _slider.fillAmount = addnumber * GManager.instance.siya;
        }
        else if (sliderType == "kando")
        {
            addnumber = 100 / 5;
            addnumber /= 100;
            addnumber = addnumber * 100;
            addnumber = Mathf.Floor(addnumber) / 100;
            _slider.fillAmount = addnumber * GManager.instance.kando;
        }
        else if (sliderType == "hp")
        {
             addnumber = 10000 / GManager.instance.Pstatus.maxHP;
            addnumber /= 10000;
            addnumber = addnumber * 10000;
            addnumber = Mathf.Floor(addnumber) / 10000;
            _slider.fillAmount = addnumber * GManager.instance.Pstatus.health;
        }
        else if (sliderType == "o8")
        {
            addnumber = 10000 / GManager.instance.maxO8;
            addnumber /= 10000;
            addnumber = addnumber * 10000;
            addnumber = Mathf.Floor(addnumber) / 10000;
            _slider.fillAmount = addnumber * GManager.instance.O8;
        }
        else if (sliderType == "lv")
        {
            addnumber = 1000000 / GManager.instance.Pstatus.maxExp;
            addnumber /= 1000000;
            addnumber = addnumber * 1000000;
            addnumber = Mathf.Floor(addnumber) / 1000000;
            _slider.fillAmount = addnumber * GManager.instance.Pstatus.inputExp;
        }
        else if (sliderType == "skill")
        {
            if (GManager.instance.skillselect != -1)
            {
                addnumber = 1000000 / GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                addnumber /= 1000000;
                addnumber = addnumber * 1000000;
                addnumber = Mathf.Floor(addnumber) / 1000000;
                _slider.fillAmount = addnumber * GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar;
            }
        }
    }

    void Update()
    {
        if (sliderType == "audio")
        {
            if (addnumber != 100 / 1 || _slider.fillAmount != addnumber * GManager.instance.audioMax)
            {
                addnumber = 100 / 1;
                addnumber /= 100;
                addnumber = addnumber * 100;
                addnumber = Mathf.Floor(addnumber) / 100;
                _slider.fillAmount = addnumber * GManager.instance.audioMax;
            }
        }
        else if (sliderType == "boss")
        {
            if (addnumber != 1000000 / script.Estatus.health || script.Estatus.health * addnumber != _slider.fillAmount)
            {
            //    boss = GameObject.Find(bossname);
            //    script = boss.GetComponent<enemyS>();
                addnumber = 1000000 / oldbosshp;
                addnumber /= 1000000;
                addnumber = addnumber * 1000000;
                addnumber = Mathf.Floor(addnumber) / 1000000;
                _slider.fillAmount = addnumber * script.Estatus.health;
            }
        }
        else if (sliderType == "siya")
        {
            if (addnumber != 10000 / 90 || _slider.fillAmount != addnumber * GManager.instance.siya)
            {
                addnumber = 10000 / 90;
                addnumber /= 10000;
                addnumber = addnumber * 10000;
                addnumber = Mathf.Floor(addnumber) / 10000;
                _slider.fillAmount = addnumber * GManager.instance.siya;
            }
        }
        else if (sliderType == "kando")
        {
            if (addnumber != 100 / 5 || _slider.fillAmount != addnumber * GManager.instance.kando)
            {
                addnumber = 100 / 5;
                addnumber /= 100;
                addnumber = addnumber * 100;
                addnumber = Mathf.Floor(addnumber) / 100;
                _slider.fillAmount = addnumber * GManager.instance.kando;
            }
        }
        else if (sliderType == "mode")
        {
            if (addnumber != 100 / 2 || _slider.fillAmount != addnumber * GManager.instance.mode)
            {
                addnumber = 100 / 2;
                addnumber /= 100;
                addnumber = addnumber * 100;
                addnumber = Mathf.Floor(addnumber) / 100;
                _slider.fillAmount = addnumber * GManager.instance.mode;
            }
        }
        else if (sliderType == "hp")
        {
            if (addnumber != 10000 / GManager.instance.Pstatus.maxHP || _slider.fillAmount != addnumber * GManager.instance.Pstatus.health)
            {
                addnumber = 10000 / GManager.instance.Pstatus.maxHP;
                addnumber /= 10000;
                addnumber = addnumber * 10000;
                addnumber = Mathf.Floor(addnumber) / 10000;
                _slider.fillAmount = addnumber * GManager.instance.Pstatus.health;
            }
        }
        else if (sliderType == "o8")
        {
            if (addnumber != 10000 / GManager.instance.maxO8 || _slider.fillAmount != addnumber * GManager.instance.O8)
            {
                addnumber = 10000 / GManager.instance.maxO8;
                addnumber /= 10000;
                addnumber = addnumber * 10000;
                addnumber = Mathf.Floor(addnumber) / 10000;
                _slider.fillAmount = addnumber * GManager.instance.O8;
            }
        }
        else if (sliderType == "lv")
        {
            if (GManager.instance.Pstatus.inputExp > GManager.instance.Pstatus.maxExp || GManager.instance.Pstatus.inputExp == GManager.instance.Pstatus.maxExp)
            {
                GManager.instance.Pstatus.maxExp += 40;
                GManager.instance.Pstatus.inputExp = 0;
                GManager.instance.Pstatus.Lv += 1;
                GameObject p = GameObject.Find("Player");
                if (p != null)
                {
                    Instantiate(GManager.instance.lvupeffect, p.transform.position, GManager.instance.lvupeffect.transform.rotation, p.transform);
                }
                GManager.instance.setrg = 12;
                GManager.instance.Pstatus.maxHP += 1;
                GManager.instance.Pstatus.attack += 1;
                GManager.instance.Pstatus.defense += 1;
                GManager.instance.Pstatus.health += 1;
            }
            //----
            if (addnumber != 1000000 / GManager.instance.Pstatus.maxExp || _slider.fillAmount != addnumber * GManager.instance.Pstatus.inputExp)
            {
                addnumber = 1000000 / GManager.instance.Pstatus.maxExp;
                addnumber /= 1000000;
                addnumber = addnumber * 1000000;
                addnumber = Mathf.Floor(addnumber) / 1000000;
                _slider.fillAmount = addnumber * GManager.instance.Pstatus.inputExp;
            }
        }
        else if (sliderType == "skill")
        {
            if (GManager.instance.skillselect != -1)
            {
                if (addnumber != 1000000 / GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar || _slider.fillAmount != addnumber * GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar)
                {
                    addnumber = 1000000 / GManager.instance.SkillID[GManager.instance.skillselect].skillmaxbar;
                    addnumber /= 1000000;
                    addnumber = addnumber * 1000000;
                    addnumber = Mathf.Floor(addnumber) / 1000000;
                    _slider.fillAmount = addnumber * GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar;
                }
            }
        }

    }
}