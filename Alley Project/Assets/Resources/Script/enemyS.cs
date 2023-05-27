using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
public class enemyS : MonoBehaviour
{
    [Header("第二形態があるかどうか")] public bool secondMode = false;
    [Header("別のスクリプトから効果付与")] public int neweffect = 0;
    [Header("ダメージを受ける状態か")]public bool damageOn = true;
    public bool knockbacktrg = true;//ノックバックのするかどうか
    public int movetrgnumber = -1;
    public int addEvent = 0;
    [Header("1=執事のコピーが倒された")]public int KillEvent = -1;
    public AudioClip bgm;//倒された後にBGMを戻すためにセット(ボス用)
    public Renderer  ren;//映っているかどうか確認用
    public int Limited = -1;//一定の数値になってると開始時に消す(イベントで1回しか登場させないために)
    public int wariai = 0;//ボスHPの割合
    int playeroldhp = 0;
    public float maxbakusanP = 0.4f;//爆散の勢い
    public int maxbakusanN = 32;//やられる時にどれくらいの経験値を爆散させるか
    [System.Serializable]
    public struct boss
    {
        //ボス関連の設定
        public string message;
        public bool bosstrg;
        public string canvasname;
        public int eventnumber;
        public GameObject BTend_objOn;
        public bool endtrg;
    }
    public boss BossTrigger;
    //会話関係
    Flowchart flowChart;
    bool isTalking = false;
    //--------
    public string enemyName;//敵の名前
    public GameObject partobj;
    //ドロップ関係
    public GameObject dropItem;
    public GameObject dropItem2;
    public GameObject dropItem3;
    public GameObject rareItem;
    public int rareNumber = 2;
    bool Dtrg = false;
    public bool easyDestroy = false;
    //ダメージ関係
    public bool isDamage = false;
    public bool damagetrg = false;
    public bool deathtrg = false;
    [System.Serializable]
    public struct status
    {
        //敵のステータス
        public float speed;
        public int health;
        public int attack;
        public int defence;
    }
    public status Estatus;
    public Animator Eanim;//敵のアニメーションをセット
    //敵のサウンド関係
    public AudioClip deathse;
    public  AudioClip damagese;
    public AudioSource audioS;
    private int inputattack;
    [Header("触る必要はない")]public int inputdamage =1;//受けたダメージ(途中でダメージ計算に使う)
    //---------------------------
    //異常状態の変数
    private GameObject flameobj = null;
    private bool flametrg = false;
    private GameObject poisonobj = null;
    private bool poisontrg = false;
    private float poisontime;
    private float poisondamage;
    private float flametime = 0;
    private float damagetime = 0;
    private float frosttime = 0;
    private int efevNumber = -1;
    private GameObject frostobj = null;
    [Header("状態異常で動き止める変数※いじらないで")] public bool stoptrg = false;
    [Header("ダメージを受けたよというトリガー")] public bool hitdamagetrg = false;
    [Header("強制キルトリガー")] public bool inputkilltrg = false;
    [Header("完全停止")] public bool absoluteStop = false;
    public GameObject bosskilloffobj = null;
    // Start is called before the first frame update
    void Start()
    {
        //ボスHPの割合が設定されてる場合は、プレイヤーステータスに応じてボスステータス変化
        if (playeroldhp != GManager.instance.Pstatus.maxHP && wariai != 0)
        {
            playeroldhp = GManager.instance.Pstatus.maxHP;
            int inputhpdamage = 0;
            inputhpdamage = (GManager.instance.Pstatus.maxHP + 10) / 10;
            inputhpdamage *= wariai;
            Estatus.attack = inputhpdamage + GManager.instance.Pstatus.defense;
        }
        if (BossTrigger.bosstrg)//ボスなら会話イベントを用意
        {
            flowChart = this.GetComponent<Flowchart>();
        }
        //難易度に応じて敵ステータスを変化
        //0はイージー、1はノーマル、2はハード
        if (GManager.instance.mode == 0)
        {
            Estatus.attack = ((Estatus.attack + 3) / 3) * 2;
            Estatus.health = ((Estatus.health + 3) / 3) * 2;
            if (easyDestroy )//イージーモードでは出現させない敵を消す
            {
                Destroy(gameObject);
            }
        }
        else if (GManager.instance.mode == 2)
        {
            Estatus.attack = ((Estatus.attack + 3) / 3) * 4;
            Estatus.health = ((Estatus.health + 3) / 3) * 4;
            maxbakusanN += 4;
        }
        //※
        //1回限りの敵(ボス、宝箱)がもうやられている場合は消す
        if (Limited != -1 && GManager.instance.Triggers[Limited] > 0)
        {
            if (BossTrigger.BTend_objOn != null)
            {
                BossTrigger.BTend_objOn.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //位置が範囲外なら消す
        if (this.transform.position.y < -16 || this.transform.position.y > 1600 || this.transform.position.x < -1600 || this.transform.position.x > 1600 || this.transform.position.z < -1600 || this.transform.position.z > 1600)
        {
            Destroy(gameObject);
        }
        else if (absoluteStop == false)//強制停止状態ではないなら
        {
            //HPが1未満なら止める
            if (partobj != null && partobj.GetComponent<enemyS>().Estatus.health < 1)
            {
                Animator partanim = partobj.GetComponent<Animator>();
                if (partanim.enabled )
                {
                    partanim.enabled = false;
                }
                partobj = null;
                Rigidbody rb = this.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
            }
            if (playeroldhp != GManager.instance.Pstatus.maxHP && wariai != 0)
            {
                playeroldhp = GManager.instance.Pstatus.maxHP;
                int inputhpdamage = 0;
                inputhpdamage = GManager.instance.Pstatus.maxHP / 10;
                if (inputhpdamage < 1)
                {
                    inputhpdamage = 1;
                    Estatus.attack = inputhpdamage + GManager.instance.Pstatus.defense;
                }
                else if (inputhpdamage > 0)
                {
                    inputhpdamage *= wariai;
                    Estatus.attack = inputhpdamage + GManager.instance.Pstatus.defense;
                }
            }
            if (stoptrg && this.GetComponent<Rigidbody>().velocity != Vector3.zero)//止まってる時に動きも停止
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            if (!GManager.instance.over)
            {
                if (inputkilltrg )//強制的に消す場合
                {
                    inputkilltrg = false;
                    killSet();
                }

                //異常状態
                if (neweffect == 1 && !stoptrg)//氷Lv5
                {
                    neweffect = 0;
                    efevNumber = 5;
                    frostobj = Instantiate(GManager.instance.skillobj[5], this.transform.position, this.transform.rotation, this.transform);
                    Destroy(frostobj.gameObject, 10f);
                    stoptrg = true;
                }
                if (flametrg)//燃焼
                {
                    flametime += Time.deltaTime;
                    damagetime += Time.deltaTime;
                    if (flametime > 4)
                    {
                        flametime = 0;
                        damagetime = 0;
                        flametrg = false;
                    }
                    else if (damagetime > 1)
                    {
                        damagetime = 0;
                        inputdamage = 1;
                        isDamage = true;
                    }
                }
                if (stoptrg && efevNumber == 3)//氷
                {
                    frosttime += Time.deltaTime;
                    if (frosttime > 2f)
                    {
                        frosttime = 0;
                        efevNumber = -1;
                        stoptrg = false;
                    }
                }
                else if (stoptrg && efevNumber == 4)
                {
                    frosttime += Time.deltaTime;
                    if (frosttime > 4f)
                    {
                        frosttime = 0;
                        efevNumber = -1;
                        stoptrg = false;
                    }
                }
                else if (stoptrg && efevNumber == 5)
                {
                    frosttime += Time.deltaTime;
                    if (frosttime > 8f)
                    {
                        frosttime = 0;
                        efevNumber = -1;
                        stoptrg = false;
                    }
                }
                if (poisontrg)//毒
                {
                    poisontime += Time.deltaTime;
                    poisondamage += Time.deltaTime;
                    if (poisontime > 10)
                    {
                        poisontime = 0;
                        poisondamage = 0;
                        poisontrg = false;
                    }
                    else if (poisondamage > 1)
                    {
                        poisondamage = 0;
                        if (Estatus.health > 1)
                        {
                            inputdamage = 1;
                            isDamage = true;
                        }
                    }
                }
                //-------------------
                if (deathtrg )//やられている場合は止める
                {
                    Rigidbody rb = this.GetComponent<Rigidbody>();
                    float Yspeed = 0;
                    Yspeed += -4;
                    rb.velocity = new Vector3(0, Yspeed, 0);
                }
                //ここからダメージ処理
                else if (isDamage  && !deathtrg )
                {
                    isDamage = false;
                    //※
                    if (partobj == null && GManager.instance.walktrg && !deathtrg && !damagetrg)
                    {
                        if (!Dtrg)
                        {
                            Dtrg = true;
                            //ダメージ計算し、HPを操作する
                            inputattack = Estatus.attack;
                            Estatus.attack = 0;
                            if (ren == null || ren.isVisible || secondMode)
                            {
                                if (1 > (inputdamage - Estatus.defence))
                                {
                                    Estatus.health -= 1;
                                }
                                else if (0 < (inputdamage - Estatus.defence))
                                {
                                    Estatus.health -= (inputdamage - Estatus.defence);
                                }
                            }
                            else if (!ren.isVisible)
                            {
                                Estatus.health -= 0;
                            }
                        }
                        if (Estatus.health > 0)
                        {
                            audioS.PlayOneShot(damagese);
                        }
                        else if (Estatus.health < 1)//HPが1未満になったらやられる処理を呼び出す
                        {
                            killSet();
                        }
                        //ダメージ処理をしたことにして、処理を終えるために呼び出す
                        damagetrg = true;
                        Invoke("Damage", 0.2f);
                    }
                    else if (partobj != null)//もしこのオブジェクトが何かのパーツなら、本体に与える
                    {
                        enemyS partS = partobj.GetComponent<enemyS>();
                        partS.inputdamage = inputdamage;
                        partS.isDamage = true;
                        Invoke("Damage", 0.2f);
                    }
                }
            }
        }
    }
    //やられる処理
    public void killSet()
    {
        deathtrg = true;
        if (KillEvent != -1)//死ぬ時発動する効果を呼び出す
        {
            Invoke("Kill_" + KillEvent, 0);
        }
        if (bgm != null)//敵スクリプトにステージ音楽がセットされてるなら、それをメインBGMにセットして再生
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
            if (!BossTrigger.endtrg)
            {
                bgmA.clip = bgm;
                bgmA.Play();
            }
        }
        if (Limited != -1)//一回限りの敵なら、その敵のトリガーをONにする
        {
            GManager.instance.Triggers[Limited] = 1;
        }
        //ボスかどうかでエフェクトを分けてる(派手さが違う)
        if (!BossTrigger.bosstrg)
        {
            Instantiate(GManager.instance.killeffect, this.transform.position, this.transform.rotation);
        }
        else if (BossTrigger.bosstrg )
        {
            Instantiate(GManager.instance.bosskilleffect, this.transform.position, this.transform.rotation);
        }
        //ドロップアイテムに関する
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, transform.rotation);
        }
        if (dropItem2 != null)
        {
            Instantiate(dropItem2, transform.position, transform.rotation);
        }
        if (dropItem3 != null)
        {
            Instantiate(dropItem3, transform.position, transform.rotation);
        }
        if (rareItem != null && Random.Range(1, (rareNumber + 1)) == rareNumber)
        {
            Instantiate(rareItem, transform.position, transform.rotation);
        }
        audioS.Stop();
        GManager.instance.monsterse = deathse;//死に際の声
        GManager.instance.setrg = 99;
        Instantiate(GManager.instance.Iexpobj, this.transform.position, this.transform.rotation);
        //敵の経験値を爆散
        Vector3 expP;
        for (int i = 0; i < maxbakusanN;)
        {
            float randomp = 0;
            randomp = Random.Range(-maxbakusanP, (maxbakusanP + 0.5f));
            expP.x = this.transform.position.x + randomp;
            randomp = Random.Range(0.1f, (maxbakusanP + 0.5f));
            expP.y = this.transform.position.y + randomp;
            randomp = Random.Range(-maxbakusanP, (maxbakusanP + 0.5f));
            expP.z = this.transform.position.z + randomp;
            Instantiate(GManager.instance.lvobj, expP, transform.rotation);
            i += 1;
        }
        //やられることによって出現するオブジェクトを操作
        if (!BossTrigger.bosstrg )
        {
            if (BossTrigger.BTend_objOn != null)
            {
                BossTrigger.BTend_objOn.SetActive(true);
            }
            Destroy(gameObject);
        }
        else if (BossTrigger.bosstrg)
        {
            if (ren != null)
            {
                ren.enabled = false;
            }
            if(bosskilloffobj != null)
            {
                this.transform.position = bosskilloffobj.transform.position;
                bosskilloffobj.SetActive(false);
            }
            StartCoroutine(Talk());//ボスの時は会話イベントも呼び出す
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        //ここの条件は、攻撃を受けているか AND 停止していないか AND 攻撃系のタグと接しているか
        if (damageOn && !secondMode && !absoluteStop && (collision.tag == "attack" || collision.tag == "attack2"))
        {
            if (GManager.instance.skillselect != -1)//スキルが発動してる時は、スキルゲージを操作する
            {
                GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar -= 1;
                GameObject hp = GameObject.Find("skillicon");
                if (hp != null)
                {
                    iTween.ShakePosition(hp.gameObject, iTween.Hash("x", 3.2f, "y", 3.2f, "time", 1.6f));
                }
            }
            hitdamagetrg = true;
            //状態異常
            if (collision.GetComponent<AddEffect>())
            {
                AddEffect ef = collision.GetComponent<AddEffect>();
                if (collision.GetComponent<AddDamage>() && collision.GetComponent<AddDamage>().nokill == true && Estatus.health + 1 < collision.GetComponent<AddDamage>().Damage - Estatus.defence)
                {
                    ;
                }
                else
                {
                    if (ef.effectnumber == 1 && knockbacktrg)//ノックバック
                    {
                        Vector3 velocity = -this.transform.forward * 30f;
                        velocity.y += 20f;
                        this.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
                    }
                    else if (ef.effectnumber == 2 && !flametrg)//燃焼
                    {
                        flameobj = Instantiate(GManager.instance.skillobj[4], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(flameobj.gameObject, 4);
                        flametrg = true;
                    }
                    else if (ef.effectnumber == 3 && !stoptrg)//氷Lv1
                    {
                        efevNumber = 3;
                        frostobj = Instantiate(GManager.instance.skillobj[5], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(frostobj.gameObject, 4f);
                        stoptrg = true;
                    }
                    else if (ef.effectnumber == 4 && !stoptrg)//氷Lv2
                    {
                        efevNumber = 4;
                        frostobj = Instantiate(GManager.instance.skillobj[5], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(frostobj.gameObject, 6f);
                        stoptrg = true;
                    }
                    else if (ef.effectnumber == 5 && !poisontrg)//毒
                    {
                        poisonobj = Instantiate(GManager.instance.skillobj[7], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(poisonobj.gameObject, 10);
                        poisontrg = true;
                    }
                    else if (ef.effectnumber == 6)//ランダム
                    {
                        int randomn = Random.Range(1, 5);
                        if (randomn == 1)
                        {
                            randomn = Random.Range(1, 5);
                            switch (randomn)
                            {
                                case 1:
                                    if (knockbacktrg)
                                    {
                                        Vector3 velocity = -this.transform.forward * 30f;
                                        velocity.y += 20f;
                                        this.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
                                    }
                                    break;
                                case 2:
                                    if (!flametrg)//燃焼
                                    {
                                        flameobj = Instantiate(GManager.instance.skillobj[4], this.transform.position, this.transform.rotation, this.transform);
                                        Destroy(flameobj.gameObject, 4);
                                        flametrg = true;
                                    }
                                    break;
                                case 3:
                                    if (!stoptrg)//氷Lv2
                                    {
                                        efevNumber = 4;
                                        frostobj = Instantiate(GManager.instance.skillobj[5], this.transform.position, this.transform.rotation, this.transform);
                                        Destroy(frostobj.gameObject, 6f);
                                        stoptrg = true;
                                    }
                                    break;
                                case 4:
                                    if (!poisontrg)//毒
                                    {
                                        poisonobj = Instantiate(GManager.instance.skillobj[7], this.transform.position, this.transform.rotation, this.transform);
                                        Destroy(poisonobj.gameObject, 10);
                                        poisontrg = true;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (ef.effectnumber == 7)//氷炎
                    {
                        if (!flametrg)//燃焼
                        {
                            flameobj = Instantiate(GManager.instance.skillobj[4], this.transform.position, this.transform.rotation, this.transform);
                            Destroy(flameobj.gameObject, 4);
                            flametrg = true;
                        }
                        if (!stoptrg)//氷Lv2
                        {
                            efevNumber = 4;
                            frostobj = Instantiate(GManager.instance.skillobj[5], this.transform.position, this.transform.rotation, this.transform);
                            Destroy(frostobj.gameObject, 6f);
                            stoptrg = true;
                        }
                    }
                    else if (ef.effectnumber == 8)//吸収
                    {
                        int randonnumber = Random.Range(1, 6);
                        if (randonnumber == 3)
                        {
                            var epos = this.transform.position;
                            epos.y += 2;
                            Instantiate(GManager.instance.skillobj[10], epos, GManager.instance.skillobj[10].transform.rotation);

                        }
                    }
                }
            }
            //--------
            if (collision.GetComponent<AddDamage>())//当たったオブジェクトにダメージスクリプトがあるなら、参照して加算
            {
                inputdamage = collision.GetComponent<AddDamage>().Damage;
            }
            else//当たったオブジェクトにダメージスクリプトが無いなら、プレイヤーステータスを参照して加算
            {
                inputdamage = (GManager.instance.Pstatus.attack + GManager.instance.WeaponID[GManager.instance.itemhand].upAttack);
            }
            if (collision.GetComponent<AddDamage>() && collision.GetComponent<AddDamage>().nokill && Estatus.health + 1 < collision.GetComponent<AddDamage>().Damage - Estatus.defence)
            {
                ;
            }
            else //想定外ではない場合は、ダメージ処理をできるようにする
            {
                isDamage = true;
            }
            Instantiate(GManager.instance.damageeffect, collision.transform.position, collision.transform.rotation);
            if (collision.tag == "attack")//貫通しないタイプの攻撃なら、それを消す
            {
                Destroy(collision.gameObject);
            }
        }
    }
    //ダメージ処理を終える処理
    void Damage()
    {
        Estatus.attack = inputattack;
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Dtrg = false;
        damagetrg = false;
    }

    //会話イベントの開始処理(ボスがやられたら)
    IEnumerator Talk()
    {
        //ボス戦を終了させる部分
        GameObject[] wall = GameObject.FindGameObjectsWithTag("wall");
        if(wall.Length != 0)
        {
            foreach (GameObject obj in wall)
            {
                Instantiate(GManager.instance.impacteffect, obj.transform.position, obj.transform.rotation);
                Destroy(obj.gameObject, 0);
            }
        }
        GManager.instance.walktrg = false;
        GManager.instance.bossbattletrg = 0;
        if (BossTrigger.canvasname != "")
        {
            GameObject canvas = GameObject.Find(BossTrigger.canvasname + "(Clone)");
            if (canvas != null)
            {
                Destroy(canvas.gameObject, 0);
            }
        }
        //ここからFungusアセットで会話イベント
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(BossTrigger.message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        //会話イベント終了処理
        GManager.instance.walktrg = true;
        if(addEvent != 0)
        {
            GManager.instance.EventNumber[BossTrigger.eventnumber] += addEvent;//指定のイベントに加算
        }
        //スキル獲得----
        GameObject[] skills = GameObject.FindGameObjectsWithTag("skill");
        if (skills.Length != 0 && skills != null)
        {
            foreach (GameObject skill in skills)
            {
                if (skill.GetComponent<skillGet>())
                {
                    skill.GetComponent<skillGet>().hole = true;
                }
            }
        }
        //--------------
        if(movetrgnumber != -1)
        {
            GManager.instance.Triggers[movetrgnumber] = 1;
        }
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Destroy");
        if (cubes != null && cubes.Length != 0)
        {
            foreach (GameObject cube in cubes)
            {
                // 消す！
                Destroy(cube);
            }
        }
        if (BossTrigger.BTend_objOn != null)
        {
            BossTrigger.BTend_objOn.SetActive(true);
        }
        if (!BossTrigger.endtrg)
        {
            Destroy(gameObject, 0.1f);
        }
        else if(BossTrigger.endtrg )
        {
            SceneManager.LoadScene("end");
        }
    }
    //やられたら発動するイベント
    void Kill_1()
    {
        if(this.GetComponent<butler>())
        {
            if (this.GetComponent<butler>().number[1] == 3 || this.GetComponent<butler>().number[1] == 5)
            {
                GameObject mainboss = GameObject.Find("BOSS5");
                butler mb = mainboss.GetComponent<butler>();
                mb.number[0] += 1;
            }
        }
    }

    private void OnParticleCollision(GameObject obj)
    {
        if(obj.GetComponent <player>())
        {
            obj.GetComponent<player>().ES = this.GetComponent<enemyS>();
            obj.GetComponent<player>().colobj = this.gameObject;
            obj.GetComponent<player>().OnDamage();
        }
    }
}
