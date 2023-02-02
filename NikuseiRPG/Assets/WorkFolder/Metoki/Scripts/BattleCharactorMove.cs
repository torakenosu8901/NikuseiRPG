using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharactorMove : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {         
            //コルーチン呼び出し
            StartCoroutine(PlayerMove());
        }
    }
    IEnumerator PlayerMove()
    {
        float elapsedTime = 0;

        while (elapsedTime < 0.1f)
        {
            this.transform.Translate(new Vector3(1.0f, 0, 0));

            elapsedTime += Time.deltaTime;
           //↓コルーチンに必須の処理
        yield return null;//次のフレームまで待機
        }
                          //上と下の処理を交互に行うことができる
        StartCoroutine(PlayerMove2());
    }
    IEnumerator PlayerMove2()
    {
        float elapsedTime = 0;

        while (elapsedTime < 0.1f)
        {
            this.transform.Translate(new Vector3(-1.0f, 0, 0));

            elapsedTime += Time.deltaTime;
            //↓コルーチンに必須の処理
            yield return null;//次のフレームまで待機
        }
       
    }

}
