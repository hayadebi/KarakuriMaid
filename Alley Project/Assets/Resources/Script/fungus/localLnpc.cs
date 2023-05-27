using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class localLnpc : MonoBehaviour
{
    public bool originalSay = false;
    public bool bgmplay = false;
    public string local = "local";
    Flowchart flowChart;
    // Start is called before the first frame update
    void Start()
    {
        flowChart = this.GetComponent<Flowchart>();
        if (GManager.instance.isEnglish == 1)
        {
            flowChart.SetBooleanVariable(local, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(flowChart.GetBooleanVariable(local) == true && GManager.instance.isEnglish == 0)
        {
            flowChart.SetBooleanVariable(local, false);
        }
        else if (flowChart.GetBooleanVariable(local) == false && GManager.instance.isEnglish == 1)
        {
            flowChart.SetBooleanVariable(local, true);
        }
        if (originalSay == true && GManager.instance.sayobjname == "" && flowChart.GetExecutingBlocks().Count != 0)
        {
                GManager.instance.sayobjname = flowChart.GetStringVariable("n");
        }
        if(bgmplay == true && flowChart.GetBooleanVariable("bgm") == true)
        {
            flowChart.SetBooleanVariable("bgm", false);
            npcsay ns = this.GetComponent<npcsay>();
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
            bgmA.clip = ns.bgm;
            bgmA.Play();
        }

    }
}
