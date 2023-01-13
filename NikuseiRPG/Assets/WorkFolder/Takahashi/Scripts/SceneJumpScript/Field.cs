using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Field : MonoBehaviour
{
    private bool battle;

    void Start()
    {
        battle = false;
    }

    void Update()
    {
        //Enterを押したらシーン移動
        if (Input.GetKeyDown(KeyCode.Return))
        {
            battle = true;
        }

        //ESCを押したらシーン移動
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //()の中身はクリアシーンのシーン名を入れてください
            SceneManager.LoadScene("ClearScene");
        }

        if (battle == true)
        {
            //()の中身はバトルシーンのシーン名を入れてください
            SceneManager.LoadScene("BattleSceneCanvas");

            battle = false;
        }
    }
}
