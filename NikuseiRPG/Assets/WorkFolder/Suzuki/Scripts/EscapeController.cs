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
            Debug.Log("�ɂ���");
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("�������ꂽ"));
            SceneManager.LoadScene("AdventureScene");
        }
        else
        {
            Debug.Log("�ɂ�����");
            yield return StartCoroutine(MessageScrollManager.Instance.MessageCo("��������Ȃ�����"));
        }
        yield return null;
    }
}
