using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����p�j�p�X�N���v�g
public class NPCController : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj;
    Animator animator;
    private Rigidbody2D rigidBody;
    [SerializeField]
    float span;//���b���Ɍ�����ς��邩
    float time;//�^�C�}�[�ϐ�
    private Vector2 input;
    public readonly float SPEED = 0.01f;
  


    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        this.rigidBody = GetComponent<Rigidbody2D>();

       

    }
   
    private void Update()
    {
        time += Time.deltaTime;
        if (time > span)
        {
            int rand = Random.Range(0, 4);//�ړ�������������߂�B
            switch (rand)
            {
                case 0:
                    input = new Vector2(1.0f, 0);//�E�ֈړ�
                    this.animator.SetFloat("x", 1.0f);
                    this.animator.SetFloat("y", 0f);
                    break;
                case 1:
                    input = new Vector2(-1.0f, 0);//���ֈړ�
                    this.animator.SetFloat("x", -1.0f);
                    this.animator.SetFloat("y", 0f);
                    break;
                case 2:
                    input = new Vector2(0, 1);//��ֈړ�
                    this.animator.SetFloat("y", 1.0f);
                    this.animator.SetFloat("x", 0f);
                    break;
                case 3:
                    input = new Vector2(0, -1);//���ֈړ�
                    this.animator.SetFloat("y", -1.0f);
                    this.animator.SetFloat("x", 0f);
                    break;

            }

            time = 0;
        }


    }


    private void FixedUpdate()
    {
        if (input == Vector2.zero)
        {
            return;
        }
        rigidBody.position += input * SPEED;//NPC�̈ړ�  
    }
  }
   


 
