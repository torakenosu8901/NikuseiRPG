using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CreateCharacterParamAsset")]
public class CharacterData : ScriptableObject
{
    //エネミーのステータスをリストで保持する
    public List<CharacterParam> EnemyParamList = new List<CharacterParam>();

    //プレイヤーのステータスをリストで保持する
    public List<CharacterParam> PlayerParamList = new List<CharacterParam>();    
}

/// <summary>
/// キャラクターが持つパラメーター
/// </summary>
[System.Serializable]
public class CharacterParam
{
    //キャラクターの名前
    [SerializeField]
    public string name = "";

    //キャラクターのタイプ(プレイヤーかエネミーかの判定用)
    [SerializeField]
    public CharacterType type = CharacterType.None;

    //キャラクターの「最大NP」
    [SerializeField]
    public int maxnp = 0;

    //キャラクターの「現在NP」
    [SerializeField]
    public int np = 0;

    //キャラクターの「ATK」
    [SerializeField]
    public int atk = 0;

    //キャラクターの「AGI」
    [SerializeField]
    public int agi = 0;

    //キャラクターの「DEF」
    [SerializeField]
    public int def = 0;

    //キャラクターの「LV」
    [SerializeField]
    public int lv = 0;

    ////キャラクターの「最大EXP」(次のレベルまでに必要な経験値数
    //[SerializeField]
    //public int maxexp = 0;

    ////キャラクターの「現在EXP」
    //[SerializeField]
    //public int exp = 0;
}

/// <summary>
/// キャラクタータイプを列挙した構造体(?)
/// </summary>
public enum CharacterType
{
    None,
    Player,
    Enemy
}