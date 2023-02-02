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
            //�R���[�`���Ăяo��
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
           //���R���[�`���ɕK�{�̏���
        yield return null;//���̃t���[���܂őҋ@
        }
                          //��Ɖ��̏��������݂ɍs�����Ƃ��ł���
        StartCoroutine(PlayerMove2());
    }
    IEnumerator PlayerMove2()
    {
        float elapsedTime = 0;

        while (elapsedTime < 0.1f)
        {
            this.transform.Translate(new Vector3(-1.0f, 0, 0));

            elapsedTime += Time.deltaTime;
            //���R���[�`���ɕK�{�̏���
            yield return null;//���̃t���[���܂őҋ@
        }
       
    }

}
