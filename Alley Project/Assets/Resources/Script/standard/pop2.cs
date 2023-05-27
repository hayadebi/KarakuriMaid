using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pop2 : MonoBehaviour
{
    public float Dxpop;
    public float Dypop;
    public float xpop;
    public float ypop;
    public float span = 1f;
    public float returnspan = 0.16f;
    private float currentTime = 0f;
    private bool scaletrg = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > span && scaletrg == false)
        {
            this.transform.localScale = new Vector3(xpop, ypop, 1);
            scaletrg = true;
            currentTime = 0f;
        }
        else if (currentTime > returnspan && scaletrg == true)
        {
            this.transform.localScale = new Vector3(Dxpop, Dypop, 1);
            scaletrg = false;
            currentTime = 0f;
        }
    }
}
