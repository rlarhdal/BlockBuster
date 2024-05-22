using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
    [SerializeField] int hp;  //���� ������ 



    public GameObject[] itemPrefabs;

    private SpriteRenderer SpriteRenderer;
    public Sprite hidBlock;

    private System.Random rand = new System.Random();


    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage() //���� �浹�� ȣ��� �޼ҵ�
    {
        hp -= 1;

        //���࿡ hp�� 0�̾ƴϸ� ������ ������ ���� 
        if (hp != 0)
        {
            SpriteRenderer.sprite = hidBlock;
        }
        else if (hp <= 0)
        {
            Death();
            if (rand.Next(100) < 10) SpawnItem();
        }


    }

    private void SpawnItem()
    {
        int randomValue = rand.Next(1, 101);

        GameObject itemPrefab = null;

        if (randomValue <= 30)
        {
            itemPrefab = itemPrefabs[0];
        }
        else if (randomValue <= 60)
        {
            itemPrefab = itemPrefabs[1];
        }
        else if (randomValue <= 90)
        {
            itemPrefab = itemPrefabs[2];
        }
        else if (randomValue <= 100)
        {
            itemPrefab = itemPrefabs[3];
        }

        // ������ ���� ��ġ ���� (���� ����� ��ġ�� ����)
        Vector2 spawnPosition = transform.position;

        // ���õ� �������� ���� ��ġ�� ����
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    public void Death()
    {
        Destroy(gameObject);

        GameManager.instance.currentScore += 100;

        GameManager.instance.blockCnt--;
        GameManager.instance.CheckBlockCnt();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.PlayeSfx("Bounce_wall");
        if (collision.gameObject.CompareTag("Ball"))  //collision �浹������ ��
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Beam"))
        {
            TakeDamage();
        }
    }

}
