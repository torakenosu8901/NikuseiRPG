using UnityEngine;
using UnityEngine.InputSystem;

//���������Ăԏ��Ԃ�ς�����B-�ȉ��̐��l���Ƒ����Ȃ�B
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    //�V���O���g���Ƃ�
    //�����̃G�l�~�[�N���X�������Ă��K��������N���X�����Ȃ�����Ă��
    //�ϐ��Ƃ��s�v�ŃC���v�b�g�}�l�[�W���[���đłƊȒP�ɒ��g�����Ă����

    //�V���O���g���N���X��
    private static InputManager Instance;
    public static InputManager instance
        //static�����ƑΏۂ���������ɂȂ苴�݂����Ȃ�B
    {
        get
        {
            if (Instance == null)
            {
                Debug.LogError("InputManager���Ȃ���!");
                return null;
            }
            return Instance;
        }
    }

    private void Init()
    {

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }
    //--------------------------

    private InputSystem inputSystem;
    //�啶��input���ĂԂƏ�������ǂ�ł������
    public InputSystem InputSystem => inputSystem;
    

    private void Awake()
    {
      Init();

        inputSystem = new InputSystem();
        //������Ȃ��ƗL��������Ȃ��B
        inputSystem.Enable();
    }


    private void Update()
    {
        if(inputSystem == null)
        {

        }
    }
}

