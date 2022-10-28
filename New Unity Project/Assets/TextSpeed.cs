using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeed : MonoBehaviour
{
    [SerializeField]
    private Text _messageText = null;

    //文字表示スピード
    [SerializeField, Range(0.01f, 0.5f)]
    private float _messageSpeed = 1f;

    private string _msg;
    // Start is called before the first frame update
    void Start()
    {
        //テキストに文字列を表示させる
        _messageText.text = _msg;
    }

    // Update is called once per frame
    void Update()
    {     //マウスの左クリックを押した
        if (Input.GetMouseButtonDown(0))
        {  //コルーチンで文字列を表記する
            StartCoroutine(MessageCo(_msg));
        }
    }

    /// <summary>
    /// 文字列を一定時間で順番に表示する
    /// </summary>
    /// <param name="msg">対象の文字列</param>
    /// <returns></returns>
    private IEnumerator MessageCo(string msg)
    {
        //表示するメッセージを格納
        var message = "";
        //先頭からの文字数
        var index = 0;
        //　先頭からの文字数が表示対象異常になるまで繰り返す
        while (msg.Length > index)
        {
            //拾う文字列を加算する
            index++;
            //対象の文字列から拾う文字数分切り出す
            message = msg.Substring(0, index);
            //テキストに表示する
            _messageText.text = message;
            //　指定の秒数待機する
            yield return new WaitForSeconds(_messageSpeed);
        }
    }
}