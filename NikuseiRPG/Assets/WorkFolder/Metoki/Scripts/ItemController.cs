using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public IEnumerator ItemPhase()
    {
        //---------------------------------------
        //   ここにアイテムの効果の反映の処理を書く |
        //---------------------------------------
        //EnemyAttack();
        //KillConfirmation();
        yield return null;
    }
}
