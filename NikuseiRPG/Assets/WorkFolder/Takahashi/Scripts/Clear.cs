using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

    void Update()
    {
        //A�{�^������������V�[���ړ�
        if (Input.GetKeyDown("joystick button 0"))
        {
            //()�̒��g�̓��C���Q�[���̃V�[���������Ă�������
            SceneManager.LoadScene("Title");

            //�m�F�p
            Debug.Log("button0��������܂���");
        }
    }
}
