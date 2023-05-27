using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class d_raw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderTexture obj = (RenderTexture)Resources.Load(GManager.instance.sayobjname);
        RawImage r = this.GetComponent<RawImage>();
        r.texture = obj;
        r = this.GetComponent<RawImage>();
        GManager.instance.sayobjname = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.sayobjname != "")
        {
            RenderTexture obj = (RenderTexture)Resources.Load(GManager.instance.sayobjname);
            RawImage r = this.GetComponent<RawImage>();
            r.texture = obj;
            r = this.GetComponent<RawImage>();
            GManager.instance.sayobjname = "";
        }
    }
}
