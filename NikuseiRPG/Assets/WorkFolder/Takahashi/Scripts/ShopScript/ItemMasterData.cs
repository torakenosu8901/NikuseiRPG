using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMasterData : ScriptableObject
{
    [SerializeField]
    private ItemData[] data;

    private static Dictionary<int, ItemData> dictionary = null;

    public static ItemData GetValue(int key)
    {
        if(dictionary == null)
        {
            dictionary = new Dictionary<int, ItemData>();
            ItemMasterData itemMasterData = Resources.Load("ItemMasterData") as ItemMasterData;
            foreach(ItemData data in itemMasterData.data)
            {
                dictionary.Add(data.id, data);
            }
        }
        return dictionary[key];
    }

    public static int GetLength()
    {
        return dictionary.Count;
    }
}

[System.Serializable]
public class ItemData
{
    public int id;
    public string name;
    public int price;
    public string text;
    public Sprite sprite;
}
