using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BorderBullet"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Paddle"))
        {
            // 플레이어 hp - damage;
            Destroy(gameObject);
        }
    }
}
