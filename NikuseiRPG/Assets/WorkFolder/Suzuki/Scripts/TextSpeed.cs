using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextSpeed : MonoBehaviour
{
    [SerializeField]
    private Text battleText;
    //文字表示スピード
    [SerializeField, Range(0.01f, 0.2f)]
    private float _messageSpeed = 0.1f;
    public string _msg;
    [Tooltip("テキストの進行管理用")]
    public bool textControl = false;
    //----------------シングルトンを後で入れてね------------
    public static TextSpeed Instance = null;
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
    public void EnemyName()
    {
        // 遭遇した敵の情報をEnemyListから貰い、敵の名前をテキストに描画
        _msg = BattleSceneManager.Instance.enemyName + "に遭遇した！！";
        StartCoroutine(MessageCo(_msg));
    }
    public void StartTextCoroutine()
    {
        StartCoroutine(MessageCo(_msg));
    }
    private IEnumerator MessageCo(string msg)
    {
        var message = "";
        //先頭からの文字数
        var index = 0;
        //　先頭からの文字数が表示対象異常になるまで繰り返す
        textControl = false;
        while (msg.Length > index)
        {
            textControl = false;
            //拾う文字列を加算する
            index++;
            //対象の文字列から拾う文字数分切り出す
            message = msg.Substring(0, index);
            //テキストに表示する
            battleText.text = message;
            //　指定の秒数待機する
            yield return new WaitForSeconds(_messageSpeed);
        }
        textControl = true;
    }
    public void PlayerAtkText()
    {
        _msg = BattleSceneManager.Instance.enemyName + "に"
        + BattleSceneManager.Instance.damage + "ダメージ与えた";
        StartTextCoroutine();
    }
    public void EnemyAtkText()
    {
       _msg = BattleSceneManager.Instance.damage + "ダメージ受けた";
       StartTextCoroutine();
    }
}