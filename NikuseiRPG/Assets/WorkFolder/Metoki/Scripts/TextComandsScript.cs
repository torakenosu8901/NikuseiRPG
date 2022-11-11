using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComandsScript : MonoBehaviour
{
    //スクリタブルオブジェクトの中身を持ってくる
    [SerializeField]
    private DamegeData damegeData;

    [SerializeField]
    private SkillList skillList;

    [SerializeField]
    private ItemList itemList;

    [SerializeField]
    private GameObject itemSentakuPanel;

    //たたかうコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject battleSentakuPanel;

    //逃げるコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject runAwaySentakuPanel;
    [SerializeField]
    private GameObject itemText;
    [SerializeField]
    private GameObject attakButton;
    [SerializeField]
    private GameObject sukillButton;
    [SerializeField]
    private GameObject attakPanel;
    [SerializeField]
    private GameObject sukillPanel;
    [SerializeField]
    private GameObject itemComand;
    [SerializeField]
    private GameObject battleComand;
    [SerializeField]
    private GameObject runAwayComand;
    [SerializeField]
    private GameObject attakButtonComand;
    //private string itemName;
    //private string skillName;

    void Start()
    {
        //開始時にsentakuPanel以外のテキストを非表示にする
        itemSentakuPanel.SetActive(true);
        battleSentakuPanel.SetActive(false);
        runAwaySentakuPanel.SetActive(false);
        attakButton.SetActive(false);
        sukillButton.SetActive(false);
        attakPanel.SetActive(false);
        sukillPanel.SetActive(false);
        itemText.SetActive(false);
    }
    void Update()
    {
        //アイテムコマンドにカーソルがいってるとき
        if (itemSentakuPanel.activeSelf)
        {
            if (Input.GetButtonDown("ItemPanel"))
            {
                itemSentakuPanel.SetActive(false);
                battleSentakuPanel.SetActive(true);
            }

            //Aボタンを押したらアイテムテキストを表示
            if (Input.GetButtonDown("ItemText"))
            {
                battleComand.SetActive(false);
                runAwayComand.SetActive(false);
                //itemText.text = itemList.itemName.ToString();
                itemText.SetActive(true);
                itemSentakuPanel.SetActive(false);
            }
        }
        else if (itemText.activeSelf)
        {
            if (Input.GetButtonDown("ItemTextBack"))
            {
                battleComand.SetActive(true);
                runAwayComand.SetActive(true);
                itemSentakuPanel.SetActive(true);
                itemText.SetActive(false);
            }
        }
        //たたかうコマンドにカーソルがいってるとき
        else if (battleSentakuPanel.activeSelf)
        {
            if (Input.GetButtonDown("BattlePanelRight"))
            {
                battleSentakuPanel.SetActive(false);
                runAwaySentakuPanel.SetActive(true);
            }
            else if (Input.GetButtonDown("BattlePanelLeft"))
            {
                battleSentakuPanel.SetActive(false);
                itemSentakuPanel.SetActive(true);
            }
            //Aボタンでたたかうに移行
            if (Input.GetButtonDown("BattleText"))
            {
                itemComand.SetActive(false);
                runAwayComand.SetActive(false);
                attakButton.SetActive(true);
                sukillButton.SetActive(true);
                attakPanel.SetActive(true);
                battleSentakuPanel.SetActive(false);
            }
        }
        //こうげきパネルにカーソルがあっているとき
        else if (attakPanel.activeSelf)
        {
            if (Input.GetButtonDown("PanelChange"))
            {
                sukillPanel.SetActive(true);
                attakPanel.SetActive(false);
            }
            if (Input.GetButtonDown("AttakPanel"))
            {
                BattleSceneManager.Instance.AttackPhase();
            }
            //行動画面に戻る
            if (Input.GetButtonDown("PanelBack"))
            {
                itemComand.SetActive(true);
                runAwayComand.SetActive(true);
                attakButton.SetActive(false);
                sukillButton.SetActive(false);
                battleSentakuPanel.SetActive(true);
                attakPanel.SetActive(false);
                sukillPanel.SetActive(false);
            }
        }
        //スキルパネルにカーソルがあっているとき
        else if (sukillPanel.activeSelf)
        {
            if (Input.GetButtonDown("SkillPanelChange"))
            {
                sukillPanel.SetActive(false);
                attakPanel.SetActive(true);
            }
            //行動画面に戻る
            if (Input.GetButtonDown("PanelBack"))
            {
                itemComand.SetActive(true);
                runAwayComand.SetActive(true);
                attakButtonComand.SetActive(true);
                attakButton.SetActive(false);
                sukillButton.SetActive(false);
                battleSentakuPanel.SetActive(true);
                attakPanel.SetActive(false);
                sukillPanel.SetActive(false);

            }
            //スキルリストを出す
            if (Input.GetButtonDown("SkillPanel"))
            {
                attakButtonComand.SetActive(false);
                //skillName = skillList.skillParamList[0].skillName;
            }
        }
        //逃げるコマンドにカーソルがいってるとき
        else if (runAwaySentakuPanel.activeSelf)
        {

            if (Input.GetButtonDown("EscapePanelLeft"))
            {
                runAwaySentakuPanel.SetActive(false);
                battleSentakuPanel.SetActive(true);
            }
            //Aボタンを押したら逃げるテキスト表示。
            if (Input.GetButtonDown("EscapeText"))
            {
                BattleSceneManager.Instance.EscapePhase();
            }
        }
    }
}
