using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    // �V���O���g��
    public static BattleSceneManager Instance = null;
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
    [SerializeField, Tooltip("�o�g���̐i�s�Ǘ��p�̃e�L�X�g")]
    private Text battleText;
    [SerializeField, Tooltip("EnemyList������")]
    private EnemyList enemyList;
    [Tooltip("�퓬�����ǂ����𔻒�")]
    private bool battlePhase = true;
    [Tooltip("���s����")]
    private bool winOrLose = true;
    /*
    [Tooltip("�G�̔ԍ����󂯎��")]
    private int enemyNumber;
    */

    //--���u���̃v���C���[�̃X�e�[�^�X--
    [SerializeField]
    int np = 3;
    // int atk = 3;
    //int agi = 2;
    //  int def = 0;
    //--------------------------------
    // �G�̃X�e�[�^�X
    private string enemyName;
    private int enemyNp;
    private int enemyAtk;
    private int enemyAgi;
    private int enemyDef;
    private int enemyLv;

    public void Start()
    {
        // �G�̏���ݒ�
        //                                   �����������enemyNumber�ɕς��鑽��
        enemyName = enemyList.EnemyParamList[0].enemyName;
        enemyNp = enemyList.EnemyParamList[0].np;
        enemyAtk = enemyList.EnemyParamList[0].atk;
        enemyAgi = enemyList.EnemyParamList[0].agi;
        enemyDef = enemyList.EnemyParamList[0].def;
        enemyLv = enemyList.EnemyParamList[0].lv;

        // ���������G�̏���EnemyList����Ⴂ�A�G�̖��O���e�L�X�g�ɕ`��
     �@ battleText.text = enemyName + "�ɑ��������I�I";
    }
    public void Update()
    {
        
        // �퓬�J�n
        if (battlePhase)
        {
            // �퓬�I������
            if (enemyNp <= 0)
            {
                battlePhase = false;
            }
            else if (np <= 0)
            {
                battlePhase = false;
                // �ǂ��炩�����������̔���
                winOrLose = false;
            }
        }
        else
        {
            // �v���C���[�̏���
            if(winOrLose)
            {
                battleText.text = enemyName + "��|�����I";
            }
            // �v���C���[�̔s�k
            else
            {
                battleText.text = "GAMEOBERA";
            }
        }
    }
}
//battleText.text = string.Format("{000}",enemyNp);
