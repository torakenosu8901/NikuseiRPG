using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SEData", fileName = "SEData")]
public class SEData : ScriptableObject
{
    public SEDataPair[] SEDataPairs;

}

[System.Serializable]
public class SEDataPair
{
    public SELabel seLabel;
    public AudioClip audioClip;
}