using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemList itemList;
    public static ItemController Instance = null;

    public string itemName;

    [SerializeField]
    private GameObject itemText; 

    public void Start()
    {
        itemName = itemList.ItemParamList[0].itemName;
    }

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
