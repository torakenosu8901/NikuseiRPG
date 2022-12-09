using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEncount : MonoBehaviour
{
    [SerializeField]
    private int EncountEnemyNum = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(TestPlayer.Instance.GetMoveNow())
        {
            CharacterDataBase.instance.UpdateEncountNum(EncountEnemyNum);
            RandomEncount.instance.CountUP();
        }
    }
}
