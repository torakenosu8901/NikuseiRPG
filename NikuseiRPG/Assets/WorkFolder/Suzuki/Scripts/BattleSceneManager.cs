using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    #region 変数一覧
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
    [Tooltip("プレイヤーの行動を管理する変数")]
    private int playerMove = 0;
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
    #endregion
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
        if (battlePhase)
        {
            switch (playerMove)
            {
            //-----------プレイヤーが攻撃を選択した場合--------------
                case 0:
                    if (whoseTurnIsIt)
                    {
                        // 敵の方が早い場合
                        EnemyAttack();
                        KillConfirmationPlayer();
                        PlayerAttack();
                        KillConfirmationEnemy();
                    }
                    else
                    {
                        // プレイヤーの方が早い場合
                        PlayerAttack();
                        KillConfirmationEnemy();
                        EnemyAttack();
                        KillConfirmationPlayer();
                    }
                    break;
            //----------プレイヤーが道具を選択した場合-------------
                case 1:
                    //---------------------------------------
                    //   ここにアイテムの効果の反映の処理を書く |
                    //---------------------------------------
                    EnemyAttack();
                    KillConfirmationPlayer();
                    break;
            //----------プレイヤーがスキルを選択した場合-----------
                case 2:
                    if (whoseTurnIsIt)
                    {
                        // 敵の方が早い場合
                        EnemyAttack();
                        KillConfirmationPlayer();
                        switch(skillNumber)
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
                        KillConfirmationPlayer();
                    }
                    break;
            //----------プレイヤーが逃げるを選択した場合-----------
                case 3:
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
                        KillConfirmationPlayer();
                    }
                    break;
            }

        }
        else
        {
            // プレイヤーの勝利
            if (winOrLose)
            {
                battleText.text =  enemyName + "を倒した！";
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
    #region 関数一覧
    //----------敵を倒したかの判定の関数--------------
    public void KillConfirmationEnemy()
    {
        if (enemyNp <= 0)
        {
            battlePhase = false;
        }
    }
    //--------プレイヤーが死んだかの判定の関数---------
    public void KillConfirmationPlayer()
    {
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
    #endregion
}
//battleText.text = string.Format("{000}",enemyNp);
