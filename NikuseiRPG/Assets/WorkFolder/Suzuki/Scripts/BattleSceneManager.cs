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
    [SerializeField,Tooltip("バトルの進行管理用のテキスト")]
    Text battleText;
    [SerializeField, Tooltip("EnemyListを入れる")]
    EnemyList enemyList;
    public void Start()
    {
        // 遭遇した敵の情報をEnemyListから貰い、敵の名前をテキストに描画
        battleText.text = enemyList.EnemyParamList[0].enemyName;
    }
}
