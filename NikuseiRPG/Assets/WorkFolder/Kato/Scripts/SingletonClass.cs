using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public abstract class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    // シーンを跨ぐ事が出来るか (マネージャ系クラスならtrue)
    protected bool canCrossScene = true;

    private static T Instance;
    public static T instance
    {
        get
        {
            if(Instance == null)
            {
                throw new System.NullReferenceException(typeof(T) + "がありません。" +
                    "呼び出す前に生成してください。");
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
