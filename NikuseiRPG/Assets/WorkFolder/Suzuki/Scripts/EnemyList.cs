using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/CreateEnemyParamAsset")]
public class EnemyList : ScriptableObject
{
    public List<EnemyParam> EnemyParamList = new List<EnemyParam>();
}
[System.Serializable]
public class EnemyParam
{
    [SerializeField]
    public string enemyName = "ƒXƒ‰ƒCƒ€";
    [SerializeField]
    public int np = 0;
    [SerializeField]
    public int atk = 0;
    [SerializeField]
    public int agi = 0;
    [SerializeField]
    public int def = 0;
    [SerializeField]
    public int lv = 0;
}