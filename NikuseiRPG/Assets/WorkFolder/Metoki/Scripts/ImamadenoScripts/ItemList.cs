using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemParamAsset")]
public class ItemList : ScriptableObject
{
    public List<ItemParam> ItemParamList = new List<ItemParam>();
}
[System.Serializable]
public class ItemParam
{
    [SerializeField]
    public string itemName = "やくそう";

    [SerializeField]
    public float itemEffect = 80;
   
}