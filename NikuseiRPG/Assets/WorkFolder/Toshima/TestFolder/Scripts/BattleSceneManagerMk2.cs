using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneManagerMk2 : MonoBehaviour
{
    //戦闘に参加するキャラクターをリストで保持(戦闘の三文字「par」は「Participation(参加)」の先頭三文字)
    [SerializeField]
    private List<CharacterParam> _parCharacter;

    public static BattleSceneManagerMk2 Instance;

    public Text genelic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _parCharacter.Add(CharacterDataBase.instance.AddPlayer());
        _parCharacter.Add(CharacterDataBase.instance.AddEnemy());
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_parCharacter == null)
        {
            Debug.LogError("戦闘参加者が設定されていません");
            return;
        }

        BattleStartPhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region パブリック関数

    /// <summary>
    /// 戦闘参加者のリストにデータを保存
    /// </summary>
    public void AddParticipationCharacter(CharacterParam characterParam)
    {
        _parCharacter.Add(characterParam);
    }

    #endregion

    #region プライベート関数

    /// <summary>
    /// 戦闘開始時に呼ばれる関数
    /// </summary>
    private void BattleStartPhase()
    {
        //ナンバリング処理
        //ReNameCharacters();

        MessageScrollManager.Instance.GeneralTxtBoxUpData(genelic);

        //テキストの更新
        EngageText();

        //行動順のソート
        SortAgirityList();

        //ソート順に則り行動を開始する
        StartCoroutine(ActionPhase());
    }

    /// <summary>
    /// テキストを更新する関数を利用する関数
    /// </summary>
    private void EngageText()
    {
        List<CharacterParam> list = new List<CharacterParam>();

        for (int i = 0; i < _parCharacter.Count; i++)
        {
            if (_parCharacter[i].type == CharacterType.Enemy)
                list.Add(_parCharacter[i]);
        }
        MessageScrollManager.Instance.EngageEnemyMessage(list);
    }

    /// <summary>
    /// 戦闘参加者のリストを「AGI」で降順ソートする
    /// </summary>
    private void SortAgirityList()
    {
        //「AGI」で昇順ソート
        _parCharacter.Sort((a, b) => a.agi - b.agi);

        //昇順を降順にする
        _parCharacter.Reverse();
    }

    /// <summary>
    /// 重複したキャラクター名にアルファベットでナンバリングをつける関数
    /// </summary>
    private void ReNameCharacters()
    {
        for (int i = 0; i < _parCharacter.Count; i++)
        {
            char count = 'B';
            for (int j = i + 1; j < _parCharacter.Count; j++)
            {
                if(_parCharacter[i].name == _parCharacter[j].name)
                {                   
                    _parCharacter[j].name += count;
                    ++count;
                }
            }

            if(count != 'B')
            {
                _parCharacter[i].name += 'A';
            }
        }
    }

    /// <summary>
    /// 戦闘終了時に呼ばれる関数
    /// </summary>
    private void BattleEndPhase() 
    {

    }

    /// <summary>
    /// 各キャラクターの行動時に呼ばれるコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActionPhase()
    {
        IEnumerator coroutine;

        for (int i = 0; i < _parCharacter.Count; i++)
        {
            switch (_parCharacter[i].type)
            {
                case CharacterType.Player:
                    if(_parCharacter[i].np > 0)
                    {
                        //プレイヤーの行動を決定するまで待機する
                        coroutine = BattleOperationMk2.Instance.OperationSelect(0, 0, _parCharacter[i]);
                        yield return StartCoroutine(coroutine);
                        Debug.Log("プレイヤーのターン終了");
                    }
                    //yield return StartCoroutine(BattleOperationMk2.Instance.ActionSelect(_parCharacter[i]));
                    break;
                case CharacterType.Enemy:
                    if (_parCharacter[i].np > 0)
                    {
                        Debug.Log("エネミーの行動順");
                        //エネミーの行動が終了するまで待機する
                        for (int j = 0; j < _parCharacter.Count; j++)
                        {
                            if(_parCharacter[j].type == CharacterType.Player)
                            {
                                Void.Instance.Move(1);
                                SoundManager.instance.PlayVC(VCLabel.VC8);
                                coroutine = Attack(_parCharacter[i], _parCharacter[j]);
                                yield return StartCoroutine(coroutine);
                                Debug.Log("エネミーのターン終了");
                            }
                        }
                    }
                    break;
                default:
                    Debug.Log("不具合発生中!!");
                    break;
            }
        }

        coroutine = ConfirmationSurvival();
        yield return StartCoroutine(coroutine);

        if ((string)coroutine.Current == "none")
        {
            yield return StartCoroutine(ActionPhase());
        }
        yield break;
    }

    public List<CharacterParam> GetCharcterList()
    {
        return _parCharacter;
    }

    public IEnumerator Attack(CharacterParam actor,CharacterParam Target)
    {
        int Damage = actor.atk - Target.atk;
        if(Damage <= 0) 
        {
            Damage = 1;
        }

        for (int i = 0; i < _parCharacter.Count; i++)
        {
            if (_parCharacter[i].name == Target.name)
            {
                _parCharacter[i].np -= Damage;
                ConfirmationSurvival();
                Debug.Log(Damage);
                BattleOperationMk2.Instance.NPberUpdate();

                if(Target.type == CharacterType.Enemy)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC10);
                }

                yield return MessageScrollManager.Instance.CharacterAttackMessage(actor,Target,Damage);
            }
        }       
        yield break;
    }

    public IEnumerator ConfirmationSurvival()
    {
        string str = "";
        int Pcount = 0;
        int Ecount = 0;
        for (int i = 0; i < _parCharacter.Count; i++)
        {
            if (_parCharacter[i].type == CharacterType.Player && _parCharacter[i].np > 0)
            {
                Pcount++;
            }
            else if (_parCharacter[i].type == CharacterType.Enemy && _parCharacter[i].np > 0)
            {
                Ecount++;
            }
        }

        if(Pcount <= 0 )
        {
            //敗北処理
            str = "lose";
            Void.Instance.Dead(0);
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("全滅した"));            
            for (int i=0;i<_parCharacter.Count;i++)
            {
                _parCharacter[i].np = _parCharacter[i].maxnp;                
            }
            _parCharacter.Clear();
            SceneManager.LoadScene("AdventureScene");
        }
        else if(Ecount <= 0)
        {
            //勝利処理
            str = "win";
            SoundManager.instance.PlayVC(VCLabel.VC9);
            Void.Instance.Dead(1);
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("敵を倒した！"));
            for (int i = 0; i < _parCharacter.Count; i++)
            {
                if (_parCharacter[i].type == CharacterType.Enemy)
                {
                    _parCharacter[i].np = _parCharacter[i].maxnp;
                }
            }
            _parCharacter.Clear();
            SceneManager.LoadScene("AdventureScene");
        }
        else
        {
            //戦闘続行
            str = "none";
        }

        yield return str;
    }

    #endregion
}
