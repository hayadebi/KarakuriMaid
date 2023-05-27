using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineOn : MonoBehaviour
{
    LineRenderer line;
    bool linetrg = true;
    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.G))
        {
            if(linetrg == true)
            {
                linetrg = false;
                line.enabled = false;
            }
            else if (linetrg == false)
            {
                linetrg = true;
                line.enabled = true;
            }
        }
    }
}
