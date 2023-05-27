using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    //【カラクリメイドの中でもっとも重要なゲームマネージャー】

    public static GManager instance = null;
    public bool houseTrg = false;//室内かどうか
    public int isEnglish = 0;//言語の判定
    public bool walktrg = false;//動けるかどうか
    public int Coin = 0;//所持金
    public int bossbattletrg = 0;//ボス戦かどうか
    //----追加変数
    public float maxO8 = 100;
    public float O8 = 100;
    public bool ESCtrg = false;
    public string password = "";
    //----
    public bool over = false; //ゲームオーバーかどうか
    public int setmenu = 0; //UIの状態
    public int itemhand = 0; //持っているアイテムの状態
    public string txtget;
    public bool endtitle = false;
    public int[] EventNumber; //各イベントの状態
    public bool pushtrg = false;
    public float clearTime = 0f;
    public float clearTime2 = 0f;
    //セーブに使う、プレイヤーの位置情報用
    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;
    public int setrg = -1;
    public int stageNumber = 1;//現在のステージ
    //設定
    public float audioMax = 0.16f;//音量
    public int mode = 1; //難易度
    public float kando = 1; //感度
    public float siya = 60; //視野
    [System.Serializable]
    public struct item
    {
        //各アイテム情報
        public string itemname;
        public string itemscript;
        public RenderTexture itemimage;
        public int eventnumber;
        public int itemprice;
        public int itemnumber;
        public GameObject itemobj;
        public string itemname2;
        public string itemscript2;
        public int gettrg;
    }

    public item[] ItemID;
    [System.Serializable]
    public struct weapon
    {
        //各武器情報
        public string itemname;
        public string itemscript;
        public RenderTexture itemimage;
        public int upAttack;
        public int itemprice;
        public int getTrigger;
        public GameObject itemobj;
        public GameObject shotobj;
        public string gunmode;
        public float bulletspeed;
        public float shotmaxtime;
        public float roadmaxtime;
        public int maxbulletnumber;
        public int bulletnumber;
        public string itemname2;
        public string itemscript2;
        public int inputeventnumber;
        public int strengthen;
    }

    public weapon[] WeaponID;
    
    public int itemselect;//現在選択しているアイテム
    //ショップ関連
    public int[] shopID;
    public int[] shopNumber;
    public int[] shopID2;
    public int[] shopNumber2;
    [System.Serializable]
    public struct player
    {
        //プレイヤーステータス
        public int maxHP;
        public int health;
        public float speed;
        public int defense;
        public int attack;
        public int Lv;
        public int maxExp;
        public int inputExp;
    }
    public player Pstatus;
    [System.Serializable]
    public struct skill
    {
        //各スキル情報
        public string skillname;
        public string skillname2;
        public string bossname;
        public string bossname2;
        public RenderTexture bossimage;
        [Multiline]
        public string skillscript;
        [Multiline]
        public string skillscript2;
        [Multiline]
        public string bossscript;
        [Multiline]
        public string bossscript2;
        public int skillget;
        public int skillmaxbar;
        public int inputskillbar;
        public Sprite skillicon;
        public GameObject skillitem;
        public int buttonNumber;
        public bool notrg;
    }
    public skill[] SkillID;
    public int skillselect;//現在選択しているスキル
    public string skillsay;
    public bool Sgetsay;

    //今だと配列にして格納すれば良かったなと反省しているエフェクト達
    public GameObject Iexpobj;
    public GameObject lvobj;
    public GameObject damageeffect;
    public GameObject shoteffect;
    public GameObject killeffect;
    public GameObject bosskilleffect;
    public GameObject lvupeffect;
    public GameObject impacteffect;

    public AudioClip monsterse;
    public int animmode = -1;
    public int[] Triggers;//各トリガー
    public string SceneText;
    public GameObject spawnUI;
    public int skillnumber = -1;
    public GameObject[] skillobj;
    public bool expTargetTrg = false;
    public int reduction = 0;//画面効果の状態
    public AudioClip[] ase; //各汎用効果音

    public string sayobjname;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
   
}