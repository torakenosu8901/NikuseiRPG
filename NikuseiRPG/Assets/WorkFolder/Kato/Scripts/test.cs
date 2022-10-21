using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.SingletonInstance.PlayBGM(BGMLabel.BGM1);
    }

    // Update is called once per frame
}

