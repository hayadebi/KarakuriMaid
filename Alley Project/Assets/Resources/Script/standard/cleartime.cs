using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleartime : MonoBehaviour
{
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 60)
        {
            GManager.instance.clearTime += 1;
            t = 0;
        }
        if (GManager.instance.clearTime  > 59)
        {
            GManager.instance.clearTime2 += 1;
            GManager.instance.clearTime = 0;
        }
    }
}
