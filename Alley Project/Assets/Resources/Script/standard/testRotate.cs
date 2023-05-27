using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotate : MonoBehaviour
{
    public bool ontest = false;
    public float addangle = 0;
    // Update is called once per frame
    void Update()
    {
        if (ontest == true)
        {
            ontest = false;
            var rot = Quaternion.Euler(0, addangle, 0);
            this.transform.rotation = this.transform.rotation * rot;
            Debug.Log("this.transform.localEulerAnglesの値は" + this.transform.localEulerAngles.y);
            Debug.Log("this.transform.rotationの値は" + this.transform.rotation);
        }
    }
}
