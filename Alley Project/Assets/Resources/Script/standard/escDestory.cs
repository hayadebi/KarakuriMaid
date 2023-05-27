using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escDestory : MonoBehaviour
{
    public bool mouseesctrg = false;
    [Header("1個前のUIに戻す指定変数(+1される)")] public int inputUInumber = -1;
    public bool mousetrg = false;
    public Animator ui = null;
    public string animname;
    public float destroytime = 0.1f;
    bool inputon = false;
    // Start is called before the first frame update
    void Start()
    {
        if (mousetrg == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        Invoke("trgOn", 1);
    }
    void trgOn()
    {
        inputon = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (inputon == true && inputUInumber == -1)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || GManager.instance.ESCtrg == true)
            {
                GManager.instance.ESCtrg = false;
                PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
                PlayerPrefs.SetInt("mode", GManager.instance.mode);
                PlayerPrefs.SetFloat("kando", GManager.instance.kando);
                PlayerPrefs.SetFloat("siya", GManager.instance.siya);
                PlayerPrefs.Save();
                GManager.instance.setmenu = 0;
                GManager.instance.walktrg = true;
                if (mousetrg == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                Destroy(gameObject, destroytime);
                //試験的にアンロード
                Resources.UnloadUnusedAssets();
                //-----------------
                if (ui != null)
                {
                    ui.Play(animname);
                }
            }
            else if (mouseesctrg == true)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return))
                {
                    GManager.instance.ESCtrg = false;
                    PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
                    PlayerPrefs.SetInt("mode", GManager.instance.mode);
                    PlayerPrefs.SetFloat("kando", GManager.instance.kando);
                    PlayerPrefs.SetFloat("siya", GManager.instance.siya);
                    PlayerPrefs.Save();
                    GManager.instance.setmenu = 0;
                    GManager.instance.walktrg = true;
                    if (mousetrg == true)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                    Destroy(gameObject, destroytime);
                    //試験的にアンロード
                    Resources.UnloadUnusedAssets();
                    //-----------------
                    if (ui != null)
                    {
                        ui.Play(animname);
                    }
                }
            }
        }
        else if(inputon == true && (inputUInumber + 1) > GManager.instance.setmenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || GManager.instance.ESCtrg == true)
            {
                PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
                PlayerPrefs.SetInt("mode", GManager.instance.mode);
                PlayerPrefs.SetFloat("kando", GManager.instance.kando);
                PlayerPrefs.SetFloat("siya", GManager.instance.siya);
                PlayerPrefs.Save();
                GManager.instance.setmenu -= 1;
                if (GManager.instance.setmenu < 1)
                {
                    GManager.instance.ESCtrg = false;
                    GManager.instance.walktrg = true;
                    if (mousetrg == true)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
                if (ui != null)
                {
                    ui.Play(animname);
                }
                Destroy(gameObject, destroytime);
                //試験的にアンロード
                Resources.UnloadUnusedAssets();
                //-----------------
            }
            else if(mouseesctrg == true)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return))
                {
                    PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
                    PlayerPrefs.SetInt("mode", GManager.instance.mode);
                    PlayerPrefs.SetFloat("kando", GManager.instance.kando);
                    PlayerPrefs.SetFloat("siya", GManager.instance.siya);
                    PlayerPrefs.Save();
                    GManager.instance.setmenu -= 1;
                    if (GManager.instance.setmenu < 1)
                    {
                        GManager.instance.ESCtrg = false;
                        GManager.instance.walktrg = true;
                        if (mousetrg == true)
                        {
                            Cursor.lockState = CursorLockMode.Locked;
                            Cursor.visible = false;
                        }
                    }
                    if (ui != null)
                    {
                        ui.Play(animname);
                    }
                    Destroy(gameObject, destroytime);
                }
            }
        }
    }
}
