using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComandsScript : MonoBehaviour
{
    [SerializeField]
    public BattleSceneManager battleSceneManager;
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
    private GameObject attakButton;
    [SerializeField]
    private GameObject sukillButton;
    [SerializeField]
    private GameObject attakPanel;
    [SerializeField]
    private GameObject sukillPanel;
    

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
    }

    void Update()
    {
        //アイテムコマンドにカーソルがいってるとき
        if (itemSentakuPanel.activeSelf)
        {

            //if (Input.GetKeyDown(KeyCode.RightArrow))
            if(Input.GetButtonDown("ItemPanel"))
            {
                itemSentakuPanel.SetActive(false);
                battleSentakuPanel.SetActive(true);
            }
          
            //Aボタンを押したらアイテムテキストを表示
            if (Input.GetButtonDown("ItemText"))
            {
                //damegeText.text = damegeData.ATK.ToString();
                battleSceneManager.ItemPhase();
            }

            if (Input.GetButtonDown("ItemTextBack"))
            {
                Debug.Log("Back");
            }
        }
        //たたかうコマンドにカーソルがいってるとき
        else if (battleSentakuPanel.activeSelf)
        {

            if(Input.GetButtonDown("BattlePanelRight"))
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
                attakButton.SetActive(true);
                sukillButton.SetActive(true);
                attakPanel.SetActive(true);
                //damegeText.text = damegeData.ATK.ToString();
            }
            else if (attakPanel.activeSelf)
            {
                if (Input.GetButtonDown("PanelChange"))
                {
                    sukillPanel.SetActive(true);
                    attakPanel.SetActive(false);
                }
                if (Input.GetButtonDown("AttakPanel"))
                {
                    battleSceneManager.AttackPhase();
                }
            }
            else if (sukillPanel.activeSelf)
            {
                if (Input.GetButtonDown("SukillPanelChange"))
                {
                    sukillPanel.SetActive(false);
                    attakPanel.SetActive(true);
                }
                if (Input.GetButtonDown("SukillPanel"))
                {
                    Debug.Log("Sukill");
                    //damegeText.text = damegeData.ATK.ToString();
                }
                if (Input.GetButtonDown("SukillTextBack"))
                {
                    Debug.Log("SukillBack");
                }
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
                battleSceneManager.EscapePhase();
                
            }
        }
    }    
}
