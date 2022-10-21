﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComandsScript : MonoBehaviour
{
    public int PlayerSerecto;
    //スクリタブルオブジェクトの中身を持ってくる
    [SerializeField]
    private DamegeData damegeData;
    //アイテムコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject sentakuPanel;
    //たたかうコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject sentakuPanelTwo;
    //逃げるコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject sentakuPanelThree;

    [SerializeField] 
    private GameObject runAwayText;

    [SerializeField]
    private GameObject itemText;

    [SerializeField]
    public Text damegeText;

    void Start()
    {
        //開始時にsentakuPanel以外のテキストを非表示にする
        sentakuPanel.SetActive(true);
        sentakuPanelTwo.SetActive(false);
        sentakuPanelThree.SetActive(false);
        runAwayText.SetActive(false);
        itemText.SetActive(false);
        //damegeText.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {

        
            //アイテムコマンドにカーソルがいってるとき
            if (sentakuPanel.activeSelf)
            {
               
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sentakuPanel.SetActive(false);
                    sentakuPanelTwo.SetActive(true);
                }
                //Aボタンを押したらアイテムテキストを表示
                //if (Input.GetKeyDown("joystick button 0"))
                if(Input.GetKeyDown(KeyCode.A))
                {
                    PlayerSerecto = 1;
                    itemText.SetActive(!itemText.activeSelf);
                }
            }
            //たたかうコマンドにカーソルがいってるとき
            else if (sentakuPanelTwo.activeSelf)
            {

               
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sentakuPanelTwo.SetActive(false);
                    sentakuPanelThree.SetActive(true);
                }
                
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    sentakuPanelTwo.SetActive(false);
                    sentakuPanel.SetActive(true);
                }
                if(Input.GetKeyDown(KeyCode.A))
                {
                PlayerSerecto = 0;
                damegeText.text = damegeData.ATK.ToString() + "ダメージ"; 
                }
            }
            //逃げるコマンドにカーソルがいってるとき
            else if (sentakuPanelThree.activeSelf)
            {
              
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    sentakuPanelThree.SetActive(false);
                    sentakuPanelTwo.SetActive(true);
                }
                //Aボタンを押したら逃げるテキスト表示。

                //if (Input.GetKeyDown("joystick button 0"))
                if(Input.GetKeyDown(KeyCode.A))
                {
                PlayerSerecto = 2;
                runAwayText.SetActive(!runAwayText.activeSelf);
                }
            }               

    }
}
