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
    [SerializeField,Tooltip("�o�g���̐i�s�Ǘ��p�̃e�L�X�g")]
    Text battleText;
    [SerializeField, Tooltip("EnemyList������")]
    EnemyList enemyList;
    public void Start()
    {
        // ���������G�̏���EnemyList����Ⴂ�A�G�̖��O���e�L�X�g�ɕ`��
        battleText.text = enemyList.EnemyParamList[0].enemyName;
    }
}
