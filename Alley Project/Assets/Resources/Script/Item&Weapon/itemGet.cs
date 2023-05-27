using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGet : MonoBehaviour
{
    bool gettrg = false;
    public int ItemNumber;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -5 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && gettrg == false && Input.GetKeyDown(KeyCode.E))
        {
            gettrg = true;
                GManager.instance.ItemID[ItemNumber].itemnumber += 1;
            GManager.instance.ItemID[ItemNumber].gettrg = 1;
                GManager.instance.setrg = 7;
            Destroy(gameObject, 0.04f);
        }

    }
}
