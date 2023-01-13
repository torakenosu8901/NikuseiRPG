using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/NPCData", fileName = "NPCData")]
public class NPCData : ScriptableObject
{
    public NPCDataPair[] NPCDataPairs;

}

[System.Serializable]
public class NPCDataPair
{
    public NPCLabel vcLabel;
    public AudioClip audioClip;
}
