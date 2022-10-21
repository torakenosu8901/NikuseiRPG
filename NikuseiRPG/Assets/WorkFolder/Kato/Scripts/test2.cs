using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    [SerializeField]
    private SEData seData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SoundManager.instance.PlaySE(SELabel.SE1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SoundManager.instance.PlaySE(SELabel.SE2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SoundManager.instance.PlaySE(SELabel.SE3);
        }
    }
}
