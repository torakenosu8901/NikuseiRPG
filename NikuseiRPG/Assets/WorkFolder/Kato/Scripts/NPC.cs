using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

//�C�x���g�V�X�e���̃X�N���v�g
[RequireComponent(typeof(Flowchart))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    string message = "";
    GameObject playerObj;
    Flowchart flowChart;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        flowChart = GetComponent<Flowchart>();
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        //�^�O�Ńv���C���[�ɓ��������烁�b�Z�[�W���o���悤��
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Talk());
        }
    }
    IEnumerator Talk()
    {
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
    }
}
