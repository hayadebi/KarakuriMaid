using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundM : MonoBehaviour
{
    public AudioClip[] se;
    AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.setrg != -1 && GManager.instance.setrg != 99)
        {
            audioS.PlayOneShot(se[GManager.instance.setrg]);
            GManager.instance.setrg = -1;
        }
        else if (GManager.instance.setrg != -1 && GManager.instance.setrg == 99)
        {
            audioS.PlayOneShot(GManager.instance.monsterse);
            GManager.instance.setrg = -1;
        }
    }
}
