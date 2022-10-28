using UnityEngine;
using UnityEngine.InputSystem;

//↓処理を呼ぶ順番を変えられる。-以下の数値だと早くなる。
[DefaultExecutionOrder(-1)]
public class InputBattleManager : MonoBehaviour
{
    //シングルトンとは
    //複数のエネミークラスがあっても必ず一つしかクラスが作れないよってやつ
    //変数とか不要でインプットマネージャーって打つと簡単に中身持ってこれる

    //シングルトンクラス化
    private static InputBattleManager Instance;
    public static InputBattleManager instance
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

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }
    //--------------------------

    private InputBattleData inputBattleData;
    //大文字inputを呼ぶと小文字を読んでくれるやつ
    public InputBattleData InputBattleData => inputBattleData;


    private void Awake()
    {
        Init();

        inputBattleData = new InputBattleData();
        //↓これないと有効化されない。
        inputBattleData.Enable();
    }


    private void Update()
    {
        if (inputBattleData == null)
        {

        }
    }
}
