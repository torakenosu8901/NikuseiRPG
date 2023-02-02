using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/CreateSkillParamAsset")]
public class SkillList : ScriptableObject
{
    public List<SkillParam> SkillParamList = new List<SkillParam>();
}
[System.Serializable]
public class SkillParam
{
    [SerializeField]
    public string skillName = "パンチ";
    [SerializeField]
    public int consumptionNp = 0;
    [SerializeField]
    public int skillAtk = 0;
}