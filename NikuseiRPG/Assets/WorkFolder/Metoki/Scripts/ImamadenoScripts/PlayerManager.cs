using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float distance;

    private Vector2 move;

    private Vector3 tagetpos;

    private void Start()
    {
        distance = 1.6f;
        tagetpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        if(move != Vector2.zero && Vector3.Distance(transform.position, tagetpos) < 0.5f)
            if(move != Vector2.zero && transform.position == tagetpos)
            {
                tagetpos += new Vector3(move.x * distance, move.y * distance, 0);
            }
        Move(tagetpos);
    }

    private void Move(Vector3 tagetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, tagetPosition, speed * Time.deltaTime);
    }
}
