using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homing : MonoBehaviour
{
    GameObject P = null;
    private bool starttrg = false;
    public float starttime = 1;
    public float speed = 8;
    AudioSource audioS;
    public AudioClip se = null;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        audioS = this.GetComponent<AudioSource>();
        Invoke("startOn", starttime);
    }
    void startOn()
    {
        starttrg = true;
        if(se != null)
        {
            audioS.PlayOneShot(se);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (P != null && starttrg == true)
        {
            Vector3 target = this.transform.forward * speed;
            this.GetComponent<Rigidbody>().velocity = target;
        }
    }
}
