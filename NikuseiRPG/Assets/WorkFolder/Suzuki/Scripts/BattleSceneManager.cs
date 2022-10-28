using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    //--------------------�ϐ��̐錾-----------------------
    [SerializeField, Tooltip("�o�g���̐i�s�Ǘ��p�̃e�L�X�g")]
    private Text battleText;
    [SerializeField, Tooltip("EnemyList������")]
    private EnemyList enemyList;
    [Tooltip("�퓬�����ǂ����𔻒�")]
    private bool battlePhase = true;
    [Tooltip("���s����")]
    private bool winOrLose = true;
    [Tooltip("��U��U�̔���")]
    private bool whoseTurnIsIt;
    [Tooltip("�󂯂�_���[�W")]
    private int damage;
    [Tooltip("�v���C���[������邩�̕ϐ�")]
    private int playerFlee;
    [Tooltip("�X�L����ԍ��ŊǗ����邽�߂̕ϐ�")]
    private int skillNumber;
    // [Tooltip("�G�̔ԍ����󂯎��")]
    // private int enemyNumber;
    //-----------���u���̃v���C���[�̃X�e�[�^�X----------
    public int np = 3;
    public int atk = 3;
    public int agi = 2;
    public int def = 0;
    //------------------�G�̃X�e�[�^�X------------------
    private string enemyName;
    private int enemyNp;
    private int enemyAtk;
    private int enemyAgi;
    private int enemyDef;
    //private int enemyLv;
    //------------------�V���O���g��--------------------
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
    public void Start()
    {
        // �G�̏���ݒ�
        //enemyNumber = ????
        //                                   �����������enemyNumber�ɕς��鑽��
        enemyName = enemyList.EnemyParamList[0].enemyName;
        enemyNp = enemyList.EnemyParamList[0].np;
        enemyAtk = enemyList.EnemyParamList[0].atk;
        enemyAgi = enemyList.EnemyParamList[0].agi;
        enemyDef = enemyList.EnemyParamList[0].def;
        // enemyLv = enemyList.EnemyParamList[0].lv;
        // ���������G�̏���EnemyList����Ⴂ�A�G�̖��O���e�L�X�g�ɕ`��
        battleText.text = enemyName + "�ɑ��������I�I";
        AgiComparison();
    }
    public void Update()
    {
        // �퓬�J�n
        if (!battlePhase)
        {
            // �v���C���[�̏���
            if (winOrLose)
            {
                battleText.text = enemyName + "��|�����I";
                //---------------------------------------
                //   �V�[���J�ڂ̏����������ɏ���
                //---------------------------------------
            }
            // �v���C���[�̔s�k
            else
            {
                battleText.text = "GAMEOBERA";
                //---------------------------------------
                //   �V�[���J�ڂ̏����������ɏ���
                //---------------------------------------
            }
        }
    }
    //----------���񂾂��̔���̊֐�--------------
    public void KillConfirmation()
    {
        if (enemyNp <= 0)
        {
            battlePhase = false;
        }
        if (np <= 0)
        {
            battlePhase = false;
            // �ǂ��炩�����������̔���
            winOrLose = false;
        }
    }
    //----�v���C���[�ƓG�ǂ��������������肷��֐�------
    public void AgiComparison()
    {
        if (enemyAgi >= agi)
        {
            whoseTurnIsIt = true;
        }
        else
        {
            whoseTurnIsIt = false;
        }
    }
    //------------�G���ʏ�U�����Ă���֐�-------------
    public void EnemyAttack()
    {
        damage = enemyAtk - def;
        np -= damage;
        battleText.text = damage + "�_���[�W�󂯂�";
        damage = 0;
    }
    //----------�v���C���[���ʏ�U������֐�-----------
    public void PlayerAttack()
    {
        damage = atk - enemyDef;
        enemyNp -= damage;
        battleText.text = enemyName + "��" + damage + "�_���[�W�^����";
        damage = 0;
    }

    public void AttackPhase()
    {
        if (whoseTurnIsIt)
        {
            // �G�̕��������ꍇ
            EnemyAttack();
            KillConfirmation();
            PlayerAttack();
            KillConfirmation();
        }
        else
        {
            // �v���C���[�̕��������ꍇ
            PlayerAttack();
            KillConfirmation();
            EnemyAttack();
            KillConfirmation();
        }
    }

    public void SkillPhase()
    {
        if (whoseTurnIsIt)
        {
            // �G�̕��������ꍇ
            EnemyAttack();
            KillConfirmation();
            switch (skillNumber)
            {
                case 0:
                    break;
            }
        }
        else
        {
            // �v���C���[�̕��������ꍇ
            switch (skillNumber)
            {
                case 0:
                    break;
            }
            EnemyAttack();
            KillConfirmation();
        }
    }

    public void ItemPhase()
    {
        //---------------------------------------
        //   �����ɃA�C�e���̌��ʂ̔��f�̏��������� |
        //---------------------------------------
        EnemyAttack();
        KillConfirmation();
    }

    public void EscapePhase()
    {
        playerFlee = Random.Range(1, 6);
        if (1 == playerFlee)
        {
            battleText.text = enemyName + "���瓦���ꂽ";

            //---------------------------------------
            //   �V�[���J�ڂ̏����������ɏ���
            //---------------------------------------
        }
        else
        {
            battleText.text = "��������Ȃ�����";
            EnemyAttack();
            KillConfirmation();
        }
    }
}
//battleText.text = string.Format("{000}",enemyNp);