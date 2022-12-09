using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CleaGrid : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Collider2D co;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TestPlayer.Instance.GetMoveNow())
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
