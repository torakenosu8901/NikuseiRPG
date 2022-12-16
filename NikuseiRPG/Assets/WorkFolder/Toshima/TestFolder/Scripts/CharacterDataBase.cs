using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBase : MonoBehaviour
{
    //キャラクターのデータを総括しているスクリプタブルオブジェクトを持つ変数
    //[SerializeField]
    public CharacterData characterData;

    public int encountenemynum = 0;

    //シングルトン化(葛藤中、本当はプライベートにしたいけど工程が多くなるのが懸念点)
    public static CharacterDataBase instance;

    /// <summary>
    /// パラメーターの一つである「CharacterType」を適切に設定する処理
    /// </summary>
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //エネミータイプを設定
        for (int i = 0; i < characterData.EnemyParamList.Count; i++)
        {
            characterData.EnemyParamList[i].type = CharacterType.Enemy;
        }

        //プレイヤータイプを設定
        for (int i = 0; i < characterData.PlayerParamList.Count; i++)
        {
            characterData.PlayerParamList[i].type = CharacterType.Player;
        }

        //BattleCharacter.Add(characterData.PlayerParamList[0]);

        ////デバック
        ////エネミー
        //for (int i = 0; i < characterData.EnemyParamList.Count; i++)
        //{
        //    Debug.Log(characterData.EnemyParamList[i].maxnp);
        //}

        ////プレイヤー
        //for (int i = 0; i < characterData.PlayerParamList.Count; i++)
        //{
        //    Debug.Log(characterData.PlayerParamList[i].maxnp);
        //}
    }

    public void UpdateEncountNum(int num)
    {
        encountenemynum = num;
    }

    public CharacterParam AddEnemy()
    {
        CharacterParam Enemy = new CharacterParam { };

        Enemy.name = characterData.EnemyParamList[encountenemynum].name;

        Enemy.type = characterData.EnemyParamList[encountenemynum].type;

        Enemy.maxnp = characterData.EnemyParamList[encountenemynum].maxnp;

        Enemy.np = characterData.EnemyParamList[encountenemynum].np;

        Enemy.atk = characterData.EnemyParamList[encountenemynum].atk;

        Enemy.def = characterData.EnemyParamList[encountenemynum].def;

        Enemy.lv = characterData.EnemyParamList[encountenemynum].lv;

        Enemy.skill = characterData.EnemyParamList[encountenemynum].skill;

        Enemy.characterUseBuffList = characterData.EnemyParamList[encountenemynum].characterUseBuffList;

        //return characterData.EnemyParamList[encountenemynum];

        return Enemy;
    }
    public CharacterParam AddPlayer()
    {
        return characterData.PlayerParamList[encountenemynum];
    }

    //public void AllHealing()
    //{
    //    for (int i = 0; i < characterData.Count; i++)
    //    {
    //        characterData[i].np = characterData[i].maxnp;
    //    }
    //}

    //public void EnemyHealing()
    //{
    //    for (int i = 0; i < BattleCharacter.Count; i++)
    //    {
    //        if (BattleCharacter[i].type == CharacterType.Enemy)
    //        {
    //            BattleCharacter[i].np = BattleCharacter[i].maxnp;                
    //        }
    //    }

    //    for(int i=1; i < BattleCharacter.Count;)
    //    {
    //        BattleCharacter.Remove(BattleCharacter[i]);
    //    }
    //}
}
