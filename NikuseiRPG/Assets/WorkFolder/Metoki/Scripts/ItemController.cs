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
        //   ‚±‚±‚ÉƒAƒCƒeƒ€‚ÌŒø‰Ê‚Ì”½‰f‚Ìˆ—‚ğ‘‚­ |
        //---------------------------------------
        //EnemyAttack();
        //KillConfirmation();
        yield return null;
    }
}
