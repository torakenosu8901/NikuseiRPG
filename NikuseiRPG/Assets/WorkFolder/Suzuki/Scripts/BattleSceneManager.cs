using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    // シングルトン
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
    [SerializeField, Tooltip("バトルの進行管理用のテキスト")]
    private Text battleText;
    [SerializeField, Tooltip("EnemyListを入れる")]
    private EnemyList enemyList;
    [Tooltip("戦闘中かどうかを判定")]
    private bool battlePhase = true;
    [Tooltip("勝敗判定")]
    private bool winOrLose = true;
    /*
    [Tooltip("敵の番号を受け取る")]
    private int enemyNumber;
    */

    //--仮置きのプレイヤーのステータス--
    [SerializeField]
    int np = 3;
    // int atk = 3;
    //int agi = 2;
    //  int def = 0;
    //--------------------------------
    // 敵のステータス
    private string enemyName;
    private int enemyNp;
    private int enemyAtk;
    private int enemyAgi;
    private int enemyDef;
    private int enemyLv;

    public void Start()
    {
        // 敵の情報を設定
        //                                   ↓ここを後でenemyNumberに変える多分
        enemyName = enemyList.EnemyParamList[0].enemyName;
        enemyNp = enemyList.EnemyParamList[0].np;
        enemyAtk = enemyList.EnemyParamList[0].atk;
        enemyAgi = enemyList.EnemyParamList[0].agi;
        enemyDef = enemyList.EnemyParamList[0].def;
        enemyLv = enemyList.EnemyParamList[0].lv;

        // 遭遇した敵の情報をEnemyListから貰い、敵の名前をテキストに描画
     　 battleText.text = enemyName + "に遭遇した！！";
    }
    public void Update()
    {
        
        // 戦闘開始
        if (battlePhase)
        {
            // 戦闘終了判定
            if (enemyNp <= 0)
            {
                battlePhase = false;
            }
            else if (np <= 0)
            {
                battlePhase = false;
                // どちらかが勝ったかの判定
                winOrLose = false;
            }
        }
        else
        {
            // プレイヤーの勝利
            if(winOrLose)
            {
                battleText.text = enemyName + "を倒した！";
            }
            // プレイヤーの敗北
            else
            {
                battleText.text = "GAMEOBERA";
            }
        }
    }
}
//battleText.text = string.Format("{000}",enemyNp);
