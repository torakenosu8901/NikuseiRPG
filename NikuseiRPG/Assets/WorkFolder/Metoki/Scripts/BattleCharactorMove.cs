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
        //↓攻撃する箇所にif文取り除いて当てはめてくだされ。
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
            player.transform.Translate(new Vector3(1.0f, 0, 0));

            elapsedTime += Time.deltaTime;
          
        yield return null;
        }
        
        StartCoroutine(PlayerMove2());
    }
    IEnumerator PlayerMove2()
    {
        float elapsedTime = 0;

        while (elapsedTime < 0.1f)
        {
            player.transform.Translate(new Vector3(-1.0f, 0, 0));

            elapsedTime += Time.deltaTime;
          
            yield return null;
        }
       
    }

}
