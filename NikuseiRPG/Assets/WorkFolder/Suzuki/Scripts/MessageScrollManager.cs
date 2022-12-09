using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
//public class TextSpeed : MonoBehaviour
//クラスを汎用性の高いものに作り変える過程で名称を適切に変更する
public class MessageScrollManager : MonoBehaviour
{
    //[SerializeField]
    //private Text battleText;
    [SerializeField]
    private Text generalText;
    //文字表示スピード
    [SerializeField, Range(0.01f, 0.2f)]
    private float _messageSpeed = 0.1f;
    public string _msg;
    [Tooltip("テキストの進行管理用")]
    public bool textControl = true;

    private Coroutine coroutine = null;
    //----------------シングルトンを後で入れてね------------
    //public static TextSpeed Instance = null;
    public static MessageScrollManager Instance = null;
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

    //public void EnemyName()
    //{
    //    // 遭遇した敵の情報をEnemyListから貰い、敵の名前をテキストに描画
    //    _msg = BattleSceneManager.Instance.enemyName + "に遭遇した！！";
    //    StartCoroutine(MessageCo(_msg));
    //}

    //public void EnemyName()
    //{
    //    // 遭遇した敵の情報をEnemyListから貰い、敵の名前をテキストに描画
    //    _msg = BattleSceneManager.Instance.enemyName + "に遭遇した！！";
    //    StartCoroutine(MessageCo(_msg));
    //}

    /// <summary>
    /// 戦闘開始時のテキスト
    /// </summary>
    /// <param name="target">遭遇した敵の名前</param>
    public void EngageEnemyMessage(List<CharacterParam> target)
    {
        if(target.Count == 1)
        {
            StartCoroutine(MessageCo(target[0].name + " と遭遇した"));
        }
        else if(target.Count > 1)
        {
            StartCoroutine(MessageCo(target[0].name + "達 と遭遇した"));
        }
        else
        {
            StartCoroutine(MessageCo("何かがいたような気がしたが…気のせいだった"));
        }
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

    /// <summary>
    /// 入れた文字列を一文字ずつ表示する処理
    /// </summary>
    /// <param name="msg">テキストに表示したい文字列</param>
    /// <returns></returns>
    public IEnumerator MessageCo(string msg)
    {
        //Inputフレーム回避
        yield return null;

        //全体で変更があった場合こっちも変える
        ChangeMessageSpeed(0.1f);
        var message = "";
        //先頭からの文字数
        var index = 0;
        //　先頭からの文字数が表示対象異常になるまで繰り返す
        textControl = true;
        while (msg.Length > index)
        {
            //拾う文字列を加算する
            index++;
            //対象の文字列から拾う文字数分切り出す
            message = msg.Substring(0, index);
            //テキストに表示する
            generalText.text = message;
            var m = message[message.Length - 1];

            if (m == '\n')
            {               
                //全体で変更があった場合こっちも変える
                ChangeMessageSpeed(0.1f);
                textControl = false;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            }
            else if(msg.Length == index)
            {
                //全体で変更があった場合こっちも変える
                ChangeMessageSpeed(0.1f);
                textControl = false;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            }
            else
            {
                textControl = true;
                //　指定の秒数待機する
                yield return new WaitForSeconds(_messageSpeed);
            }
            yield return null;
        }
        //全体で変更があった場合こっちも変える
        ChangeMessageSpeed(0.1f);
        textControl = true;
    }

    //public void PlayerAtkText()
    //{
    //    _msg = BattleSceneManager.Instance.enemyName + "に"
    //    + BattleSceneManager.Instance.damage + "ダメージ与えた" + "\n" 
    //    + BattleSceneManager.Instance.damage + "ダメージ受けた";
    //    StartTextCoroutine();
    //}
    //public void EnemyAtkText()
    //{
    //    _msg = BattleSceneManager.Instance.damage + "ダメージ受けた" + "\n" 
    //       + BattleSceneManager.Instance.enemyName + "に"
    //    + BattleSceneManager.Instance.damage + "ダメージ与えた";
    //    StartTextCoroutine();
    //}

    /// <summary>
    /// 使用するテキストボックスを更新する
    /// </summary>
    /// <param name="obj">更新先のテキストボックス</param>
    public void GeneralTxtBoxUpData(Text text)
    {
        generalText = text;
    }

    /// <summary>
    /// 攻撃が成功したときの処理をテキストに起こす(*外島)
    /// </summary>
    /// <param name="actor">行動主のパラメーター</param>
    /// <param name="target">対象者のパラメーター</param>
    /// <param name="damage">行動主が対象者に与えたダメージ</param>
    public IEnumerator CharacterAttackMessage(CharacterParam actor, CharacterParam target, int damage)
    {
        if(actor.type == CharacterType.Player)
        {
            yield return StartCoroutine(MessageCo(actor.name + " が " + target.name + " に\n" + damage + " のダメージを与えた"));
        }
        else if (actor.type == CharacterType.Enemy)
        {
            yield return StartCoroutine(MessageCo(target.name + " が " + actor.name + " から\n" + damage + " のダメージを受けた"));
        }

        yield break;
    }

    /// <summary>
    /// メッセージのスクロールスピードを変更する関数
    /// </summary>
    /// <param name="speed">変更後のスクロールスピード</param>
    public void ChangeMessageSpeed(float speed)
    {
        _messageSpeed = speed;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && textControl)
        {
            textControl = false;
            ChangeMessageSpeed(0.0f);
        }
    }
}