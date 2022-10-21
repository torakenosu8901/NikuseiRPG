using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SoundData",fileName = "SoundData")]
public class SoundData : ScriptableObject
{
    public BGMDataPair[] bgmDataPairs;

}

[System.Serializable]
public class BGMDataPair
{
    public BGMLabel bgmLabel;
    public AudioClip audioClip;
}
