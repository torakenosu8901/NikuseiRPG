using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    public Canvas canvas;
   
    private void Start()
    {
        //started�͉������u�Ԃ����@�\�����Ȃ�
        InputManager.instance.InputSystem.Player.Pause.started += PrintPause;
        //canseled�͗������u�Ԃɋ@�\����
        InputManager.instance.InputSystem.Player.Pause.canceled -= PrintPause;
    }

    private void Update()
    {
        //�� SetActive���y��
        //  �������Aenabled���g���Ȃ��^������B(GameObject�^)
        // Canvas��\��
        //Text.txt;
        if(Input.GetKey(KeyCode.D))
        {
            canvas.enabled = false;
            //txt.enabled = false;
        }
        // Canvas�\��
        if(Input.GetKey(KeyCode.E))
        {
            canvas.enabled = false;
        }
        //Junp�ɐݒ肳�ꂽ�{�^���������ꂽ�Ƃ�
        if(InputManager.instance.InputSystem.Player.Junp.IsPressed() == true)
        {
            Debug.Log("Junp�@!");
        }
    }

    private void PrintPause(InputAction.CallbackContext context)
    {
        Debug.Log("�|�[�Y��ʕ\��");
    }
}
