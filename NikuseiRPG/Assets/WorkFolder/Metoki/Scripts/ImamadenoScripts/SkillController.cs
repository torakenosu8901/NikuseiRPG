using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public SkillList skillList;
    public static SkillController Instance = null;

    [SerializeField]
    public int skillNumber;

    [SerializeField]
    private GameObject skillText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void SkillPhase()
    {
       //ÉXÉLÉãÇÃèàóù
    }
}
