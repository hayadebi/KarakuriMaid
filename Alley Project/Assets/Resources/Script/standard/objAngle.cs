using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objAngle : MonoBehaviour
{
    //この作品の頃はif文など…一部無駄に長く書いていますが現在は改善されているので気にしないで。

    public bool allangle = false;//全方位回転するか
    public bool startangle = false;//開始時に回転するか
    public float limitangle = -1;//回転の制限
    GameObject P;//プレイヤー
    enemyS es = null;//自分の敵スクリプト
    bool movetrg = true;//動けるかどうか
    public bool ptrg = false;//プレイヤーに付けているか
    public GameObject[] targetobj = null;//向く先の対象達
    public int indextarget = -1;//外部から参照するのに使う
    // Start is called before the first frame update
    void Start()
    {
        es = this.GetComponent<enemyS>();
        if(es != null && this.GetComponent <Collider >())//敵ではない場合動きを制限する
        {
            movetrg = false;
        }
        P = GameObject.Find("Player");
        if (P != null && startangle == true)//今見るともっと簡略に書けるねと思う開始時のみの回転部分
        {
            if (indextarget == -1)
            {
                var rotation = Quaternion.LookRotation(P.transform.position - this.transform.position);

                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
            if (indextarget != -1 && targetobj != null)
            {
                var rotation = Quaternion.LookRotation(targetobj[indextarget].transform.position - this.transform.position);

                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (P != null && startangle == false && movetrg == true && ptrg == false)//今見るともっと簡略に書けるねと思う、イベント時以外常に回転させる部分
        {
            if (indextarget == -1)
            {
                var rotation = Quaternion.LookRotation(P.transform.position - this.transform.position);
                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
            if (indextarget != -1 && targetobj != null)
            {
                if (targetobj[indextarget] != null)
                {
                    var rotation = Quaternion.LookRotation(targetobj[indextarget].transform.position - this.transform.position);
                    if (allangle == false)
                    {
                        rotation.x = 0;
                        rotation.z = 0;
                    }
                    this.transform.rotation = rotation;
                    //変換
                    if (limitangle != -1)
                    {
                        float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                        if (rotateY > limitangle)
                        {
                            Vector3 er = this.transform.localEulerAngles;
                            er.y = limitangle;
                            this.transform.localEulerAngles = er;
                        }
                        else if (rotateY < -limitangle)
                        {
                            Vector3 er = this.transform.localEulerAngles;
                            er.y = -limitangle;
                            this.transform.localEulerAngles = er;
                        }
                    }
                }
            }
        }
        else if (P != null && startangle == false && movetrg == true && ptrg == true && GManager.instance.Triggers[23] != 1)
        {
            if (indextarget == -1)
            {
                var rotation = Quaternion.LookRotation(P.transform.position - this.transform.position);
                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
            if (indextarget != -1 && targetobj != null)
            {
                var rotation = Quaternion.LookRotation(targetobj[indextarget].transform.position - this.transform.position);
                if (allangle == false)
                {
                    rotation.x = 0;
                    rotation.z = 0;
                }
                this.transform.rotation = rotation;
                //変換
                if (limitangle != -1)
                {
                    float rotateY = (this.transform.localEulerAngles.y > 180) ? this.transform.localEulerAngles.y - 360 : this.transform.localEulerAngles.y;
                    if (rotateY > limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = limitangle;
                        this.transform.localEulerAngles = er;
                    }
                    else if (rotateY < -limitangle)
                    {
                        Vector3 er = this.transform.localEulerAngles;
                        er.y = -limitangle;
                        this.transform.localEulerAngles = er;
                    }
                }
            }
        }
    }

    //範囲内かどうかによって動きを制限させる
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "area" && es != null && movetrg == false)
        {
            // 表示されている場合の処理
            movetrg = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "area" && es != null && movetrg == false)
        {
            // 表示されている場合の処理
            movetrg = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "area" && es != null && movetrg == true)
        {
            // 表示されている場合の処理
            movetrg = false;
        }
    }
}
