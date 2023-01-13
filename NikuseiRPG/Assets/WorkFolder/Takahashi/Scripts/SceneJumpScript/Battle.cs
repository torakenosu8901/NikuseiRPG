using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    void Update()
    {
        //Enterを押したらシーン移動
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //()の中身はメインゲームのシーン名を入れてください
            SceneManager.LoadScene("Main");
        }
    }
}
