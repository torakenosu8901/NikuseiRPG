﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private GameObject DataBase;

    Coroutine coroutine;

    private void Start()
    {
        coroutine = StartCoroutine(TitleCallAndTitleBGM());
        //int i = Random.Range(0, 2);
        //if(i == 0)
        //{
        //    SoundManager.instance.PlayVC(VCLabel.VC1);
        //}
        //else
        //{
        //    SoundManager.instance.PlayVC(VCLabel.VC2);
        //}

    }

    void Update()
    {
        ////Aボタンを押したらシーン移動
        //if(Input.GetKeyDown("joystick button 0"))
        //{
        //    //()の中身はメインゲームのシーン名を入れてください
        //    SceneManager.LoadScene("Main");

        //    //確認用
        //    Debug.Log("button0が押されました");
        //}

        //Enterを押したらシーン移動
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    //()の中身はメインゲームのシーン名を入れてください
        //    SceneManager.LoadScene("Main");
        //}

        //Zを押したらシーン移動(*外島)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //初期化処理
            //GameObject obj = new GameObject();

            //リファクタリング(改変)は諦めた、これよりバトルシーンのリメイク(自作)を始める(2022/11/16 4:19 外島プログラマー)
            //obj.AddComponent<MessageScrollManager>();
            //obj.AddComponent<InputBattleManager>();

            StopCoroutine(coroutine);

            DontDestroyOnLoad(DataBase);

            SoundManager.instance.PlayBGM(BGMLabel.BGM2);

            //()の中身はメインゲームのシーン名を入れてください
            SceneManager.LoadScene("AdventureScene");
        }
    }

    public IEnumerator TitleCallAndTitleBGM()
    {
        SoundManager.instance.PlayVC(VCLabel.VC1);

        yield return new WaitForSeconds(3);

        SoundManager.instance.PlayBGM(BGMLabel.BGM1);

        yield break;
    }
}
