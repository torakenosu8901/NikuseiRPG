using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeController : MonoBehaviour
{
    public static EscapeController Instance = null;
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
    public IEnumerator EscapePhase()
    {
        int playerFlee = Random.Range(1, 2);
        if (1 == playerFlee)
        {
            Debug.Log("‚É‚°‚ë");
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("“¦‚°‚«‚ê‚½"));
            SceneManager.LoadScene("AdventureScene");
        }
        else
        {
            Debug.Log("‚É‚°‚ç‚ê‚ñ");
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("“¦‚°‚«‚ê‚È‚©‚Á‚½"));
        }
        yield return null;
    }
}
