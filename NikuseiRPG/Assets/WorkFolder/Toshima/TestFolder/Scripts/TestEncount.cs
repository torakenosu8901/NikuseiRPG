using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEncount : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(TestPlayer.Instance.GetMoveNow())
        {
            RandomEncount.instance.CountUP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
