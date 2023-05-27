using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timespawn : MonoBehaviour
{
    public bool offwalk = true;
    public Transform parePos = null;
    
    public float inputtime;
    private float gettime;
    private bool spawntrg = false;
    public GameObject obj;
    [Header("nullじゃなかったら発射")] 
    public Transform targetPos = null;
    public float shotspeed = 32;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawntrg == false)
        {
            gettime += Time.deltaTime;
            if(gettime > inputtime )
            {
                GManager.instance.walktrg = offwalk;
                if (parePos == null)
                {
                    Instantiate(obj, transform.position, transform.rotation);
                }
                else if (parePos != null)
                {
                    if (targetPos == null)
                    {
                        Instantiate(obj, parePos.transform.position, parePos.transform.rotation, parePos.transform);
                    }
                    else if(targetPos != null)
                    {
                        Vector3 vec = targetPos.position - parePos.position;
                        vec.Normalize();
                        vec = Quaternion.Euler(0, 0, 0) * vec;
                        vec *= shotspeed;
                        var t = Instantiate(obj, parePos.transform.position, parePos.transform.rotation);
                        t.GetComponent<Rigidbody>().velocity = vec;
                    }
                }
                spawntrg = true;
            }
        }
    }
}
