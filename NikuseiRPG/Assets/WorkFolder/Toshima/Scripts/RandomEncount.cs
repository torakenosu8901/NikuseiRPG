using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncount : MonoBehaviour
{
    [SerializeField]
    private string SceneName;

    [SerializeField]
    private float RandomCount;

    public static RandomEncount instance;

    // Start is called before the first frame update
    void Start()
    {
        RandomCount = 0;

        if(instance == null)
        {
            instance = this;
        }
    }

    public void CountUP()
    {
        if (RandomCount >= 100f)
        {
            AdventureIndex.Instance.UpdateAdventurePosition(TestPlayer.Instance.GetPos());
            SoundManager.instance.PlayBGM(BGMLabel.BGM3);
            SceneManager.LoadScene(SceneName);
        }

        RandomCount += 25f * Time.deltaTime;        
    }
}
