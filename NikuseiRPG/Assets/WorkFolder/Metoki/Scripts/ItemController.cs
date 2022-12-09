using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //戦闘に参加するキャラクターをリストで保持(戦闘の三文字「par」は「Participation(参加)」の先頭三文字)
    [SerializeField]
    private List<CharacterParam> _parCharacter;


    public ItemList itemList;
    public CharacterData characterData;
    public static ItemController Instance = null;
    
    [Tooltip("回復")]
    public int heel;

    [SerializeField]
    public int itemNumber;

    [SerializeField]
    private GameObject itemText;

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
    public void Start()
    {
        _parCharacter.Add(CharacterDataBase.instance.AddPlayer());
        _parCharacter.Add(CharacterDataBase.instance.AddEnemy());
    }

   
    public IEnumerator ItemPhase(int num)
    {
        switch (num)
        {
            case 0:
                heel = 0;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
       
        yield return null;
    }
}
