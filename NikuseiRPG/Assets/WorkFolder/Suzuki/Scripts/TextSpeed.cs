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
    public bool textControl = true;

    private Coroutine coroutine = null;
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
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(MessageCo(_msg));
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
            var m = message[message.Length - 1];
            if (m == '\n')
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));
            }
            else
            {
                //　指定の秒数待機する
                yield return new WaitForSeconds(_messageSpeed);
            }           
        }
        //全体で変更があった場合こっちも変える
        ChangeMessageSpeed(0.1f);
        textControl = true;
    }
    public void PlayerAtkText()
    {
        _msg = BattleSceneManager.Instance.enemyName + "に"
        + BattleSceneManager.Instance.damage + "ダメージ与えた" + "\n" 
        + BattleSceneManager.Instance.damage + "ダメージ受けた";
        StartTextCoroutine();
    }
    public void EnemyAtkText()
    {
        _msg = BattleSceneManager.Instance.damage + "ダメージ受けた" + "\n" 
           + BattleSceneManager.Instance.enemyName + "に"
        + BattleSceneManager.Instance.damage + "ダメージ与えた";
        StartTextCoroutine();
    }

    public void ChangeMessageSpeed(float speed)
    {
        _messageSpeed = speed;
    }
}