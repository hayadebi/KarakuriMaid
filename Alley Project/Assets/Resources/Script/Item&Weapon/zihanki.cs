using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zihanki : MonoBehaviour
{
    public string tagname = "Player";
    public GameObject selectUI;
    public GameObject notUI;
    public int itemID;
    public GameObject destroyobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.tag == tagname && Input.GetKeyDown(KeyCode.E) && GManager.instance.walktrg == true && GManager.instance.setmenu == 0)
        {
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
            Instantiate(selectUI, transform.position, transform.rotation);
        }
    }
    public void NotCoin()
    {
        Instantiate(notUI, transform.position, transform.rotation);
    }
    public void CloseUI()
    {
        GManager.instance.walktrg = true;
        GManager.instance.setmenu = 0;
        Destroy(destroyobj.gameObject);
    }
    public void ShopCoin()
    {
        if((GManager.instance.ItemID[itemID].itemprice - 1)<GManager.instance.Coin)
        {
            GManager.instance.Coin -= GManager.instance.ItemID[itemID].itemprice;
            GManager.instance.ItemID[itemID].itemnumber += 1;
            GManager.instance.ItemID[itemID].gettrg = 1;
            GManager.instance.setrg = 27;
            GManager.instance.skillsay = "zihanki";
            GManager.instance.Sgetsay = true;
            CloseUI();
        }
        else
        {
            NotCoin();
            CloseUI();
        }
    }
}
