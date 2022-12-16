using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemParamAsset")]
public class ItemData : ScriptableObject
{
    public List<ItemParam> ItemParamList = new List<ItemParam>();
}

[System.Serializable]
public class ItemParam
{
    [SerializeField]
    public ItemType itemType = ItemType.None;

    [SerializeField]
    public string itemName = "";

    [SerializeField]
    public NumType numType = NumType.None;

    [SerializeField]
    public RelatedStatusType relatedStatus = RelatedStatusType.None;

    [SerializeField]
    public int itemPower = 0;

    [SerializeField]
    public int itemRange = 0;

    [SerializeField]
    public int itemPersistence = 0;

}

/// <summary>
/// アイテムタイプを列挙した構造体(?)
/// </summary>
public enum ItemType
{
    None,
    Damage,
    Heal,
    Buff,
    Debuff,
    Special
}

public enum NumType
{
    None,
    Ratio,
    Fixed
}