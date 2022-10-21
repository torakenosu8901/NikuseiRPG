using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private SoundData soundData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SoundManager.instance.PlayBGM(BGMLabel.BGM1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.instance.PlayBGM(BGMLabel.BGM2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundManager.instance.PlayBGM(BGMLabel.BGM3);
        }
    }

    // Update is called once per frame
}

