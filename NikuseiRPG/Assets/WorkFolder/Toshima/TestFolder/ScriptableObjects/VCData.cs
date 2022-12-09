using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/VCData", fileName = "VCData")]
public class VCData : ScriptableObject
{
    public VCDataPair[] VCDataPairs;

}

[System.Serializable]
public class VCDataPair
{
    public VCLabel vcLabel;
    public AudioClip audioClip;
}