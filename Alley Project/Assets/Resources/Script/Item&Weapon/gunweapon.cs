using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunweapon : MonoBehaviour
{
    public bool rifletrg = false;
    public bool snipertrg = false;
    public int gunNumber;
    public GameObject shotpos;
    public GameObject targetpos;
    player ps;
    public int systemsenumber = -1;
    public AudioClip shotse;
    AudioSource audioS;
    public GameObject Bullet;
    private float shottime = 0;
    public int shotnumber = 1;
    public float angleminnumberx;
    public float xangleaddx = 1;
    public float angleminnumbery;
    public float xangleaddy = 1;
    public float angleminnumberz;
    public float xangleaddz = 1;
    float reroadtime = 0;
    bool oktrg = false;
    float loadtime = 0;//視点切り替えトリガー
    Vector3 vec;
    GameObject P;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        anim = GameObject.Find("全体").GetComponent<Animator>();
        ps = P.GetComponent<player>();
        GManager.instance.WeaponID[gunNumber].bulletnumber = PlayerPrefs.GetInt("Wbn2" + GManager.instance.itemhand, GManager.instance.WeaponID[GManager.instance.itemhand].bulletnumber);
        audioS = GetComponent<AudioSource>();
        GManager.instance.WeaponID[gunNumber].gunmode = "Shoot!";
        oktrg = true;
        GManager.instance.Triggers[23] = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (loadtime > 0)
        {
            loadtime -= Time.deltaTime;
        }
        //-------------------
        if (GManager.instance.over == false && GManager.instance.walktrg == true)
        {
            //新要素、右クリックで覗く
            if (Input.GetMouseButtonDown(1) && GManager.instance.Triggers[23] == 0 && loadtime < 0.01f)
            {
                GManager.instance.Triggers[23] = 1;
                ps.subtrg = false;
                ps.c.transform.position = ps.c3.transform.position;
                ps.c.transform.rotation = ps.c3.transform.rotation;
                ps.c.transform.parent = ps.c3.transform;

            }
            else if (Input.GetMouseButtonUp(1) && GManager.instance.Triggers[23] == 1)
            {
                loadtime = 2;
                GManager.instance.Triggers[23] = 0;
                ps.subtrg = false;
                var mainP = ps.cmr.transform.position;
                mainP.y -= 0.06626f;
                mainP.z += 0.01756f;
                ps.c.transform.position = mainP;
                ps.c.transform.rotation = ps.cmr.transform.rotation;
                ps.c.transform.parent = ps.cmr.transform;
               
            }
            //--------------
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (GManager.instance.WeaponID[gunNumber].gunmode != "Reloading")
                {
                    audioS.Stop();
                    anim.Play("handR");
                    GManager.instance.WeaponID[gunNumber].bulletnumber = 0;
                    GManager.instance.WeaponID[gunNumber].gunmode = "Reloading";
                    GManager.instance.setrg = 5;
                }
            }
            //----ショット
            if (GManager.instance.WeaponID[gunNumber].bulletnumber > 0)
            {
                if (Input.GetMouseButton(0) && snipertrg == false && oktrg == true)
                {
                    oktrg = false;
                    GManager.instance.WeaponID[gunNumber].gunmode = "Cool time";
                    vec = (targetpos.transform.position - shotpos.transform.position).normalized;
                    Shot();
                }
                else if (snipertrg == true && oktrg == true)
                {
                    if (Input.GetMouseButtonDown(0) && GManager.instance.Triggers[23] == 0)
                    {
                        GManager.instance.Triggers[23] = 1;
                        ps.subtrg = false;
                        ps.c.transform.position = ps.c3.transform.position;
                        ps.c.transform.rotation = ps.c3.transform.rotation;
                        ps.c.transform.parent = ps.c3.transform;
                    }
                    else if (Input.GetMouseButtonUp(0) && GManager.instance.Triggers[23] == 1)
                    {
                        GManager.instance.Triggers[23] = 0;
                        oktrg = false;
                        GManager.instance.WeaponID[gunNumber].gunmode = "Cool time";
                        ps.subtrg = false;
                        var mainP = ps.cmr.transform.position;
                        mainP.y -= 0.06626f;
                        mainP.z += 0.01756f;
                        ps.c.transform.position = mainP;
                        ps.c.transform.rotation = ps.cmr.transform.rotation;
                        ps.c.transform.parent = ps.cmr.transform;
                        vec = targetpos.transform.position - shotpos.transform.position;
                        
                        Shot();
                    }
                }
                if (Input.GetMouseButtonUp(0) && rifletrg == true)
                {
                    audioS.Stop();
                }
            }
            //----
            if (oktrg == false)
            {
                shottime += Time.deltaTime;
            }
            if (shottime > GManager.instance.WeaponID[gunNumber].shotmaxtime && GManager.instance.WeaponID[gunNumber].gunmode != "Shoot!" && GManager.instance.WeaponID[gunNumber].bulletnumber != 0)
            {
                shottime = 0;
                oktrg = true;
                GManager.instance.WeaponID[gunNumber].gunmode = "Shoot!";
            }
            if (GManager.instance.WeaponID[gunNumber].bulletnumber < 1)
            {
                reroadtime += Time.deltaTime;
                if (reroadtime > GManager.instance.WeaponID[gunNumber].roadmaxtime)
                {
                    reroadtime = 0;
                    shottime = 0;
                    oktrg = false;
                    GManager.instance.Triggers[23] = 0;
                    GManager.instance.setrg = 6;
                    anim.Play("handG");
                    GManager.instance.WeaponID[gunNumber].bulletnumber = GManager.instance.WeaponID[gunNumber].maxbulletnumber;
                    GManager.instance.WeaponID[gunNumber].gunmode = "Reload completed";
                }
            }
        }

    }

    void Shot()
    {

        //anim.Play("handA");
        GManager.instance.WeaponID[gunNumber].bulletnumber -= 1;
        audioS.PlayOneShot(shotse);
        if (systemsenumber != -1)
        {
            GManager.instance.setrg = systemsenumber;
        }
        Instantiate(GManager.instance.shoteffect, shotpos.transform.position, P.transform.rotation);
        float shotSpeed = GManager.instance.WeaponID[gunNumber].bulletspeed;
        for (int i = 0; i < shotnumber; i++)
        {
            vec.Normalize();
            vec = Quaternion.Euler(-angleminnumberx + ((angleminnumberx * xangleaddx) * i), -angleminnumbery + ((angleminnumbery * xangleaddy) * i), -angleminnumberz + ((angleminnumberz * xangleaddz) * i)) * vec;
            //vec = Quaternion.Euler(0, 0, 176 + (8 * i)) * vec;
            vec *= shotSpeed;
            if (Bullet != null)
            {
                var t = Instantiate(Bullet, shotpos.transform.position, P.transform.rotation);

                t.GetComponent<Rigidbody>().velocity = vec;
            }
        }
        if (GManager.instance.WeaponID[gunNumber].bulletnumber < 1)
        {
            anim.Play("handR");
            if (rifletrg == true)
            {
                audioS.Stop();
            }
            GManager.instance.WeaponID[gunNumber].gunmode = "Reloading";
            GManager.instance.setrg = 5;
        }
    }

    void AnimR()
    {
        anim.Play("handG");
    }

}
