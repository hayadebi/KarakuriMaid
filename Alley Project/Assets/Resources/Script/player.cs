using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class player : MonoBehaviour
{
    //【今後修正しながら使っていくプレイヤースクリプトの原型】

    [Header("停止気にするな")] public bool stoptrg = false;//優先度高めにプレイヤーを停止させるトリガー
    public float overhight = 9999;//ゲームオーバーになる高さ
    private bool highttrg = false;//高さでゲームオーバーにさせるかどうか
    //private bool jumpr = false;
    public enemyS ES;//敵の情報を取得する用
    public GameObject colobj;//当たったオブジェクトを格納
    float knockspeed = 16f;//ノックバックスピード
    [Header("重力適応用")]public  bool jumptrg = false;//ジャンプトリガー
    public float spacetime = 0;//スペースキー押してる時間確認
    public float nogroundtime = 0;//地面に接していない時間
    public int jumpmode = 0;//ジャンプ時のモード、それに応じて上昇や下降をする
    public bool isGround = true;//地面に接してるか
    public float maxjump;//最大ジャンプ力
    public float maxhight = 4;//最大ジャンプの高さ※優先度低め
    public float jumpspeed = 16;//ジャンプスピード
    public float errorjumptime = 0;//ジャンプ時に予期せぬことが起きた時用
    public float gravity = 32;//重力値
    //public bool downtrg = false;
    Vector3 startjumppos;//ジャンプ時の位置
    Vector3 oldjumppos;//開始時の位置
    public Transform character;//プレイヤーに対応するトランスフォーム
    public Transform body;//プレイヤーのモデルに対応するトランスフォーム
    public bool o8removetrg = true;//酸素トリガー
    public bool lostevent = false;//負けイベントかどうか
    float o8time = 0;//酸素の時間
    private bool damagetrg = false;//ダメージトリガー
    private bool movetrg = false;//移動時にアニメーションさせるかどうか
    [System.Serializable]
    public struct animstring
    {
        public string numbername;//アニメーターの変数名
    }
    public animstring AnimName;
    //移動で加算させるxyzそれぞれの値
    public float xSpeed = 0;
    public float ySpeed = 0;
    public float zSpeed = 0;
    public GameObject overUI;//ゲームオーバーUI
    //プレイヤーのサウンド関係
    public AudioClip walkse;
    public AudioClip groundse;
    private AudioSource audioSource;

    private string redTag = "red";

    public Animator anim;//プレイヤーのアニメーションセット
    Rigidbody rb;//プレイヤーの物理挙動をセット
    //カメラ関係の取得
    public GameObject c;
    public Camera cm;
    public GameObject c2;
    public Camera cm2;
    GameObject csr;
    public GameObject c3;
    public Camera cm3;
    public GameObject cmr;
    private Vector3 offset;
    public bool subtrg = false;
    //回転に使う値
    float X_Rotation = 0;
    float Y_Rotation = 0;
    private int damage = 1;//受けたダメージを格納
    public int NoWaterLayer = 15;//水の上かどうか用
    private int ColLayer = 9;
    //状態異常関連の変数
    private bool flametrg = false;
    private GameObject flameobj = null;
    private float flametime = 0;
    private float damagetime = 0;
    private GameObject poisonobj = null;
    private bool poisontrg = false;
    private float poisontime;
    private float poisondamage;
    private float frosttime = 0;
    private int efevNumber = -1;
    private GameObject frostobj = null;
    public int nextjump = 0;
    public bool startjump = false;
    // Start is called before the first frame update

    void Start()
    {
        //カメラ関係のリセット、取得
        GManager.instance.Triggers[23] = 0;
        c = GameObject.Find("MainC");
        cm = c.GetComponent<Camera>();
        c2 = GameObject.Find("SubC");
        cm2 = c2.GetComponent<Camera>();
        csr = GameObject.Find("CSR");
        c3 = GameObject.Find("snC");
        cm3 = c3.GetComponent<Camera>();
        cmr = GameObject.Find("CMR");
        cm.depth = 0;
        cm2.depth = -1;
        cm3.depth = -1;
        if (!cm.enabled)
        {
            cm.enabled = true;
        }
        if (cm2.enabled )
        {
            cm2.enabled = false;
        }
        if (cm3.enabled )
        {
            cm3.enabled = false;
        }
        offset = csr.transform.position - this.transform.position;
        cm.fieldOfView = GManager.instance.siya;
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        //セーブしてある位置情報を取得し、プレイヤーのスポーン地点を決定する
        GManager.instance.posX = PlayerPrefs.GetFloat("posX", 0f);
        GManager.instance.posY = PlayerPrefs.GetFloat("posY", 0f);
        GManager.instance.posZ = PlayerPrefs.GetFloat("posZ", 0f);
        Vector3 ppos = this.transform.position;
            ppos.x = GManager.instance.posX;
            ppos.y = GManager.instance.posY;
        ppos.z = GManager.instance.posZ;
        this.transform.position = ppos;
        anim.SetInteger(AnimName.numbername, 1);
        ySpeed -= 160;
        oldjumppos = this.transform.position;
        Invoke("startJump",0.3f);
    }
    //ジャンプできる状態にする
    void startJump()
    {
        startjump = true;
    }

    //視点回転に関する
    float maxYAngle = 135f;
    float minYAngle = 45f;
    //接してるタグについて
    private bool untagtrg = false;

    void FixedUpdate()
    {
        //条件は、ゲームオーバーではないか AND 動ける状態か AND UIが開いていないか
        if (!GManager.instance.over && GManager.instance.walktrg && GManager.instance.setmenu == 0)
        {
            //移動してない時の重力操作
            if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !jumptrg && rb.useGravity == false)
            {
                ySpeed -= gravity;
                rb.useGravity = true;
            }
            else if(rb.useGravity)//重力がある状態
            {
                if (isGround  || untagtrg)//簡単に言えば地面に接しているかどうか
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || jumptrg)
                    {
                        ySpeed = 0;
                        rb.useGravity = false;
                    }
                }
            }
            //異常状態
            if (flametrg)//燃焼
            {
                flametime += Time.deltaTime;
                damagetime += Time.deltaTime;
                if (flametime > 3)
                {
                    flametime = 0;
                    damagetime = 0;
                    flametrg = false;
                }
                else if (damagetime > 1)
                {
                    damagetime = 0;
                    damage = 1;
                    OnDamage();
                }
            }
            if (poisontrg)//毒
            {
                poisontime += Time.deltaTime;
                poisondamage += Time.deltaTime;
                if (poisontime > 5)
                {
                    poisontime = 0;
                    poisondamage = 0;
                    poisontrg = false;
                }
                else if (poisondamage > 1)
                {
                    poisondamage = 0;
                    if (GManager.instance.Pstatus.health > 1)
                    {
                        damage = 1;
                        OnDamage();
                    }
                }
            }
            else if (stoptrg && efevNumber == 4)//氷
            {
                frosttime += Time.deltaTime;
                if (frosttime > 1f)
                {
                    frosttime = 0;
                    efevNumber = -1;
                    stoptrg = false;
                    startjump = true;
                }
            }
            //-------------
        }
        //メニュー画面出現
        if (GManager.instance.setmenu < 1 && GManager.instance.walktrg && Input.GetKeyDown(KeyCode.Escape) && !stoptrg)
        {
            GameObject m = GameObject.Find("menu(Clone)");
            GManager.instance.ESCtrg = false;
            GManager.instance.walktrg = true;
            jumpmode = 0;
            jumptrg = false;
            nextjump = 0;
            ySpeed = 0;
            spacetime = 0;
            nogroundtime = 0;
            if (m == null)
            {
                GManager.instance.setmenu += 1;
                GManager.instance.walktrg = false;
                GManager.instance.setrg = 16;
                GameObject ui = (GameObject)Resources.Load("menu");
                Instantiate(ui, transform.position, transform.rotation);
            }
        }
        //マウスカーソル切り替え
        if (Input.GetMouseButtonDown(2) && Cursor.visible )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonDown(2) && !Cursor.visible )
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //--------------------------------------
        
        if (GManager.instance.walktrg  && !GManager.instance.over && !stoptrg)
        {
            //視野設定
            if (cm.fieldOfView != GManager.instance.siya)
            {
                cm.fieldOfView = GManager.instance.siya;
            }
            //視点移動
            //マウスのX,Y軸がどれほど移動したかを取得
            X_Rotation = 0;
            Y_Rotation = 0;
            X_Rotation = Input.GetAxis("Mouse X");
            Y_Rotation = Input.GetAxis("Mouse Y");
            character.transform.Rotate(new Vector3(0, X_Rotation * GManager.instance.kando, 0));
            body.transform.Rotate(-Y_Rotation * GManager.instance.kando, 0, 0);

            //Y軸の設定
            float nowAngle = body.transform.localEulerAngles.x;
            if (minYAngle > nowAngle)
            {
                Vector3 rot = body.transform.localEulerAngles;
                rot.x = minYAngle;
                body.transform.localEulerAngles = rot;
            }
            else if (nowAngle > maxYAngle)
            {
                Vector3 rot = body.transform.localEulerAngles;
                rot.x = maxYAngle;
                body.transform.localEulerAngles = rot;
            }
            //酸素が無くなったらゲームオーバー
            if (o8removetrg)
            {
                o8time += Time.deltaTime;
                if (o8time > 4)
                {
                    o8time = 0;
                    if (GManager.instance.O8 > -0.1f)
                    {
                        GManager.instance.O8 -= 1;
                    }
                }
                if (GManager.instance.O8 < 0f)
                {
                    Instantiate(GManager.instance.killeffect, transform.position, transform.rotation);
                    GameOver();
                }
            }
            //落下ゲームオーバー
            if (overhight < 9999)
            {
                if (this.transform.position.y < overhight && !highttrg)
                {
                    highttrg = true;
                    Instantiate(GManager.instance.killeffect, transform.position, transform.rotation);
                    GameOver();
                }
            }
            //----
            if (!damagetrg)
            {
                //----ジャンプ----
                    spacetime = Input.GetAxis ("Jump");
                //予期せぬ挙動の対策
                if (jumptrg && !isGround)
                {
                    errorjumptime += Time.deltaTime;
                    if (errorjumptime > 4)
                    {
                        errorjumptime = 0;
                        jumpmode = 0;
                        jumptrg = false;
                        ySpeed = 0;
                        spacetime = 0;
                        nogroundtime = 0;
                        rb.useGravity = false;
                        isGround = true;
                    }
                }
                else if (isGround == true && oldjumppos.y < this.transform.position.y)
                {
                    errorjumptime += Time.deltaTime;
                    if (errorjumptime > 4)
                    {
                        errorjumptime = 0;
                        jumpmode = 0;
                        jumptrg = false;
                        ySpeed = 0;
                        spacetime = 0;
                        nogroundtime = 0;
                        rb.useGravity = false;
                        isGround = true;
                    }
                }
                //ジャンプ可能な場合
                if (isGround || jumpmode == 3 )
                {
                    if(startjump == true)
                    {
                        spacetime = 1;
                    }
                    //※
                    if (jumptrg == false && spacetime != 0 && isGround)//ジャンプをまだしてない場合、ジャンプを始める
                    {
                        jumptrg = true;
                        rb.useGravity = false;
                        nextjump = 1;
                        ySpeed = 0;
                        if (jumpmode != 3)
                        {
                            startjumppos = this.transform.position;
                        }
                        else if (jumpmode > 1 || untagtrg == true)
                        {
                            startjumppos.y = oldjumppos.y;
                        }
                        jumpmode = 2;
                        GManager.instance.setrg = 0;
                        movetrg = false;
                        audioSource.loop = false;
                        if (anim.GetInteger(AnimName.numbername) != 3)
                        {
                            anim.SetInteger(AnimName.numbername, 3);
                        }
                        else if (anim.GetInteger(AnimName.numbername) == 3)
                        {
                            anim.SetInteger(AnimName.numbername, 7);
                        }
                        audioSource.Stop();
                        ySpeed += spacetime * jumpspeed;//以前のジャンプ力じゃなかったらここで調整
                    }
                    //※
                    else if (jumptrg == true && jumpmode == 3 && isGround)//ジャンプ後の正常な着地
                    {
                        jumpmode = 0;
                        jumptrg = false;
                        errorjumptime = 0;
                        nextjump = 0;
                        ySpeed = 0;
                        spacetime = 0;
                        nogroundtime = 0;
                    }
                    else if (jumptrg == false && nogroundtime != 0)//想定外な着地
                    {
                        jumpmode = 0;
                        jumptrg = false;
                        errorjumptime = 0;
                        nextjump = 0;
                        ySpeed = 0;
                        spacetime = 0;
                        nogroundtime = 0;
                    }
                }
                if (!isGround && jumptrg == true)
                {
                    //スキル、2段ジャンプの部分
                    if (jumptrg == true && Input.GetKeyDown(KeyCode.Space) && nextjump == 1 && GManager.instance.skillselect == 10 && GManager.instance.stageNumber > 7)
                    {
                        nextjump = 2;
                        rb.useGravity = false;
                        errorjumptime = 0;
                        ySpeed = 0;
                            startjumppos = this.transform.position;
                        jumpmode = 2;
                        nogroundtime = 0;
                        GManager.instance.setrg = 35;
                        movetrg = false;
                        audioSource.loop = false;
                        if (anim.GetInteger(AnimName.numbername) != 3)
                        {
                            anim.SetInteger(AnimName.numbername, 3);
                        }
                        else if (anim.GetInteger(AnimName.numbername) == 3)
                        {
                            anim.SetInteger(AnimName.numbername, 7);
                        }
                        audioSource.Stop();
                        ySpeed += spacetime * jumpspeed;//以前のジャンプ力じゃなかったらここで調整
                    }
                    //----------
                    if (jumpmode == 2 && this.transform.position.y < startjumppos.y + (maxjump) && spacetime != 0 && nogroundtime < 1.3f)
                    {
                        ySpeed += spacetime * jumpspeed;//以前のジャンプ力じゃなかったらここで調整

                    }
                    //※
                    //想定していたジャンプ後の位置より高い場合は、ジャンプを終わらせる
                    else if (jumpmode == 2 && (this.transform.position.y > startjumppos.y + (maxjump - 1) || nogroundtime > 1.2f || spacetime == 0))
                    {
                        ySpeed -= gravity;
                        jumpmode = 3;
                    }
                    nogroundtime += Time.deltaTime;//地面に接していない時間を記録
                    if (jumpmode == 3)//mode 3はジャンプ終了を意味し、終わる間はプレイヤーを下降させる
                    {
                        ySpeed -= gravity;
                    }
                    //※
                    if (nogroundtime > 4 && anim.GetInteger(AnimName.numbername) != 2)//地面に接していない時間が想定より長い場合は、アニメーションを落下用にする
                    {
                        anim.SetInteger(AnimName.numbername, 2);
                    }
                }
                else if (!isGround && jumptrg == false)
                {
                    //また別パターンの想定外な場合は、強制的に終わらす
                    nogroundtime += Time.deltaTime;
                    if (nogroundtime != 0)
                    {
                        ySpeed -= gravity;
                    }
                    if (nogroundtime > 4)
                    {
                        if (anim.GetInteger(AnimName.numbername) != 2)
                        {
                            anim.SetInteger(AnimName.numbername, 2);
                        }
                    }
                }
                //正常な状態と想定外かを調べ、想定外な場合は強制的に下降させる
                if (this.transform.position.y < startjumppos.y + (maxjump ))
                {
                    ;
                }
                else if(!isGround || errorjumptime > 2 || nogroundtime > 2 || this.transform.position.y > maxhight)
                {
                    ySpeed -= gravity ;
                }
                //----ここからは移動----
                if ((!movetrg && !jumptrg) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
                {
                    //この部分では歩きの効果音、アニメーションを操作
                    movetrg = true;
                    if (ColLayer != NoWaterLayer)
                    {
                        audioSource.clip = walkse;
                    }
                    else if (ColLayer == NoWaterLayer)
                    {
                        audioSource.clip = groundse;
                    }
                    if (!jumptrg)
                    {
                        audioSource.loop = true;
                        audioSource.Play();
                    }
                    anim.SetInteger(AnimName.numbername, 2);
                }

                //移動メイン部分
                var inputX = Input.GetAxisRaw("Horizontal");
                var inputZ = Input.GetAxisRaw("Vertical");
                if (GManager.instance.skillselect == 3)//スキルによる移動強化
                {
                    inputX *= -1;
                    inputZ *= -1;
                }
                var tempVc = new Vector3(inputX, 0, inputZ);
                if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
                var vec = transform.right * tempVc.x + transform.forward * tempVc.z;
                var movevec = vec * GManager.instance.Pstatus.speed + Vector3.up * (ySpeed * 0.3f);//プレイヤーの移動速度などもここで
                //------------
                rb.velocity = movevec;
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !jumptrg)
                {
                    //移動してない場合、またはジャンプ中の時はアニメーションや音を止める
                    if (movetrg)
                    {
                        movetrg = false;
                    }
                    anim.SetInteger(AnimName.numbername, 1);
                    audioSource.loop = false;
                    audioSource.Stop();
                }
            }
        }
        else if (!GManager.instance.walktrg)//動けない場合は音を止め、物理的な動きも止める
        {
            audioSource.Stop();
            if(rb.velocity != Vector3.zero)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
    //ダメージ処理
    public void OnDamage()
    {
        if (!poisontrg &&!flametrg && !stoptrg)//状態異常ではない場合は、エフェクトをだし、ノックバックさせる
        {
            Instantiate(GManager.instance.damageeffect, colobj.transform.position, colobj.transform.rotation);
            rb.velocity = Vector3.zero;
            Vector3 velocity = -this.transform.forward * knockspeed;
            velocity.y += 0.64f;
            //風力を与える
            rb.AddForce(velocity, ForceMode.VelocityChange);
        }
        if (!damagetrg )//まだダメージ処理が終わってない場合
        {
            GameObject hp = GameObject.Find("HPbar");
            iTween.ShakePosition(hp.gameObject, iTween.Hash("x", 3.2f, "y", 3.2f, "time", 1.6f));
            if (1 > (damage - GManager.instance.Pstatus.defense))//ダメージ計算後、ダメージが1未満の場合は1ダメージ与える
            {
                GManager.instance.Pstatus.health -= 1;
            }
            else if (0 < (damage - GManager.instance.Pstatus.defense))//ダメージ計算後が0より大きい場合は、その分のダメージを与える
            {
                GManager.instance.Pstatus.health -= (damage - GManager.instance.Pstatus.defense);
            }
            if (colobj && colobj.tag == "bullet")//当たったオブジェクトが弾、投げものだった場合はそれを消す
            {
                    Destroy(colobj.gameObject, 0.1f);
            }
        }
        if (GManager.instance.Pstatus.health > 0 && !damagetrg)//HPが0より大きいなら効果音とアニメーション
        {
            GManager.instance.setrg = 1;
            anim.SetInteger(AnimName.numbername, 5);
        }
        else if (GManager.instance.Pstatus.health < 1)//HPが1未満なら死亡演出とゲームオーバーを呼び出す
        {
            Instantiate(GManager.instance.killeffect, transform.position, transform.rotation);
            GameOver();
        }
        if (!poisontrg && !flametrg )//状態異常によるダメージではないならダメージ処理とし、最後のダメージを呼び出す
        {
            damagetrg = true;
            Invoke("Damage", 1f);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        ColLayer = collision.gameObject.layer;
        //接してるのが地面のタグ、またはその他想定条件なら着地してる判定にする
        if (collision.tag == "ground" && !isGround)
        {
            isGround = true;
        }
        else if (collision.tag == "Untagged" && !isGround && collision.transform.position.y < this.transform.position.y && !untagtrg && (errorjumptime > 2 || nogroundtime > 2))
        {
            untagtrg = true;
            isGround = true;
        }
        //※
        if (!GManager.instance.over && GManager.instance.walktrg )
        {
            if (collision.gameObject.tag == redTag)//即死オブジェクトに触れてるなら強制的にゲームオーバー
            {
                Instantiate(GManager.instance.killeffect, transform.position, transform.rotation);
                GameOver();
            }
            if (collision.tag == "enemy" || collision.tag == "bullet")//敵、または弾に触れてるなら
            {
                //追加状態異常
                if (collision.GetComponent<AddEffect>() && collision.GetComponent<AddEffect>().enabled == true )//&& GManager.instance.skillselect == -1)
                {
                    AddEffect ef = collision.GetComponent<AddEffect>();
                    if (ef.effectnumber == 1)//ノックバック
                    {
                        Vector3 velocity = -this.transform.forward * 24f;
                        velocity.y += 0.8f;
                        this.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
                    }
                    else if(ef.effectnumber == 3 && GManager.instance.skillselect == -1)//反転操作
                    {
                        GManager.instance.skillnumber = 3;
                    }
                    else if (ef.effectnumber == 2 && !flametrg)//燃焼
                    {
                        flameobj = Instantiate(GManager.instance.skillobj[4], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(flameobj.gameObject, 4);
                        flametrg = true;
                    }
                    else if (ef.effectnumber == 5 && !poisontrg)//毒
                    {
                        poisonobj = Instantiate(GManager.instance.skillobj[7], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(poisonobj.gameObject, 8);
                        poisontrg = true;
                    }
                    else if (ef.effectnumber == 4 && !stoptrg)//氷Lv2
                    {
                        efevNumber = 4;
                        frostobj = Instantiate(GManager.instance.skillobj[5], this.transform.position, this.transform.rotation, this.transform);
                        Destroy(frostobj.gameObject, 3f);
                        stoptrg = true;
                    }
                    else if (ef.effectnumber == 7 && GManager.instance.skillselect == -1)//時限爆弾
                    {
                        GManager.instance.skillnumber = 7;
                    }
                    else if (ef.effectnumber == 9)//致命傷
                    {
                        GManager.instance.skillnumber = 11;
                    }
                }
                //-------------
                if (collision.GetComponent<AddDamage>())//ダメージスクリプトが接してるオブジェクトにあるなら、参照してダメージに加算
                {
                    damage = collision.GetComponent<AddDamage>().Damage;
                    if(GManager.instance.skillselect == 11 && GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar < 2)
                    {
                        damage = damage * 3/2;
                    }
                }
                else if(collision.GetComponent<enemyS>())//敵スクリプトが接してるオブジェクトにあるなら、参照してダメージに加算
                {
                    ES = collision.gameObject.GetComponent<enemyS>();
                    damage = ES.Estatus.attack;
                    if (GManager.instance.skillselect == 11 && GManager.instance.SkillID[GManager.instance.skillselect].inputskillbar < 2)
                    {
                        damage = damage * 3 / 2;
                    }
                }
                //スキルによる効果
                if(GManager.instance.skillselect == 10 && GManager.instance.stageNumber > 7 )
                {
                    damage = 1;
                }
                colobj = collision.gameObject;
                OnDamage();//ここでダメージ処理を呼び出す
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        //地面などに接しているなら地面判定にする
        if (col.tag == "ground" && !isGround)
        {
            isGround = true;
        }
        //※
        else if (col.tag == "Untagged" && !isGround && col.transform.position.y < this.transform.position.y && !untagtrg && (errorjumptime > 2 || nogroundtime > 2))
        {
            untagtrg = true;
            isGround = true;
        }
        //ジャンプ中で地面に接したら、着地状態へ
        else if (col.tag == "ground" && isGround && jumptrg && jumpmode == 2 && nogroundtime == 0 && spacetime == 0)
        {
            jumpmode = 0;
            jumptrg = false;
            ySpeed = 0;
            spacetime = 0;
            nogroundtime = 0;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        //地面などから離れたら地面判定を解除
        if (col.tag == "ground" )
        {
            untagtrg = false;
            isGround = false;
            startjump = false;
        }
        else if (col.tag == "Untagged" && untagtrg == true && spacetime != 0 && jumpmode < 3)
        {
            untagtrg = false;
            isGround = false;
            startjump = false;
        }
    }
    //ゲームオーバー処理
    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        c.gameObject.transform.parent = null;
        if (GManager.instance.bossbattletrg > 0)//ボス戦を終了
        {
            GManager.instance.bossbattletrg = 0;
        }
        GManager.instance.over = true;
        audioSource.Stop();
        GManager.instance.setrg = 4;
        
        Instantiate(GManager.instance.Iexpobj, this.transform.position, this.transform.rotation);
        //プレイヤーの経験値を爆散
        Vector3 expP;
        for(int i = 0; i < 32;)
        {
            float randomp = 0;
            randomp = Random.Range(-0.4f, 0.5f);
            expP.x = this.transform.position.x + randomp;
            randomp = Random.Range(-0.4f, 0.5f);
            expP.z = this.transform.position.z + randomp;
            randomp = Random.Range(0.1f, 0.5f);
            expP.y = this.transform.position.y + randomp;
            Instantiate(GManager.instance.lvobj, expP, transform.rotation);
            i += 1;
        }
        
        Instantiate(overUI, transform.position, transform.rotation);//ゲームオーバーのUIを表示
        Resources.UnloadUnusedAssets();
        Destroy(gameObject,0.1f);
    }
    //ダメージ処理を終了させる
    void Damage()
    {
        if (!GManager.instance.over)
        {
            anim.SetInteger(AnimName.numbername, 1);
            rb.velocity = Vector3.zero;
            damagetrg = false;
        }
    }
}
