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
        //���U������ӏ���if����菜���ē��Ă͂߂Ă�������B
        if (Input.GetKeyUp(KeyCode.P))
        {         
            //�R���[�`���Ăяo��
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
