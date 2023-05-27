using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillUI : MonoBehaviour
{
    public Text nametext;
    public Image iconimage;
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.skillselect != -1)
        {
            iconimage.sprite = GManager.instance.SkillID[GManager.instance.skillselect].skillicon;
            if (GManager.instance.isEnglish == 0)
            {
                nametext.text = GManager.instance.SkillID[GManager.instance.skillselect].skillname;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                nametext.text = GManager.instance.SkillID[GManager.instance.skillselect].skillname2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}