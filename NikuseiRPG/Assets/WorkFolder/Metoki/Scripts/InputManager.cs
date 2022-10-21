using UnityEngine;
using UnityEngine.InputSystem;

//↓処理を呼ぶ順番を変えられる。-以下の数値だと早くなる。
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    //シングルトンとは
    //複数のエネミークラスがあっても必ず一つしかクラスが作れないよってやつ
    //変数とか不要でインプットマネージャーって打つと簡単に中身持ってこれる

    //シングルトンクラス化
    private static InputManager Instance;
    public static InputManager instance
        //staticを作ると対象がメモリ上になり橋みたくなる。
    {
        get
        {
            if (Instance == null)
            {
                Debug.LogError("InputManagerがないよ!");
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
    //大文字inputを呼ぶと小文字を読んでくれるやつ
    public InputSystem InputSystem => inputSystem;
    

    private void Awake()
    {
      Init();

        inputSystem = new InputSystem();
        //↓これないと有効化されない。
        inputSystem.Enable();
    }


    private void Update()
    {
        if(inputSystem == null)
        {

        }
    }
}

