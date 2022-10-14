using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Create DamegeData")]
public class DamegeData : ScriptableObject
{
    // データ群の先頭をstringにして名前等に設定するとInspectorで見たときに項目TOPに表示されるので見やすくなります。
    [SerializeField]
    private string WazaName = "Attack";

    // privateでも[SerializeField]をつけることでInspectorで確認できるようになります。
    [SerializeField]
    float ATK = 1;
}