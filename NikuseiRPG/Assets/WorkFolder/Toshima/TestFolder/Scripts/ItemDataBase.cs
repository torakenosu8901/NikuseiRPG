using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    [SerializeField]
    public ItemData ItemData;

    public static ItemDataBase instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
