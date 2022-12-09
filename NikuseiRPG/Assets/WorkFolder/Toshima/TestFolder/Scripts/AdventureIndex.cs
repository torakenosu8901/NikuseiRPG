using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureIndex : MonoBehaviour
{
    [SerializeField]
    private Vector3 AdventurePlayerPosition;

    [SerializeField]
    private bool FirstOmen;

    public static AdventureIndex Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        AdventurePlayerPosition = Vector3.zero;

        FirstOmen = true;
    }

    public void ChangeOmenBool(bool b)
    {
        FirstOmen= b;
    }

    public void UpdateAdventurePosition(Vector3 pos)
    {
        AdventurePlayerPosition = new Vector3(pos.x,pos.y,-1);
    }

    public Vector3 GetAdventurePosition()
    {
        return AdventurePlayerPosition;
    }

    public bool GetOmen()
    {
        return FirstOmen;
    }
}
