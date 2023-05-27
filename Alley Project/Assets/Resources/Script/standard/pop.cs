using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pop : MonoBehaviour
{
    public float xpop;
    public float ypop;
    public float span = 1f;
    private float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > span)
        {
            iTween.ShakePosition(gameObject , iTween.Hash("x", xpop, "y", ypop, "time", 0.5f, "delay", 0.25f));
            currentTime = 0f;
        }
    }
}
