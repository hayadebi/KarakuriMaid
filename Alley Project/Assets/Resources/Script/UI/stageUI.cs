using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class stageUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttoninv;
    private Button button;
    public int stagenumber = 1;
    public int eventIndex = 0;
    public int inputevent = 1;
    public Sprite nullimage;
    public Sprite stageimage;
    public Image stageRaw;
    public GameObject Fade;
    public AudioSource audioS;
    public AudioClip clickse;
    bool clicktrg = false;
    // Start is called before the first frame update
   
    void Start()
    {
        buttoninv = this.GetComponent<Image>();
        button = this.GetComponent<Button>();
        if(GManager.instance.EventNumber[eventIndex] < inputevent && button.enabled == true)
        {
            button.enabled = false;
            button = this.GetComponent<Button>();
            buttoninv.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
            buttoninv = this.GetComponent<Image>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GManager.instance.EventNumber[eventIndex] > (inputevent - 1) && button.enabled == true)
        {
            stageRaw.sprite = stageimage;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GManager.instance.EventNumber[eventIndex] > (inputevent - 1) && button.enabled == true)
        {
            stageRaw.sprite = nullimage;
        }
    }
    public void stageButton()
    {
        if (clicktrg == false && GManager.instance.EventNumber[eventIndex] > (inputevent - 1) && button.enabled == true)
        {
            clicktrg = true;
            audioS.PlayOneShot(clickse);
            Instantiate(Fade, transform.position, transform.rotation);
            Resources.UnloadUnusedAssets();
            Invoke("StageGO", 3f);
        }
    }

    void StageGO()
    {
        
        GManager.instance.walktrg = true;
        GManager.instance.houseTrg = false;
        GManager.instance.posX = 0;
        PlayerPrefs.SetFloat("posX", GManager.instance.posX);
        GManager.instance.posY = 0;
        PlayerPrefs.SetFloat("posY", GManager.instance.posY);
        GManager.instance.posZ = 0;
        PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
        GManager.instance.stageNumber = stagenumber;
        SaveDate();
        SceneManager.LoadScene("stage" + GManager.instance.stageNumber);
    }
    void SaveDate()
    {
        PlayerPrefs.SetFloat("siya", GManager.instance.siya);
        PlayerPrefs.SetInt("mode", GManager.instance.mode);
        PlayerPrefs.SetInt("itemhand", GManager.instance.itemhand);
        for (int i = 0; i < (GManager.instance.EventNumber.Length);)
        {
            PlayerPrefs.SetInt("EventNumber" + i, GManager.instance.EventNumber[i]);
            i += 1;
        }
        PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
        PlayerPrefs.SetInt("mode", GManager.instance.mode);
        PlayerPrefs.SetFloat("clearTime", GManager.instance.clearTime);
        PlayerPrefs.SetFloat("clearTime2", GManager.instance.clearTime2);
        GameObject P = GameObject.Find("Player");
        PlayerPrefs.SetFloat("posX", GManager.instance.posX);
        PlayerPrefs.SetFloat("posY", GManager.instance.posY);
        PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
        PlayerPrefs.SetInt("stageNumber", GManager.instance.stageNumber);
        PlayerPrefs.SetInt("itemselect", GManager.instance.itemselect);
        PlayerPrefs.SetInt("maxHP", GManager.instance.Pstatus.maxHP);
        PlayerPrefs.SetInt("health", GManager.instance.Pstatus.health);
        PlayerPrefs.SetInt("attack", GManager.instance.Pstatus.attack);
        PlayerPrefs.SetInt("defense", GManager.instance.Pstatus.defense);
        PlayerPrefs.SetFloat("speed", GManager.instance.Pstatus.speed);
        PlayerPrefs.SetInt("Coin", GManager.instance.Coin);
        PlayerPrefs.SetFloat("maxO8", GManager.instance.maxO8);
        PlayerPrefs.SetFloat("O8", GManager.instance.O8);
        PlayerPrefs.SetInt("Lv", GManager.instance.Pstatus.Lv);
        PlayerPrefs.SetInt("maxExp", GManager.instance.Pstatus.maxExp);
        PlayerPrefs.SetInt("inputExp", GManager.instance.Pstatus.inputExp);
        for (int i = 0; i < (GManager.instance.ItemID.Length);)
        {
            PlayerPrefs.SetInt("itemnumber" + i, GManager.instance.ItemID[i].itemnumber);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.WeaponID.Length);)
        {
            PlayerPrefs.SetInt("weapongettrg" + i, GManager.instance.WeaponID[i].getTrigger);
            i += 1;
        }
        for (int i = 0; i < (GManager.instance.Triggers.Length);)
        {
            PlayerPrefs.SetInt("Triggers" + i, GManager.instance.Triggers[i]);
            i += 1;
        }
        PlayerPrefs.SetInt("Wat" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
        PlayerPrefs.SetFloat("Wbs" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletspeed);
        PlayerPrefs.SetInt("Wbn" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].maxbulletnumber);
        PlayerPrefs.SetFloat("Wst" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].shotmaxtime);
        PlayerPrefs.SetFloat("Wrt" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].roadmaxtime);
        for (int i = 0; i < (GManager.instance.SkillID.Length);)
        {
            PlayerPrefs.SetInt("skillget" + i, GManager.instance.SkillID[i].skillget);
            i += 1;
        }
        PlayerPrefs.Save();
    }
}

