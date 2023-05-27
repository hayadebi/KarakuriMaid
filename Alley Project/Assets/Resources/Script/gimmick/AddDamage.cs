using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDamage : MonoBehaviour
{
    public int Damage = 1;
    public bool nokill = false;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.mode == 0)
        {
            Damage -= 1;
        }
        else if (GManager.instance.mode == 2)
        {
            Damage += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
