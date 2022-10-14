using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComandsScript : MonoBehaviour
{
    //�A�C�e���R�}���h�̏�ɂ��Ԃ���J�[�\��
    [SerializeField]
    private GameObject sentakuPanel;
    //���������R�}���h�̏�ɂ��Ԃ���J�[�\��
    [SerializeField]
    private GameObject sentakuPanelTwo;
    //������R�}���h�̏�ɂ��Ԃ���J�[�\��
    [SerializeField]
    private GameObject sentakuPanelThree;

    [SerializeField] 
    private GameObject runAwayText;

    [SerializeField]
    private GameObject itemText;

    [SerializeField]
    private GameObject damegeText;

    void Start()
    {
        //�J�n����sentakuPanel�ȊO�̃e�L�X�g���\���ɂ���
        sentakuPanel.SetActive(true);
        sentakuPanelTwo.SetActive(false);
        sentakuPanelThree.SetActive(false);
        runAwayText.SetActive(false);
        itemText.SetActive(false);
        damegeText.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {

        
            //�A�C�e���R�}���h�ɃJ�[�\���������Ă�Ƃ�
            if (sentakuPanel.activeSelf)
            {
               
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sentakuPanel.SetActive(false);
                    sentakuPanelTwo.SetActive(true);
                }
                //A�{�^������������A�C�e���e�L�X�g��\��
                //if (Input.GetKeyDown("joystick button 0"))
                if(Input.GetKeyDown(KeyCode.A))
                {
                    itemText.SetActive(!itemText.activeSelf);
                }
            }
            //���������R�}���h�ɃJ�[�\���������Ă�Ƃ�
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
                   damegeText.SetActive(!damegeText.activeSelf);
                }
            }
            //������R�}���h�ɃJ�[�\���������Ă�Ƃ�
            else if (sentakuPanelThree.activeSelf)
            {
              
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    sentakuPanelThree.SetActive(false);
                    sentakuPanelTwo.SetActive(true);
                }
                //A�{�^�����������瓦����e�L�X�g�\���B

                //if (Input.GetKeyDown("joystick button 0"))
                if(Input.GetKeyDown(KeyCode.A))
                {
                    runAwayText.SetActive(!runAwayText.activeSelf);
                }
        }               

    }
}
