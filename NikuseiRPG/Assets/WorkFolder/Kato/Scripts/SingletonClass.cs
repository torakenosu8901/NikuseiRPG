using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public abstract class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    // �V�[�����ׂ������o���邩 (�}�l�[�W���n�N���X�Ȃ�true)
    protected bool canCrossScene = true;

    private static T Instance;
    public static T instance
    {
        get
        {
            if(Instance == null)
            {
                throw new System.NullReferenceException(typeof(T) + "������܂���B" +
                    "�Ăяo���O�ɐ������Ă��������B");
            }

            return Instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this as T;
        else Destroy(this);

        if (canCrossScene == true) DontDestroyOnLoad(this);
    }
}
