using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのインプット
/// </summary>
public class PlayerInputs : MonoBehaviour
{
    public System.Action<Vector2> PlayerMove;

    public PlayerActions playerActions;

    private void Start()
    {
        PlayerMove = playerActions.PlayerMove;
    }

    private void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) <= 1)
        {

        }
    }
}
