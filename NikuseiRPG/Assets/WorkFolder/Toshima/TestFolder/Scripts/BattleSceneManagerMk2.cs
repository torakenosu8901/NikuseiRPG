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
        if (_parCharacter == null)
        {
            Debug.LogError("戦闘参加者が設定されていません");
            return;
        }

        //Debug.Log(_parCharacter[0].characterUseBuffList.Count);

        SoundManager.instance.PlayVC(VCLabel.VC3);


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
        ReNameCharacters();

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
                if (_parCharacter[i].name == _parCharacter[j].name)
                {
                    _parCharacter[j].name += count;
                    ++count;
                }
            }

            if (count != 'B')
            {
                _parCharacter[i].name += 'A';
            }
        }
    }

    /// <summary>
    /// 戦闘終了時に呼ばれる関数
    /// </summary>
    private IEnumerator BattleEndPhase()
    {
        Debug.Log(_parCharacter.Count);
        for (int i = 0; i < _parCharacter.Count; i++)
        {
            yield return StartCoroutine(BuffEndProcess(_parCharacter[i], true));
        }

        _parCharacter.Clear();

        SoundManager.instance.PlayBGM(BGMLabel.BGM2);

        //TitleScene
        //SceneManager.LoadScene("AdventureScene");
        SceneManager.LoadScene("TitleScene");
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

            yield return StartCoroutine(BuffEndProcess(_parCharacter[i]));

            switch (_parCharacter[i].type)
            {
                case CharacterType.Player:
                    if (_parCharacter[i].np > 0)
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
                            if (_parCharacter[j].type == CharacterType.Player)
                            {
                                Void.Instance.Move(1);
                                SoundManager.instance.PlayVC(VCLabel.VC6);

                                yield return new WaitForSeconds(1f);

                                SoundManager.instance.PlayVC(VCLabel.VC4);
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

    public IEnumerator Attack(CharacterParam actor, CharacterParam Target)
    {
        int Damage = actor.atk - Target.def;
        if (Damage <= 0)
        {
            Damage = 1;
        }

        Target.np -= Damage;
        ConfirmationSurvival();
        //Debug.Log(Damage);
        BattleOperationMk2.Instance.NPberUpdate();

        if (Target.type == CharacterType.Enemy)
        {
            int Way = Random.Range(0, 2);
            if(Way == 0)
            {
                SoundManager.instance.PlayVC(VCLabel.VC1);

                yield return new WaitForSeconds(1f);

                SoundManager.instance.PlayVC(VCLabel.VC7);
            }
            else if(Way == 1)
            {
                SoundManager.instance.PlayVC(VCLabel.VC2);

                yield return new WaitForSeconds(1f);

                SoundManager.instance.PlayVC(VCLabel.VC7);
            }
        }

        yield return MessageScrollManager.Instance.CharacterAttackMessage(actor, Target, Damage);
    }

    public IEnumerator UseItemAction(CharacterParam actor, CharacterParam Target, int UseItemNum = 0)
    {
        //Debug.Log(UseItemNum);

        int Damage = ItemDataBase.instance.ItemData.ItemParamList[UseItemNum].itemPower;
        if (Damage <= 0)
        {
            Damage = 1;
        }

        if (ItemDataBase.instance.ItemData.ItemParamList[UseItemNum].itemType == ItemType.Damage)
        {
            Target.np -= Damage;
            ConfirmationSurvival();
            //Debug.Log(Damage);
            BattleOperationMk2.Instance.NPberUpdate();

            if (Target.type == CharacterType.Enemy)
            {
                // SoundManager.instance.PlayVC(VCLabel.VC10);
            }

            yield return MessageScrollManager.Instance.CharacterAttackMessage(actor, Target, Damage);
        }
        else if (ItemDataBase.instance.ItemData.ItemParamList[UseItemNum].itemType == ItemType.Heal)
        {
            Target.np += Damage;

            if (Target.np >= Target.maxnp)
            {
                Target.np = Target.maxnp;
            }

            ConfirmationSurvival();
            Debug.Log(Damage);
            BattleOperationMk2.Instance.NPberUpdate();

            if (Target.type == CharacterType.Player)
            {
                //SoundManager.instance.PlayVC(VCLabel.VC10);
            }
            else if (Target.type == CharacterType.Enemy)
            {

            }

            yield return MessageScrollManager.Instance.CharacterHealMessage(actor, Target, Damage);
        }
        else if (ItemDataBase.instance.ItemData.ItemParamList[UseItemNum].itemType == ItemType.Buff)
        {

            BuffProcess(actor, Target, UseItemNum, Damage, true);

            yield return MessageScrollManager.Instance.CharacterBuffMessage(actor, Target, Damage);
        }

        yield break;
    }

    public IEnumerator UseSkillAction(CharacterParam actor, CharacterParam Target, int UseItemNum = 0)
    {
        //Debug.Log(UseItemNum);

        if (actor.skill[UseItemNum].skillType == SkillType.Damage)
        {
            float value = actor.atk * actor.skill[UseItemNum].skillPower - Target.def;

            int Damage = (int)value;
            if (Damage <= 0)
            {
                Damage = 1;
            }
            Target.np -= Damage;
            ConfirmationSurvival();
            //Debug.Log(Damage);
            BattleOperationMk2.Instance.NPberUpdate();

            if (Target.type == CharacterType.Enemy)
            {
                int Way = Random.Range(0, 3);
                if (Way == 0)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC3);
                }
                else if (Way == 1)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC4);
                }
                else if(Way == 2)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC5);
                }
            }

            yield return MessageScrollManager.Instance.CharacterAttackMessage(actor, Target, Damage);
            yield break;
        }
        else if (actor.skill[UseItemNum].skillType == SkillType.Heal)
        {
            float value = Target.maxnp * actor.skill[UseItemNum].skillPower;

            int Damage = (int)value;
            if (Damage <= 0)
            {
                Damage = 1;
            }

            Target.np += Damage;

            if (Target.np >= Target.maxnp)
            {
                Target.np = Target.maxnp;
            }

            ConfirmationSurvival();
            Debug.Log(Damage);
            BattleOperationMk2.Instance.NPberUpdate();

            if (Target.type == CharacterType.Player)
            {
                int Way = Random.Range(0, 3);
                if (Way == 0)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC6);
                }
                else if (Way == 1)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC7);
                }
                else if (Way == 2)
                {
                    SoundManager.instance.PlayVC(VCLabel.VC8);
                }
            }
            else if (Target.type == CharacterType.Enemy)
            {

            }

            yield return MessageScrollManager.Instance.CharacterHealMessage(actor, Target, Damage);
            yield break;
        }
        else if (actor.skill[UseItemNum].skillType == SkillType.Buff)
        {
            float value = actor.atk * actor.skill[UseItemNum].skillPower - actor.atk;

            int Damage = (int)value;
            //if (Damage <= 0)
            //{
            //    Damage = 1;
            //}

            Debug.Log(Damage);

            int Way = Random.Range(0, 3);
            if (Way == 0)
            {
                SoundManager.instance.PlayVC(VCLabel.VC9);
            }
            else if (Way == 1)
            {
                SoundManager.instance.PlayVC(VCLabel.VC10);
            }
            else if (Way == 2)
            {
                SoundManager.instance.PlayVC(VCLabel.VC11);
            }

            BuffProcess(actor, Target, UseItemNum, Damage, false);

            yield return MessageScrollManager.Instance.CharacterBuffMessage(actor, Target, Damage);
            yield break;
        }
        //Debug.Log(UseItemNum);
        yield break;
    }


    private void BuffProcess(CharacterParam actor, CharacterParam Target, int UseItemNum, int Valuee, bool UseItem)
    {

        for (int i = 0; i < Target.characterUseBuffList.Count; i++)
        {
            if (ItemDataBase.instance.ItemData.ItemParamList[UseItemNum].itemName == Target.characterUseBuffList[i].buddName && UseItem)
            {
                return;
            }
            if (actor.skill[UseItemNum].skillName == Target.characterUseBuffList[i].buddName && !UseItem)
            {
                return;
            }
        }

        CharacterUseBuffList addBuff = new CharacterUseBuffList();

        ItemParam itemParam = ItemDataBase.instance.ItemData.ItemParamList[UseItemNum];

        CharacterUseSkillSet characterUseSkillSet = actor.skill[UseItemNum];

        if (UseItem)
        {
            switch (ItemDataBase.instance.ItemData.ItemParamList[UseItemNum].relatedStatus)
            {
                case RelatedStatusType.MaxNp:
                    Target.maxnp += Valuee;
                    break;

                case RelatedStatusType.Np:
                    Target.np += Valuee;
                    break;

                case RelatedStatusType.Atk:
                    Target.atk += Valuee;
                    break;

                case RelatedStatusType.Def:
                    Target.def += Valuee;
                    break;

                case RelatedStatusType.Agi:
                    Target.agi += Valuee;
                    break;
                default:
                    break;
            }

            addBuff.buddName = itemParam.itemName;
            addBuff.relatedStatusType = itemParam.relatedStatus;
            addBuff.numType = itemParam.numType;
            addBuff.buffPower = itemParam.itemPower;
            addBuff.buffPersistence = itemParam.itemPersistence;

        }
        else
        {
            switch (characterUseSkillSet.statustype)
            {
                case RelatedStatusType.MaxNp:
                    Target.maxnp += Valuee;
                    break;

                case RelatedStatusType.Np:
                    Target.np += Valuee;
                    break;

                case RelatedStatusType.Atk:
                    Target.atk += Valuee;
                    break;

                case RelatedStatusType.Def:
                    Target.def += Valuee;
                    break;

                case RelatedStatusType.Agi:
                    Target.agi += Valuee;
                    break;
                default:
                    break;
            }

            addBuff.buddName = characterUseSkillSet.skillName;
            addBuff.relatedStatusType = characterUseSkillSet.statustype;
            addBuff.numType = characterUseSkillSet.numtype;
            float risingValue = actor.atk * characterUseSkillSet.skillPower - actor.atk;
            addBuff.buffPower = (int)risingValue;
            addBuff.buffPersistence = characterUseSkillSet.skillPersistence;
        }

        Target.characterUseBuffList.Add(addBuff);

        ConfirmationSurvival();
        //Debug.Log(Valuee);
        BattleOperationMk2.Instance.NPberUpdate();
    }

    private IEnumerator BuffEndProcess(CharacterParam Target, bool Init = false)
    {
        //Debug.Log(Init);
        //Debug.Log(Target.characterUseBuffList.Count);
        for (int i = 0; i < Target.characterUseBuffList.Count; i++)
        {
            if (Target.characterUseBuffList[i].buffPersistence <= 0 || Init)
            {
                switch (Target.characterUseBuffList[i].relatedStatusType)
                {
                    case RelatedStatusType.MaxNp:
                        Target.maxnp -= Target.characterUseBuffList[i].buffPower;

                        if (Target.maxnp < Target.np)
                        {
                            Target.np = Target.maxnp;
                        }

                        break;

                    case RelatedStatusType.Np:
                        Target.np -= Target.characterUseBuffList[i].buffPower;

                        if (Target.maxnp < Target.np)
                        {
                            Target.np = Target.maxnp;
                        }

                        break;

                    case RelatedStatusType.Atk:
                        Target.atk -= Target.characterUseBuffList[i].buffPower;
                        break;

                    case RelatedStatusType.Def:
                        Target.def -= Target.characterUseBuffList[i].buffPower;
                        break;

                    case RelatedStatusType.Agi:
                        Target.agi -= Target.characterUseBuffList[i].buffPower;
                        break;
                    default:
                        break;
                }

                Target.characterUseBuffList.Remove(Target.characterUseBuffList[i]);

                if (!Init)
                {
                    yield return StartCoroutine(MessageScrollManager.Instance.MessageCo(Target.name + " の能力がもとに戻った"));
                }
            }
            else
            {
                Target.characterUseBuffList[i].buffPersistence -= 1;
            }
            BattleOperationMk2.Instance.NPberUpdate();            
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
                //Debug.Log(_parCharacter[i].np);
                Pcount++;
                //Debug.Log(Pcount);
            }
            else if (_parCharacter[i].type == CharacterType.Enemy && _parCharacter[i].np > 0)
            {
                //Debug.Log(_parCharacter[i].np);
                Ecount++;
                //Debug.Log(Ecount);
            }
        }

        Debug.Log(Pcount);
        Debug.Log(Ecount);


        if (Pcount <= 0)
        {
            //敗北処理
            str = "lose";
            Void.Instance.Dead(0);
            SoundManager.instance.PlayVC(VCLabel.VC5);
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("全滅した"));
            for (int i = 0; i < _parCharacter.Count; i++)
            {
                _parCharacter[i].np = _parCharacter[i].maxnp;
            }
            //_parCharacter.Clear();
            //SceneManager.LoadScene("AdventureScene");
            Debug.Log(_parCharacter.Count);

            StartCoroutine(BattleEndPhase());

        }
        else if (Ecount <= 0)
        {
            //勝利処理
            str = "win";            
            Void.Instance.Dead(1);
            SoundManager.instance.PlayVC(VCLabel.VC8);
            yield return new WaitForSeconds(1f);
            SoundManager.instance.PlayVC(VCLabel.VC9);
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("敵を倒した！"));
            //_parCharacter.Clear();
            //SceneManager.LoadScene("AdventureScene");
            Debug.Log(_parCharacter.Count);

            StartCoroutine(BattleEndPhase());

        }
        else
        {
            //戦闘続行
            str = "none";

            //Debug.Log(_parCharacter.Count);
        }

        yield return str;
    }

    #endregion
}
