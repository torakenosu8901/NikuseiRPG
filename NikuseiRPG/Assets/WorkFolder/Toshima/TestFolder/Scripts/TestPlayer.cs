﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    //移動用のRigidbodyを持つ変数
    [SerializeField]
    private Rigidbody2D rb;

    //プレイヤーの移動速度を持つ変数
    [SerializeField]
    private float playerMoveSpeed;

    [SerializeField]
    private bool MoveNow;

    public static TestPlayer Instance;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitPos(Vector3 InitPos)
    {
        this.gameObject.transform.position = new Vector3(InitPos.x,InitPos.y,-1);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (playerMoveSpeed * Input.GetAxis("Horizontal"), playerMoveSpeed * Input.GetAxis("Vertical"));

        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            MoveNow = false;
        }
        else
        {
            MoveNow = true;
        }
    }

    public bool GetMoveNow()
    {
        return MoveNow;
    }

    public Vector3 GetPos()
    {
        return rb.position;
    }
}
