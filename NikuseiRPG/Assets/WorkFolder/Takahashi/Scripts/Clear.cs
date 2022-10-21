using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

    void Update()
    {
        //Aボタンを押したらシーン移動
        if (Input.GetKeyDown("joystick button 0"))
        {
            //()の中身はメインゲームのシーン名を入れてください
            SceneManager.LoadScene("Title");

            //確認用
            Debug.Log("button0が押されました");
        }
    }
}
