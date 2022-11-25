using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�ړ��p
/// </summary>
public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
    }

    public void PlayerMove(Vector2 moveDirection)
    {
        playerRB.velocity = moveDirection;
    }
}
