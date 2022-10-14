using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //Aボタンを押したらシーン移動
        if(Input.GetKeyDown("joystick button 0"))
        {
            //()の中身はメインゲームのシーン名を入れてください
            SceneManager.LoadScene("Main");

            //確認用
            Debug.Log("button0が押されました");
        }
    }
}
