using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeed : MonoBehaviour
{
    [SerializeField]
    private Text _messageText = null;

    //�����\���X�s�[�h
    [SerializeField, Range(0.01f, 0.5f)]
    private float _messageSpeed = 1f;

    private string _msg;
    // Start is called before the first frame update
    void Start()
    {
        //�e�L�X�g�ɕ������\��������
        _messageText.text = _msg;
    }

    // Update is called once per frame
    void Update()
    {     //�}�E�X�̍��N���b�N��������
        if (Input.GetMouseButtonDown(0))
        {  //�R���[�`���ŕ������\�L����
            StartCoroutine(MessageCo(_msg));
        }
    }

    /// <summary>
    /// ���������莞�Ԃŏ��Ԃɕ\������
    /// </summary>
    /// <param name="msg">�Ώۂ̕�����</param>
    /// <returns></returns>
    private IEnumerator MessageCo(string msg)
    {
        //�\�����郁�b�Z�[�W���i�[
        var message = "";
        //�擪����̕�����
        var index = 0;
        //�@�擪����̕��������\���Ώۈُ�ɂȂ�܂ŌJ��Ԃ�
        while (msg.Length > index)
        {
            //�E������������Z����
            index++;
            //�Ώۂ̕����񂩂�E�����������؂�o��
            message = msg.Substring(0, index);
            //�e�L�X�g�ɕ\������
            _messageText.text = message;
            //�@�w��̕b���ҋ@����
            yield return new WaitForSeconds(_messageSpeed);
        }
    }
}