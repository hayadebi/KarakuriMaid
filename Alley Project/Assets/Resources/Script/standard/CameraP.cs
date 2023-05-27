using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraP : MonoBehaviour
{
    protected Ray _ray;
    protected RaycastHit _rayhit;
    public float area;
    public GameObject C;
    public GameObject P;
    public GameObject[] target;
    int Tnumber = 0;
    private void Start()
    {
        
    }
    void Update()
    {
        Vector3 pos = C.transform.position - P.transform.position;
        _ray = new Ray(C.transform.position, - pos);
        if (Physics.Raycast(_ray, out _rayhit))
        {
            if (_rayhit.collider.tag != "Player")
            {
                // Ray が当たっている間は対象オブジェクトのシェーダを alphaShader にする
                if (target[Tnumber] == null)
                {
                    target[Tnumber] = _rayhit.collider.gameObject;
                }
                else if (target[Tnumber] != null)
                {
                        Tnumber += 1;
                        target[Tnumber] = _rayhit.collider.gameObject;
                    for (int i = 0;i < target.Length;)
                    {
                        if(target[Tnumber] == target[i])
                        {
                            target[Tnumber] = null;
                            Tnumber -= 1;
                        }
                        i++;
                    }
                }
                if (target[Tnumber].layer != 13)
                {
                    target[Tnumber].layer = 13;
                }
            }
            else
            {
                for (int i = 0; i < target.Length;)
                {
                    if (target[i] != null && target[i].layer == 13)
                    {
                        target[i].layer = 9;
                        target[i] = null;
                    }
                    i++;
                }
                Tnumber = 0;
                // Ray が当たっていない時は defaultShader に戻す
            }
        }
    }
}