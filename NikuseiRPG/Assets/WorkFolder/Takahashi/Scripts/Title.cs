using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//このスクリプトはBackGroundにアタッチしてください

public class Title : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    //フェードイン・フェードアウト速度の変更
    float fadeSpeed = 0.006f;
    float red, green, blue, alfa;

    public bool isFadeOut = false;
    public bool isFadeIn = false;
    public string changeSceneName;

    Image fadeImage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    void Update()
    {
        if(isFadeIn)
        {
            StartFadeIn();
        }

        if(isFadeOut)
        {
            StartFadeOut();
        }

        //Enterを押したらスタート
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(sound1);

            isFadeOut = true;

            //ここにシーン名を入れる
            changeSceneName = "Main";
        }

    }
    void StartFadeIn()
    {
        alfa -= fadeSpeed;
        SetAlpha();
        if (alfa <= 0)
        {
            isFadeIn = false;
            fadeImage.enabled = false;
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;
        alfa += fadeSpeed;
        SetAlpha();
        if (alfa >= 1)
        {
            isFadeOut = false;

            //ここにはシーン名を書かないでください
            if (changeSceneName != "")
            {
                Debug.Log(changeSceneName + "に遷移します。");
                SceneManager.LoadScene(changeSceneName);
            }
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
