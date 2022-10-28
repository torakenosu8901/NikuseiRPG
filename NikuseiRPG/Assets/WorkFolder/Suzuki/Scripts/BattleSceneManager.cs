using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    //--------------------変数の宣言-----------------------
    [SerializeField, Tooltip("バトルの進行管理用のテキスト")]
    private Text battleText;
    [SerializeField, Tooltip("EnemyListを入れる")]
    private EnemyList enemyList;
    [Tooltip("戦闘中かどうかを判定")]
    private bool battlePhase = true;
    [Tooltip("勝敗判定")]
    private bool winOrLose = true;
    [Tooltip("先攻後攻の判定")]
    private bool whoseTurnIsIt;
    [Tooltip("受けるダメージ")]
    private int damage;
    [Tooltip("プレイヤーが逃れるかの変数")]
    private int playerFlee;
    [Tooltip("スキルを番号で管理するための変数")]
    private int skillNumber;
    // [Tooltip("敵の番号を受け取る")]
    // private int enemyNumber;
    //-----------仮置きのプレイヤーのステータス----------
    public int np = 3;
    public int atk = 3;
    public int agi = 2;
    public int def = 0;
    //------------------敵のステータス------------------
    private string enemyName;
    private int enemyNp;
    private int enemyAtk;
    private int enemyAgi;
    private int enemyDef;
    //private int enemyLv;
    //------------------シングルトン--------------------
    public static BattleSceneManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void Start()
    {
        // 敵の情報を設定
        //enemyNumber = ????
        //                                   ↓ここを後でenemyNumberに変える多分
        enemyName = enemyList.EnemyParamList[0].enemyName;
        enemyNp = enemyList.EnemyParamList[0].np;
        enemyAtk = enemyList.EnemyParamList[0].atk;
        enemyAgi = enemyList.EnemyParamList[0].agi;
        enemyDef = enemyList.EnemyParamList[0].def;
        // enemyLv = enemyList.EnemyParamList[0].lv;
        // 遭遇した敵の情報をEnemyListから貰い、敵の名前をテキストに描画
        battleText.text = enemyName + "に遭遇した！！";
        AgiComparison();
    }
    public void Update()
    {
        // 戦闘開始
        if (!battlePhase)
        {
            // プレイヤーの勝利
            if (winOrLose)
            {
                battleText.text = enemyName + "を倒した！";
                //---------------------------------------
                //   シーン遷移の処理をここに書く
                //---------------------------------------
            }
            // プレイヤーの敗北
            else
            {
                battleText.text = "GAMEOBERA";
                //---------------------------------------
                //   シーン遷移の処理をここに書く
                //---------------------------------------
            }
        }
    }
    //----------死んだかの判定の関数--------------
    public void KillConfirmation()
    {
        if (enemyNp <= 0)
        {
            battlePhase = false;
        }
        if (np <= 0)
        {
            battlePhase = false;
            // どちらかが勝ったかの判定
            winOrLose = false;
        }
    }
    //----プレイヤーと敵どっちが早いか判定する関数------
    public void AgiComparison()
    {
        if (enemyAgi >= agi)
        {
            whoseTurnIsIt = true;
        }
        else
        {
            whoseTurnIsIt = false;
        }
    }
    //------------敵が通常攻撃してくる関数-------------
    public void EnemyAttack()
    {
        damage = enemyAtk - def;
        np -= damage;
        battleText.text = damage + "ダメージ受けた";
        damage = 0;
    }
    //----------プレイヤーが通常攻撃する関数-----------
    public void PlayerAttack()
    {
        damage = atk - enemyDef;
        enemyNp -= damage;
        battleText.text = enemyName + "に" + damage + "ダメージ与えた";
        damage = 0;
    }

    public void AttackPhase()
    {
        if (whoseTurnIsIt)
        {
            // 敵の方が早い場合
            EnemyAttack();
            KillConfirmation();
            PlayerAttack();
            KillConfirmation();
        }
        else
        {
            // プレイヤーの方が早い場合
            PlayerAttack();
            KillConfirmation();
            EnemyAttack();
            KillConfirmation();
        }
    }

    public void SkillPhase()
    {
        if (whoseTurnIsIt)
        {
            // 敵の方が早い場合
            EnemyAttack();
            KillConfirmation();
            switch (skillNumber)
            {
                case 0:
                    break;
            }
        }
        else
        {
            // プレイヤーの方が早い場合
            switch (skillNumber)
            {
                case 0:
                    break;
            }
            EnemyAttack();
            KillConfirmation();
        }
    }

    public void ItemPhase()
    {
        //---------------------------------------
        //   ここにアイテムの効果の反映の処理を書く |
        //---------------------------------------
        EnemyAttack();
        KillConfirmation();
    }

    public void EscapePhase()
    {
        playerFlee = Random.Range(1, 6);
        if (1 == playerFlee)
        {
            battleText.text = enemyName + "から逃げれた";

            //---------------------------------------
            //   シーン遷移の処理をここに書く
            //---------------------------------------
        }
        else
        {
            battleText.text = "逃げきれなかった";
            EnemyAttack();
            KillConfirmation();
        }
    }
}
//battleText.text = string.Format("{000}",enemyNp);