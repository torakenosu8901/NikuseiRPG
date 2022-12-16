using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BattleOperationMk2 : MonoBehaviour
{
    #region 選択演出用のゲームオブジェクト達

    //使用するテキストウィンドウをまとめた変数
    [SerializeField]
    private List<GameObject> CommandObject;

    //最初の選択肢を表すテキストをまとめた変数
    [SerializeField]
    private List<Text> FirstStep;

    //アイテム一覧のテキストをまとめた変数
    [SerializeField]
    private List<Text> ItemDerivation;

    //攻撃方法の示すテキストをまとめた変数
    [SerializeField]
    private List<Text> FightDerivation;

    //スキル一覧のテキストをまとめた変数
    [SerializeField]
    private List<Text> SkillDerivation;

    //エネミー一覧のテキストをまとめた変数
    [SerializeField]
    private List<Text> EnemyDerivation;

    //プレイヤー一覧のテキストをまとめた変数
    [SerializeField]
    private List<Text> PlayerDerivation;

    [SerializeField]
    private List<Slider> NPber;


    #endregion

    //シングルトン化
    public static BattleOperationMk2 Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //初期化処理
        //InitTextBox();       
    }

    private void Start()
    {
        NPberUpdate();
    }

    public void NPberUpdate()
    {
        NPber[0].maxValue = BattleSceneManagerMk2.Instance.GetCharcterList()[0].maxnp;
        NPber[1].maxValue = BattleSceneManagerMk2.Instance.GetCharcterList()[1].maxnp;

        NPber[0].value = BattleSceneManagerMk2.Instance.GetCharcterList()[0].np;
        NPber[1].value = BattleSceneManagerMk2.Instance.GetCharcterList()[1].np;
    }

    private void TextUpdate(int step,int scrollnum, CharacterParam actor = null)
    {
        List<CharacterParam> ParamList = BattleSceneManagerMk2.Instance.GetCharcterList();
        int count = 0;

        switch (step)
        {
            case 0:
                //処理なし
                break; 

            case 1:
                //アイテム一覧のテキスト更新
                for (int i = 0; i < ItemDerivation.Count; i++)
                {
                    int num = ItemDerivation.Count * scrollnum + i;
                    if(ItemDataBase.instance.ItemData.ItemParamList.Count > num)
                    {
                        ItemDerivation[i].text = ItemDataBase.instance.ItemData.ItemParamList[num].itemName;
                    }
                    else
                    {
                        ItemDerivation[i].text = "";
                    }
                }
                break;

            case 2:
                //処理なし
                break;

            case 3:
                //スキル一覧のテキスト更新
                for (int i = 0; i < SkillDerivation.Count; i++)
                {
                    int num = SkillDerivation.Count * scrollnum + i;
                    if (actor.skill.Count > num)
                    {
                        SkillDerivation[i].text = actor.skill[num].skillName;
                    }
                    else
                    {
                        SkillDerivation[i].text = "";
                    }
                }
                break;

            case 4:
                //エネミー一覧のテキスト更新
                
                for (int i = 0; i < ParamList.Count; i++)
                {
                    if(ParamList[i].type == CharacterType.Enemy && count< EnemyDerivation.Count)
                    {
                        EnemyDerivation[count].text = ParamList[i].name;
                        count++;
                    }
                }

                //Debug.Log(count);
                //Debug.Log(EnemyDerivation.Count);

                for (int i = count; i < EnemyDerivation.Count;i++)
                {
                    EnemyDerivation[i].text = "";
                    //EnemyDerivation.Remove(EnemyDerivation[i]);
                    //Debug.Log(EnemyDerivation.Count);
                }
                break;

            case 5:
                //プレイヤー一覧のテキスト更新
                for (int i = 0; i < ParamList.Count; i++)
                {
                    if (ParamList[i].type == CharacterType.Player && count < PlayerDerivation.Count)
                    {
                        PlayerDerivation[count].text = ParamList[i].name;
                        count++;
                    }
                }

                //Debug.Log(count);
                //Debug.Log(EnemyDerivation.Count);

                for (int i = count; i < PlayerDerivation.Count;i++)
                {
                    PlayerDerivation[i].text = "";
                    //PlayerDerivation.Remove(PlayerDerivation[i]);
                    //Debug.Log(EnemyDerivation.Count);
                }

                break;
        }
    }

    /// <summary>
    /// 選択肢のα地を初期化する関数
    /// </summary>
    /// <param name="step">現在処理がどこまで進んでいるのかを示す</param>
    private void InitTextBox(int step,CharacterParam actor = null)
    {
        switch (step)
        {
            case 0:
                TextOpaque(0, 0);
                for (int i = 1; i < FirstStep.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(0, 0);
                break;

            case 1:
                TextOpaque(1, 0);
                for (int i = 1; i < ItemDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(1, 0);
                break;

            case 2:
                TextOpaque(2, 0);
                for (int i = 1; i < FightDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(2, 0);
                break;

            case 3:
                TextOpaque(3, 0);
                for (int i = 1; i < SkillDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(3, 0, actor);
                break;

            case 4:
                TextOpaque(4, 0);
                for (int i = 1; i < EnemyDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(4,0);
                break;

            case 5:
                TextOpaque(5, 0);
                for (int i = 1; i < PlayerDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(5, 0);
                break;
        }
    }

    public IEnumerator OperationSelect(int step, int previous, CharacterParam player, int selectionNum = 0)
    {

        //string str = "";

        //選択処理用の変数
        int num = 0;

        //選択処理の最大値
        int maxnum = 0;

        //Debug.Log(num);

        InitTextBox(step,player);

        int count = 0;

        switch (step)
        {
            case 0:
                CommandObject[step].SetActive(true);

                for (int i = 0; i < FirstStep.Count; i++)
                {
                    if (FirstStep[i].text != "")
                    {
                        count++;
                    }
                }

                maxnum = count;
                break;

            case 1:
                CommandObject[step].SetActive(true);

                for (int i = 0; i < ItemDerivation.Count; i++)
                {
                    if (ItemDerivation[i].text != "")
                    {
                        count++;
                    }
                }

                maxnum = count;
                break;

            case 2:
                CommandObject[step].SetActive(true);

                for (int i = 0; i < FightDerivation.Count; i++)
                {
                    if (FightDerivation[i].text != "")
                    {
                        count++;
                    }
                }

                maxnum = count;
                break;

            case 3:
                CommandObject[step].SetActive(true);

                for (int i = 0; i < SkillDerivation.Count; i++)
                {
                    if (SkillDerivation[i].text != "")
                    {
                        count++;
                    }
                }

                maxnum = count;
                break;

            case 4:
                CommandObject[step].SetActive(true);

                for (int i = 0; i < EnemyDerivation.Count; i++)
                {
                    if (EnemyDerivation[i].text != "")
                    {
                        count++;
                    }
                }

                maxnum = count;
                break;

            case 5:
                CommandObject[step].SetActive(true);

                for (int i = 0; i < PlayerDerivation.Count; i++)
                {
                    if (PlayerDerivation[i].text != "")
                    {
                        count++;
                    }
                }

                maxnum = count;                
                break;
        }
       
        //Debug.Log(num); 

        while (true)
        {            
            //右矢印キーが入力された時の処理
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                //現在選択中のパネルを半透明にする
                TextTranslucent(step,num);

                //次のパネルを選択する
                num = (num + 1) % maxnum;

                //デバック用
                //Debug.Log(num);

                //次のパネルを非透明にする
                TextOpaque(step, num);

            }
            //右矢印キーが入力された時の処理
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //現在選択中のパネルを半透明にする
                TextTranslucent(step, num);

                //前のパネルを選択する
                num = (num == 0) ? maxnum - 1 : --num;

                //デバック用
                //Debug.Log(num);

                //前のパネルを非透明にする
                TextOpaque(step,num);
            }
            //決定キーとしている「Z」が押されたらパネルを選択する
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                //1フレーム待機することでInputフレーム回避を狙う
                yield return null;

                //Debug.Log(step);
                //Debug.Log(previous);
                //Debug.Log(num);

                switch (step)
                {
                    case 0:
                        if(num == 0)
                        {
                            //アイテム選択に移行する
                            TextUpdate(1, 0);

                            yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(1, step, player));
                            //yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("アイテムなぞ…\n使ってんじゃ…\nねぇえええええ！！"));
                        }
                        else if(num == 1)
                        {
                            //攻撃タイプ選択に移行する
                            CommandObject[0].SetActive(false);
                            //IEnumerator coroutine = BattleOperationMk2.Instance.OperationSelect(2 , step);
                            //yield return StartCoroutine(coroutine);
                            yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(2 , step, player));
                        }
                        else if(num == 2)
                        {
                            //逃走処理
                            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("逃走は未実装故…\n戦え！！命の続く限り！！"));
                        }
                        yield break;

                    case 1:
                        //アイテム一覧の処理を書く
                        CommandObject[1].SetActive(false);
                        yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(JudgmentItemType(num), step, player, num));
                        yield break;

                    case 2:                      
                        if (num == 0)
                        {
                            //通常攻撃の処理を書く
                            CommandObject[2].SetActive(false);
                            yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(4, step, player));
                        }
                        else if (num == 1)
                        {
                            //スキル選択に移行する    
                            CommandObject[2].SetActive(false);
                            yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(3, step, player));
                            //yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("技なんてねぇ！！\n俺の武器はこの拳のみだぁ！！"));
                        }
                        yield break;

                    case 3:
                        CommandObject[3].SetActive(false);
                        yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(JudgmentSkillType(num,player), step, player, num));
                        yield break;

                    case 4:
                        List<CharacterParam> ParamList = BattleSceneManagerMk2.Instance.GetCharcterList();

                        if (previous == 1)
                        {
                            //アイテムの処理
                            for (int i = 0; i < ParamList.Count; i++)
                            {
                                if (EnemyDerivation[num].text == ParamList[i].name)
                                {
                                    CommandObject[4].SetActive(false);
                                    CommandObject[0].SetActive(true);
                                    InitTextBox(0);
                                    Void.Instance.Move(0);
                                    //SoundManager.instance.PlayVC(VCLabel.VC3);
                                    yield return StartCoroutine(BattleSceneManagerMk2.Instance.UseItemAction(player, ParamList[i],selectionNum));
                                }
                            }

                        }
                        else if(previous == 2)
                        {
                            //通常攻撃の処理
                            for (int i = 0; i < ParamList.Count; i++)
                            {
                                if (EnemyDerivation[num].text == ParamList[i].name)
                                {
                                    CommandObject[4].SetActive(false);
                                    CommandObject[0].SetActive(true);
                                    InitTextBox(0);
                                    Void.Instance.Move(0);
                                    //SoundManager.instance.PlayVC(VCLabel.VC3);
                                    yield return StartCoroutine(BattleSceneManagerMk2.Instance.Attack(player, ParamList[i]));
                                }
                            }
                        }
                        else if(previous == 3)
                        {
                            //スキル処理
                            for (int i = 0; i < ParamList.Count; i++)
                            {
                                if (EnemyDerivation[num].text == ParamList[i].name)
                                {
                                    CommandObject[4].SetActive(false);
                                    CommandObject[0].SetActive(true);
                                    InitTextBox(0);
                                    Void.Instance.Move(0);
                                    //SoundManager.instance.PlayVC(VCLabel.VC3);
                                    yield return StartCoroutine(BattleSceneManagerMk2.Instance.UseSkillAction(player, ParamList[i], selectionNum));
                                    yield break;
                                }
                            }
                        }
                        //Debug.Log(selectionNum);
                        yield break;

                    case 5:
                        ParamList = BattleSceneManagerMk2.Instance.GetCharcterList();

                        if (previous == 1)
                        {
                            //アイテムの処理
                            for (int i = 0; i < ParamList.Count; i++)
                            {
                                if (PlayerDerivation[num].text == ParamList[i].name)
                                {
                                    CommandObject[5].SetActive(false);
                                    CommandObject[0].SetActive(true);
                                    InitTextBox(0);
                                    Void.Instance.Move(0);
                                    //SoundManager.instance.PlayVC(VCLabel.VC3);
                                    yield return StartCoroutine(BattleSceneManagerMk2.Instance.UseItemAction(player, ParamList[i], selectionNum));
                                }
                            }

                        }                       
                        else if (previous == 3)
                        {
                            //スキル処理
                            for (int i = 0; i < ParamList.Count; i++)
                            {
                                if (PlayerDerivation[num].text == ParamList[i].name)
                                {
                                    CommandObject[5].SetActive(false);
                                    CommandObject[0].SetActive(true);
                                    InitTextBox(0);
                                    Void.Instance.Move(0);
                                    //SoundManager.instance.PlayVC(VCLabel.VC3);
                                    yield return StartCoroutine(BattleSceneManagerMk2.Instance.UseSkillAction(player, ParamList[i], selectionNum));
                                    yield break;
                                }
                            }
                        }
                        Debug.Log(step);
                        yield break;
                }                
            }
            else if(Input.GetKeyDown(KeyCode.X))
            {
                //1フレーム待機することでInputフレーム回避を狙う
                yield return null;
                switch (step)
                {
                    case 0:
                        continue;

                    case 1:
                        CommandObject[step].SetActive(false);
                        IEnumerator coroutine = BattleOperationMk2.Instance.OperationSelect(0, step, player);
                        yield return StartCoroutine(coroutine);
                        yield break;

                    case 2:
                        CommandObject[step].SetActive(false);
                        coroutine = BattleOperationMk2.Instance.OperationSelect(0, step, player);
                        yield return StartCoroutine(coroutine);
                        yield break;

                    case 3:
                        CommandObject[step].SetActive(false);
                        coroutine = BattleOperationMk2.Instance.OperationSelect(2, step, player);
                        yield return StartCoroutine(coroutine);
                        yield break;

                    case 4:
                        CommandObject[step].SetActive(false);
                        coroutine = BattleOperationMk2.Instance.OperationSelect(previous, step, player);
                        yield return StartCoroutine(coroutine);
                        yield break;

                    case 5:
                        CommandObject[step].SetActive(false);
                        coroutine = BattleOperationMk2.Instance.OperationSelect(previous, step, player);
                        yield return StartCoroutine(coroutine);

                        yield break;
                }               
            }
            yield return null;
        }
    }

    private void TextTranslucent(int step,int num)
    {
        switch (step)
        {
            case 0:
                FirstStep[num].color = new Color(FirstStep[num].color.r, FirstStep[num].color.g, FirstStep[num].color.b, 0.5f);
                break;

            case 1:
                ItemDerivation[num].color = new Color(ItemDerivation[num].color.r, ItemDerivation[num].color.g, ItemDerivation[num].color.b, 0.5f); ;
                break;

            case 2:
                FightDerivation[num].color = new Color(FightDerivation[num].color.r, FightDerivation[num].color.g, FightDerivation[num].color.b, 0.5f);
                break;

            case 3:
                SkillDerivation[num].color = new Color(SkillDerivation[num].color.r, SkillDerivation[num].color.g, SkillDerivation[num].color.b, 0.5f);
                break;

            case 4:
                EnemyDerivation[num].color = new Color(EnemyDerivation[num].color.r, EnemyDerivation[num].color.g, EnemyDerivation[num].color.b, 0.5f);
                break;

            case 5:
                PlayerDerivation[num].color = new Color(PlayerDerivation[num].color.r, PlayerDerivation[num].color.g, PlayerDerivation[num].color.b, 0.5f);
                break;
        }
    }

    private void TextOpaque(int step, int num)
    {
        switch (step)
        {
            case 0:
                FirstStep[num].color = new Color(FirstStep[num].color.r, FirstStep[num].color.g, FirstStep[num].color.b, 1f);
                break;

            case 1:
                ItemDerivation[num].color = new Color(ItemDerivation[num].color.r, ItemDerivation[num].color.g, ItemDerivation[num].color.b, 1f); ;
                break;

            case 2:
                FightDerivation[num].color = new Color(FightDerivation[num].color.r, FightDerivation[num].color.g, FightDerivation[num].color.b, 1f);
                break;

            case 3:
                SkillDerivation[num].color = new Color(SkillDerivation[num].color.r, SkillDerivation[num].color.g, SkillDerivation[num].color.b, 1f);
                break;

            case 4:
                EnemyDerivation[num].color = new Color(EnemyDerivation[num].color.r, EnemyDerivation[num].color.g, EnemyDerivation[num].color.b, 1f);
                break;

            case 5:
                PlayerDerivation[num].color = new Color(PlayerDerivation[num].color.r, PlayerDerivation[num].color.g, PlayerDerivation[num].color.b, 1f);
                break;
        }
    }

    public int JudgmentSkillType(int num,CharacterParam actor)
    {
        int returnNum = 0;

        switch (actor.skill[num].skillType)
        {
            case SkillType.None:
                returnNum = 0;
                break;
            case SkillType.Damage:
                returnNum = 4;

                break;
            case SkillType.Heal:
                returnNum = 5;

                break;
            case SkillType.Buff:
                returnNum = 5;

                break;
            case SkillType.Debuff:
                returnNum = 4;

                break;
            case SkillType.Special:
                returnNum = 0;

                break;
        }

        return returnNum;
    }

    public int JudgmentItemType(int num)
    {
        int returnNum = 0;

        switch (ItemDataBase.instance.ItemData.ItemParamList[num].itemType)
        {
            case ItemType.None:
                returnNum = 0;
                break;
            case ItemType.Damage:
                returnNum = 4;

                break;
            case ItemType.Heal:
                returnNum = 5;

                break;
            case ItemType.Buff:
                returnNum = 5;

                break;
            case ItemType.Debuff:
                returnNum = 4;

                break;
            case ItemType.Special:
                returnNum = 0;

                break;
        }

        return returnNum;
    }

    public enum First
    {
        Item,
        Fight,
        Run
    }


}