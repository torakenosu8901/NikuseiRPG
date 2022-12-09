using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

//イベントシステムのスクリプト
[RequireComponent(typeof(Flowchart))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    string message = "";
    GameObject playerObj;
    Flowchart flowChart;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        flowChart = GetComponent<Flowchart>();
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        //タグでプレイヤーに当たったらメッセージを出すように
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Talk());
        }
    }
    IEnumerator Talk()
    {
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
    }
}
