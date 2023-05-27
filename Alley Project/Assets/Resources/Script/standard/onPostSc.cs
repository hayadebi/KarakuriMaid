using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class onPostSc : MonoBehaviour
{
    public PostProcessVolume ppv;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.reduction == 1)
        {
            ppv.enabled = false;
        }
        else if (GManager.instance.reduction == 0)
        {
            ppv.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.reduction == 1 && ppv.enabled == true)
        {
            ppv.enabled = false;
        }
        else if (GManager.instance.reduction == 0 && ppv.enabled == false)
        {
            ppv.enabled = true;
        }
    }
}
