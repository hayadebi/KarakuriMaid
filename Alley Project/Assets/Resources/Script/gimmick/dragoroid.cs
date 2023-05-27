using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class dragoroid : MonoBehaviour
{
    //【とあるボスのスクリプト】
    //大半の部分は他のモンスターでも使いまわしてる

    public objAngle ang;//ボスの視点回転スクリプトを取得
    //技の範囲
    public int maxrandom = 5;
    public int minrandom = 1;
    bool resettrg = false;//状態をリセットさせるかどうか

    [Header("1はスタートタイム、4はイベントリセット")] public float[] time; //各時間
    public int ontrg = 0; //技の進行状態
    [Header("0はボスの位置")] public Transform[] Bpos; //各位置
    public int eventnumber = 0; //現在の技
    public AudioClip[] Ase; //各効果音
    public Vector3[] target; //各対象
    public Renderer ren; //画面に映っているかに使う
    enemyS objE; //敵スクリプトを取得
    [Header("2は覚醒時のエフェクト")] public GameObject[] Bullet; //各技などのオブジェクト
    [Header("2までデフォルトで使ってる")] public bool[] trg; //各トリガー
    int oldevent = 0; //前に使った技(同じ技を連続で発動させないために比較させる)
    Rigidbody rb;
    GameObject p;//プレイヤー取得用
    public GameObject secondObj = null;//第二形態
    [Header("触らなくていい")] public int wariaihp = 420;//第二形態、または強化が発動する条件のHP
    bool stoptrg = false;
    //Fungus(会話イベントのアセット)
    private bool isTalking;
    private Flowchart flowChart;
    public string message = "second";
    // Start is called before the first frame update
    void Start()
    {
        //開始時にリセットや取得、計算をする
        flowChart = this.GetComponent<Flowchart>();
        rb = this.GetComponent<Rigidbody>();
        p = GameObject.Find("Player");
        objE = this.GetComponent<enemyS>();
        eventnumber = Random.Range(minrandom, maxrandom);
        oldevent = eventnumber;
        if (GManager.instance.mode == 0)
        {
            wariaihp = objE.Estatus.health / 3 * 2 / 2;
        }
        else if (GManager.instance.mode == 1)
        {
            wariaihp = objE.Estatus.health / 3 * 3 / 2;
        }
        else if (GManager.instance.mode == 2)
        {
            wariaihp = objE.Estatus.health / 3 * 4 / 2;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!trg[0] && GManager.instance.bossbattletrg == 1)//開始から数秒後にボスを動かすためのタイマー
        {
            time[0] += Time.deltaTime;
            if (time[0] > time[1])
            {
                trg[0] = true;
                time[0] = 0;
                objE.Estatus.speed = GManager.instance.Pstatus.speed + 1f;
            }
        }
        else if (trg[0])//動けるなら
        {
            if (wariaihp > objE.Estatus.health && !trg[1] && !trg[2])//強化させるHPの条件を満たすなら
            {
                eventnumber = -1;
                trg[1] = true;
                trg[2] = true;
                objE.Eanim.SetInteger("Anumber", 0);
                if (!stoptrg)
                {
                    rb.velocity = Vector3.zero;
                }
                objE.Estatus.defence += 4;
                minrandom = -1;
                maxrandom = 9;
                objE.secondMode = true;
                //会話イベントからの第二形態へ
                StartCoroutine(Talk());
            }
            if (resettrg && minrandom != -1)//リセットさせる状態なら
            {
                resettrg = false;
                ontrg = 0;
                trg[1] = false;
                objE.audioS.Stop();
                //技をランダムに選択して、前の技と被らなくなるまで繰り返す
                eventnumber = Random.Range(minrandom, maxrandom);
                //同じ技を発動しないように
                for (int i = oldevent; i == eventnumber;)
                {
                    eventnumber = Random.Range(minrandom, maxrandom);
                }
                oldevent = eventnumber;//決定後、前の技を上書きする
            }
            if (!GManager.instance.over && GManager.instance.walktrg && !objE.deathtrg && !trg[1] && minrandom != -1)
            {
                //リセット用のタイマーが0より大きいなら、0以下になるまでタイマーを減らす。
                //0以下になったら、リセットする準備をする
                if (time[4] > 0)
                {
                    time[4] -= Time.deltaTime;
                    if (time[4] <= 0 )
                    {
                        time[4] = 0;
                        Eventreset();
                    }
                }
                //条件を満たしているなら、該当する技を呼び出す
                if (GManager.instance.bossbattletrg == 1 && p != null && eventnumber != -1)
                {
                    Invoke("Event" + eventnumber, 0f);
                }
            }
        }
    }
    //ここからはボスの技
    void Event1()
    {
        if (ontrg == 0)//ontrgは技の進行状態
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            Invoke("Ev1_1", 0.15f);
        }
    }
    void Ev1_1()
    {
        objE.audioS.PlayOneShot(Ase[2]);
        Instantiate(Bullet[3], Bpos[3].position, Bullet[3].transform.rotation);
        Instantiate(Bullet[3], Bpos[4].position, Bullet[3].transform.rotation);
        Invoke("Ev1_2", 2f);
    }
    void Ev1_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 2f;
    }

    void Event2()
    {
        var newpos = p.transform.position;
        if (ontrg == 0)
        {
            ontrg = 1;
            newpos.y = this.transform.position.y;
            time[6] = Vector3.Distance(this.transform.position, newpos);
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev2_1", 1f);
        }
        else if (ontrg == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newpos, time[6] / 60f);
        }
    }
    void Ev2_1()
    {
        if (ontrg == 1)
        {
            ontrg = 2;
            objE.audioS.PlayOneShot(Ase[0]);
            GManager.instance.setrg = 28;
            Instantiate(Bullet[1], Bpos[1].transform.position, Bullet[1].transform.rotation);
            for (int i = 0; i < 6;)
            {
                Vector3 vec = Bpos[2].forward * 2;
                vec.Normalize();
                vec = Quaternion.Euler(0, 0 + ((60 * 1) * i), 0) * vec;
                vec *= 24;
                var t = Instantiate(Bullet[0], Bpos[2].position, Bullet[0].transform.rotation);
                t.GetComponent<Rigidbody>().velocity = vec;
                i++;
            }
            Invoke("Ev2_2", 1f);
        }
    }
    void Ev2_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.3f;
    }

    //広範囲に火炎弾を飛ばす技イベント
    void Event3()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 4);//ボスのアニメーション
            Invoke("Ev3_1", 0.4f);
        }
    }
    //ここからボスが広範囲攻撃する
    void Ev3_1()
    {
        //エフェクトなどの演出
        objE.audioS.PlayOneShot(Ase[1]);
        Instantiate(GManager.instance.shoteffect, Bpos[3].position, GManager.instance.shoteffect.transform.rotation);

        //プレイヤーに向けて20度ごとに火炎弾を
        for (int i = 0; i < 9;)
        {
            var pp = p.transform.position;
            pp.y += 0.3f;
            Vector3 vec = pp - Bpos[3].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, -90 + ((20 * 1) * i), 0) * vec;
            vec *= 28;
            var t = Instantiate(Bullet[3], Bpos[3].position, Bullet[3].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            i++;
        }
        Invoke("Ev3_2", 1f);
    }
    //技終了処理
    void Ev3_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 1.13f;
    }

    void Event4()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 5);
            GManager.instance.setrg = 29;
            Invoke("Ev4_1", 1f);
        }
    }
    void Ev4_1()
    {
        ontrg = 2;
        objE.audioS.PlayOneShot(Ase[2]);
        Vector3 upP = p.transform.position;
        upP.y = 21;
        Instantiate(Bullet[4], upP, Bullet[4].transform.rotation);
        Invoke("Ev4_2", 1f);
    }
    void Ev4_2()
    {
        ontrg = 3;
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 2f;
    }
    void Event5()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev5_1", 0.45f);
        }
    }
    void Ev5_1()
    {
        objE.audioS.PlayOneShot(Ase[1]);
        Instantiate(GManager.instance.shoteffect, Bpos[4].position, GManager.instance.shoteffect.transform.rotation);

        for (int i = 0; i < 3;)
        {
            var pp = p.transform.position;
            pp.y += 0.3f;
            Vector3 vec = pp - Bpos[4].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, -30 + ((30 * 1) * i), 0) * vec;
            vec *= 24;
            var t = Instantiate(Bullet[5], Bpos[4].position, Bullet[5].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
            i++;
        }
        Invoke("Ev5_2", 1f);
    }
    void Ev5_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 2f;
    }

    void Event6()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 2);
            objE.audioS.PlayOneShot(Ase[3]);
            Invoke("Ev6_1", 1f);
        }
    }
    void Ev6_1()
    {
        ontrg = 2;
        objE.audioS.PlayOneShot(Ase[2]);
        Vector3 upP = p.transform.position;
        upP.y = 21;
        Instantiate(Bullet[4], upP, Bullet[4].transform.rotation);
        for(int i = 0;i < 2;)
        {
            Vector3 upR = p.transform.position;
            upR.x = Random.Range(Bpos[5].transform.position.x, Bpos[6].transform.position.x);
            upR.z = Random.Range(Bpos[5].transform.position.z, Bpos[6].transform.position.z);
            upR.y = 21;
            GameObject rock = Instantiate(Bullet[4], upR, Bullet[4].transform.rotation);
            rock.GetComponent<useG>().gravity = Random.Range(16, 40);
            i++;
        }
        Invoke("Ev6_2", 1.1f);
    }
    void Ev6_2()
    {
        ontrg = 3;
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 3f;
    }
    void Event7()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.Eanim.SetInteger("Anumber", 3);
            objE.audioS.PlayOneShot(Ase[4]);
            Invoke("Ev7_1", 1.1f);
        }
    }
    void Ev7_1()
    {
        objE.audioS.PlayOneShot(Ase[5]);
        Instantiate(Bullet[6], Bpos[4].position, Bullet[6].transform.rotation);
            var pp = p.transform.position;
            pp.y += 0.16f;
            Vector3 vec = pp - Bpos[4].position;
            vec.Normalize();
            vec = Quaternion.Euler(0, 0, 0) * vec;
            vec *= 32;
            var t = Instantiate(Bullet[7], Bpos[4].position, Bullet[7].transform.rotation);
            t.GetComponent<Rigidbody>().velocity = vec;
        Invoke("Ev7_2", 2f);
    }
    void Ev7_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        time[4] = 3f;
    }
    void Event8()
    {
        if (ontrg == 0)
        {
            ontrg = 1;
            objE.audioS.PlayOneShot(Ase[6]);
            ang.enabled = false;
            objE.Eanim.SetInteger("Anumber", 1);
            Invoke("Ev8_1", 0.4f);
        }
    }
    void Ev8_1()
    {
        objE.audioS.PlayOneShot(Ase[7]);
        Instantiate(Bullet[8], Bpos[4].position, Bpos[4].rotation,Bpos[4].transform);
        Invoke("Ev8_2", 2f);
    }
    void Ev8_2()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        ang.enabled = true;
        time[4] = 2f;
    }
    //リセットの準備
    void Eventreset()
    {
        if (!trg[1])
        {
            GameObject[] ats = GameObject.FindGameObjectsWithTag("bossA");
            if (ats.Length != 0)
            {
                foreach (GameObject at in ats)
                {
                    Destroy(at.gameObject);
                }
            }
            GameObject[] bl = GameObject.FindGameObjectsWithTag("bullet");
            if (bl.Length != 0)
            {
                foreach (GameObject at in bl)
                {
                    if (at.GetComponent<enemyTrigger>())
                    {
                        Destroy(at.gameObject);
                    }
                }
            }
            objE.Eanim.SetInteger("Anumber", 0);
            resettrg = true;
        }
    }
    //会話イベントの開始処理
    public IEnumerator Talk()
    {
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        //会話終了
        //第二形態になったら切り替えるやつ
        secondStart();
    }
    //ここからは第二形態に移行するためのイベント
    void secondStart()
    {
        Instantiate(Bullet[2], p.transform.position, Bullet[2].transform.rotation);
        Invoke("second1", 0.3f);
    }
    void second1()
    {
        secondObj.SetActive(true);
        objE.Eanim = secondObj.GetComponent<Animator>();
        objE.audioS = secondObj.GetComponent<AudioSource>();
        this.GetComponent<objAngle>().enabled = false;
        this.tag = "Untagged";
        GameObject[] scobjs = GameObject.FindGameObjectsWithTag("second");
        foreach (GameObject scobj in scobjs)
        {
            scobj.SetActive(false);
        }
        secondObj.transform.parent = this.transform;
        secondObj.GetComponent<enemyS>().damageOn = false;
        Invoke("second2", 0.3f);
    }
    void second2()
    {
        objE.Eanim.SetInteger("Anumber", 2);
        objE.audioS.PlayOneShot(Ase[3]);
        Invoke("secondEnd", 2.1f);
    }
    void secondEnd()
    {
        objE.Eanim.SetInteger("Anumber", 0);
        secondObj.GetComponent<enemyS>().damageOn = true;
        minrandom = 5;
        resettrg = true;
    }
}
