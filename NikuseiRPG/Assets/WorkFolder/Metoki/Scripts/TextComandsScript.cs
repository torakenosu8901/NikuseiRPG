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
    private GameObject sentakuPanel;
    //たたかうコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject sentakuPanelTwo;
    //逃げるコマンドの上にかぶさるカーソル
    [SerializeField]
    private GameObject sentakuPanelThree;

    //逃げるコマンドを押した後に出るテキスト
    [SerializeField]
    private GameObject runAwayText;
    //アイテムコマンドを押した後に出るテキスト
    [SerializeField]
    private GameObject itemText;

    [SerializeField]
     public Text damegeText;

     /*[Serializefield]
     private List<DamegeDate> object01;*/

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
    //void Update()
    void Update()
    {
        //アイテムコマンドにカーソルがいってるとき
        if (sentakuPanel.activeSelf)
        {

            //if (Input.GetKeyDown(KeyCode.RightArrow))
            if(Input.GetButtonDown("Itempanel"))
            {
                sentakuPanel.SetActive(false);
                sentakuPanelTwo.SetActive(true);
            }
          
            //Aボタンを押したらアイテムテキストを表示
            //if (Input.GetKeyDown("joystick button 0"))
            if (Input.GetKeyDown(KeyCode.A))
            {
                battleSceneManager.ItemPhase();
                itemText.SetActive(!itemText.activeSelf);
            }
        }
        //たたかうコマンドにカーソルがいってるとき
        else if (sentakuPanelTwo.activeSelf)
        {


            //if (Input.GetKeyDown(KeyCode.RightArrow))
            if(Input.GetButtonDown("Battlepanelright"))
            {
                sentakuPanelTwo.SetActive(false);
                sentakuPanelThree.SetActive(true);
            }

            //else if (Input.GetKeyDown(KeyCode.LeftArrow))
            else if (Input.GetButtonDown("Battlepanelleft"))
            {
                sentakuPanelTwo.SetActive(false);
                sentakuPanel.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                battleSceneManager.AttackPhase();
                damegeText.text = damegeData.ATK.ToString();
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                battleSceneManager.EscapePhase();
                runAwayText.SetActive(!runAwayText.activeSelf);
            }
        }
    }
    /*public void DamegeDate()
    {
        string.text = 
    }*/
}
