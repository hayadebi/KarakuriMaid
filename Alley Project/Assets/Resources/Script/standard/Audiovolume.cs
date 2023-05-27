using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiovolume : MonoBehaviour
{
    public bool battletrg = false;
    private bool isadd = false;
    float oldvolume;
    public bool setrg = false;
    void Start()
    {
        //アタッチされているAudioSource取得
        AudioSource audio = GetComponent<AudioSource>();
        if (setrg == false)
        {
            audio.volume = GManager.instance.audioMax / 4;
            oldvolume = GManager.instance.audioMax / 4;
        }
        else if (setrg == true)
        {
            audio.volume = GManager.instance.audioMax / 2 ;
            oldvolume = GManager.instance.audioMax / 2;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (oldvolume != GManager.instance.audioMax)
        {
            AudioSource audio = GetComponent<AudioSource>();
            if (setrg == false)
            {
                audio.volume = GManager.instance.audioMax / 4;
                oldvolume = GManager.instance.audioMax / 4;
            }
            else if (setrg == true)
            {
                audio.volume = GManager.instance.audioMax / 2;
                oldvolume = GManager.instance.audioMax / 2;
            }
        }
        if(battletrg == false && GManager.instance.walktrg == false && isadd == false)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.enabled = false;
            isadd = true;
        }
        else if (battletrg == false && GManager.instance.walktrg == true && isadd == true)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.enabled = true;
            isadd = false;
        }
    }
}
