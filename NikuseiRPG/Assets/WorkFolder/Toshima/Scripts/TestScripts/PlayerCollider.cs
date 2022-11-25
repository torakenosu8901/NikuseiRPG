using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public System.Action SceneLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneLoad();
    }
}
