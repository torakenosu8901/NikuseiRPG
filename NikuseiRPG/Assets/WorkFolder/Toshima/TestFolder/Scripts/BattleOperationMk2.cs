using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleOperationMk2 : MonoBehaviour
{
    #region 選択演出用のゲームオブジェクト達

    //アイテムリスト反映
    public ItemList itemList;

    public SkillList skillList;

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

    //アイテムリスト
    [SerializeField]
    private GameObject itemListPanel;

    [SerializeField]
    private Text itemListText;

    [SerializeField]
    private Text itemListTextSecond;

    [SerializeField]
    private List<Text> itemListTextThird;

    [SerializeField]
    private GameObject sentakPanel;

    [SerializeField]
    private GameObject sentakPanelSecond;

    [SerializeField]
    private GameObject sentakPanelThird;

    private string[] itemName = new string[3];

    //スキルリスト
    [SerializeField]
    private GameObject skillListPanel;

    [SerializeField]
    private Text skillListText;

    [SerializeField]
    private Text skillListTextSecond;

    [SerializeField]
    private Text skillListTextThird;

    [SerializeField]
    private GameObject skillPanel;

    [SerializeField]
    private GameObject skillPanelSecond;

    [SerializeField]
    private GameObject skillPanelSared;

    private string[] skillName = new string[2];
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
        Debug.Log(1);
        NPberUpdate();
        Debug.Log(2);
        sentakPanel.SetActive(false);
        Debug.Log(3);
        sentakPanelSecond.SetActive(false);
        Debug.Log(4);
        sentakPanelThird.SetActive(false);
        Debug.Log(5);
        itemListPanel.SetActive(false);
        Debug.Log(6);
       
        skillListPanel.SetActive(false);
        Debug.Log(10);
        /*skillName[0] = skillList.SkillParamList[0].skillName;
        Debug.Log(11);
        skillName[1] = skillList.SkillParamList[1].skillName;
        Debug.Log(12);
        skillName[2] = skillList.SkillParamList[2].skillName;*/
        /*itemName[0] = itemList.ItemParamList[0].itemName;
        Debug.Log(11);
        skillName[1] = itemList.ItemParamList[1].itemName;
        Debug.Log(12);
        skillName[2] = itemList.ItemParamList[2].itemName;*/
        Debug.Log(13);

     
    }

    public void NPberUpdate()
    {
        NPber[0].maxValue = BattleSceneManagerMk2.Instance.GetCharcterList()[0].maxnp;
        NPber[1].maxValue = BattleSceneManagerMk2.Instance.GetCharcterList()[1].maxnp;

        NPber[0].value = BattleSceneManagerMk2.Instance.GetCharcterList()[0].np;
        NPber[1].value = BattleSceneManagerMk2.Instance.GetCharcterList()[1].np;
    }

    private void TextUpdate(int step, int scrollnum)
    {
        switch (step)
        {
            case 0:
                //処理なし
                break;

            case 1:
                //アイテム一覧のテキスト更新
                if(scrollnum == 0)
                {
                    for (int i = 0; i < itemList.ItemParamList.Count; i++)
                    {
                        
                        ItemDerivation[i].text = itemList.ItemParamList[i].itemName;
                    }
                }
                break;

            case 2:
                //処理なし
                break;

            case 3:
                //スキル一覧のテキスト更新
                if(scrollnum == 0)
                {
                    for (int i = 0; i < skillList.SkillParamList.Count; i++)
                    {
                        SkillDerivation[i].text = skillList.SkillParamList[i].skillName;
                    }
                }
                break;

            case 4:
                //エネミー一覧のテキスト更新
                List<CharacterParam> ParamList = BattleSceneManagerMk2.Instance.GetCharcterList();
                int count = 0;
                for (int i = 0; i < ParamList.Count; i++)
                {
                    if (ParamList[i].type == CharacterType.Enemy && count < EnemyDerivation.Count)
                    {
                        EnemyDerivation[count].text = ParamList[i].name;
                        count++;
                    }
                }

                Debug.Log(count);
                Debug.Log(EnemyDerivation.Count);

                for (int i = count; i < EnemyDerivation.Count;)
                {
                    EnemyDerivation[i].text = "";
                    EnemyDerivation.Remove(EnemyDerivation[i]);
                    Debug.Log(EnemyDerivation.Count);
                }
                break;

            case 5:
                //プレイヤー一覧のテキスト更新
                break;
        }
    }

    /// <summary>
    /// 選択肢のα地を初期化する関数
    /// </summary>
    /// <param name="step">現在処理がどこまで進んでいるのかを示す</param>
    private void InitTextBox(int step)
    {
        switch (step)
        {
            case 0:
                TextOpaque(0, 0);
                for (int i = 1; i < FirstStep.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                break;

            case 1:
                TextOpaque(1, 0);
                for (int i = 1; i < ItemDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                break;

            case 2:
                TextOpaque(2, 0);
                for (int i = 1; i < FightDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                break;

            case 3:
                TextOpaque(3, 0);
                for (int i = 1; i < SkillDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                break;

            case 4:
                TextOpaque(4, 0);
                for (int i = 1; i < EnemyDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                TextUpdate(4, 0);
                break;

            case 5:
                TextOpaque(5, 0);
                for (int i = 1; i < PlayerDerivation.Count; i++)
                {
                    TextTranslucent(step, i);
                }
                break;
        }
    }

    public IEnumerator OperationSelect(int step, int previous, CharacterParam player)
    {

        //string str = "";

        //選択処理用の変数
        int num = 0;

        //選択処理の最大値
        int maxnum = 0;

        //Debug.Log(num);

        InitTextBox(step);

        switch (step)
        {
            case 0:
                CommandObject[step].SetActive(true);
                maxnum = FirstStep.Count;
                break;

            case 1:
                CommandObject[step].SetActive(true);
                maxnum = ItemDerivation.Count;
                break;

            case 2:
                CommandObject[step].SetActive(true);
                maxnum = FightDerivation.Count;
                break;

            case 3:
                CommandObject[step].SetActive(true);
                maxnum = SkillDerivation.Count;
                break;

            case 4:
                CommandObject[step].SetActive(true);
                maxnum = EnemyDerivation.Count;
                break;

            case 5:
                CommandObject[step].SetActive(true);
                maxnum = PlayerDerivation.Count;
                break;
        }

        //Debug.Log(num); 

        while (true)
        {
            //右矢印キーが入力された時の処理
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {

                //現在選択中のパネルを半透明にする
                TextTranslucent(step, num);

                //次のパネルを選択する
                num = (num + 1) % maxnum;

                //デバック用
                Debug.Log(num);

                //次のパネルを非透明にする
                TextOpaque(step, num);

            }
            //右矢印キーが入力された時の処理
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                //現在選択中のパネルを半透明にする
                TextTranslucent(step, num);

                //前のパネルを選択する
                num = (num == 0) ? maxnum - 1 : --num;

                //デバック用
                Debug.Log(num);

                //前のパネルを非透明にする
                TextOpaque(step, num);
            }
            //決定キーとしている「Z」が押されたらパネルを選択する
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                //1フレーム待機することでInputフレーム回避を狙う
                yield return null;
                switch (step)
                {
                    case 0:
                        if (num == 0)
                        {
                            //                yield break;
                            //            }
                            //        
                            //        
                            //            if (Input.GetKeyDown(KeyCode.A))
                            //            {
                            //                //アイテム処理
                            //                yield return StartCoroutine(ItemController.Instance.ItemPhase());

                            //                yield break;
                            //            }
                            //        
                            //       
                            //            if (Input.GetKeyDown(KeyCode.A))
                            //            {
                            //                //アイテム処理
                            //                yield return StartCoroutine(ItemController.Instance.ItemPhase());

                            //                yield break;
                            //            }
                            //        }
                            //    
                            //
                            //else if (num == 1)
                            //{
                            //    //攻撃タイプ選択に移行する
                            //    CommandObject[0].SetActive(false);
                            //    //IEnumerator coroutine = BattleOperationMk2.Instance.OperationSelect(2 , step);
                            //    //yield return StartCoroutine(coroutine);
                            //    yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(2, step, player));
                            //    yield break;
                            //}
                            //else if (num == 2)
                            //{
                            //    //逃走処理
                            //    yield return StartCoroutine(EscapeController.Instance.EscapePhase());

                            //    yield break;
                            //通常攻撃の処理を書く
                            TextUpdate(1, 0);

                            CommandObject[1].SetActive(false);
                            yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(1, step, player));
                            yield break;
                        }
                        break;

                    case 1:
                        {
                        //アイテム一覧の処理を書く
                            Debug.Log("Ok");
                            yield return StartCoroutine(ItemController.Instance.ItemPhase(num));
                            yield break;
                        }
                    break;

                    case 2:
                        if (num == 0)
                        {
                            //通常攻撃の処理を書く
                            CommandObject[2].SetActive(false);
                            yield return StartCoroutine(BattleOperationMk2.Instance.OperationSelect(4, step, player));
                            yield break;
                        }
                        else if (num == 1)
                        {
                            skillListPanel.SetActive(false);

                            if (skillListPanel.activeSelf)
                            {
                                skillPanel.SetActive(true);
                                skillListText.text = skillName[0];
                                skillListTextSecond.text = skillName[1];
                                skillListTextThird.text = skillName[2];
                                if (Input.GetKeyDown(KeyCode.RightArrow))
                                {
                                    skillPanel.SetActive(false);
                                    skillPanelSecond.SetActive(true);
                                }
                                else if (Input.GetKeyDown(KeyCode.RightArrow))
                                {
                                    skillPanelSecond.SetActive(false);
                                    skillPanelSared.SetActive(true);
                                }
                                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                                {
                                    skillPanel.SetActive(true);
                                    skillPanelSecond.SetActive(false);
                                }
                                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                                {
                                    skillPanelSared.SetActive(false);
                                    skillPanelSecond.SetActive(true);
                                }
                                if (Input.GetKeyDown(KeyCode.A))
                                {
                                    skillListPanel.SetActive(false);
                                }
                            //スキル選択に移行する
                            TextUpdate(1, 0);
                            }
                        }
                        break;

                    case 3:
                        maxnum = SkillDerivation.Count;
                        yield break;

                    case 4:
                        if (num == 0)
                        {
                            List<CharacterParam> ParamList = BattleSceneManagerMk2.Instance.GetCharcterList();
                            for (int i = 0; i < ParamList.Count; i++)
                            {
                                if (EnemyDerivation[num].text == ParamList[i].name)
                                {
                                    CommandObject[4].SetActive(false);
                                    CommandObject[0].SetActive(true);
                                    InitTextBox(0);
                                    Void.Instance.Move(0);
                                    SoundManager.instance.PlayVC(VCLabel.VC3);
                                    yield return StartCoroutine(BattleSceneManagerMk2.Instance.Attack(player, ParamList[i]));
                                }
                            }

                            yield break;
                        }
                        else if (num == 1)
                        {

                        }
                        else if (num == 2)
                        {

                        }
                        else if (num == 3)
                        {

                        }
                        yield break;

                    case 5:
                        maxnum = PlayerDerivation.Count;
                        yield break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.X))
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

    private void TextTranslucent(int step, int num)
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

    public enum First
    {
        Item,
        Fight,
        Run
    }


}