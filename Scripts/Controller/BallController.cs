using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    private float ballSpeed = 4f;
    private Rigidbody2D rigidbody2D;
    private Vector3 pos;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Init();
    }

    private void Init()
    {
        pos = new Vector3(0f, -4f, 0f);
        transform.position = pos;
        rigidbody2D.velocity = Vector2.up;
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1; //Random.Range ���� ���� �� ���� ���� �ȵ�   0, 1  // 0�̸� -1 �̰� �ƴϸ� 1�̴� 
        float y = 1f;
        //float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody2D.velocity = new Vector2(x * ballSpeed, y * ballSpeed);
    }

    private void Update()
    {
        // ���� ��ġ
        Vector3 currentPosition = transform.position;

        // y ���� -5.5���� ������ ����
        if (currentPosition.y < -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BottomWall")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}
